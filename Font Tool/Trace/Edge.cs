using System.Drawing;

namespace AutoTrace
{
	public class Edge
	{
		private ReferenceContainer<PointF> p1;
		private ReferenceContainer<PointF> p2;
		private Edge next;
		private bool startOfLoop = false;

		public ReferenceContainer<PointF> P1
		{
			get { return p1; }
			set { p1 = value; }
		}

		public ReferenceContainer<PointF> P2
		{
			get { return p2; }
			set { p2 = value; }
		}

		public Edge Next
		{
			get { return next; }
			set { next = value; }
		}

		public bool IsFirst
		{
			get { return startOfLoop; }
			set { startOfLoop = value; }
		}

		public Edge(PointF p1, PointF p2)
			: this(p1, p2, false)
		{ }

		public Edge(PointF p1, PointF p2, bool isFirst)
		{
			this.p1 = new ReferenceContainer<PointF>(p1);
			this.p2 = new ReferenceContainer<PointF>(p2);
			this.startOfLoop = isFirst;
		}

		public Edge(ReferenceContainer<PointF> p1, PointF p2)
		{
			this.p1 = p1;
			this.p2 = new ReferenceContainer<PointF>(p2);
			this.startOfLoop = false;
		}
	}
}
