using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace FontTool
{
	public enum ColorDirection { East = 1, West = 2, North = 3, South = 4 };

	[System.Reflection.ObfuscationAttribute(Feature = "renaming", ApplyToMembers = true)]
	[XmlType(TypeName = "TouchOfColorEffect", Namespace = "http://darkabstraction.com/schemas/font.xsd")]
	public class TouchOfColorEffect : BitmapEffect
	{
		private ColorDirection Direction
		{
			get
			{
				return (Parameters != null && Parameters.Contains("direction")) ? (ColorDirection)Parameters["direction"].Value : ColorDirection.East;
			}
		}

		private int Steps
		{
			get
			{
				return (Parameters != null && Parameters.Contains("steps")) ? (int)Parameters["steps"].Value : 0;
			}
		}

		private Color Color
		{
			get
			{
				return (Parameters != null && Parameters.Contains("color")) ? (System.Drawing.Color)((XmlColor)Parameters["color"].Value) : Color.Black;
			}
		}

		public TouchOfColorEffect()
			: this(ColorDirection.East, 1, Color.Black)
		{
		}

		public TouchOfColorEffect(ColorDirection direction, int steps, Color color)
		{
			this.Name = "TouchOfColor";
			this.Parameters = new ParameterCollection() {
				new EffectParameter("direction", direction),
				new EffectParameter("steps", steps),
				new EffectParameter("color", new XmlColor(color))
			};
		}

		public TouchOfColorEffect(List<EffectParameter> parameters) : base("TouchOfColor", parameters) { }

		public override Bitmap Apply(Bitmap bitmap, OffsetableUnicodeCharacter character = null, FontInSize size = null)
		{
			return TouchOfColorEffect.EffectIteration(bitmap, Direction, Steps, Color);
		}

		public static Bitmap EffectIteration(Bitmap bitmap, ColorDirection Direction, int Steps, Color Color)
		{
			Bitmap outputBitmap = null;
			if (bitmap != null)
			{
				Size size = bitmap.Size;
				outputBitmap = (Bitmap)bitmap.Clone();
				BitmapProcessing.FastBitmap processor = new BitmapProcessing.FastBitmap(outputBitmap);
				processor.LockImage();
				{
					if (Direction == ColorDirection.East) {
						// Start left, go right.
						for (int column = 0; column < size.Width; column++)
						{
							bool atLeftEdge = (column == 0);
							for (int row = 0; row < size.Height; row++)
							{
								Color thisColor = processor.GetPixel(column, row);
								if (thisColor.A > 0)
								{
									// Look left.
									bool leftPixelIsBlank = atLeftEdge;
									if (!atLeftEdge)
									{
										Color leftPixel = processor.GetPixel(column - 1, row);
										if (leftPixel.A == 0)
											leftPixelIsBlank = true;
									}
									if (leftPixelIsBlank)
									{
										int step = 1;
										for (int x = column; x < column + Steps; x++)
										{
											if (x >= size.Width)
												break;
											else
											{
												int thisIndex = column + (step - 1);
												Color actualColor = (step == 1) ? thisColor : processor.GetPixel(thisIndex, row);
												if (actualColor.A == 0)
													break;
												Color postColor = Mix(actualColor, Color.FromArgb(Color.A / step, Color));
												// Replace the color.
												processor.SetPixel(thisIndex, row, postColor);
											}
											step++;
										}
									}
								}
							}
						}
					}
					else if (Direction == ColorDirection.West) {
						// Start right, go left.
						for (int column = size.Width - 1; column >= 0; column--)
						{
							bool atRightEdge = (column == (size.Width - 1));
							for (int row = 0; row < size.Height; row++)
							{
								Color thisColor = processor.GetPixel(column, row);
								if (thisColor.A > 0)
								{
									// Look right.
									bool rightPixelIsBlank = atRightEdge;
									if (!atRightEdge)
									{
										Color rightPixel = processor.GetPixel(column + 1, row);
										if (rightPixel.A == 0)
											rightPixelIsBlank = true;
									}
									if (rightPixelIsBlank)
									{
										int step = 1;
										for (int x = column; x > column - Steps; x--)
										{
											if (x < 0)
												break;
											else
											{
												int thisIndex = column - (step - 1);
												Color actualColor = (step == 1) ? thisColor : processor.GetPixel(thisIndex, row);
												if (actualColor.A == 0)
													break;
												Color postColor = Mix(actualColor, Color.FromArgb(Color.A / step, Color));
												// Replace the color.
												processor.SetPixel(thisIndex, row, postColor);
											}
											step++;
										}
									}
								}
							}
						}
					}
					else if (Direction == ColorDirection.North) {
						// Start end, go to beginning.
						for (int row = size.Height - 1; row >= 0; row--)
						{
							bool atBottomEdge = (row == size.Height - 1);
							for (int column = 0; column < size.Width; column++)
							{
								Color thisColor = processor.GetPixel(column, row);
								if (thisColor.A > 0)
								{
									// Look down.
									bool bottomPixelIsBlank = atBottomEdge;
									if (!atBottomEdge)
									{
										Color bottomPixel = processor.GetPixel(column, row + 1);
										if (bottomPixel.A == 0)
											bottomPixelIsBlank = true;
									}
									if (bottomPixelIsBlank)
									{
										int step = 1;
										for (int y = row; y > row - Steps; y--)
										{
											if (y < 0)
												break;
											else
											{
												int thisIndex = row - (step - 1);
												Color actualColor = (step == 1) ? thisColor : processor.GetPixel(column, thisIndex);
												if (actualColor.A == 0)
													break;
												Color postColor = Mix(actualColor, Color.FromArgb(Color.A / step, Color));
												// Replace the color.
												processor.SetPixel(column, thisIndex, postColor);
											}
											step++;
										}
									}
								}
							}
						}
					}
					else if (Direction == ColorDirection.South) {
						// Start beginning, to end.
						for (int row = 0; row < size.Height; row++)
						{
							bool atTopEdge = (row == 0);
							for (int column = 0; column < size.Width; column++)
							{
								Color thisColor = processor.GetPixel(column, row);
								if (thisColor.A > 0)
								{
									// Look up.
									bool topPixelIsBlank = atTopEdge;
									if (!atTopEdge)
									{
										Color topPixel = processor.GetPixel(column, row - 1);
										if (topPixel.A == 0)
											topPixelIsBlank = true;
									}
									if (topPixelIsBlank)
									{
										int step = 1;
										for (int y = row; y < row + Steps; y++)
										{
											if (y >= size.Height)
												break;
											else
											{
												int thisIndex = row + (step - 1);
												Color actualColor = (step == 1) ? thisColor : processor.GetPixel(column, thisIndex);
												if (actualColor.A == 0)
													break;
												Color postColor = Mix(actualColor, Color.FromArgb(Color.A / step, Color));
												// Replace the color.
												processor.SetPixel(column, thisIndex, postColor);
											}
											step++;
										}
									}
								}
							}
						}
					}
				}
				processor.UnlockImage();
				//
				if (bitmap != null)
					bitmap.Dispose();
			}
			return outputBitmap;
		}

		public static Color Mix(Color Target, Color Blend, bool IgnoreAlpha = false)
		{
			//R = (1 - Blend Alpha) * Target + Blend Alpha * Blend
			double bA = Blend.A / 255.0;
			double tA = 1 - bA;
			double r = (tA * Target.R / 255.0) + (bA * Blend.R / 255.0);
			double g = (tA * Target.G / 255.0) + (bA * Blend.G / 255.0);
			double b = (tA * Target.B / 255.0) + (bA * Blend.B / 255.0);
			double a = (IgnoreAlpha) ? ((Target.A / 255.0) + (bA)) / 2.0 : 1;
			return Color.FromArgb((int)(a * 255), (int)(r * 255), (int)(g * 255), (int)(b * 255));
		}

		public static Color ScreenBackup(Color Target, Color Blend, bool IgnoreAlpha = false)
		{
			//R = 1 - (1-Target) x (1-Blend)
			double tA = Target.A / 255.0;
			double bA = Blend.A / 255.0;
			double r = 1 - (1 - tA * Target.R / 255.0) * (1 - bA * Blend.R / 255.0);
			double g = 1 - (1 - tA * Target.G / 255.0) * (1 - bA * Blend.G / 255.0);
			double b = 1 - (1 - tA * Target.B / 255.0) * (1 - bA * Blend.B / 255.0);
			double a = (IgnoreAlpha) ? 1 - (1 - Target.A / 255.0) * (1 - Blend.A / 255.0) : 1;
			return Color.FromArgb((int)(a * 255), (int)(r * 255), (int)(g * 255), (int)(b * 255));
		}

		public override OffsetableUnicodeCharacter Delta(OffsetableUnicodeCharacter character)
		{
			return character;
		}

		//
		public new static Dictionary<string, ParameterDescription> ParameterDescriptions = new Dictionary<string, ParameterDescription>
		{
			{"direction", new ParameterDescription("Direction to apply the color to (example: East applies the color from left edges across to right edges.", typeof(ColorDirection), new ComboBox())},
			{"steps", new ParameterDescription("Amount of times (in pixels) to apply the color (to a lesser degree each time).", typeof(ColorDirection), new MaskedTextBox()) },
			{"color", new ParameterDescription("ARGB value that determines the color to add.", typeof(XmlColor), new Panel()) }
		};

		public override Dictionary<string, ParameterDescription> getDescriptions()
		{
			return TouchOfColorEffect.ParameterDescriptions;
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
