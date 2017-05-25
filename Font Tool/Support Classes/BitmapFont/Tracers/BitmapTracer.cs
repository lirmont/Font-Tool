using System.Drawing;
using System.Xml.Serialization;

namespace FontTool
{
	[System.Reflection.ObfuscationAttribute(Feature = "properties renaming")]
	[XmlType(TypeName = "BitmapTracer", Namespace = "http://darkabstraction.com/schemas/font.xsd")]
	public class BitmapTracer : Tracer, ITracer
	{
		private AutoTrace.TraceConfiguration configuration;

		[XmlElement(ElementName = "configuration")]
		public AutoTrace.TraceConfiguration Configuration
		{
			get { return configuration; }
			set { configuration = value; }
		}

		public BitmapTracer() { }

		public BitmapTracer(AutoTrace.TraceConfiguration configuration)
		{
			this.configuration = configuration;
		}

		AutoTrace.Tracing ITracer.TraceImage(Bitmap image) {
			return AutoTrace.SupportFunctions.TraceImage(image, configuration);
		}
	}
}
