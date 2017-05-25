using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.IO;

namespace FontTool
{
	public static partial class SupportFunctions
	{
		public static Bitmap GetClonedBitmap(Bitmap bitmap, bool flipped = false)
		{
			if (bitmap != null && bitmap.PixelFormat != PixelFormat.Undefined)
			{
				Bitmap newBitmap = new Bitmap(bitmap.Width, bitmap.Height);
				// Lock the new bitmap's graphics object.
				using (Graphics g = Graphics.FromImage(newBitmap))
				{
					g.DrawImage(bitmap, Point.Empty);
				}
				if (flipped)
					newBitmap.RotateFlip(RotateFlipType.RotateNoneFlipY);
				return newBitmap;
			} else
				return null;
		}

		public static Bitmap GetResizedImage(Size allLayerSize, PointF startAt, PointF drawAt, Bitmap image, bool discardExistingImage = false)
		{
			Bitmap newImageInLayerSize = new Bitmap(allLayerSize.Width, allLayerSize.Height);
			// Calculate the requested point as the net of the starting point and the offset to draw the image at (measured from lower-left with +Y moving upwards).
			PointF requestedPoint = new PointF(startAt.X + drawAt.X, startAt.Y + drawAt.Y);
			// Draw image.
			DrawImageAtPointFromLowerRight(newImageInLayerSize, image, requestedPoint);
			// Discard existing image.
			if (discardExistingImage)
				image.Dispose();
			return newImageInLayerSize;
		}

		public static Bitmap ResizeImageAndDiscardOldImage(Size allLayerSize, PointF startAt, PointF drawAt, Bitmap image)
		{
			return GetResizedImage(allLayerSize, startAt, drawAt, image, discardExistingImage: true);
		}

		public static void DrawImageAtPointFromLowerRight(Bitmap canvas, Bitmap image, PointF drawAt)
		{
			// X': Start at lower-left. No change.
			float xNot = drawAt.X;
			// Y': Move down by the height of the canvas. Move up by the height of the point. Move up by the height of the incoming image.
			float yNot = canvas.Height - drawAt.Y - image.Height;
			// This point, (X', Y'), represents a given point, (X, Y), as interpreted from the lower-left of the device context moving upward (rather than the upper-left corner, moving downward along +Y).
			PointF actuallyDrawAt = new PointF(xNot, yNot);
			using (Graphics g = Graphics.FromImage(canvas))
			{
				g.DrawImage(image, actuallyDrawAt);
			}
			canvas.Tag = image.Tag;
		}

