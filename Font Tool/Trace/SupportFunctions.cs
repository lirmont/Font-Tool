using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using AForge.Imaging;
using System.Drawing.Drawing2D;

namespace AutoTrace
{
	using Polygon = List<ClipperLib.IntPoint>;
	using Polygons = List<List<ClipperLib.IntPoint>>;
	using TriangleNet.Meshing.Algorithm;

	public static class SupportFunctions
	{
		private static PointF[] PolygonToPointFArray(Polygon pg, float scale)
		{
			PointF[] result = new PointF[pg.Count];
			for (int i = 0; i < pg.Count; ++i)
			{
				result[i].X = (float)pg[i].X / scale;
				result[i].Y = (float)pg[i].Y / scale;
			}
			return result;
		}

		public static List<TriangleNet.Meshing.IMesh> ConsumePolyNodeLookingForHoles(ClipperLib.PolyNode polyNode, TraceConfiguration configuration)
		{
			List<TriangleNet.Geometry.Polygon> polygons = new List<TriangleNet.Geometry.Polygon>();
			consumeSinglePolyNode(polyNode, polygons, configuration);
			//
			List<TriangleNet.Meshing.IMesh> set = new List<TriangleNet.Meshing.IMesh>();
			foreach (TriangleNet.Geometry.IPolygon polygon in polygons)
			{
				set.Add(TriangleNet.Geometry.IPolygonExtensions.Triangulate(polygon, triangulator: new SweepLine()));
			}
			//
			return set;
		}

		private static void consumeSinglePolyNode(ClipperLib.PolyNode polyNode, List<TriangleNet.Geometry.Polygon> set, TraceConfiguration configuration)
		{
			foreach (ClipperLib.PolyNode thisNode in polyNode.Childs)
				consumeNonHolePolygonNode(thisNode, set, configuration);
		}

		private static void consumeNonHolePolygonNode(ClipperLib.PolyNode polyNode, List<TriangleNet.Geometry.Polygon> set, TraceConfiguration configuration)
		{
			// Have polygon.
			System.Diagnostics.Debug.Assert(polyNode.IsHole == false);
			if (polyNode.Contour.Count > 0)
			{
				// 1 simple polygon is contained in pts. Add polygon.
				TriangleNet.Geometry.Polygon mainPolygon = contourToPoly2TriPolygon(polyNode, configuration);
				// Add hole.
				if (polyNode.ChildCount > 0)
				{
					// Now, per iteration, try to connect between holes and the parent polygon.
					foreach (ClipperLib.PolyNode childNode in polyNode.Childs)
						if (childNode.IsHole)
						{
							TriangleNet.Geometry.Polygon holePolygon = contourToPoly2TriPolygon(childNode, configuration);
							mainPolygon.AddContour(holePolygon.Points, hole: true);
							// Discrete polygons that may contain holes.
							if (childNode.ChildCount > 0)
								foreach (ClipperLib.PolyNode discreteChildNode in childNode.Childs)
									consumeNonHolePolygonNode(discreteChildNode, set, configuration);
						}
						// Unlikely to reach here. More likely to reach similar function as child of hole (treated as top-node).
						else
							consumeNonHolePolygonNode(childNode, set, configuration);
				}
				set.Add(mainPolygon);
			}
		}

		private static TriangleNet.Geometry.Polygon contourToPoly2TriPolygon(ClipperLib.PolyNode thisNode, TraceConfiguration configuration)
		{
			PointF[] pts = PolygonToPointFArray(thisNode.Contour, 1.0f);
			// Do smoothing here.
			List<PointF> resultLine = null;
			if (configuration.LineSmoothing)
			{
				PolyLine line = new PolyLine(pts);
				// Interpolate then smooth.
				line.InterpolateSharpCorners(configuration.InterpolateSharpCornersBy);
				resultLine = line.Smooth();
			}
			else
				resultLine = new List<PointF>(pts);
			//
			List<TriangleNet.Geometry.Vertex> points = new List<TriangleNet.Geometry.Vertex>();
			foreach (PointF pt in resultLine)
				points.Add(new TriangleNet.Geometry.Vertex(pt.X, pt.Y));
			TriangleNet.Geometry.Polygon polygon = new TriangleNet.Geometry.Polygon();
			polygon.AddContour(points);
			return polygon;
		}

		public static void ConsumePolyNode(ClipperLib.PolyNode polyNode, GraphicsPath path)
		{
			foreach (ClipperLib.PolyNode thisNode in polyNode.Childs)
			{
				PointF[] pts = PolygonToPointFArray(thisNode.Contour, 1.0f);
				if (pts.Length > 2)
					path.AddPolygon(pts);
				pts = null;
				//
				if (thisNode.ChildCount > 0)
					ConsumePolyNode(thisNode, path);
			}
		}

