// CombinedTouchOfColor
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace FontTool
{
	[System.Reflection.ObfuscationAttribute(Feature = "renaming", ApplyToMembers = true)]
	[XmlType(TypeName = "CombinedTouchOfColorEffect", Namespace = "http://darkabstraction.com/schemas/font.xsd")]
	public class CombinedTouchOfColorEffect : BitmapEffect
	{
		private int Steps
		{
			get
			{
				return (Parameters != null && Parameters.Contains("steps")) ? (int)Parameters["steps"].Value : 0;
			}
		}

		private Color LeftColor
		{
			get
			{
				return (Parameters != null && Parameters.Contains("left-color")) ? (System.Drawing.Color)((XmlColor)Parameters["left-color"].Value) : Color.Black;
			}
		}

		private Color RightColor
		{
			get
			{
				return (Parameters != null && Parameters.Contains("right-color")) ? (System.Drawing.Color)((XmlColor)Parameters["right-color"].Value) : Color.Black;
			}
		}

		public CombinedTouchOfColorEffect()
			: this(1, Color.White, Color.Black)
		{
		}

		public CombinedTouchOfColorEffect(int steps, Color leftColor, Color rightColor)
		{
			this.Name = "CombinedTouchOfColor";
			this.Parameters = new ParameterCollection() {
				new EffectParameter("left-color", new XmlColor(leftColor)),
				new EffectParameter("right-color", new XmlColor(rightColor)),
				new EffectParameter("steps", steps)
			};
		}

		public CombinedTouchOfColorEffect(List<EffectParameter> parameters) : base("CombinedTouchOfColor", parameters) { }

		public override Bitmap Apply(Bitmap bitmap, OffsetableUnicodeCharacter character = null, FontInSize size = null)
		{
			return EffectIteration(bitmap);
		}

		private Bitmap EffectIteration(Bitmap bitmap)
		{
			Bitmap outputBitmap = null;
			outputBitmap = (Bitmap)bitmap.Clone();
			outputBitmap = TouchOfColorEffect.EffectIteration(outputBitmap, ColorDirection.West, Steps, RightColor);
			outputBitmap = TouchOfColorEffect.EffectIteration(outputBitmap, ColorDirection.North, Steps, RightColor);
			outputBitmap = TouchOfColorEffect.EffectIteration(outputBitmap, ColorDirection.East, Steps, LeftColor);
			outputBitmap = TouchOfColorEffect.EffectIteration(outputBitmap, ColorDirection.South, Steps, LeftColor);
			bitmap.Dispose();
			return outputBitmap;
		}

		public override OffsetableUnicodeCharacter Delta(OffsetableUnicodeCharacter character)
		{
			return character;
		}

		//
		public new static Dictionary<string, ParameterDescription> ParameterDescriptions = new Dictionary<string, ParameterDescription>
		{
			{"steps", new ParameterDescription("Amount of times (in pixels) to apply the color (to a lesser degree each time).", typeof(ColorDirection), new MaskedTextBox()) },
			{"left-color", new ParameterDescription("ARGB value that determines the color to add to the left and top.", typeof(XmlColor), new Panel()) },
			{"right-color", new ParameterDescription("ARGB value that determines the color to add to the right and bottom.", typeof(XmlColor), new Panel()) }
		};

		public override Dictionary<string, ParameterDescription> getDescriptions()
		{
			return CombinedTouchOfColorEffect.ParameterDescriptions;
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

