using System;
using System.Drawing;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Windows.Forms;

namespace FontTool
{
	[System.Reflection.ObfuscationAttribute(Feature = "renaming", ApplyToMembers = true)]
	[XmlType(TypeName = "ReplaceColorEffect", Namespace = "http://darkabstraction.com/schemas/font.xsd")]
	public class ReplaceColorEffect : BitmapEffect
	{
		private Color oldColor
		{
			get
			{
				return (Parameters != null && Parameters.Contains("old-color")) ? (System.Drawing.Color)((XmlColor)Parameters["old-color"].Value) : Color.Black;
			}
		}

		private Color newColor
		{
			get
			{
				return (Parameters != null && Parameters.Contains("new-color")) ? (System.Drawing.Color)((XmlColor)Parameters["new-color"].Value) : Color.Black;
			}
		}

		public ReplaceColorEffect()
			: this(Color.Black, Color.WhiteSmoke)
		{
		}

		public ReplaceColorEffect(Color oldColor, Color newColor)
		{
			this.Name = "ReplaceColor";
			this.Parameters = new ParameterCollection() {
				new EffectParameter("old-color", new XmlColor(oldColor)),
				new EffectParameter("new-color", new XmlColor(newColor))
			};
		}

		public ReplaceColorEffect(List<EffectParameter> parameters) : base("ReplaceColor", parameters) { }

		public override Bitmap Apply(Bitmap bitmap, OffsetableUnicodeCharacter character = null, FontInSize size = null)
		{
			return EffectIteration(bitmap);
		}

		private Bitmap EffectIteration(Bitmap bitmap)
		{
			Bitmap outputBitmap = (Bitmap)bitmap.Clone();
			if (outputBitmap != null)
			{
				//
				BitmapProcessing.FastBitmap processor = new BitmapProcessing.FastBitmap(outputBitmap);
				processor.LockImage();
				{
					for (int column = 0; column < outputBitmap.Width; column++)
						for (int row = 0; row < outputBitmap.Height; row++)
						{
							Color pixel = processor.GetPixel(column, row);
							if (pixel.ToArgb() == oldColor.ToArgb())
								processor.SetPixel(column, row, newColor);
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
			{"old-color", new ParameterDescription("ARGB value that determines the color to replace.", typeof(XmlColor), new Panel()) },
			{"new-color", new ParameterDescription("ARGB value that determines the color to draw as a replacement.", typeof(XmlColor), new Panel()) }
		};

		public override Dictionary<string, ParameterDescription> getDescriptions()
		{
			return ReplaceColorEffect.ParameterDescriptions;
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