		public static Polygons PolyTreeToPolygons(ClipperLib.PolyTree tree)
		{
			Polygons result = new Polygons();
			AddPolyNodeToPolygons(tree, ref result);
			return result;
		}

		private static void AddPolyNodeToPolygons(ClipperLib.PolyNode polynode, ref Polygons polygons)
		{
			if (polynode.Contour.Count > 0)
				polygons.Add(polynode.Contour);
			for (int i = 0; i < polynode.ChildCount; i++)
				AddPolyNodeToPolygons(polynode.Childs[i], ref polygons);
		}

		private static List<ClipperLib.IntPoint> getClipperPoints(List<PointF> list)
		{
			List<ClipperLib.IntPoint> points = new List<ClipperLib.IntPoint>();
			foreach (PointF point in list)
				points.Add(new ClipperLib.IntPoint(point.X, point.Y));
			return points;
		}

		private static List<ClipperLib.IntPoint> getClipperPoints(PolyLine line)
		{
			return getClipperPoints(new List<PointF>(line.PointsF));
		}

		private static void twoOrMorePointsPerPixelAddPoint(List<PointF> outline, ref PointF point)
		{
			PointF? oldPoint = null;
			if (outline.Count > 1)
				oldPoint = outline[outline.Count - 1];
			//
			if (oldPoint == null)
				outline.Add(point);
			else
			{
				// It's not a duplicate. Add it.
				if (oldPoint.Value.X != point.X || oldPoint.Value.Y != point.Y)
					outline.Add(point);
			}
		}

		private static void optimizedAddPoint(List<PointF> outline, ref PointF point)
		{
			PointF? oldPoint = null;
			if (outline.Count > 1)
				oldPoint = outline[outline.Count - 1];
			//
			if (oldPoint == null)
				outline.Add(point);
			else
			{
				// It's not a duplicate. Add it.
				if (oldPoint.Value.X != point.X || oldPoint.Value.Y != point.Y)
				{
					if (outline.Count > 2)
					{
						PointF oldOldPoint = outline[outline.Count - 2];
						// X is shifting.
						if (oldPoint.Value.X != point.X && oldPoint.Value.Y == point.Y)
						{
							// Rewrite old point's data to this point.
							if (oldOldPoint.X != point.X && oldOldPoint.Y == point.Y)
							{
								PointF change = outline[outline.Count - 1];
								change.X = point.X;
								outline[outline.Count - 1] = change;
								return;
							}
						}
						// Y is shifting.
						else if (oldPoint.Value.X == point.X && oldPoint.Value.Y != point.Y)
						{
							// Rewrite old point's data to this point.
							if (oldOldPoint.X == point.X && oldOldPoint.Y != point.Y)
							{
								PointF change = outline[outline.Count - 1];
								change.Y = point.Y;
								outline[outline.Count - 1] = change;
								return;
							}
						}
					}
					outline.Add(point);
				}
			}
		}

		public static List<List<PolyLine>> ConvertBlobsToMarchingSquaresPolyLines(MarchingSquare imageMarch, List<List<Blob>> blobHierarchy, bool pixelBasedSmooth, int cheatLeft = 0)
		{
			List<List<PolyLine>> passes = new List<List<PolyLine>>();
			foreach (List<Blob> blobsInPass in blobHierarchy)
			{
				List<PolyLine> outlinesInThisPass = new List<PolyLine>();
				foreach (Blob actualPart in blobsInPass)
				{
					List<PointF> outline = new List<PointF>();
					Point startLocation = SupportFunctions.GetFirstPixelXYFromBlob(actualPart);
					Point borderPoint = new Point(startLocation.X - cheatLeft, startLocation.Y);
					PointF startPoint = imageMarch.FindStartPoint(startLocation: borderPoint);
					imageMarch.DoMarch(delegate(PointF point)
					{
						twoOrMorePointsPerPixelAddPoint(outline, ref point);
					}, startPoint: startPoint);
					//
					if (pixelBasedSmooth)
					{
						PolyLine thisOutline = new PolyLine(outline);
						outline = thisOutline.pixelBasedSmooth();
					}
					// Add the line.
					outlinesInThisPass.Add(new PolyLine(outline));
				}
				// Put the converted lines into the pass.
				passes.Add(outlinesInThisPass);
			}
			// Send it all back.
			return passes;
		}

