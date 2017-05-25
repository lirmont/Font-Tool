using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using OpenTK.Graphics.OpenGL;
using System.Runtime.InteropServices;

namespace FontTool
{
	public class Texture
	{
		public static Dictionary<uint, ImageDescription> Handles = new Dictionary<uint, ImageDescription>();

		public static uint LoadTexture(string filename)
		{
			if (String.IsNullOrEmpty(filename))
				throw new ArgumentException(filename);
			return Texture.LoadTexture(new Bitmap(filename));
		}

		public static uint LoadTexture(Stream stream)
		{
			return Texture.LoadTexture(new Bitmap(stream));
		}

		public static uint LoadTexture(Bitmap bmp)
		{
			uint id = 0U;
			if (bmp != null)
			{
				try
				{
					id = (uint)GL.GenTexture();
					GL.BindTexture(TextureTarget.Texture2D, id);
					//
					BitmapData bmp_data = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
					//
					GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, bmp_data.Width, bmp_data.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, bmp_data.Scan0);
					//
					bmp.UnlockBits(bmp_data);
					// We haven't uploaded mipmaps, so disable mipmapping (otherwise the texture will not appear). On newer video cards, we can use GL.GenerateMipmaps() or GL.Ext.GenerateMipmaps() to create mipmaps automatically. In that case, use TextureMinFilter.LinearMipmapLinear to enable them.
					GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
					GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);
					//
					GL.BindTexture(TextureTarget.Texture2D, 0);
				}
				catch { }
			}
			return id;
		}

		public static uint LoadTexture(int width, int height, int stride, IntPtr dataPointer)
		{
			uint id = 0U;
			if (dataPointer != IntPtr.Zero)
			{
				try
				{
					id = (uint)GL.GenTexture();
					GL.BindTexture(TextureTarget.Texture2D, id);
					//
					GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, width, height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, dataPointer);
					// We haven't uploaded mipmaps, so disable mipmapping (otherwise the texture will not appear). On newer video cards, we can use GL.GenerateMipmaps() or GL.Ext.GenerateMipmaps() to create mipmaps automatically. In that case, use TextureMinFilter.LinearMipmapLinear to enable them.
					GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
					GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);
					//
					GL.BindTexture(TextureTarget.Texture2D, 0);
				}
				catch { }
			}
			return id;
		}

		public static void DisposeTexture(ref uint id)
		{
			if (id > 0)
			{
				GL.DeleteTexture(id);
				id = 0;
			}
		}
	}
}
