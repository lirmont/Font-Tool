using System.Collections.Generic;
using System.Drawing;
using TriangleNet.Geometry;
using TriangleNet.Meshing;
using System.Xml.Serialization;

namespace FontTool
{
	[System.Reflection.ObfuscationAttribute(Feature = "properties renaming")]
	[XmlType(TypeName = "BitmapBoundaryTracer", Namespace = "http://darkabstraction.com/schemas/font.xsd")]
	public class BitmapBoundaryTracer : Tracer, ITracer
	{
		public static GenericMesher Triangulator = new GenericMesher();

		public BitmapBoundaryTracer() { }

		AutoTrace.Tracing ITracer.TraceImage(Bitmap image) {
			return BitmapBoundaryTracer.TraceImage(image);
		}

		public static new AutoTrace.Tracing TraceImage(Bitmap image) {
			bool imageExists = (image != null) ? true : false;
			if (imageExists)
			{
				//
				Polygon polygon = new Polygon();
				// Create rectangle.
				polygon.AddContour(new List<Vertex> {
					new Vertex(0, image.Height),
					new Vertex(0, 0),
					new Vertex(image.Width, 0),
					new Vertex(image.Width, image.Height),
				});
				// Get mesh of rectangle.
				var mesh = Triangulator.Triangulate(polygon);
				AutoTrace.Tracing tracing = new AutoTrace.Tracing(image, mesh);
				return tracing;
			}
			else
				return new AutoTrace.Tracing(image);
		}
	}
}
