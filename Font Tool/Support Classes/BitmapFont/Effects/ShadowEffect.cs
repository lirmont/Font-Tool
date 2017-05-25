using System;
using System.Drawing;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Windows.Forms;

namespace FontTool
{
	[System.Reflection.ObfuscationAttribute(Feature = "renaming", ApplyToMembers = true)]
	[XmlType(TypeName = "ShadowEffect", Namespace = "http://darkabstraction.com/schemas/font.xsd")]
	public class ShadowEffect : BitmapEffect
	{
		private int x {
			get {
				return (Parameters != null && Parameters.Contains("x")) ? (int)Parameters["x"].Value : 0;
			}
		}
		private int y {
			get
			{
				return (Parameters != null && Parameters.Contains("y")) ? (int)Parameters["y"].Value : 0;
			}
		}
		private Color color {
			get
			{
				return (Parameters != null && Parameters.Contains("color")) ? (System.Drawing.Color)((XmlColor)Parameters["color"].Value) : Color.Black;
			}
		}

		public ShadowEffect() : this(0, 0, Color.Black)
		{
		}

		public ShadowEffect(int x, int y, Color color)
		{
			this.Name = "Shadow";
			this.Parameters = new ParameterCollection() {
				new EffectParameter("x", x),
				new EffectParameter("y", y),
				new EffectParameter("color", new XmlColor(color))
			};
		}

		public ShadowEffect(List<EffectParameter> parameters) : base("Shadow", parameters) { }

		public override Bitmap Apply(Bitmap bitmap, OffsetableUnicodeCharacter character = null, FontInSize size = null)
		{
			return EffectIteration(bitmap);
		}

		private Bitmap EffectIteration(Bitmap bitmap)
		{
			int width = (int)Math.Abs(x), height = (int)Math.Abs(y);
			Bitmap outputBitmap = null;
			if (bitmap != null)
			{
				outputBitmap = new Bitmap(bitmap.Width + width, bitmap.Height + height);
				Bitmap solidBitmap = (Bitmap)bitmap.Clone();
				for (int column = 0; column < solidBitmap.Width; column++)
					for (int row = 0; row < solidBitmap.Height; row++)
					{
						Color thisColor = solidBitmap.GetPixel(column, row);
						if (thisColor.A > 0)
							solidBitmap.SetPixel(column, row, Color.FromArgb(thisColor.A, color));
					}
				// Create appropriately sized image.
				using (Graphics g = Graphics.FromImage(outputBitmap))
				{
					Point shadowPoint = new Point(x > 0 ? x : 0, y > 0 ? y : 0);
					Point imagePoint = new Point(x > 0 ? 0 : width, y > 0 ? 0 : height);
					// Splat shadow image.
					g.DrawImage(solidBitmap, shadowPoint);
					// Splat actual image.
					g.DrawImage(bitmap, imagePoint);
				}
				//
				solidBitmap.Dispose();
				bitmap.Dispose();
			}
			return outputBitmap;
		}

		public override OffsetableUnicodeCharacter Delta(OffsetableUnicodeCharacter character)
		{
			int width = (int)Math.Abs(x), height = (int)Math.Abs(y);
			int widthDelta = width;
			int ascendsDelta = (y > 0) ? -y : 0;
			OffsetableUnicodeCharacter derivedCharacter = new OffsetableUnicodeCharacter((UnicodeCharacter)character, character.OffsetWidth + widthDelta, character.OffsetX + 0, character.OffsetY + ascendsDelta, 1f, 1f);
			return derivedCharacter;
		}

		//
		public new static Dictionary<string, ParameterDescription> ParameterDescriptions = new Dictionary<string, ParameterDescription>
		{
			{"x", new ParameterDescription("Amount (in pixels) to horizontally slide the shadow.", typeof(float), new MaskedTextBox())},
			{"y", new ParameterDescription("Amount (in pixels) to vertically slide the shadow.", typeof(float), new MaskedTextBox()) },
			{"color", new ParameterDescription("ARGB value that determines the color of the shadow.", typeof(XmlColor), new Panel()) }
		};

		public override Dictionary<string, ParameterDescription> getDescriptions()
		{
			return ShadowEffect.ParameterDescriptions;
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
