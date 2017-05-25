using System;
using System.Collections.Generic;
using System.Text;
using OpenTK.Graphics.OpenGL;

#pragma warning disable
namespace FontTool.Shapes
{
	public class Color
	{
		public double A
		{
			get { return color.A / 255.0; }
		}
		public double R
		{
			get { return color.R / 255.0; }
		}
		public double G
		{
			get { return color.G / 255.0; }
		}
		public double B
		{
			get { return color.B / 255.0; }
		}

		public System.Drawing.Color color = System.Drawing.Color.White;
		public string name;

		public Color(string name = "", System.Drawing.Color? color = null)
		{
			if (color != null)
				this.color = color ?? System.Drawing.Color.White;
			this.name = name;
		}

		public void Render()
		{
			GL.Color4(R, G, B, A); //Gl.glColor4d(R, G, B, A);
		}
	}

	public class Rect
	{
		public double x, y, width, height;

		public double Height
		{
			get { return height; }
			set { height = value; }
		}

		public double Width
		{
			get { return width; }
			set { width = value; }
		}

		public double Y
		{
			get { return y; }
			set { y = value; }
		}

		public double X
		{
			get { return x; }
			set { x = value; }
		}

		public Rect(double x = 0, double y = 0, double width = 1, double height = 1)
		{
			this.x = x;
			this.y = y;
			this.width = width;
			this.height = height;
		}
	}

	public class Line
	{
		public Point Start, End;
		public double distanceLeadingUpToLine;

		public double Length
		{
			get { return Start.DistanceToPoint(End); }
		}

		public double X1Property
		{
			get { return Start.X; }
			set { Start.X = value; }
		}

		public double Y1Property
		{
			get { return Start.Y; }
			set { Start.Y = value; }
		}

		public double Z1Property
		{
			get { return Start.Z; }
			set { Start.Z = value; }
		}

		public double X2Property
		{
			get { return End.X; }
			set { End.X = value; }
		}

		public double Y2Property
		{
			get { return End.Y; }
			set { End.Y = value; }
		}

		public double Z2Property
		{
			get { return End.Z; }
			set { End.Z = value; }
		}

		public Line(Point Start, Point End, double distanceLeadingUpToLine = 0)
		{
			this.Start = Start;
			this.End = End;
			this.distanceLeadingUpToLine = distanceLeadingUpToLine;
		}

		public Point EquationOfLine(Point t)
		{
			return this.Start + t * (this.End - this.Start);
		}

		public Point PointAtDistance(double distanceOnLine)
		{
			// Calculate the percentage of the requested distance against the distance of the line.
			double percentageOfDistance = (this.Length > 0) ? distanceOnLine / this.Length : 0;
			// Get a point-vector that represents the percentage of the line the point will end at; multiplied against the distance.
			Point percentagePoint = new Point(percentageOfDistance);
			// Line: <x, y, z> = Start<x, y, z> + t<x, y, z> * (End<x, y, z> - Start<x, y, z>)
			Point pointOnLine = EquationOfLine(percentagePoint);
			// Send the point back.
			return pointOnLine;
		}
	}

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

		public static Point operator /(Point A, Point B)
		{
			return new Point(A.X / B.X, A.Y / B.Y, A.Z / B.Z);
		}

		public override string ToString()
		{
			return string.Format("Shapes.Point({0}, {1}, {2})", new object[] { X, Y, Z });
		}
	}
}