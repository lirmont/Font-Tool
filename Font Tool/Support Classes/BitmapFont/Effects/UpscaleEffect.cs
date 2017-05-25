using System.Collections.Generic;
using System.Drawing;
using System.Xml.Serialization;
using System.Windows.Forms;

namespace FontTool
{
	[System.Reflection.ObfuscationAttribute(Feature = "renaming", ApplyToMembers = true)]
	[XmlType(TypeName = "UpscaleEffect", Namespace = "http://darkabstraction.com/schemas/font.xsd")]
	public class UpscaleEffect : BitmapEffect
	{
		private int scale
		{
			get {
				return (Parameters != null && Parameters.Contains("scale")) ? (int)Parameters["scale"].Value : 0;
			}
		}

		public UpscaleEffect() : this(1)
		{
		}

		public UpscaleEffect(int scale)
		{
			this.Name = "Upscale";
			this.Parameters = new ParameterCollection() {
				new EffectParameter("scale", scale)
			};
		}

		public UpscaleEffect(List<EffectParameter> parameters) : base("Upscale", parameters) { }

		public override Bitmap Apply(Bitmap bitmap, OffsetableUnicodeCharacter character = null, FontInSize size = null)
		{
			return EffectIteration(bitmap);
		}

		private Bitmap EffectIteration(Bitmap bitmap)
		{
			Bitmap outputBitmap = null;
			if (bitmap != null)
			{
				if (scale >= 1)
				{
					outputBitmap = new Bitmap(bitmap.Width * scale, bitmap.Height * scale);
					// Create appropriately sized image.
					using (Graphics g = Graphics.FromImage(outputBitmap))
					{
						g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
						g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
						g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
						g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
						g.DrawImage(bitmap, new RectangleF(0, 0, outputBitmap.Width, outputBitmap.Height));
					}
				}
				bitmap.Dispose();
			}
			return outputBitmap;
		}

		public override OffsetableUnicodeCharacter Delta(OffsetableUnicodeCharacter character)
		{
			OffsetableUnicodeCharacter derivedCharacter = new OffsetableUnicodeCharacter((UnicodeCharacter)character, character.OffsetWidth, character.OffsetX, character.OffsetY, scale, scale);
			return derivedCharacter;
		}

		//
		public new static Dictionary<string, ParameterDescription> ParameterDescriptions = new Dictionary<string, ParameterDescription>
		{
			{"scale", new ParameterDescription("Scale to redraw image at.", typeof(int), new MaskedTextBox()) }
		};

		public override Dictionary<string, ParameterDescription> getDescriptions()
		{
			return UpscaleEffect.ParameterDescriptions;
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
