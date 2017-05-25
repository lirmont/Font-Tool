using System;
using System.Drawing;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Windows.Forms;
using ColorControl;

namespace FontTool
{
	[System.Reflection.ObfuscationAttribute(Feature = "renaming", ApplyToMembers = true)]
	[XmlType(TypeName = "VerticalColorGradientEffect", Namespace = "http://darkabstraction.com/schemas/font.xsd")]
	public class VerticalColorGradientEffect : BitmapEffect
	{
		private Color descendingColor
		{
			get
			{
				return (Parameters != null && Parameters.Contains("descending-color")) ? (System.Drawing.Color)((XmlColor)Parameters["descending-color"].Value) : Color.Black;
			}
		}

		private Color baselineColor
		{
			get
			{
				return (Parameters != null && Parameters.Contains("baseline-color")) ? (System.Drawing.Color)((XmlColor)Parameters["baseline-color"].Value) : Color.Black;
			}
		}

		private Color ascendingColor
		{
			get
			{
				return (Parameters != null && Parameters.Contains("ascending-color")) ? (System.Drawing.Color)((XmlColor)Parameters["ascending-color"].Value) : Color.Black;
			}
		}

		public VerticalColorGradientEffect()
			: this(Color.Black, Color.WhiteSmoke, Color.White)
		{
		}

		public VerticalColorGradientEffect(Color descendingColor, Color baselineColor, Color ascendingColor)
		{
			this.RedrawOnCharacterOffsetChanged = true;
			this.Name = "VerticalColorGradient";
			this.Parameters = new ParameterCollection() {
				new EffectParameter("descending-color", new XmlColor(descendingColor)),
				new EffectParameter("baseline-color", new XmlColor(baselineColor)),
				new EffectParameter("ascending-color", new XmlColor(ascendingColor))
			};
		}

		public VerticalColorGradientEffect(List<EffectParameter> parameters) : base("VerticalColorGradient", parameters) { }

		public override Bitmap Apply(Bitmap bitmap, OffsetableUnicodeCharacter character = null, FontInSize size = null)
		{
			return EffectIteration(bitmap, character, size);
		}

		private Bitmap EffectIteration(Bitmap bitmap, OffsetableUnicodeCharacter character, FontInSize size)
		{
			Bitmap outputBitmap = (Bitmap)bitmap.Clone();
			if (outputBitmap != null && character != null && size != null)
			{
				List<Color> gradient = stepColors(size.TargetDescent, size.TargetAscent);
				int maxSteps = (int)(size.TargetDescent + size.TargetAscent);
				//
				if (gradient.Count > 0)
				{
					BitmapProcessing.FastBitmap processor = new BitmapProcessing.FastBitmap(outputBitmap);
					processor.LockImage();
					{
						for (int column = 0; column < outputBitmap.Width; column++)
							for (int row = 0; row < outputBitmap.Height; row++)
							{
								Color pixel = processor.GetPixel(column, row);
								if (pixel.A > 0)
								{
									int rowComplement = outputBitmap.Height - row - 1;
									int desiredRowInGradient = (int)Math.Max(0, Math.Min(gradient.Count - 1, rowComplement + size.TargetDescent + character.OffsetY));
									//
									if (gradient.Count > desiredRowInGradient)
										processor.SetPixel(column, row, gradient[desiredRowInGradient]);
								}
							}
					}
					processor.UnlockImage();
				}
			}
			//
			if (bitmap != null)
				bitmap.Dispose();
			//
			return outputBitmap;
		}

		private List<Color> stepColors(double minorStepCount, double majorStepCount)
		{
			CIELab start = new RGB(descendingColor);
			CIELab baseline = new RGB(baselineColor);
			CIELab end = new RGB(ascendingColor);
			List<Color> list = new List<Color>();
			if (minorStepCount > 0 && majorStepCount > 0)
			{
				list.AddRange(stepToColor(start, baseline, minorStepCount));
				list.RemoveAt(list.Count - 1);
				list.AddRange(stepToColor(baseline, end, majorStepCount));
			}
			else if (minorStepCount > 0)
			{
				list.AddRange(stepToColor(start, baseline, minorStepCount));
			}
			else if (majorStepCount > 0)
			{
				list.AddRange(stepToColor(baseline, end, majorStepCount));
			}
			return list;
		}

		private static List<Color> stepToColor(CIELab start, CIELab end, double stepCount)
		{
			double deltaX = (end.L - start.L) / (double)stepCount;
			double deltaY = (end.A - start.A) / (double)stepCount;
			double deltaZ = (end.B - start.B) / (double)stepCount;
			List<Color> list = new List<Color>();
			for (int i = 1; i <= stepCount; i++)
			{
				CIELab next = new CIELab(start.L + i * deltaX, start.A + i * deltaY, start.B + i * deltaZ);
				list.Add((Color)(RGB)next);
			}
			return list;
		}

		public override OffsetableUnicodeCharacter Delta(OffsetableUnicodeCharacter character)
		{
			return character;
		}

		//
		public new static Dictionary<string, ParameterDescription> ParameterDescriptions = new Dictionary<string, ParameterDescription>
		{
			{"descending-color", new ParameterDescription("ARGB value that determines the color at the target descent.", typeof(XmlColor), new Panel()) },
			{"baseline-color", new ParameterDescription("ARGB value that determines the color at the baseline.", typeof(XmlColor), new Panel()) },
			{"ascending-color", new ParameterDescription("ARGB value that determines the color at the target ascent.", typeof(XmlColor), new Panel()) }
		};

		public override Dictionary<string, ParameterDescription> getDescriptions()
		{
			return VerticalColorGradientEffect.ParameterDescriptions;
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
