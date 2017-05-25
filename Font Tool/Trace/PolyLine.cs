using System.Collections.Generic;
using System.Drawing;
using System;

namespace AutoTrace
{
	public class PolyLine
	{
		private List<ReferenceContainer<PointF>> points = new List<ReferenceContainer<PointF>>();

		public List<ReferenceContainer<PointF>> Points
		{
			get { return points; }
			set { points = value; }
		}

		public PointF[] PointsF
		{
			get
			{
				List<PointF> list = new List<PointF>();
				foreach (ReferenceContainer<PointF> point in points)
					list.Add(point.Value);
				return list.ToArray();
			}
		}

		public PointF? LastPoint
		{
			get
			{
				if (points.Count > 0)
					return points[points.Count - 1];
				return null;
			}
		}

		public PolyLine()
		{ }

		public PolyLine(IEnumerable<PointF> points)
		{
			Add(points);
		}

		public void Add(PointF point)
		{
			// Jump out early if the previous point and this point are the same.
			PointF? lastPoint = LastPoint;
			if (lastPoint != null && lastPoint.Value.X == point.X && lastPoint.Value.Y == point.Y)
				return;
			// Add the point otherwise.
			points.Add(new ReferenceContainer<PointF>(point));
		}

		public void Add(IEnumerable<PointF> points)
		{
			foreach (PointF point in points)
				Add(point);
		}

		public void InterpolateSharpCorners(double length)
		{
			// Select all edges longer than length.
			List<List<int>> lines = getLongEdges(length);
			//
			for (int i = 0; i < lines.Count; i++)
			{
				List<int> thisLine = lines[i];
				int p0Index = thisLine[0];
				int p1Index = thisLine[1];
				double distance = Distance(p0Index, p1Index);
				// Subdivide Edge
				if (distance > length)
					subdivideEdge(p0Index, p1Index);
			}
		}

		private void subdivideEdge(int p0Index, int p1Index, double closeness = 1.0)
		{
			//
			List<ReferenceContainer<PointF>> pointsToAdd = new List<ReferenceContainer<PointF>>();
			//
			ReferenceContainer<PointF> p0 = points[p0Index];
			ReferenceContainer<PointF> p1 = points[p1Index];
			//
			double distance = Distance(p0, p1);
			PointF midpoint = Average(p0, p1);
			// If the distance is greater than 2 units, then attempt to double up the corner.
			if (distance > 2.0)
			{
				// Get point just 1 square distance.
				PointF mp0 = PointAtDistance(closeness, p0, p1);
				PointF mp1 = PointAtDistance(distance - closeness, p0, p1);
				//
				pointsToAdd.Add(new ReferenceContainer<PointF>(mp0));
				pointsToAdd.Add(new ReferenceContainer<PointF>(midpoint));
				pointsToAdd.Add(new ReferenceContainer<PointF>(mp1));
				//
				this.points.InsertRange(p0Index + 1, pointsToAdd);
			}
			else // Otherwise, just subdivide the edge with a midpoint.
				this.points.Insert(p0Index + 1, new ReferenceContainer<PointF>(midpoint));
		}

		// FIXME: Smoothing results in bizarre points.
		public List<PointF> Smooth(float t = 0.5f, double? tooFar = null)
		{
			List<PointF> unsmoothedPoints = new List<PointF>(this.PointsF);
			//
			List<PointF> smoothed = new List<PointF>();
			for (int i = 0; i < unsmoothedPoints.Count + 1; i++)
			{
				// Initially, look at this point and the following three.
				PointF smoothedPoint = getSmoothedPointAroundIndex(t, unsmoothedPoints, i);
				smoothed.Add(smoothedPoint);
				//
				if (i > 1)
				{
					PointF previous = smoothed[smoothed.Count - 2];
					PointF thisPoint = smoothed[smoothed.Count - 1];
					if (previous.X == smoothedPoint.X && previous.Y == smoothedPoint.Y)
						smoothed.RemoveAt(smoothed.Count - 1);
				}
			}
			//
			smoothPointsInPlaceGivenIndexLookBack(smoothed, lookBack: 5);
			// If the first point and the last point are the same, bad results occur during triangulation (StackOverflowException).
			PointF p = smoothed[0];
			PointF pN = smoothed[smoothed.Count - 1];
			while (p.X == pN.X && p.Y == pN.Y && (smoothed.Count > 2))
			{
				smoothed.RemoveAt(smoothed.Count - 1);
				pN = smoothed[smoothed.Count - 1];
			}
			//
			return smoothed;
		}

