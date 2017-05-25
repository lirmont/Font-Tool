using System.Drawing;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Windows.Forms;

namespace FontTool
{
	[System.Reflection.ObfuscationAttribute(Feature = "renaming", ApplyToMembers = true)]
	[XmlType(TypeName = "BorderEffect", Namespace = "http://darkabstraction.com/schemas/font.xsd")]
	public class BorderEffect : BitmapEffect
	{
		private bool subtractPreviousStep
		{
			get { return (Parameters != null && Parameters.Contains("subtract-previous-step")) ? (bool)Parameters["subtract-previous-step"].Value : false; }
		}

		private Color color {
			get
			{
				return (Parameters != null && Parameters.Contains("color")) ? (System.Drawing.Color)((XmlColor)Parameters["color"].Value) : Color.Black;
			}
		}

		public BorderEffect() : this(Color.Black, false)
		{
		}

		public BorderEffect(Color color, bool subtractPreviousStep)
		{
			this.Name = "Border";
			this.Parameters = new ParameterCollection() {
				new EffectParameter("color", new XmlColor(color)),
				new EffectParameter("subtract-previous-step", subtractPreviousStep),
			};
		}

		public BorderEffect(List<EffectParameter> parameters) : base("Border", parameters) { }

		public override Bitmap Apply(Bitmap bitmap, OffsetableUnicodeCharacter character = null, FontInSize size = null)
		{
			if (subtractPreviousStep)
			{
				Bitmap solidBitmap = GetPaddedImage((Bitmap)bitmap.Clone());
				Bitmap outputBitmap = EffectIteration(bitmap);
				// Walk output bitmap
				return SupportFunctions.SubtractImage(solidBitmap, outputBitmap);
			} else
				return EffectIteration(bitmap);
		}

		private static int iterationWidthAdjustment = 2;
		private static int iterationHeightAdjustment = 2;

		private Bitmap EffectIteration(Bitmap bitmap)
		{
			Bitmap outputBitmap = null;
			if (bitmap != null)
			{
				Bitmap solidBitmap = (Bitmap)bitmap.Clone();
				outputBitmap = GetPaddedImage(solidBitmap, outputBitmap);
				//
				BitmapProcessing.FastBitmap processor = new BitmapProcessing.FastBitmap(outputBitmap);
				BitmapProcessing.FastBitmap subProcessor = new BitmapProcessing.FastBitmap(solidBitmap);
				processor.LockImage();
				{
					subProcessor.LockImage();
					{
						for (int column = 0; column < solidBitmap.Width; column++)
							for (int row = 0; row < solidBitmap.Height; row++)
							{
								Color thisColor = subProcessor.GetPixel(column, row);
								if (thisColor.A > 0)
								{
									int x = column + 1, y = row + 1;
									List<Point> pointsToCheck = getPointsAroundSurroundingPointGivenBounds(x, y, outputBitmap.Size);
									foreach (Point p in pointsToCheck)
									{
										Color checkedColor = processor.GetPixel(p.X, p.Y);
										if (checkedColor.A == 0)
											processor.SetPixel(p.X, p.Y, color);
									}
								}
							}
					}
					subProcessor.UnlockImage();
					subProcessor.Dispose();
					//
					solidBitmap.Dispose();
				}
				processor.UnlockImage();
				//
				bitmap.Dispose();
			}
			return outputBitmap;
		}

		private static Bitmap GetPaddedImage(Bitmap inputBitmap, Bitmap outputBitmap = null)
		{
			outputBitmap = new Bitmap(inputBitmap.Width + iterationWidthAdjustment, inputBitmap.Height + iterationHeightAdjustment);
			// Create appropriately sized image.
			using (Graphics g = Graphics.FromImage(outputBitmap))
			{
				Point awayFromEdge = new Point(iterationWidthAdjustment / 2, iterationHeightAdjustment / 2);
				// Splat original image.
				g.DrawImage(inputBitmap, awayFromEdge);
			}
			return outputBitmap;
		}

		private static List<Point> getPointsAroundSurroundingPointGivenBounds(int x, int y, Size size)
		{
			Point location = new Point(x, y);
			List<Point> points = new List<Point>();
			for (int i = -1; i < 2; i++)
			{
				for (int j = -1; j < 2; j++)
				{
					if (i != 0 || j != 0)
					{
						int newX = x + i, newY = y + j;
						if (0 <= newX && newX < size.Width && 0 <= newY && newY < size.Height)
							points.Add(new Point(newX, newY));
					}
				}
			}
			return points;
		}

		public override OffsetableUnicodeCharacter Delta(OffsetableUnicodeCharacter character)
		{
			int widthDelta = iterationWidthAdjustment;
			// Because the image is expanded by the width and height (both typically 2), push the image back.
			int ascendsDelta = (iterationHeightAdjustment > 0) ? -iterationHeightAdjustment / 2 : 0;
			int advancesDelta = -widthDelta / 2;
			//
			return new OffsetableUnicodeCharacter((UnicodeCharacter)character, character.OffsetWidth + widthDelta, character.OffsetX + advancesDelta, character.OffsetY + ascendsDelta, 1f, 1f);
		}

		//
		public new static Dictionary<string, ParameterDescription> ParameterDescriptions = new Dictionary<string, ParameterDescription>
		{
			{"color", new ParameterDescription("ARGB value that determines the color of the border.", typeof(XmlColor), new Panel()) },
			{"subtract-previous-step", new ParameterDescription("Should this effect yield only the border or not.", typeof(bool), new CheckBox()) }
		};

		public override Dictionary<string, ParameterDescription> getDescriptions()
		{
			return BorderEffect.ParameterDescriptions;
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
