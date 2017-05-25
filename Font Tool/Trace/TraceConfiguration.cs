using System;
using System.Xml.Serialization;

namespace AutoTrace
{
	[System.Reflection.ObfuscationAttribute(Feature = "properties renaming")]
	[XmlType(TypeName = "TraceConfiguration", Namespace = "http://darkabstraction.com/schemas/font.xsd")]
	public class TraceConfiguration
	{
		private int dilation = 4;
		private bool lineSmoothing = true;
		private double interpolateSharpCornersBy = 0;
		private bool pixelSmoothing = true;
		private double solidification = 0;
		private bool invertedSolid = false;
		private double xAdjustment = 0;
		private double yAdjustment = 0;
		private double zAdjustment = 0;
		private bool fillEdges = true;

		[XmlAttribute(AttributeName = "dilation")]
		public int Dilation
		{
			get { return Math.Max(0, dilation); }
			set { dilation = value; }
		}

		[XmlAttribute(AttributeName = "line-smoothing")]
		public bool LineSmoothing
		{
			get { return lineSmoothing; }
			set { lineSmoothing = value; }
		}

		[XmlAttribute(AttributeName = "interpolate-sharp-corners-by")]
		public double InterpolateSharpCornersBy
		{
			get { return Math.Max(0, interpolateSharpCornersBy); }
			set { interpolateSharpCornersBy = value; }
		}

		[XmlAttribute(AttributeName = "pixel-smoothing")]
		public bool PixelSmoothing
		{
			get { return pixelSmoothing; }
			set { pixelSmoothing = value; }
		}

		[XmlAttribute(AttributeName = "solidify-by")]
		public double Solidification
		{
			get { return solidification; }
			set { solidification = value; }
		}

		[XmlAttribute(AttributeName = "inverted-normals")]
		public bool InvertedSolid
		{
			get { return invertedSolid; }
			set { invertedSolid = value; }
		}

		[XmlAttribute(AttributeName = "x-offset")]
		public double XAdjustment
		{
			get { return xAdjustment; }
			set { xAdjustment = value; }
		}

		[XmlAttribute(AttributeName = "y-offset")]
		public double YAdjustment
		{
			get { return yAdjustment; }
			set { yAdjustment = value; }
		}

		[XmlAttribute(AttributeName = "z-offset")]
		public double ZAdjustment
		{
			get { return zAdjustment; }
			set { zAdjustment = value; }
		}

		[XmlAttribute(AttributeName = "fill-edges")]
		public bool FillEdges
		{
			get { return fillEdges; }
			set { fillEdges = value; }
		}

		[XmlIgnore]
		public int Padding
		{
			get
			{
				return 2 * Dilation;
			}
		}

		public TraceConfiguration() : this(0, false, false, 0, false) { }

		public TraceConfiguration(int dilation, bool smoothing = true, bool pixelSmoothing = true, double interpolateSharpCornersBy = 0, bool fillEdges = true)
		{
			this.dilation = dilation;
			this.lineSmoothing = smoothing;
			this.pixelSmoothing = pixelSmoothing;
			this.interpolateSharpCornersBy = interpolateSharpCornersBy;
			this.fillEdges = fillEdges;
		}
	}
}
