using System.Xml.Serialization;
using System;

namespace FontTool
{
	static partial class SupportFunctions
	{
		public static XmlSerializer bitmapFontSerializer = null;
		public static XmlSerializer BitmapFontSerializer {
			get {
				if (bitmapFontSerializer == null)
					bitmapFontSerializer = new XmlSerializer(typeof(BitmapFont), new Type[] {
						typeof(ClearToBaseImageEffect),
						typeof(ShadowEffect),
						typeof(WidenEffect),
						typeof(TransformEffect),
						typeof(ReplaceColorEffect),
						typeof(BorderEffect),
						typeof(UpscaleEffect),
						typeof(VerticalColorGradientEffect),
						typeof(TouchOfColorEffect),
						typeof(CombinedTouchOfColorEffect),
						typeof(CloseCropEffect),
						typeof(MultiplyEffect),
						typeof(GlyphShiftEffect),
						typeof(ItalicizeEffect),
						typeof(VerticalLineEffect),
						typeof(AutoTrace.TraceConfiguration),
						typeof(BitmapBoundaryTracer),
						typeof(BitmapTracer),
					});
				return bitmapFontSerializer;
			}
		}

		public static XmlSerializer ldmlSerializer = null;
		public static XmlSerializer LDMLSerializer {
			get {
				if (ldmlSerializer == null) {
					XmlRootAttribute ldmlAsXmlRoot = new XmlRootAttribute();
					ldmlAsXmlRoot.ElementName = "ldml";
					ldmlAsXmlRoot.IsNullable = false;
					//
					ldmlSerializer = new XmlSerializer(typeof(CLDR.LocaleDataMarkupLanguage.ldml), ldmlAsXmlRoot);
				}
				return ldmlSerializer;
			}
		}

		public static XmlSerializer supplementalDataSerializer = null;
		public static XmlSerializer SupplementalDataSerializer
		{
			get
			{
				if (supplementalDataSerializer == null)
				{
					XmlRootAttribute supplementalDataAsXmlRoot = new XmlRootAttribute();
					supplementalDataAsXmlRoot.ElementName = "supplementalData";
					supplementalDataAsXmlRoot.IsNullable = false;
					//
					supplementalDataSerializer = new XmlSerializer(typeof(CLDR.SupplementalDataLocaleDataMarkupLanguage.supplementalData), supplementalDataAsXmlRoot);
				}
				return supplementalDataSerializer;
			}
		}
	}
}