		private static void smoothPointsInPlaceGivenIndexLookBack(List<PointF> smoothed, int lookBack = 1)
		{
			if (smoothed.Count > 0)
			{
				// Smooth just the part surrounding the first points.
				for (int i = -lookBack; i < lookBack + 1; i++)
				{
					// Rewrite the root points.
					int safeIndex = i % smoothed.Count;
					int index = (safeIndex >= 0) ? safeIndex : (smoothed.Count + safeIndex);
					smoothed[index] = getSmoothedPointAroundIndex(0.5f, smoothed, index);
				}
			}
		}

		private static PointF getSmoothedPointAroundIndex(float t, List<PointF> unsmoothedPoints, int i)
		{
			int p0Index = (i + (unsmoothedPoints.Count - 1)) % unsmoothedPoints.Count;
			int p1Index = (i + 0) % unsmoothedPoints.Count;
			int p2Index = (i + 1) % unsmoothedPoints.Count;
			int p3Index = (i + 2) % unsmoothedPoints.Count;
			PointF p0 = unsmoothedPoints[p0Index];
			PointF p1 = unsmoothedPoints[p1Index];
			PointF p2 = unsmoothedPoints[p2Index];
			PointF p3 = unsmoothedPoints[p3Index];
			return PointOnCurve(p0, p1, p2, p3, t);
		}

		public List<PointF> pixelBasedSmooth()
		{
			List<PointF> preliminaryPoints = new List<PointF>(this.PointsF);
			List<PointF> unsmoothedPoints = new List<PointF>(); ;
			// If pixel-based, look for one of 4 finite pixel shapes (partial square).
			unsmoothedPoints = new List<PointF>();
			for (int i = 0; i < preliminaryPoints.Count; i++)
			{
				// Initially, look at this point and the following three.
				int p0Index = i;
				int p1Index = (i + 1) % preliminaryPoints.Count;
				int p2Index = (i + 2) % preliminaryPoints.Count;
				PointF p0 = preliminaryPoints[p0Index];
				PointF p1 = preliminaryPoints[p1Index];
				PointF p2 = preliminaryPoints[p2Index];
				//
				PointF p1Delta0 = new PointF(Math.Abs(p1.X - p0.X), Math.Abs(p1.Y - p0.Y));
				PointF p2Delta1 = new PointF(Math.Abs(p2.X - p1.X), Math.Abs(p2.Y - p1.Y));
				// 
				bool horizontalThenVertical = (p1Delta0.X == 1 && p2Delta1.Y == 1 && p1Delta0.Y == 0 && p2Delta1.X == 0) ? true : false;
				bool verticalThenHorizontal = (p1Delta0.Y == 1 && p2Delta1.X == 1 && p1Delta0.X == 0 && p2Delta1.Y == 0) ? true : false;
				// If this is the appropriate case, a more accurate point can be derived. 
				if (horizontalThenVertical || verticalThenHorizontal)
				{
					// Test 4th point for full square (false positive).
					int p3Index = (i + 3) % preliminaryPoints.Count;
					PointF p3 = preliminaryPoints[p3Index];
					bool inALine = ((p0.X == p1.X && p1.X == p3.X) || (p0.Y == p1.Y && p1.Y == p3.Y)) ? true : false;
					bool isSquare = (!inALine && ((p0.Y == p3.Y && p0.X != p3.X) || (p0.X == p3.X && p0.Y != p3.Y))) ? true : false;
					if (!isSquare)
					{
						// Add the line that removes p1.
						unsmoothedPoints.Add(p0);
						unsmoothedPoints.Add(p2);
						// Advance to looking at this iteration's p2 (causes duplicates).
						i++;
					}
					else
						unsmoothedPoints.Add(p0);
				}
				else // Advance to looking at this iteration's p1.
					unsmoothedPoints.Add(p0);
			}
			//
			return unsmoothedPoints;
		}

