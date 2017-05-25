using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace FontTool
{
	[System.Reflection.ObfuscationAttribute(Feature = "renaming", ApplyToMembers = true)]
	[XmlType(TypeName = "CloseCropEffect", Namespace = "http://darkabstraction.com/schemas/font.xsd")]
	public class CloseCropEffect : BitmapEffect
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

		public CloseCropEffect()
			: this(ColorDirection.East, 1)
		{
		}

		public CloseCropEffect(ColorDirection direction, int steps)
		{
			this.Name = "CloseCrop";
			this.Parameters = new ParameterCollection() {
				new EffectParameter("direction", direction),
				new EffectParameter("steps", steps)
			};
		}

		public CloseCropEffect(List<EffectParameter> parameters) : base("CloseCrop", parameters) { }

		public override Bitmap Apply(Bitmap bitmap, OffsetableUnicodeCharacter character = null, FontInSize size = null)
		{
			return CloseCropEffect.EffectIteration(bitmap, Direction, Steps);
		}

		public static Bitmap EffectIteration(Bitmap bitmap, ColorDirection Direction, int Steps)
		{
			int? component = null;
			int steps = Steps - 1;
			//
			Bitmap outputBitmap = null;
			if (bitmap != null)
			{
				Size size = bitmap.Size;
				outputBitmap = (Bitmap)bitmap.Clone();
				// Create appropriately sized image.
				using (Graphics g = Graphics.FromImage(outputBitmap))
				{
					//
					if (Direction == ColorDirection.East)
					{
						// Start left, go right.
						for (int column = 0; column < size.Width; column++)
						{
							if (component != null && component.Value != column)
								break;
							//
							for (int row = 0; row < size.Height; row++)
							{
								Color thisColor = outputBitmap.GetPixel(column, row);
								if (thisColor.A > 0)
								{
									if (component == null)
										component = column;
									//
									if (component.Value <= column + steps)
										outputBitmap.SetPixel(column, row, Color.Transparent);
									else
										break;
								}
							}
						}
					}
					else if (Direction == ColorDirection.West)
					{
						// Start right, go left.
						for (int column = size.Width - 1; column >= 0; column--)
						{
							if (component != null && component.Value != column)
								break;
							//
							for (int row = 0; row < size.Height; row++)
							{
								Color thisColor = outputBitmap.GetPixel(column, row);
								if (thisColor.A > 0)
								{
									if (component == null)
										component = column;
									//
									if (component.Value >= column - steps)
										outputBitmap.SetPixel(column, row, Color.Transparent);
									else
										break;
								}
							}
						}
					}
					//
					if (Direction == ColorDirection.North)
					{
						// Start end, go to beginning.
						for (int row = size.Height - 1; row >= 0; row--)
						{
							if (component != null && component.Value != row)
								break;
							//
							for (int column = 0; column < size.Width; column++)
							{
								Color thisColor = outputBitmap.GetPixel(column, row);
								if (thisColor.A > 0)
								{
									if (component == null)
										component = row;
									//
									if (component.Value <= row - steps)
										outputBitmap.SetPixel(column, row, Color.Transparent);
									else
										break;
								}
							}
						}
					}
					else if (Direction == ColorDirection.South)
					{
						// Start beginning, to end.
						for (int row = 0; row < size.Height; row++)
						{
							if (component != null && component.Value != row)
								break;
							//
							for (int column = 0; column < size.Width; column++)
							{
								Color thisColor = outputBitmap.GetPixel(column, row);
								if (thisColor.A > 0)
								{
									if (component == null)
										component = row;
									//
									if (component.Value >= row + steps)
										outputBitmap.SetPixel(column, row, Color.Transparent);
									else
										break;
								}
							}
						}
					}
					//
				}
				bitmap.Dispose();
			}
			outputBitmap = SupportFunctions.TrimBitmap(outputBitmap);
			return outputBitmap;
		}

		public override OffsetableUnicodeCharacter Delta(OffsetableUnicodeCharacter character)
		{
			int widthDelta = 0;
			if (Direction == ColorDirection.East)
				widthDelta = -Steps;
			else if (Direction == ColorDirection.West)
				widthDelta = -Steps;
			//
			OffsetableUnicodeCharacter derivedCharacter = new OffsetableUnicodeCharacter((UnicodeCharacter)character, character.OffsetWidth + widthDelta, character.OffsetX, character.OffsetY, 1f, 1f);
			return derivedCharacter;
		}

		//
		public new static Dictionary<string, ParameterDescription> ParameterDescriptions = new Dictionary<string, ParameterDescription>
		{
			{"direction", new ParameterDescription("Direction to apply the color to (example: East applies the color from left edges across to right edges.", typeof(ColorDirection), new ComboBox())},
			{"steps", new ParameterDescription("Amount of times (in pixels) to apply the color (to a lesser degree each time).", typeof(ColorDirection), new MaskedTextBox()) }
		};

		public override Dictionary<string, ParameterDescription> getDescriptions()
		{
			return CloseCropEffect.ParameterDescriptions;
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
