using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml.Serialization;
using System.Windows.Forms;

namespace FontTool
{
	[System.Reflection.ObfuscationAttribute(Feature = "renaming", ApplyToMembers = true)]
	[XmlType(TypeName = "GlyphShiftEffect", Namespace = "http://darkabstraction.com/schemas/font.xsd")]
	public class GlyphShiftEffect : BitmapEffect
	{
		private int width {
			get {
				return (Parameters != null && Parameters.Contains("width")) ? (int)Parameters["width"].Value : 0; 
			}
		}

		private int ascends
		{
			get
			{
				return (Parameters != null && Parameters.Contains("ascends")) ? (int)Parameters["ascends"].Value : 0;
			}
		}

		private int advances
		{
			get
			{
				return (Parameters != null && Parameters.Contains("advances")) ? (int)Parameters["advances"].Value : 0;
			}
		}

		public GlyphShiftEffect()
			: this(0, 0, 0)
		{ }

		public GlyphShiftEffect(int width, int ascends, int advances)
		{
			this.Name = "GlyphShift";
			this.Parameters = new ParameterCollection() {
				new EffectParameter("width", width),
				new EffectParameter("ascends", ascends),
				new EffectParameter("advances", advances),
			};
		}

		public GlyphShiftEffect(List<EffectParameter> parameters) : base("GlyphShift", parameters) { }

		public override Bitmap Apply(Bitmap bitmap, OffsetableUnicodeCharacter character = null, FontInSize size = null)
		{
			Bitmap outputBitmap = (Bitmap)bitmap.Clone();
			//
			return outputBitmap;
		}

		public override OffsetableUnicodeCharacter Delta(OffsetableUnicodeCharacter character)
		{
			OffsetableUnicodeCharacter derivedCharacter = new OffsetableUnicodeCharacter((UnicodeCharacter)character, character.OffsetWidth + width, character.OffsetX + advances, character.OffsetY + ascends, 1f, 1f);
			return derivedCharacter;
		}

		//
		public new static Dictionary<string, ParameterDescription> ParameterDescriptions = new Dictionary<string, ParameterDescription>
		{
			{"width", new ParameterDescription("Number to adjust width of each character by.", typeof(float), new MaskedTextBox()) },
			{"ascends", new ParameterDescription("Number to adjust ascent of each character by.", typeof(float), new MaskedTextBox()) },
			{"advances", new ParameterDescription("Number to adjust advancement of each character by.", typeof(float), new MaskedTextBox()) },
		};

		public override Dictionary<string, ParameterDescription> getDescriptions()
		{
			return GlyphShiftEffect.ParameterDescriptions;
		}

		public override string getDescription(string parameterName)
		{
			return GetDescriptionFromParameters(parameterName, ParameterDescriptions);
		}

		public override Control getValidatingControl(string parameterName, object value)
		{
			return GetValidatingControlFromParameters(parameterName, value, descriptions: getDescriptions());
		}
	}
}
