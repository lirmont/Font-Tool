using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace TextAnalyzer
{
	public class RealizationConfiguration
	{
		Font font = null;
		float ascentInPixels = 0;
		float descentInPixels = 0;
		float lineSpacingInPixels = 0;
		int basePixelX = 0, basePixelY = 0;
		float additionalBaseLine = 0;
		bool antialiased = false;
		bool alternateLayout = false;
		Color antialiasMatteColor = Color.White;

		public Font Font
		{
			get { return font; }
			set { font = value; }
		}

		public float AscentInPixels
		{
			get { return ascentInPixels; }
			set { ascentInPixels = value; }
		}

		public float DescentInPixels
		{
			get { return descentInPixels; }
			set { descentInPixels = value; }
		}

		public float LineSpacingInPixels
		{
			get { return lineSpacingInPixels; }
			set { lineSpacingInPixels = value; }
		}

		public int BasePixelX
		{
			get { return basePixelX; }
			set { basePixelX = value; }
		}

		public int BasePixelY
		{
			get { return basePixelY; }
			set { basePixelY = value; }
		}

		public float AdditionalBaseLine
		{
			get { return additionalBaseLine; }
			set { additionalBaseLine = value; }
		}

		public bool Antialiased
		{
			get { return antialiased; }
			set { antialiased = value; }
		}

		public bool AlternateLayout
		{
			get { return alternateLayout; }
			set { alternateLayout = value; }
		}

		public Color AntialiasMatteColor
		{
			get { return antialiasMatteColor; }
			set { antialiasMatteColor = value; }
		}

		public RealizationConfiguration(Font font, bool antialiased = false, bool alternateLayout = false, Color? antialiasMatteColor = null)
		{
			this.font = font;
			// Get the design unit metrics from the font family.
			int emSizeInDU = font.FontFamily.GetEmHeight(font.Style);
			int ascentInDU = font.FontFamily.GetCellAscent(font.Style);
			int descentInDU = font.FontFamily.GetCellDescent(font.Style);
			int lineSpacingInDU = font.FontFamily.GetLineSpacing(font.Style);
			// Calculate the GraphicsUnit metrics from the font.
			this.ascentInPixels = ascentInDU * (font.Size / emSizeInDU);
			this.descentInPixels = descentInDU * (font.Size / emSizeInDU);
			this.lineSpacingInPixels = lineSpacingInDU * (font.Size / emSizeInDU);
			//
			float fPixelY = Math.Max(ascentInPixels + descentInPixels, lineSpacingInPixels * 1.5f);
			basePixelX = (int)Math.Ceiling(font.Size);
			basePixelY = (int)Math.Ceiling(fPixelY);
			additionalBaseLine = (lineSpacingInPixels % 1 == 0) ? 0 : 1 - (lineSpacingInPixels % 1);
			basePixelY += 1;
			//
			this.antialiased = antialiased;
			this.alternateLayout = alternateLayout;
			this.antialiasMatteColor = (antialiasMatteColor != null) ? antialiasMatteColor.Value : Color.White;
		}

		public Size GetSizeForText(string text)
		{
			int pixelX = getWidthOfCharacter(text, font);
			return new Size(pixelX, basePixelY);
		}

		private int getWidthOfCharacter(string text, Font font)
		{
			int pixelX = basePixelX;
			using (Bitmap tester = new Bitmap(1, 1))
			{
				using (Graphics g = Graphics.FromImage(tester))
				{
					SizeF size = g.MeasureString(text, font);
					pixelX = (int)Math.Ceiling(size.Width) + 1;
				}
			}
			return pixelX;
		}

		public Bitmap GetClippedBitmap(string text, out int ascent)
		{
			//
			Bitmap right = RealizationConfiguration.GetCharacterImage(text, this);
			Bitmap left = RealizationConfiguration.GetAnnotatedCharacterImage(right, this);
			// Positive: ascent; Negative: descent.
			Rectangle clippingRectangle = RealizationConfiguration.GetClippingRectangle(right);
			Bitmap clippedBitmap = null;
			if (clippingRectangle.Size.Width > 0 && clippingRectangle.Size.Height > 0)
			{
				clippedBitmap = new Bitmap(clippingRectangle.Size.Width, clippingRectangle.Size.Height);
				using (Graphics g = Graphics.FromImage(clippedBitmap))
				{
					g.DrawImage(right, new Point(-clippingRectangle.X, -clippingRectangle.Y));
				}
			}
			else
				clippedBitmap = null;
			//
			ascent = RealizationConfiguration.GetAscent(left, clippingRectangle.Bottom, keyColor: Color.Blue);
			right.Dispose();
			left.Dispose();
			//
			return clippedBitmap;
		}

		public static Rectangle GetClippingRectangle(Bitmap right)
		{
			int x1 = getLeft(right);
			int y2 = getBottom(right);
			int x2 = getRight(right);
			int y1 = getTop(right, x1: x1, x2: x2);
			return new Rectangle(x1, y1, x2 - x1, y2 - y1);
		}

		private static int getTop(Bitmap cleanImage, int x1 = 0, int? x2 = null)
		{
			int D = cleanImage.Height;
			bool found = false;
			x2 = (x2 != null) ? x2 : cleanImage.Width;
			for (int i = 0; i < cleanImage.Height; i++)
			{
				for (int r = x1; r < x2.Value; r++)
				{
					Color color = cleanImage.GetPixel(r, i);
					int thisColor = color.ToArgb();
					if (thisColor != 0)
					{
						D = i;
						found = true;
						break;
					}
				}
				if (found)
					break;
			}
			return D;
		}

		private static int getLeft(Bitmap image)
		{
			int C = 0;
			bool found = false;
			for (int r = 0; r < image.Width; r++)
			{
				for (int i = 0; i < image.Height; i++)
				{
					Color color = image.GetPixel(r, i);
					int thisColor = color.ToArgb();
					if (thisColor != 0)
					{
						C = r;
						found = true;
						break;
					}
				}
				if (found)
					break;
			}
			return C;
		}

		private static int getRight(Bitmap image)
		{
			int E = image.Width;
			bool found = false;
			for (int r = image.Width - 1; r >= 0; r--)
			{
				for (int i = image.Height - 1; i >= 0; i--)
				{
					Color color = image.GetPixel(r, i);
					int thisColor = color.ToArgb();
					if (thisColor != 0)
					{
						E = r + 1;
						found = true;
						break;
					}
				}
				if (found)
					break;
			}
			return E;
		}

		public static int GetAscent(Bitmap annotatedImage, int bottom, Color? keyColor = null)
		{
			int B = 0, A = bottom;
			// Perform analysis.
			B = 0;
			int searchingForColor = (keyColor != null) ? keyColor.Value.ToArgb() : Color.Red.ToArgb();
			for (int i = annotatedImage.Height - 1; i >= 0; i--)
			{
				Color color = annotatedImage.GetPixel(0, i);
				if (color.ToArgb() == searchingForColor)
				{
					B = i + 1;
					break;
				}
			}
			return B - A;
		}

		private static int getBottom(Bitmap image)
		{
			int A = image.Height;
			bool found = false;
			for (int i = image.Height - 1; i >= 0; i--)
			{
				for (int r = 0; r < image.Width; r++)
				{
					Color color = image.GetPixel(r, i);
					int thisColor = color.ToArgb();
					if (thisColor != 0)
					{
						A = i + 1;
						found = true;
						break;
					}
				}
				if (found)
					break;
			}
			return A;
		}

		public Bitmap GetAnnotatedCharacterImageFromText(string text, int? extraAscentMarker = null)
		{
			Bitmap right = RealizationConfiguration.GetCharacterImage(text, this);
			Bitmap left = RealizationConfiguration.GetAnnotatedCharacterImage(right, this, extraAscentMarker: extraAscentMarker);
			right.Dispose();
			// Trim off excess space.
			if (left != null)
				left = FontTool.SupportFunctions.TrimBitmap(left);
			return left;
		}

		public static Bitmap GetAnnotatedCharacterImage(Bitmap right, RealizationConfiguration
			 config, int? extraAscentMarker = null)
		{
			Size size = right.Size;
			Bitmap left = new Bitmap(size.Width, size.Height);
			PointF textOrigin = new PointF(0, (config.AlternateLayout) ? config.AdditionalBaseLine : 0);
			using (Graphics g = Graphics.FromImage(left))
			{
				g.TranslateTransform(textOrigin.X, textOrigin.Y);
				// Draws near the top of letters.
				{
					float height = config.LineSpacingInPixels - config.AscentInPixels;
					PointF lineOrigin = new PointF(0, height);
					PointF lineOriginEnd = new PointF(size.Width, height);
					g.DrawLine(Pens.DodgerBlue, lineOrigin, lineOriginEnd);
				}
				// Ascent draws at the baseline.
				{
					float height = config.AscentInPixels;
					PointF lineOrigin = new PointF(0, height);
					PointF lineOriginEnd = new PointF(size.Width, height);
					g.DrawLine(Pens.Blue, lineOrigin, lineOriginEnd);
				}
				// Descent draws near hook in y.
				{
					float height = config.AscentInPixels + config.DescentInPixels;
					PointF lineOrigin = new PointF(0, height);
					PointF lineOriginEnd = new PointF(size.Width, height);
					g.DrawLine(Pens.CadetBlue, lineOrigin, lineOriginEnd);
				}
				if (extraAscentMarker != null)
				{
					float height = config.LineSpacingInPixels - config.AscentInPixels;
					int topLine = (int)Math.Round(height);
					Pen p = (topLine >= Math.Ceiling(config.AscentInPixels - extraAscentMarker.Value)) ? (Pen)Pens.Green.Clone() : (Pen)Pens.Red.Clone();
					p.StartCap = LineCap.RoundAnchor;
					p.EndCap = LineCap.RoundAnchor;
					PointF lineOrigin = new PointF(-1, config.AscentInPixels - extraAscentMarker.Value);
					PointF lineOriginEnd = new PointF(size.Width, config.AscentInPixels - extraAscentMarker.Value);
					g.DrawLine(p, lineOrigin, lineOriginEnd);
				}
				//
				g.DrawImage(right, Point.Empty);
			}
			return left;
		}

		public static Bitmap GetCharacterImage(string text, RealizationConfiguration config)
		{
			Size size = config.GetSizeForText(text);
			Bitmap right = new Bitmap(size.Width, size.Height);
			PointF textOrigin = new PointF(0, (config.AlternateLayout) ? config.AdditionalBaseLine : 0);
			using (Graphics g = Graphics.FromImage(right))
			{
				GraphicsPath path = new GraphicsPath();
				path.AddString(text, config.Font.FontFamily, (int)config.Font.Style, config.Font.Size, Point.Empty, StringFormat.GenericDefault);
				if (config.Antialiased)
				{
					g.Clear(config.AntialiasMatteColor);
					g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
				}
				else
					g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit;
				//
				g.DrawString(text, config.Font, Brushes.Black, textOrigin, StringFormat.GenericDefault);
				if (config.Antialiased && config.AntialiasMatteColor == Color.Black)
				{
					Region clipRegion = new Region(path);
					clipRegion.Complement(new Region(new Rectangle(Point.Empty, right.Size)));
					g.IntersectClip(clipRegion);
					g.Clear(Color.Transparent);
				}
			}
			if (config.Antialiased && config.AntialiasMatteColor != Color.Black)
				right.MakeTransparent(config.AntialiasMatteColor);
			return right;
		}
	}
}