		public static List<List<Blob>> GetBlobContainmentHierarchy(IEnumerable<Blob> blobs)
		{
			List<List<Blob>> blobPassesStack = new List<List<Blob>>();
			List<Blob> unsortedBlobs = new List<Blob>(blobs);
			int pass = 0;
			while (unsortedBlobs.Count > 0)
			{
				// Create the pass.
				if (blobPassesStack.Count <= pass)
					blobPassesStack.Add(new List<Blob>());
				// Get a reference to the pass.
				List<Blob> thisPass = blobPassesStack[pass];
				for (int i = 0; i < unsortedBlobs.Count; i++)
				{
					Blob thisBlob = unsortedBlobs[i];
					List<Blob> possibleChildren = new List<Blob>(unsortedBlobs);
					possibleChildren.Remove(thisBlob);
					// If this blob is not contained by anything else in the current list, it belongs in this pass.
					if (!possibleChildren.Exists(item => item.Area > thisBlob.Area && thisBlob.Rectangle.IntersectsWith(item.Rectangle)))
						thisPass.Add(thisBlob);
				}
				// Remove all blobs added to this pass.
				foreach (Blob blob in thisPass)
					unsortedBlobs.Remove(blob);
				// Move to the next pass.
				if (unsortedBlobs.Count > 0)
					pass++;
			}
			return blobPassesStack;
		}

		public static List<Pass> GroupLevelsOfAPolyLineStackIntoPasses(List<List<PolyLine>> blobLinePassesStack, List<List<PolyLine>> holeLinePassesStack)
		{
			List<Pass> passes = new List<Pass>();
			int passCount = Math.Max(blobLinePassesStack.Count, holeLinePassesStack.Count);
			for (int i = 0; i < passCount; i++)
			{
				List<PolyLine> subjects = (blobLinePassesStack.Count > i) ? blobLinePassesStack[i] : null;
				List<PolyLine> holes = (holeLinePassesStack.Count > i) ? holeLinePassesStack[i] : null;
				//
				if (subjects != null)
				{
					Pass thisPass = new Pass(subjects, holes);
					passes.Add(thisPass);
				}
				else if (passes.Count > 0)
				{
					Pass lastPass = passes[passes.Count - 1];
					lastPass.Holes.AddRange(holes);
				}
			}
			//
			return passes;
		}

		public static void UnionPassesTogether(List<Pass> passes, ClipperLib.PolyTree finalTree)
		{
			ClipperLib.Clipper finalClipper = new ClipperLib.Clipper();
			for (int i = 0; i < passes.Count; i++)
			{
				Pass thisPass = passes[i];
				ClipperLib.PolyTree thisTree = thisPass.Solution;
				ClipperLib.PolyType type = (i == 0) ? ClipperLib.PolyType.ptSubject : ClipperLib.PolyType.ptClip;
				// Add what makes up the union of subjects.
				foreach (ClipperLib.PolyNode polygon in thisTree.m_AllPolys)
					finalClipper.AddPath(polygon.Contour, type, true);
			}
			// Union all the passes together.
			finalClipper.Execute(ClipperLib.ClipType.ctUnion, finalTree);
		}

		public static Bitmap GetBlobs(Bitmap image, bool negativeSpace = false)
		{
			Size size = image.Size;
			Bitmap blobsBitmap = new Bitmap(size.Width, size.Height);
			BitmapProcessing.FastBitmap bmp = new BitmapProcessing.FastBitmap(image);
			BitmapProcessing.FastBitmap alpha = new BitmapProcessing.FastBitmap(blobsBitmap);
			bmp.LockImage();
			alpha.LockImage();
			{
				for (int x = 0; x < size.Width; x++)
				{
					for (int y = 0; y < size.Height; y++)
					{
						Color pixel = bmp.GetPixel(x, y);
						if (negativeSpace)
						{
							if (pixel.A > 0)
								alpha.SetPixel(x, y, Color.FromArgb(0));
							else
								alpha.SetPixel(x, y, Color.White);
						}
						else
						{
							if (pixel.A > 0)
								alpha.SetPixel(x, y, Color.White);
							else
								alpha.SetPixel(x, y, Color.FromArgb(0));
						}
					}
				}
			}
			alpha.UnlockImage();
			bmp.UnlockImage();
			return blobsBitmap;
		}

		public static Point GetFirstPixelXYFromBlob(Blob blob)
		{
			Point p = Point.Empty;
			Bitmap thisImage = blob.Image.ToManagedImage(true);
			// Get first pixel.
			BitmapProcessing.FastBitmap bmp = new BitmapProcessing.FastBitmap(thisImage);
			bmp.LockImage();
			{
				for (int x = 0; x < thisImage.Width; x++)
				{
					for (int y = 0; y < thisImage.Height; y++)
					{
						Color pixel = bmp.GetPixel(x, y);
						if (pixel.A > 0)
						{
							p = new Point(x, y);
							// Escape the loop.
							x = thisImage.Width;
							y = thisImage.Height;
						}
					}
				}
			}
			bmp.UnlockImage();
			return p;
		}