		private PointF Average(PointF p1, PointF p2)
		{
			return new PointF((p1.X + p2.X) / 2f, (p1.Y + p2.Y) / 2f);
		}

		private static double Distance(PointF p1, PointF p2)
		{
			return Math.Sqrt(Math.Pow((p2.X - p1.X), 2) + Math.Pow((p2.Y - p1.Y), 2));
		}

		private double Distance(int p1Index, int p2Index)
		{
			ReferenceContainer<PointF> p1 = Points[p1Index];
			ReferenceContainer<PointF> p2 = Points[p2Index];
			return Distance(p1, p2);
		}

		private List<List<int>> getLongEdges(double distance)
		{
			List<List<int>> list = new List<List<int>>();
			int count = points.Count;
			for (int i = 0; i < count; i++)
			{
				int p0Index = i % count;
				int p1Index = (i + 1) % count;
				ReferenceContainer<PointF> p0 = points[p0Index];
				ReferenceContainer<PointF> p1 = points[p1Index];
				double thisDistance = Distance(p0, p1);
				if (thisDistance > distance)
					list.Add(new List<int> { p0Index, p1Index });
			}
			// Reverse it.
			list.Reverse();
			//
			return list;
		}

		public static PointF PointAtDistance(double distance, PointF A, PointF B)
		{
			// Calculate the vector from o to g.
			double vectorX = B.X - A.X;
			double vectorY = B.Y - A.Y;
			// Calculate the proportion of hypotenuse.
			double factor = distance / Math.Sqrt(vectorX * vectorX + vectorY * vectorY);
			// Factor the lengths.
			vectorX *= factor;
			vectorY *= factor;
			// Calculate and Draw the new vector.
			return new PointF((float)(A.X + vectorX), (float)(A.Y + vectorY));
		}

		private static bool doTwoConsecutiveLengthsExist(double length, List<ReferenceContainer<PointF>> closedPolygon)
		{
			for (int i = 0; i < closedPolygon.Count; i++)
			{
				int p0Index = i;
				int p1Index = (i + 1) % closedPolygon.Count;
				int p2Index = (i + 2) % closedPolygon.Count;
				ReferenceContainer<PointF> p0 = closedPolygon[p0Index];
				ReferenceContainer<PointF> p1 = closedPolygon[p1Index];
				ReferenceContainer<PointF> p2 = closedPolygon[p2Index];
				double distance = Distance(p0, p1);
				if (distance >= length)
				{
					double secondDistance = Distance(p1, p2);
					if (secondDistance >= length)
					{
						return true;
					}
				}
			}
			return false;
		}

		public double LongestLength
		{
			get
			{
				double longestLine = 0;
				int i = 0;
				int count = Points.Count;
				while (i < count + 3)
				{
					int p0Index = i % count;
					int p1Index = (i + 1) % count;
					//
					ReferenceContainer<PointF> p0 = Points[p0Index];
					ReferenceContainer<PointF> p1 = Points[p1Index];
					//
					double distance = Distance(p0, p1);
					if (distance > longestLine)
						longestLine = distance;
					//
					i++;
				}
				return longestLine;
			}
		}

