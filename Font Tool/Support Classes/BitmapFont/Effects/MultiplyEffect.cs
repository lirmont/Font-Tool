using System;
using System.Drawing;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Windows.Forms;
using ColorControl;

namespace FontTool
{
	[System.Reflection.ObfuscationAttribute(Feature = "renaming", ApplyToMembers = true)]
	[XmlType(TypeName = "MultiplyEffect", Namespace = "http://darkabstraction.com/schemas/font.xsd")]
	public class MultiplyEffect : BitmapEffect
	{
		private Color color
		{
			get
			{
				return (Parameters != null && Parameters.Contains("color")) ? (System.Drawing.Color)((XmlColor)Parameters["color"].Value) : Color.WhiteSmoke;
			}
		}

		public MultiplyEffect()
			: this(Color.WhiteSmoke)
		{
		}

		public MultiplyEffect(Color color)
		{
			this.Name = "Multiply";
			this.Parameters = new ParameterCollection() {
				new EffectParameter("color", new XmlColor(color)),
			};
		}

		public MultiplyEffect(List<EffectParameter> parameters) : base("Multiply", parameters) { }

		public override Bitmap Apply(Bitmap bitmap, OffsetableUnicodeCharacter character = null, FontInSize size = null)
		{
			return EffectIteration(bitmap, character, size);
		}

		private Bitmap EffectIteration(Bitmap bitmap, OffsetableUnicodeCharacter character, FontInSize size)
		{
			Bitmap outputBitmap = (Bitmap)bitmap.Clone();
			if (outputBitmap != null && character != null && size != null)
			{
				//
				BitmapProcessing.FastBitmap processor = new BitmapProcessing.FastBitmap(outputBitmap);
				processor.LockImage();
				{
					for (int column = 0; column < outputBitmap.Width; column++)
						for (int row = 0; row < outputBitmap.Height; row++)
						{
							Color pixel = processor.GetPixel(column, row);
							if (pixel.A > 0)
							{
								Color replacementPixel = Color.FromArgb((pixel.A * color.A) / 255, (pixel.R * color.R) / 255, (pixel.G * color.G) / 255, (pixel.B * color.B) / 255);
								processor.SetPixel(column, row, replacementPixel);
							}
						}
				}
				processor.UnlockImage();
			}
			//
			if (bitmap != null)
				bitmap.Dispose();
			//
			return outputBitmap;
		}

		public override OffsetableUnicodeCharacter Delta(OffsetableUnicodeCharacter character)
		{
			return character;
		}

		//
		public new static Dictionary<string, ParameterDescription> ParameterDescriptions = new Dictionary<string, ParameterDescription>
		{
			{"color", new ParameterDescription("ARGB value that determines the color to multiply all other colors by.", typeof(XmlColor), new Panel()) }, 
		};

		public override Dictionary<string, ParameterDescription> getDescriptions()
		{
			return MultiplyEffect.ParameterDescriptions;
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
