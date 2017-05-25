using System.Drawing;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Windows.Forms;

namespace FontTool
{
	[System.Reflection.ObfuscationAttribute(Feature = "renaming", ApplyToMembers = true)]
	[XmlType(TypeName = "VerticalLineEffect", Namespace = "http://darkabstraction.com/schemas/font.xsd")]
	public class VerticalLineEffect : BitmapEffect
	{
		private Color color {
			get
			{
				return (Parameters != null && Parameters.Contains("color")) ? (System.Drawing.Color)((XmlColor)Parameters["color"].Value) : Color.Black;
			}
		}

		private int x {
			get
			{
				return (Parameters != null && Parameters.Contains("x")) ? (int)Parameters["x"].Value : 0;
			}
		}

		private bool doNotOverwriteExistingData
		{
			get { return (Parameters != null && Parameters.Contains("do-not-overwrite-existing-data")) ? (bool)Parameters["do-not-overwrite-existing-data"].Value : true; }
		}

		private int requiredHorizontalNeighbors
		{
			get
			{
				return (Parameters != null && Parameters.Contains("required-horizontal-neighbors")) ? (int)Parameters["required-horizontal-neighbors"].Value : 1;
			}
		}

		public VerticalLineEffect() : this(Color.Black, 0, true, 1)
		{
		}

		public VerticalLineEffect(Color color, int x, bool doNotOverwriteExistingData, int requiredHorizontalNeighbors)
		{
			this.Name = "VerticalLine";
			this.Parameters = new ParameterCollection() {
				new EffectParameter("color", new XmlColor(color)),
				new EffectParameter("x", x),
				new EffectParameter("do-not-overwrite-existing-data", doNotOverwriteExistingData),
				new EffectParameter("required-horizontal-neighbors", requiredHorizontalNeighbors),
			};
		}

		public VerticalLineEffect(List<EffectParameter> parameters) : base("VerticalLine", parameters) { }

		public override Bitmap Apply(Bitmap bitmap, OffsetableUnicodeCharacter character = null, FontInSize size = null)
		{
			Bitmap solidBitmap = GetPaddedImage((Bitmap)bitmap.Clone());
			Bitmap outputBitmap = EffectIteration(solidBitmap);
			return outputBitmap;
		}

		private Bitmap EffectIteration(Bitmap bitmap)
		{
			if (bitmap != null)
			{
				int offsetX = (int)bitmap.Tag;
				int column = offsetX + x;
				int width = bitmap.Width;
				int extraPixelsToCheck = requiredHorizontalNeighbors;
				if (column < width)
				{
					//
					BitmapProcessing.FastBitmap processor = new BitmapProcessing.FastBitmap(bitmap);
					processor.LockImage();
					{
						// Iterate the column, documenting where to put pixels.
						List<int> yValues = new List<int>();
						if (extraPixelsToCheck <= 0)
						{
							for (int row = 0; row < bitmap.Height; row++)
							{
								if (doNotOverwriteExistingData)
								{
									Color thisColor = processor.GetPixel(column, row);
									if (thisColor.A == 0)
										yValues.Add(row);
								}
								else
									yValues.Add(row);
							}
						}
						else
						{
							for (int row = 0; row < bitmap.Height; row++)
							{
								Color thisColor = processor.GetPixel(column, row);
								if (doNotOverwriteExistingData && thisColor.A > 0)
								{
									// Do nothing.
								}
								else
								{
									bool rightExists = false;
									if ((column + 1) < width)
									{
										Color rightColor = processor.GetPixel(column + 1, row);
										if (rightColor.A > 0)
											rightExists = true;
									}
									// If right exists and only 1 is needed, done.
									if (extraPixelsToCheck == 1 && rightExists)
										yValues.Add(row);
									// Otherwise, check left.
									else
									{
										bool leftExists = false;
										if ((column - 1) >= 0)
										{
											Color leftColor = processor.GetPixel(column - 1, row);
											if (leftColor.A > 0)
												leftExists = true;
										}
										if (extraPixelsToCheck == 2 && rightExists && leftExists)
											yValues.Add(row);
										else if (extraPixelsToCheck == 1 && leftExists)
											yValues.Add(row);
									}
								}
							}
						}
						// Set the color where appropriate.
						Color color = this.color;
						foreach (int y in yValues)
						{
							processor.SetPixel(column, y, color);
						}
					}
					processor.UnlockImage();
				}
			}
			return bitmap;
		}

		private Bitmap GetPaddedImage(Bitmap inputBitmap, Bitmap outputBitmap = null)
		{
			int encompassingWidth = inputBitmap.Width;
			int offsetX = 0;
			// If the x location is past the image border.
			if (x >= 0)
			{
				if ((x + 1) > inputBitmap.Width)
				{
					// Do not bother with 2 or more neighbors this far out. Reason: impossible.
					if (requiredHorizontalNeighbors <= 1)
						encompassingWidth = x + 1;
				}
			}
			else {
				if (x == -1 && requiredHorizontalNeighbors <= 1) {
					encompassingWidth += 1;
					offsetX += 1;
				}
				else if (requiredHorizontalNeighbors <= 0)
				{
					encompassingWidth += System.Math.Abs(x);
					offsetX += System.Math.Abs(x);
				}
			}
			outputBitmap = new Bitmap(encompassingWidth, inputBitmap.Height);
			// Create appropriately sized image.
			using (Graphics g = Graphics.FromImage(outputBitmap))
			{
				Point awayFromEdge = new Point(offsetX, 0);
				// Splat original image.
				g.DrawImage(inputBitmap, awayFromEdge);
			}
			outputBitmap.Tag = offsetX;
			return outputBitmap;
		}

		public override OffsetableUnicodeCharacter Delta(OffsetableUnicodeCharacter character)
		{
			int offsetX = System.Math.Min(0, x);
			return new OffsetableUnicodeCharacter((UnicodeCharacter)character, character.OffsetWidth, character.OffsetX + offsetX, character.OffsetY, 1f, 1f);
		}

		//
		public new static Dictionary<string, ParameterDescription> ParameterDescriptions = new Dictionary<string, ParameterDescription>
		{
			{"color", new ParameterDescription("ARGB value that determines the color of the border.", typeof(XmlColor), new Panel()) },
			{"x", new ParameterDescription("Location to draw vertical line at.", typeof(int), new NumericUpDown()) },
			{"do-not-overwrite-existing-data", new ParameterDescription("Location to draw vertical line at.", typeof(bool), new CheckBox()) },
			{"required-horizontal-neighbors", new ParameterDescription("Number of horizontal neighbors required in order to place a pixel.", typeof(int), new NumericUpDown()) },
		};

		public override Dictionary<string, ParameterDescription> getDescriptions()
		{
			return VerticalLineEffect.ParameterDescriptions;
		}

		public override string getDescription(string parameterName)
		{
			return GetDescriptionFromParameters(parameterName, ParameterDescriptions);
		}

		public override Control getValidatingControl(string parameterName, object value)
		{
			Control control = GetValidatingControlFromParameters(parameterName, value, descriptions: getDescriptions());
			if (parameterName == "required-horizontal-neighbors" && control is NumericUpDown)
			{
				NumericUpDown numericUpDown = control as NumericUpDown;
				numericUpDown.Minimum = 0;
				numericUpDown.Maximum = 2;
				return numericUpDown;
			}
			else if (parameterName == "x" && control is NumericUpDown) {
				NumericUpDown numericUpDown = control as NumericUpDown;
				numericUpDown.Minimum = int.MinValue;
				numericUpDown.Maximum = int.MaxValue;
				return numericUpDown;
			}
			return control;
		}
	}
}
