using System.Collections.Generic;
using System.Drawing;
using System.Xml.Serialization;
using System.Windows.Forms;

namespace FontTool
{
	[System.Reflection.ObfuscationAttribute(Feature = "renaming", ApplyToMembers = true)]
	[XmlType(TypeName = "TransformEffect", Namespace = "http://darkabstraction.com/schemas/font.xsd")]
	public class TransformEffect : BitmapEffect
	{
		private int width
		{
			get {
				return (Parameters != null && Parameters.Contains("width")) ? (int)Parameters["width"].Value : 0;
			}
		}
		private int advances
		{
			get
			{
				return (Parameters != null && Parameters.Contains("advances")) ? (int)Parameters["advances"].Value : 0;
			}
		}
		private int ascends
		{
			get
			{
				return (Parameters != null && Parameters.Contains("ascends")) ? (int)Parameters["ascends"].Value : 0;
			}
		}

		public TransformEffect() : this(0, 0, 0)
		{
		}

		public TransformEffect(int width, int advances, int ascends)
		{
			this.Name = "Transform";
			this.Parameters = new ParameterCollection() {
				new EffectParameter("width", width),
				new EffectParameter("advances", advances),
				new EffectParameter("ascends", ascends)
			};
		}

		public TransformEffect(List<EffectParameter> parameters) : base("Transform", parameters) { }

		public override Bitmap Apply(Bitmap bitmap, OffsetableUnicodeCharacter character = null, FontInSize size = null)
		{
			return EffectIteration(bitmap);
		}

		private Bitmap EffectIteration(Bitmap bitmap)
		{
			return bitmap;
		}

		public override OffsetableUnicodeCharacter Delta(OffsetableUnicodeCharacter character)
		{
			OffsetableUnicodeCharacter derivedCharacter = new OffsetableUnicodeCharacter((UnicodeCharacter)character, character.OffsetWidth + width, character.OffsetX + advances, character.OffsetY + ascends, 1, 1);
			return derivedCharacter;
		}

		//
		public new static Dictionary<string, ParameterDescription> ParameterDescriptions = new Dictionary<string, ParameterDescription>
		{
			{"width", new ParameterDescription("Amount (in pixels) to increase or decrease character width.", typeof(float), new MaskedTextBox()) },
			{"advances", new ParameterDescription("Amount (in pixels) to horizontally slide the image.", typeof(float), new MaskedTextBox()) },
			{"ascends", new ParameterDescription("Amount (in pixels) to vertically slide the image.", typeof(float), new MaskedTextBox())}
		};

		public override Dictionary<string, ParameterDescription> getDescriptions()
		{
			return TransformEffect.ParameterDescriptions;
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
