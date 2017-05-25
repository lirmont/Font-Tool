using System;
using System.Collections.Generic;
using System.Drawing;

namespace AutoTrace
{
	public class Pass
	{
		private List<PolyLine> subjects = new List<PolyLine>();
		private List<PolyLine> holes = new List<PolyLine>();
		public int PolygonsToBeUnioned { get { return Math.Max(0, subjects.Count - 1); } }
		public int PolygonsToBeSubtracted { get { return holes.Count; } }

		public List<PolyLine> Subjects
		{
			get { return subjects; }
			set { subjects = value; }
		}

		public List<PolyLine> Holes
		{
			get { return holes; }
			set { holes = value; }
		}

		public ClipperLib.PolyTree Solution
		{
			get
			{
				ClipperLib.PolyTree solutionTree = new ClipperLib.PolyTree();
				// Union all the subjects in this pass.
				ClipperLib.PolyTree unions = UnionParts(subjects);
				// Subtractions.
				if (PolygonsToBeSubtracted > 0)
				{
					// Union all the holes in this pass.
					ClipperLib.PolyTree unionedHoles = UnionParts(holes);
					// Prepare to subtract that union out.
					ClipperLib.Clipper clipper = new ClipperLib.Clipper();
					// Add what makes up the union of subjects.
					foreach (ClipperLib.PolyNode polygon in unions.m_AllPolys)
						clipper.AddPath(polygon.Contour, ClipperLib.PolyType.ptSubject, true);
					// Add what makes up the union of holes.
					foreach (ClipperLib.PolyNode polygon in unionedHoles.m_AllPolys)
						clipper.AddPath(polygon.Contour, ClipperLib.PolyType.ptClip, true);
					// Subtract the holes.
					clipper.Execute(ClipperLib.ClipType.ctDifference, solutionTree);
				}
				else
					solutionTree = unions;
				// Return the solution.
				return solutionTree;
			}
		}

		private static ClipperLib.PolyTree UnionParts(List<PolyLine> objectsToUnion)
		{
			// Prepare a solution.
			ClipperLib.PolyTree unions = new ClipperLib.PolyTree();
			ClipperLib.Clipper unionsClipper = new ClipperLib.Clipper();
			//
			int polygonsToBeUnioned = Math.Max(0, objectsToUnion.Count - 1);
			// Make sure there is an initial subject to union and subtract against.
			if (objectsToUnion.Count > 0)
			{
				List<ClipperLib.IntPoint> points = GetClipperPoints(objectsToUnion[0]);
				unionsClipper.AddPath(points, ClipperLib.PolyType.ptSubject, true);
				// Catch all union with empty clip.
				if (polygonsToBeUnioned == 0)
					unionsClipper.Execute(ClipperLib.ClipType.ctUnion, unions);
			}
			else // Solution is empty.
				return unions;
			// Union of subjects.
			if (objectsToUnion.Count > 1)
			{
				for (int i = 1; i < objectsToUnion.Count; i++)
					unionsClipper.AddPath(GetClipperPoints(objectsToUnion[i]), ClipperLib.PolyType.ptClip, true);
				// Union everything.
				unionsClipper.Execute(ClipperLib.ClipType.ctUnion, unions);
			}
			return unions;
		}

		public Pass()
		{ }

		public Pass(List<PolyLine> subjects) : this(subjects, null)
		{ }

		public Pass(List<PolyLine> subjects, List<PolyLine> holes)
		{
			if (subjects != null)
				this.subjects = subjects;
			if (holes != null)
				this.holes = holes;
		}

		private static List<ClipperLib.IntPoint> GetClipperPoints(List<PointF> list)
		{
			List<ClipperLib.IntPoint> points = new List<ClipperLib.IntPoint>();
			foreach (PointF point in list)
				points.Add(new ClipperLib.IntPoint(point.X, point.Y));
			return points;
		}

		private static List<ClipperLib.IntPoint> GetClipperPoints(PolyLine line)
		{
			return GetClipperPoints(new List<PointF>(line.PointsF));
		}
	}
}
