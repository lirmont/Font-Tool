using OpenTK;

namespace FontTool.Trace.Outbound
{
	public class Triangle
	{
		private int p0, p1, p2;
		private Vector3d? normal = null;

		public int P0
		{
			get { return p0; }
			set { p0 = value; }
		}

		public int P1
		{
			get { return p1; }
			set { p1 = value; }
		}

		public int P2
		{
			get { return p2; }
			set { p2 = value; }
		}

		public Vector3d? Normal
		{
			get { return normal; }
			set { normal = value; }
		}

		public Triangle(int p0, int p1, int p2, Vector3d? normal = null)
		{
			this.p0 = p0;
			this.p1 = p1;
			this.p2 = p2;
			this.normal = normal;
		}
	}
}
