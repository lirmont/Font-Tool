using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System;

namespace AutoTrace
{
	public static class Filter
	{
		public enum MorphologyType
		{
			Dilation, Erosion
		}

		public static Bitmap Dilate(Bitmap sourceBitmap, int passes)
		{
			Bitmap thisBitmap = (Bitmap)sourceBitmap.Clone();
			for (int i = 0; i < passes; i++)
				thisBitmap = DilateAndErodeFilter(thisBitmap, 3, MorphologyType.Dilation);
			return thisBitmap;
		}

		public static Bitmap DilateAndErodeFilter(Bitmap sourceBitmap, int matrixSize, MorphologyType morphType, bool applyBlue = true, bool applyGreen = true, bool applyRed = true)
		{
			BitmapData sourceData = sourceBitmap.LockBits(new Rectangle(0, 0, sourceBitmap.Width, sourceBitmap.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
			byte[] pixelBuffer = new byte[sourceData.Stride * sourceData.Height];
			byte[] resultBuffer = new byte[sourceData.Stride * sourceData.Height];
			Marshal.Copy(sourceData.Scan0, pixelBuffer, 0, pixelBuffer.Length);
			sourceBitmap.UnlockBits(sourceData);
			int filterOffset = (int)Math.Max(1, (matrixSize - 1) / 2);
			int calcOffset = 0;
			int byteOffset = 0;
			byte morphResetValue = 0;
			if (morphType == MorphologyType.Erosion)
				morphResetValue = 255;
			//
			for (int offsetY = filterOffset; offsetY < sourceBitmap.Height - filterOffset; offsetY++)
			{
				for (int offsetX = filterOffset; offsetX < sourceBitmap.Width - filterOffset; offsetX++)
				{
					byteOffset = offsetY * sourceData.Stride + offsetX * 4;
					int blue = morphResetValue;
					int green = morphResetValue;
					int red = morphResetValue;
					int alpha = 0;
					int nonTrivialPixelHits = 0;
					if (morphType == MorphologyType.Dilation)
					{
						for (int filterY = -filterOffset; filterY <= filterOffset; filterY++)
						{
							for (int filterX = -filterOffset; filterX <= filterOffset; filterX++)
							{
								calcOffset = byteOffset + (filterX * 4) + (filterY * sourceData.Stride);
								// Reds a single pixel. Moves towards white.
								if (pixelBuffer[calcOffset + 3] > 0)
								{
									// Add pixel.
									blue += pixelBuffer[calcOffset];
									green += pixelBuffer[calcOffset + 1];
									red += pixelBuffer[calcOffset + 2];
									alpha += pixelBuffer[calcOffset + 3];
									// Count it for a future average.
									nonTrivialPixelHits++;
								}
							}
						}
					}
					/*else if (morphType == MorphologyType.Erosion)
					{
						for (int filterY = -filterOffset; filterY <= filterOffset; filterY++)
						{
							for (int filterX = -filterOffset; filterX <= filterOffset; filterX++)
							{
								calcOffset = byteOffset + (filterX * 4) + (filterY * sourceData.Stride);
								if (pixelBuffer[calcOffset] < blue)
									blue = pixelBuffer[calcOffset];
								if (pixelBuffer[calcOffset + 1] < green)
									green = pixelBuffer[calcOffset + 1];
								if (pixelBuffer[calcOffset + 2] < red)
									red = pixelBuffer[calcOffset + 2];
							}
						}
					}*/
					// Set.
					bool alreadyOccupied = (pixelBuffer[byteOffset + 3] != 0) ? true : false;
					bool noHits = (nonTrivialPixelHits == 0) ? true : false;
					// Blue
					if (applyBlue == false || alreadyOccupied || noHits)
						blue = pixelBuffer[byteOffset];
					else
						blue = (byte)Math.Max(0, Math.Min(255, Math.Round(blue / (float)nonTrivialPixelHits, MidpointRounding.AwayFromZero)));
					// Green
					if (applyGreen == false || alreadyOccupied || noHits)
						green = pixelBuffer[byteOffset + 1];
					else
						green = (byte)Math.Max(0, Math.Min(255, Math.Round(green / (float)nonTrivialPixelHits, MidpointRounding.AwayFromZero)));
					// Red
					if (applyRed == false || alreadyOccupied || noHits)
						red = pixelBuffer[byteOffset + 2];
					else
						red = (byte)Math.Max(0, Math.Min(255, Math.Round(red / (float)nonTrivialPixelHits, MidpointRounding.AwayFromZero)));
					// Alpha
					if (alreadyOccupied || noHits)
						alpha = pixelBuffer[byteOffset + 3];
					else
						alpha = (byte)Math.Max(0, Math.Min(255, Math.Round(alpha / (float)nonTrivialPixelHits, MidpointRounding.AwayFromZero)));
					//
					resultBuffer[byteOffset] = (byte)blue;
					resultBuffer[byteOffset + 1] = (byte)green;
					resultBuffer[byteOffset + 2] = (byte)red;
					resultBuffer[byteOffset + 3] = (byte)alpha;
				}
			}
			Bitmap resultBitmap = new Bitmap(sourceBitmap.Width, sourceBitmap.Height);
			BitmapData resultData = resultBitmap.LockBits(new Rectangle(0, 0, resultBitmap.Width, resultBitmap.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
			Marshal.Copy(resultBuffer, 0, resultData.Scan0, resultBuffer.Length);
			resultBitmap.UnlockBits(resultData);
			//
			return resultBitmap;
		}
	}
}
