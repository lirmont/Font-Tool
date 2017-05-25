using System.Collections.Generic;
using System.Xml.Serialization;
using System.Drawing;

namespace FontTool
{
	[System.Reflection.ObfuscationAttribute(Feature = "properties renaming")]
	[XmlType(TypeName = "Tracer", Namespace = "http://darkabstraction.com/schemas/font.xsd")]
	public class Tracer : ITracer
	{
		[XmlIgnore]
		public bool HasConfiguration
		{
			get {
				return (this is BitmapTracer) ? true : false;
			}
		}

		public Tracer() { }

		AutoTrace.Tracing ITracer.TraceImage(Bitmap image) {
			return Tracer.TraceImage(image);
		}

		public static AutoTrace.Tracing TraceImage(Bitmap image) {
			return new AutoTrace.Tracing(image);
		}
	}
}
