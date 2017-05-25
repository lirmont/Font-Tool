using System;
using System.Drawing;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Windows.Forms;

namespace FontTool
{
	[System.Reflection.ObfuscationAttribute(Feature = "renaming", ApplyToMembers = true)]
	[XmlType(TypeName = "ItalicizeEffect", Namespace = "http://darkabstraction.com/schemas/font.xsd")]
	public class ItalicizeEffect : BitmapEffect
	{
		private int steps
		{
			get
			{
				return (Parameters != null && Parameters.Contains("steps")) ? (int)(Parameters["steps"].Value) : 1;
			}
		}

		public ItalicizeEffect()
			: this(1)
		{
		}

		public ItalicizeEffect(int steps)
		{
			this.RedrawOnCharacterOffsetChanged = true;
			this.Name = "Italicize";
			this.Parameters = new ParameterCollection() {
				new EffectParameter("steps", steps)
			};
		}

		public ItalicizeEffect(List<EffectParameter> parameters) : base("Italicize", parameters) { }

		public override Bitmap Apply(Bitmap bitmap, OffsetableUnicodeCharacter character = null, FontInSize size = null)
		{
			return EffectIteration(bitmap, character, size);
		}

		private Bitmap EffectIteration(Bitmap bitmap, OffsetableUnicodeCharacter character, FontInSize size)
		{
			int totalRows = bitmap.Height;
			int totalColumns = bitmap.Width;
			//
			List<int> offsets = stepOffsets(size.TargetDescent, size.TargetAscent);
			// Track horizontal canvas size increases.
			int addLeft = 0, addRight = 0;
			calculateWidthIncreases(character, size, totalRows, offsets, ref addLeft, ref addRight);
			//
			Size newSize = new Size(addLeft + bitmap.Width + addRight, bitmap.Height);
			Point drawAt = new Point(addLeft, 0);
			// Get a copy of the incoming bitmap (likely in a larger canvas).
			Bitmap outputBitmap = SupportFunctions.GetResizedImage(newSize, Point.Empty, drawAt, (Bitmap)bitmap.Clone(), discardExistingImage: false);
			if (outputBitmap != null && character != null && size != null)
			{
				//
				if (offsets.Count > 0)
				{
					BitmapProcessing.FastBitmap processor = new BitmapProcessing.FastBitmap(outputBitmap);
					processor.LockImage();
					{
						for (int row = 0; row < totalRows; row++)
						{
							// Get values from bottom of bounding box first.
							int offset = getOffset(row, totalRows, character, size, offsets);
							// Move to right: read from right to left, moving pixels right.
							if (offset > 0)
								for (int column = addLeft + totalColumns - 1; column >= addLeft; column--)
								{
									if (!(column + offset < addLeft + totalColumns + addRight))
										System.Diagnostics.Debugger.Break();
									Color pixel = processor.GetPixel(column, row);
									processor.SetPixel(column + offset, row, pixel);
									processor.SetPixel(column, row, Color.Transparent);
								}
							// Move to left: read from left to right, moving pixels left.
							else if (offset < 0)
								for (int column = addLeft; column < addLeft + totalColumns; column++)
								{
									if (!(column + offset >= 0))
										System.Diagnostics.Debugger.Break();
									// Get the pixel at this location.
									Color pixel = processor.GetPixel(column, row);
									// Move it one (or more) to the left.
									processor.SetPixel(column + offset, row, pixel);
									processor.SetPixel(column, row, Color.Transparent);
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

		private static void calculateWidthIncreases(OffsetableUnicodeCharacter character, FontInSize size, int totalRows, List<int> offsets, ref int addLeft, ref int addRight)
		{
			// Get first and last offset. These are later used
			int firstOffset = getOffset(0, totalRows, character, size, offsets);
			int lastOffset = getOffset(totalRows, totalRows, character, size, offsets);
			// As a statistic, calculate the total width increase.
			int componentMax = Math.Max(Math.Abs(firstOffset), Math.Abs(lastOffset));
			int totalWidthIncrease = Math.Max(componentMax, Math.Abs(lastOffset - firstOffset));
			// Figure out how the canvas the existing image exists in will change.
			if (totalWidthIncrease > 0)
			{
				//
				if (firstOffset > 0)
					addRight += firstOffset;
				else if (firstOffset < 0)
					addLeft += Math.Abs(firstOffset);
				//
				if (lastOffset > 0)
					addRight += lastOffset;
				else if (lastOffset < 0)
					addLeft += Math.Abs(lastOffset);
			}
		}

		private static int getOffset(int row, int totalRows, OffsetableUnicodeCharacter character, FontInSize size, List<int> offsets)
		{
			int rowComplement = totalRows - row - 1;
			int desiredRowInOffsets = (int)Math.Max(0, Math.Min(offsets.Count - 1, rowComplement + size.TargetDescent + character.OffsetY));
			return offsets[desiredRowInOffsets];
		}

		private List<int> stepOffsets(double minorStepCount, double majorStepCount)
		{
			List<int> list = new List<int>();
			int rawRange = (int)Math.Ceiling(majorStepCount + minorStepCount);
			// Find next multiple of the value in "steps".
			int range = findNextMultipleOfSteps(rawRange);
			int distancePerStep = range / (steps + 1);
			bool stepsAreEven = ((steps % 2) == 0) ? true : false;
			int startingOffset = (stepsAreEven) ? -(steps - 1) / 2 : -steps / 2;
			for (int i = 0; i <= steps; i++)
			{
				int thisOffset = startingOffset + i;
				for (int r = 0; r < distancePerStep; r++)
					list.Add(thisOffset);
			}
			return list;
		}

		private int findNextMultipleOfSteps(int value)
		{
			int multipleOf = steps + 1;
			int remainder = value % multipleOf;
			int result = value - remainder;
			if (remainder > (multipleOf / 2))
				result += multipleOf;
			return result;
		}

		public override OffsetableUnicodeCharacter Delta(OffsetableUnicodeCharacter character)
		{
			int increaseWidth = 0;
			int offsetX = 0;
			OffsetableUnicodeCharacter derivedCharacter = new OffsetableUnicodeCharacter((UnicodeCharacter)character, character.OffsetWidth + increaseWidth, character.OffsetX + offsetX, character.OffsetY, 1f, 1f);
			return derivedCharacter;
		}

		//
		public new static Dictionary<string, ParameterDescription> ParameterDescriptions = new Dictionary<string, ParameterDescription>
		{
			{"steps", new ParameterDescription("Steps (forward or backward) to cut the bounding box of the font's line height into.", typeof(float), new MaskedTextBox()) },
		};

		public override Dictionary<string, ParameterDescription> getDescriptions()
		{
			return ItalicizeEffect.ParameterDescriptions;
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