		// Input image to 3d geometry.
		public static Tracing TraceImage(Bitmap imageToTrace, TraceConfiguration configuration)
		{
			// At least 1 pixel of padding around the whole image is required to make use of the blob feature.
			int halfEssentialPadding = 1;
			int padding = 2 * halfEssentialPadding + Math.Max(0, configuration.Padding);
			float halfPadding = padding / 2.0f;
			Bitmap image = new Bitmap(imageToTrace.Width + padding, imageToTrace.Height + padding);
			using (Graphics g = Graphics.FromImage(image))
			{
				g.DrawImage(imageToTrace, new RectangleF(new PointF(halfPadding, halfPadding), new Size(imageToTrace.Width, imageToTrace.Height)));
			}
			//
			Size size = image.Size;
			//
			Bitmap mainBlobsImage = GetBlobs(image, negativeSpace: false);
			Bitmap blobImage = GetBlobs(image, negativeSpace: true);
			// Process major space binary image.
			BlobCounter mainBlobCounter = new BlobCounter();
			mainBlobCounter.ProcessImage(mainBlobsImage);
			Blob[] mainBlobs = mainBlobCounter.GetObjects(mainBlobsImage, true);
			// Process negative space binary image.
			BlobCounter holeCounter = new BlobCounter();
			holeCounter.ProcessImage(blobImage);
			Blob[] holeBlobs = holeCounter.GetObjects(blobImage, true);
			// Only include actual holes. Discard anything that touches the padding of the image (because it's not a hole contained in major space).
			List<Blob> holesList = new List<Blob>();
			foreach (Blob blob in holeBlobs)
			{
				Rectangle bounds = blob.Rectangle;
				if (!(bounds.X == 0 || bounds.X == size.Width || bounds.Y == 0 || bounds.Y == size.Height))
					holesList.Add(blob);
			}
			// Prepare a place to store passes (1 pass per contained field). Each pair of passes needs to be individually unioned and subtracted. Each result needs to be unioned.
			List<List<Blob>> blobPassesStack = GetBlobContainmentHierarchy(mainBlobs);
			List<List<Blob>> holePassesStack = GetBlobContainmentHierarchy(holesList);
			// Lines.
			List<List<PolyLine>> blobLinePassesStack = ConvertBlobsToMarchingSquaresPolyLines(new MarchingSquare(image), blobPassesStack, configuration.PixelSmoothing, cheatLeft: 1);
			List<List<PolyLine>> holeLinePassesStack = ConvertBlobsToMarchingSquaresPolyLines(new MarchingSquare(blobImage), holePassesStack, configuration.PixelSmoothing);
			// Group the parts up into passes.
			List<Pass> passes = GroupLevelsOfAPolyLineStackIntoPasses(blobLinePassesStack, holeLinePassesStack);
			//
			ClipperLib.PolyTree finalTree = new ClipperLib.PolyTree();
			UnionPassesTogether(passes, finalTree);
			// Have finished tree now. Need polygons for drawing.
			Polygons solution = PolyTreeToPolygons(finalTree);
			// Face pass (includes smoothing).
			List<TriangleNet.Meshing.IMesh> set = ConsumePolyNodeLookingForHoles(finalTree, configuration);
			// Final pass.
			Bitmap dilatedBitmap = null;
			dilatedBitmap = Filter.Dilate(image, configuration.Dilation);
			// Send it back.
			float negligibleCoordinateSpace = halfPadding;
			// Remove the negligible portion from the set. The negligible portion is caused by the demand to have padding of an image for the purposes of tracing and for the purpose of texturing (i.e. dilation). NOTE: this negligible coordinate space remains significant to the texture coordinates, which are relative to actual positions, as they represent physical pixels.
			foreach (TriangleNet.Meshing.IMesh mesh in set)
			{
				foreach (TriangleNet.Geometry.Vertex vertex in mesh.Vertices)
				{
					vertex.x -= negligibleCoordinateSpace;
					vertex.y -= negligibleCoordinateSpace;
				}
			}
			// Abstract the meshes as a tracing.
			return new Tracing(dilatedBitmap, set, new PointF(negligibleCoordinateSpace, negligibleCoordinateSpace), configuration.Solidification, configuration.InvertedSolid, configuration.XAdjustment, configuration.YAdjustment, configuration.ZAdjustment, configuration.FillEdges);
		}
	}
}
