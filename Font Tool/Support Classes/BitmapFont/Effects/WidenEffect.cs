using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml.Serialization;
using System.Windows.Forms;

namespace FontTool
{
	[System.Reflection.ObfuscationAttribute(Feature = "renaming", ApplyToMembers = true)]
	[XmlType(TypeName = "WidenEffect", Namespace = "http://darkabstraction.com/schemas/font.xsd")]
	public class WidenEffect : BitmapEffect
	{
		private uint width {
			get {
				return (Parameters != null && Parameters.Contains("width")) ? (uint)Parameters["width"].Value : 0; 
			}
		}

		public WidenEffect()
			: this(1)
		{ }

		public WidenEffect(uint width)
		{
			this.Name = "Widen";
			this.Parameters = new ParameterCollection() {
				new EffectParameter("width", width)
			};
		}

		public WidenEffect(List<EffectParameter> parameters) : base("Widen", parameters) { }

		public override Bitmap Apply(Bitmap bitmap, OffsetableUnicodeCharacter character = null, FontInSize size = null)
		{
			Bitmap outputBitmap = (Bitmap)bitmap.Clone();
			for (int i = 0; i < width; i++)
				outputBitmap = EffectIteration(outputBitmap);
			//
			if (bitmap != null)
				bitmap.Dispose();
			//
			return outputBitmap;
		}

		private Bitmap EffectIteration(Bitmap bitmap)
		{
			Bitmap outputBitmap = null;
			if (bitmap != null)
			{
				outputBitmap = new Bitmap(bitmap.Width + 2, bitmap.Height);
				// Split into 2 images.
				int halfWidth = bitmap.Width / 2;
				int secondHalfWidth = bitmap.Width - halfWidth;
				secondHalfWidth = halfWidth = Math.Max(halfWidth, secondHalfWidth);
				//
				Bitmap left = bitmap.Clone(new Rectangle(Point.Empty, new Size(halfWidth, bitmap.Height)), bitmap.PixelFormat);
				Bitmap right = bitmap.Clone(new Rectangle(new Point(bitmap.Width - halfWidth, 0), new Size(halfWidth, bitmap.Height)), bitmap.PixelFormat);
				// Create appropriately sized image.
				using (Graphics g = Graphics.FromImage(outputBitmap))
				{
					// Splat horizontally centered original image.
					g.DrawImage(bitmap, new Point((int)1, 0));
					// Splat offset left image.
					g.DrawImage(left, new Point(0, 0));
					// Splat offset right image.
					g.DrawImage(right, new Point(outputBitmap.Width - right.Width, 0));
				}
				//
				left.Dispose();
				right.Dispose();
				bitmap.Dispose();
			}
			return outputBitmap;
		}

		public override OffsetableUnicodeCharacter Delta(OffsetableUnicodeCharacter character)
		{
			OffsetableUnicodeCharacter derivedCharacter = new OffsetableUnicodeCharacter((UnicodeCharacter)character, character.OffsetWidth + width * 2, character.OffsetX - width, character.OffsetY, 1f, 1f);
			return derivedCharacter;
		}

		//
		public new static Dictionary<string, ParameterDescription> ParameterDescriptions = new Dictionary<string, ParameterDescription>
		{
			{"width", new ParameterDescription("Number of times to perform the widen pass. Each pass increases width by 2.", typeof(float), new MaskedTextBox()) }
		};

		public override Dictionary<string, ParameterDescription> getDescriptions()
		{
			return WidenEffect.ParameterDescriptions;
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
