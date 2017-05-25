using System;
using OpenTK.Graphics.OpenGL;

#pragma warning disable
namespace ColorControl.Shapes
{
	public class Point
	{
		public double X, Y, Z;

		public double x
		{
			get { return X; }
			set { X = value; }
		}

		public double y
		{
			get { return Y; }
			set { Y = value; }
		}

		public double z
		{
			get { return Z; }
			set { Z = value; }
		}

		public double S
		{
			get { return X; }
			set { X = value; }
		}

		public double T
		{
			get { return Y; }
			set { Y = value; }
		}

		public double R
		{
			get { return Z; }
			set { Z = value; }
		}

		public Point(double X, double Y, double Z = 0)
		{
			this.X = X;
			this.Y = Y;
			this.Z = Z;
		}

		public Point(double C)
		{
			this.X = C;
			this.Y = C;
			this.Z = C;
		}

		public double DistanceToPoint(Point B)
		{
			Point squaredDifference = (B - this) * (B - this);
			return Math.Sqrt(squaredDifference.X + squaredDifference.Y + squaredDifference.Z);
		}

		public void Render()
		{
			GL.Vertex3(X, Y, Z);
		}

		public void RenderAsTextureCoordinate()
		{
			GL.TexCoord3(S, T, R);
		}

		public static Point operator -(Point B, Point A)
		{
			return new Point(B.X - A.X, B.Y - A.Y, B.Z - A.Z);
		}

		public static Point operator +(Point A, Point B)
		{
			return new Point(A.X + B.X, A.Y + B.Y, A.Z + B.Z);
		}

		public static Point operator *(Point A, Point B)
		{
			return new Point(A.X * B.X, A.Y * B.Y, A.Z * B.Z);
		}
	}
}