		public List<int> LongestLine
		{
			get
			{
				double longestLine = 0;
				int i = 0;
				int count = Points.Count;
				int? lineP0Index = null;
				int? lineP1Index = null;
				while (i < count + 3)
				{
					int p0Index = i % count;
					int p1Index = (i + 1) % count;
					//
					ReferenceContainer<PointF> p0 = Points[p0Index];
					ReferenceContainer<PointF> p1 = Points[p1Index];
					//
					double distance = Distance(p0, p1);
					if (distance > longestLine)
					{
						longestLine = distance;
						//
						lineP0Index = p0Index;
						lineP1Index = p1Index;
					}
					//
					i++;
				}
				//
				if (lineP0Index != null && lineP1Index != null)
					return new List<int> { lineP0Index.Value, lineP1Index.Value };
				//
				return new List<int>();
			}
		}

		private bool isDeltaTooHigh(PointF smoothedPoint, PointF p1, PointF p2, double maxDelta)
		{
			double delta = Math.Sqrt(Math.Pow((smoothedPoint.X - p1.X), 2) + Math.Pow((smoothedPoint.Y - p1.Y), 2));
			double deltaB = Math.Sqrt(Math.Pow((p2.X - smoothedPoint.X), 2) + Math.Pow((p1.Y - smoothedPoint.Y), 2));
			//
			if (delta < maxDelta && deltaB < maxDelta)
				return false;
			else
				return true;
		}

		/// <summary>
		/// Calculates interpolated point between two points using Catmull-Rom Spline
		/// </summary>
		/// <remarks>
		/// Points calculated exist on the spline between points two and three.
		/// </remarks>
		/// <param name="p0">First Point</param>
		/// <param name="p1">Second Point</param>
		/// <param name="p2">Third Point</param>
		/// <param name="p3">Fourth Point</param>
		/// <param name="t">
		/// Normalised distance between second and third point /// where the spline point will be calculated/// </param>
		/// <returns>
		/// Calculated Spline Point
		/// </returns>
		public static PointF PointOnCurve(PointF p0, PointF p1, PointF p2, PointF p3, float t = 0.5f)
		{
			PointF ret = new PointF();
			float t2 = t * t;
			float t3 = t2 * t;
			ret.X = 0.5f * ((2.0f * p1.X) + (-p0.X + p2.X) * t + (2.0f * p0.X - 5.0f * p1.X + 4 * p2.X - p3.X) * t2 + (-p0.X + 3.0f * p1.X - 3.0f * p2.X + p3.X) * t3);
			ret.Y = 0.5f * ((2.0f * p1.Y) + (-p0.Y + p2.Y) * t + (2.0f * p0.Y - 5.0f * p1.Y + 4 * p2.Y - p3.Y) * t2 + (-p0.Y + 3.0f * p1.Y - 3.0f * p2.Y + p3.Y) * t3);
			return ret;
		}

		/*
		public static Poly2Tri.TriangulationPoint PointOnCurve(Poly2Tri.TriangulationPoint p0, Poly2Tri.TriangulationPoint p1, Poly2Tri.TriangulationPoint p2, Poly2Tri.TriangulationPoint p3, float t = 0.5f)
		{
			float t2 = t * t;
			float t3 = t2 * t;
			double x = 0.5f * ((2.0f * p1.X) + (-p0.X + p2.X) * t + (2.0f * p0.X - 5.0f * p1.X + 4 * p2.X - p3.X) * t2 + (-p0.X + 3.0f * p1.X - 3.0f * p2.X + p3.X) * t3);
			double y = 0.5f * ((2.0f * p1.Y) + (-p0.Y + p2.Y) * t + (2.0f * p0.Y - 5.0f * p1.Y + 4 * p2.Y - p3.Y) * t2 + (-p0.Y + 3.0f * p1.Y - 3.0f * p2.Y + p3.Y) * t3);
			//
			return new Poly2Tri.TriangulationPoint(x, y);
		}*/
	}
}
