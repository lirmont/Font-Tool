using OpenTK;
using System.Collections.Generic;

namespace FontTool.Trace.Outbound
{
	public class Vertex
	{
		private Vector3d? position = null;
		private Vector3d? normal = null;
		private Vector3d? uvw = null;
		private Dictionary<string, Vector4d> auxiliary = new Dictionary<string, Vector4d>();

		public Vector3d? Position
		{
			get { return position; }
			set { position = value; }
		}

		public Vector3d? Normal
		{
			get { return normal; }
			set { normal = value; }
		}

		public Vector3d? UVW
		{
			get { return uvw; }
			set { uvw = value; }
		}

		public Dictionary<string, Vector4d> Auxiliary
		{
			get { return auxiliary; }
			set { auxiliary = value; }
		}

		public Vertex(Vertex other, Vector3d normal)
		{
			this.position = new Vector3d(other.Position.Value.X, other.Position.Value.Y, other.Position.Value.Z);
			this.uvw = new Vector3d(other.UVW.Value.X, other.UVW.Value.Y, other.UVW.Value.Z);
			this.normal = new Vector3d(normal.X, normal.Y, normal.Z);
			this.auxiliary = other.auxiliary;
		}

		public Vertex(double x, double y, double z)
		{
			this.position = new Vector3d(x, y, z);
		}

		public Vertex(double x, double y, double z, double nx, double ny, double nz, double u, double v, double w) : this(x, y, z)
		{
			this.normal = new Vector3d(nx, ny, nz);
			this.uvw = new Vector3d(u, v, w);
		}

		public void addAuxiliaryEntry(string name, double value)
		{
			addAuxiliaryEntry(name, new Vector4d(value, 0, 0, 0));
		}

		public void addAuxiliaryEntry(string name, double value, double secondValue)
		{
			addAuxiliaryEntry(name, new Vector4d(value, secondValue, 0, 0));
		}

		public void addAuxiliaryEntry(string name, double value, double secondValue, double thirdValue)
		{
			addAuxiliaryEntry(name, new Vector4d(value, secondValue, thirdValue, 0));
		}

		public void addAuxiliaryEntry(string name, Vector4d values)
		{
			if (!this.auxiliary.ContainsKey(name))
				this.auxiliary.Add(name, values);
			else
				this.auxiliary[name] = values;
		}

		public static implicit operator Vertex(TriangleNet.Geometry.Vertex v)
		{
			return new Vertex(v.x, v.y, 0);
		}
	}
}