		public static void SaveImageToDesktop(Bitmap image, string stub = "")
		{
			if (image != null && image.PixelFormat != PixelFormat.Undefined)
				image.Save(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), string.Format("{0}{1}.png", new object[] { stub, image.GetHashCode() })));
		}

		public static Bitmap TrimBitmap(Bitmap source, bool preserveLeftSide = false)
		{
			Rectangle minimumRectangle = default(Rectangle);
			BitmapData data = null;
			try
			{
				data = source.LockBits(new Rectangle(0, 0, source.Width, source.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
				byte[] buffer = new byte[data.Height * data.Stride];
				Marshal.Copy(data.Scan0, buffer, 0, buffer.Length);
				int? xMin = (preserveLeftSide) ? 0 : (int?)null;
				int? xMax = null;
				int? yMin = (preserveLeftSide) ? 0 : (int?)null;
				int? yMax = null;
				uint alphaCutoff = 0;
				// Left pass.
				if (xMin == null)
				{
					for (int x = 0; x < data.Width; x++)
					{
						for (int y = 0; y < data.Height; y++)
						{
							// BGRA format.
							byte alpha = buffer[(y * data.Stride) + (x * 4) + 3];
							if (alpha > alphaCutoff)
							{
								xMin = x;
								// Escape.
								x = data.Width;
								y = data.Height;
							}
						}
					}
				}
				// If the first pass yields no non-transparent pixels, end it.
				if (xMin == null)
					return null;
				// Top pass.
				if (yMin == null)
				{
					for (int y = 0; y < data.Height; y++)
					{
						for (int x = xMin.Value; x < data.Width; x++)
						{
							// BGRA format.
							byte alpha = buffer[(y * data.Stride) + (x * 4) + 3];
							if (alpha > alphaCutoff)
							{
								yMin = y;
								// Escape.
								x = data.Width;
								y = data.Height;
							}
						}
					}
				}
				yMin = yMin ?? 0;
				// Right pass.
				for (int x = data.Width; x > 0; x--)
				{
					for (int y = yMin.Value; y < data.Height; y++)
					{
						// BGRA format.
						byte alpha = buffer[(y * data.Stride) + ((x - 1) * 4) + 3];
						if (alpha > alphaCutoff)
						{
							xMax = x;
							// Escape.
							x = 0;
							y = data.Height;
						}
					}
				}
				xMax = xMax ?? source.Width;
				// Bottom pass.
				for (int y = data.Height; y > 0; y--)
				{
					for (int x = xMin.Value; x < xMax; x++)
					{
						// BGRA format.
						byte alpha = buffer[((y - 1) * data.Stride) + (x * 4) + 3];
						if (alpha > alphaCutoff)
						{
							yMax = y;
							// Escape.
							x = xMax.Value + 1;
							y = 0;
						}
					}
				}
				yMax = yMax ?? source.Height;
				//
				buffer = null;
				// Compose the rectangle.
				minimumRectangle = Rectangle.FromLTRB(xMin.Value, yMin.Value, xMax.Value, yMax.Value);
			}
			finally
			{
				source.UnlockBits(data);
				data = null;
			}
			//
			Bitmap dest = new Bitmap(minimumRectangle.Width, minimumRectangle.Height);
			Rectangle destRect = new Rectangle(0, 0, minimumRectangle.Width, minimumRectangle.Height);
			using (Graphics graphics = Graphics.FromImage(dest))
			{
				graphics.DrawImage(source, destRect, minimumRectangle, GraphicsUnit.Pixel);
			}
			dest.Tag = new PointF(minimumRectangle.X, minimumRectangle.Y);
			// Zero steps in this program need to keep the step prior to a trim.
			source.Dispose();
			//
			return dest;
		}

		public static SizeF GetRegionForText(string text, Font f, SizeF proposedSize)
		{
			SizeF size = proposedSize;
			if (text != null)
			{
				string glyphString = string.Format("{0}", text);
				using (Bitmap bitmap = new Bitmap(1, 1))
				{
					using (Graphics g = Graphics.FromImage(bitmap))
					{
						ApplyGraphicsSettings(g);
						//
						size = g.MeasureString(glyphString, f, PointF.Empty, StringFormat.GenericTypographic);
					}
				}
			}
			return new SizeF(size.Width * 2, size.Height * 2);
		}

		public static void ApplyGraphicsSettings(Graphics g, bool higherQuality = true)
		{
			if (higherQuality)
			{
				g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
				g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
				g.PageUnit = GraphicsUnit.Point;
				g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
				g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
				g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
			}
			else
			{
				g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
				g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
				g.PageUnit = GraphicsUnit.Pixel;
				g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
				g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit;
				g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
			}
		}

		public static Bitmap CreateTextImage(string text, Font f, Size proposedSize, Color? backgroundColor = null, Size? region = null, Brush drawingBrush = null, bool antialiased = true)
		{
			SizeF thisRegion = (region != null) ? region.Value : SupportFunctions.GetRegionForText(text, f, proposedSize);
			if (thisRegion.Width == 0 || thisRegion.Height == 0)
				return new Bitmap(1, 1);
			else
			{
				Bitmap bitmap = new Bitmap(
					(int)Math.Max(1, Math.Round(thisRegion.Width, MidpointRounding.AwayFromZero) * 2),
					(int)Math.Max(1, Math.Round(thisRegion.Height, MidpointRounding.AwayFromZero) * 2)
				);
				bool wantResetTransparency = false;
				using (Graphics g = Graphics.FromImage(bitmap))
				{
					// Set background color if requested.
					if (backgroundColor != null || !antialiased)
						g.Clear((backgroundColor != null) ? backgroundColor.Value : Color.Transparent);
					else
					{
						g.Clear(Color.White);
						wantResetTransparency = true;
					}
					//
					ApplyGraphicsSettings(g, higherQuality: antialiased);
					//
					if (text != null)
					{
						Brush thisDrawingBrush = drawingBrush ?? Brushes.Black;
						string glyphString = string.Format("{0}", text);
						SizeF s = g.MeasureString(glyphString, f, PointF.Empty, StringFormat.GenericTypographic);
						Point p = new Point((int)Math.Ceiling(s.Width / 4.0f), (int)Math.Ceiling(s.Height / 4.0f));
						g.DrawString(glyphString, f, thisDrawingBrush, p, StringFormat.GenericDefault);
					}
				}
				if (wantResetTransparency)
					bitmap.MakeTransparent(Color.White);
				// Trim padding around the image (if necessary).
				return SupportFunctions.TrimBitmap(bitmap);
			}
		}

		public static Bitmap SubtractImage(Bitmap solidBitmap, Bitmap outputBitmap)
		{
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
								processor.SetPixel(column, row, Color.Transparent);
							}
						}
				}
				subProcessor.UnlockImage();
				subProcessor.Dispose();
				//
				solidBitmap.Dispose();
			}
			processor.UnlockImage();
			return outputBitmap;
		}
	}
}
