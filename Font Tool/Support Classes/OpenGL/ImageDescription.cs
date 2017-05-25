using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using System.IO;
using OpenTK.Graphics.OpenGL;

namespace FontTool
{
	public class ImageDescription
	{
		//
		static string TextureEntry =
@"<Texture> {Texture Name} {
	""{Texture File}""
	<Scalar> minfilter { {Texture Minification Filter} }
	<Scalar> magfilter { {Texture Magnification Filter} }
	<Scalar> anisotropic-degree { {Texture Anistropic Degree} }
	<Scalar> quality-level { normal }
	<Scalar> wrap { {Wrap Mode} }
	<Transform> {
		<Matrix3> {
			{Inverse of Texture Width (decimal)} 0 0
			0 {Inverse of Texture Height (decimal)} 0
			0 0 1
		}
	}
}";
		public static string GetImageDescriptionAsTextureEntry(int width, int height, string name, Plugins.Configuration configuration, string relativeBasePath = null, bool flipped = false)
		{
			string safeTextureName = name.Replace("#", "").Replace("-", "").Replace(" ", "");
			string fileStub = string.Format("{0}.png", name);
			string relativeFileName = (relativeBasePath == null) ? fileStub : string.Format("{0}/{1}", new object[] { relativeBasePath, fileStub });
			// Make the texture entry.
			string template = ImageDescription.TextureEntry;
			template = template.Replace("{Texture Width}", string.Format("{0}", width));
			template = template.Replace("{Texture Height}", string.Format("{0}", height));
			template = template.Replace("{Wrap Mode}", (flipped) ? "REPEAT" : "CLAMP");
			template = template.Replace("{Inverse of Texture Width (decimal)}", string.Format("{0}", 1 / (decimal)width));
			decimal inverseOfHeight = (flipped) ? -1 / (decimal)height : 1 / (decimal)height;
			template = template.Replace("{Inverse of Texture Height (decimal)}", string.Format("{0}", inverseOfHeight));
			template = template.Replace("{Texture Name}", safeTextureName);
			template = template.Replace("{Texture File}", relativeFileName);
			template = template.Replace("{Texture Minification Filter}", configuration.MinificationFilter);
			template = template.Replace("{Texture Magnification Filter}", configuration.MagnificationFilter);
			template = template.Replace("{Texture Anistropic Degree}", string.Format("{0}", configuration.AnistropicDegree));
			// Send it back.
			return template;
		}

		public static string GetImageDescriptionAsTextureEntry(Bitmap image, string name, Plugins.Configuration configuration, string relativeBasePath = null)
		{
			return GetImageDescriptionAsTextureEntry(image.Width, image.Height, name, configuration, relativeBasePath: relativeBasePath);
		}

		public static string GetImageDescriptionAsTextureEntry(ImageDescription description, Plugins.Configuration configuration, string relativeBasePath = null, bool flipped = false)
		{
			return GetImageDescriptionAsTextureEntry((int)Math.Ceiling(description.SignificantWidth), (int)Math.Ceiling(description.SignificantHeight), description.Name, configuration, relativeBasePath: relativeBasePath, flipped: flipped);
		}

		// Mode
		public static bool ForcePowerOfTwoDimensions = false;

		//
		private ulong id;
		private int contextId, stride;
		private int width, height;
		private int significantWidth, significantHeight;
		private GCHandle dataHandle;
		private string name;
		private bool isPlaceholder = true;
		private bool repeat = false;
		private bool pushToContext = true;
		private bool canBePushedToContext = true;
		private string filename = null;
		private TextureMagFilter sampling = TextureMagFilter.Nearest;
		private bool makeBackgroundTransparent = false;
		private OpenTK.Vector2 textureScale = new OpenTK.Vector2(1f, 1f);

		public bool MakeBackgroundTransparent
		{
			get { return makeBackgroundTransparent; }
			set { makeBackgroundTransparent = value; }
		}

		public string Name
		{
			get { return name; }
			set { name = value; }
		}

		public IntPtr DataPointer
		{
			get
			{
				if (this.dataHandle.IsAllocated)
					return this.dataHandle.AddrOfPinnedObject();
				return IntPtr.Zero;
			}
		}

		public TextureMagFilter Sampling
		{
			get { return sampling; }
			set
			{
				sampling = value;
				// Update the texture's sampling, if necessary.
				pushImageToGraphicsContextIfNecessary();
			}
		}

		public string Filename
		{
			get
			{
				if (filename != null)
					return filename;
				else
					return "";
			}
			set { filename = value; }
		}

		public bool IsPlaceholder
		{
			get { return isPlaceholder; }
			set { isPlaceholder = value; }
		}

		//
		public ulong Id
		{
			get { return id; }
			set { id = value; }
		}

		//
		public int Width
		{
			get { return width; }
			set { width = value; }
		}

		public int Height
		{
			get { return height; }
			set { height = value; }
		}

		//
		public int Stride
		{
			get { return stride; }
			set { stride = value; }
		}

		public int ContextId
		{
			get
			{
				if (pushToContext && contextId == 0)
					pushImageToGraphicsContextIfNecessary();
				return contextId;
			}
			set { contextId = value; }
		}

		private Color? backgroundColor = null;

		public Color BackgroundColor
		{
			get
			{
				try
				{
					if (backgroundColor != null)
						return backgroundColor.Value;
					else
						return (this.Width > 1 && this.Height > 1) ? this.Bitmap.GetPixel(1, 1) : Color.Black;
				}
				catch (Exception)
				{
					return Color.Black;
				}
			}
		}

		public DateTime LastWrite
		{
			get
			{
				try
				{
					if (filename != null)
						return File.GetLastWriteTime(filename);
				}
				catch { }
				return DateTime.MinValue;
			}
		}

		public Bitmap Bitmap
		{
			get
			{
				return new Bitmap(this.width, this.height, this.stride, System.Drawing.Imaging.PixelFormat.Format32bppArgb, this.DataPointer);
			}
			private set { }
		}

		public Bitmap SignificantBitmap
		{
			get
			{
				Bitmap possiblyLargerBitmap = Bitmap;
				Bitmap bitmap = new Bitmap(this.significantWidth, this.significantHeight);
				using (Graphics g = Graphics.FromImage(bitmap))
					g.DrawImage(possiblyLargerBitmap, 0, 0, possiblyLargerBitmap.Width, possiblyLargerBitmap.Height);
				possiblyLargerBitmap.Dispose();
				return bitmap;
			}
			private set { }
		}

		public OpenTK.Vector2 TextureScale
		{
			get { return textureScale; }
			set { textureScale = value; }
		}

		public float SignificantWidth
		{
			get { return TextureScale.X * this.Width; }
		}

		public float SignificantHeight
		{
			get { return TextureScale.Y * this.Height; }
		}

		public string SafeName
		{
			get {
				if (Name != null)
					return Name.Replace("#", "").Replace("-", "").Replace(" ", "");
				else
					return "NoName";
			}
		}

		public string SafeFilename
		{
			get
			{
				return string.Format("{0}.png", SafeName);
			}
		}

		public ImageDescription(ulong id, Bitmap bitmap = null, bool repeat = false, bool pushToContext = true, bool makeBackgroundTransparent = false, Color? backgroundColor = null, TextureMagFilter sampling = TextureMagFilter.Nearest, bool canBePushedToContext = true)
		{
			this.id = id;
			this.repeat = repeat;
			this.pushToContext = pushToContext;
			this.canBePushedToContext = canBePushedToContext;
			if (!canBePushedToContext)
				this.pushToContext = false;
			this.sampling = sampling;
			this.backgroundColor = backgroundColor;
			this.makeBackgroundTransparent = makeBackgroundTransparent;
			replaceWithBitmap(bitmap);
		}

		public ImageDescription(ulong id, string filename, bool repeat = false, bool pushToContext = true, bool makeBackgroundTransparent = false, TextureMagFilter sampling = TextureMagFilter.Nearest)
			: this(id, bitmap: new Bitmap(filename), repeat: repeat, pushToContext: pushToContext, makeBackgroundTransparent: makeBackgroundTransparent, sampling: sampling)
		{
			this.filename = filename;
		}

		public ImageDescription()
			: this(0)
		{
		}

		private Bitmap padImageToNextPowerOfTwo(Bitmap bitmap)
		{
			// Force power of two to handle compatibility for clients that may require them.
			if (canBePushedToContext && ForcePowerOfTwoDimensions && bitmap != null && bitmap.PixelFormat != System.Drawing.Imaging.PixelFormat.Undefined)
			{
				int oldWidth = bitmap.Width;
				int oldHeight = bitmap.Height;
				int newWidth = (int)SupportFunctions.NextPowerOfTwo((uint)oldWidth);
				int newHeight = (int)SupportFunctions.NextPowerOfTwo((uint)oldHeight);
				//
				bool differentWidth = newWidth != oldWidth;
				bool differentHeight = newHeight != oldHeight;
				if (differentWidth || differentHeight)
				{
					Bitmap newBitmap = new Bitmap(newWidth, newHeight);
					using (Graphics g = Graphics.FromImage(newBitmap))
						g.DrawImage(bitmap, 0, 0, bitmap.Width, bitmap.Height);
					bitmap = newBitmap;
				}
				if (differentWidth)
					textureScale.X = oldWidth / (float)newWidth;
				if (differentHeight)
					textureScale.Y = oldHeight / (float)newHeight;
			}
			return bitmap;
		}

		public void replaceWithBitmap(Bitmap bitmap = null, bool respectMakeBackgroundTransparent = true, bool significant = true)
		{
			//
			bool isPlaceholder = false;
			if (bitmap == null)
			{
				bitmap = new Bitmap(1, 1);
				bitmap.SetPixel(0, 0, Color.FromArgb(0, 0, 0, 0));
				isPlaceholder = true;
			}
			//
			int significantWidth = bitmap.Width;
			int significantHeight = bitmap.Height;
			bitmap = padImageToNextPowerOfTwo(bitmap);
			//
			int width = 0, height = 0, stride = 0;
			byte[] thisByteArray = new byte[0];
			using (Bitmap bm2 = bitmap)
			{
				//
				width = bm2.Width;
				height = bm2.Height;
				Rectangle rect2 = new Rectangle(0, 0, bm2.Width, bm2.Height);
				BitmapData bm2Data = bm2.LockBits(rect2, ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
				try
				{
					IntPtr bm2Ptr = bm2Data.Scan0;
					stride = bm2Data.Stride;
					thisByteArray = new byte[Math.Abs(bm2Data.Stride) * bm2.Height];
					Marshal.Copy(bm2Ptr, thisByteArray, 0, thisByteArray.Length);
					bm2Ptr = IntPtr.Zero;
				}
				finally
				{
					bm2.UnlockBits(bm2Data); //Lock End
				}
			}
			//
			this.width = width;
			this.height = height;
			if (significant)
			{
				this.significantWidth = significantWidth;
				this.significantHeight = significantHeight;
			}
			this.stride = stride;
			this.isPlaceholder = isPlaceholder;
			//
			if (this.DataPointer != IntPtr.Zero)
				this.dataHandle.Free();
			// Pin byte array to make pointer available to data.
			this.dataHandle = GCHandle.Alloc(thisByteArray, GCHandleType.Pinned);
			//
			bitmap.Dispose();
			// Make the background of the image transparent after the fact if and only if the code is supposed to respect such a directive and it hasn't already.
			if (respectMakeBackgroundTransparent)
			{
				if (this.makeBackgroundTransparent)
					makeBackgroundOfBitmapTransparent();
			}
			//
			pushImageToGraphicsContextIfNecessary();
		}

		public Bitmap GetClonedBitmap(bool flipped = false) {
			return SupportFunctions.GetClonedBitmap(SignificantBitmap, flipped: flipped);
		}

		public void replaceWithBitmap(string filename)
		{
			replaceWithBitmap(new Bitmap(filename));
		}

		private void pushImageToGraphicsContextIfNecessary()
		{
			if (pushToContext)
				contextId = (int)Texture.LoadTexture(Width, Height, Stride, DataPointer);
		}

		public void makeBackgroundOfBitmapTransparent()
		{
			Bitmap thisBitmap = this.Bitmap;
			thisBitmap.MakeTransparent(this.BackgroundColor);
			this.replaceWithBitmap(thisBitmap, respectMakeBackgroundTransparent: false, significant: false);
		}

		public void Save(string filename = null)
		{
			if (filename == null)
				filename = Filename;
			// Try to save the image.
			if (filename != null)
			{
				try
				{
					this.SignificantBitmap.Save(filename);
				}
				catch (Exception) { }
			}
		}

		public void Dispose()
		{
			if (this.DataPointer != IntPtr.Zero)
				this.dataHandle.Free();
		}

		~ImageDescription()
		{
			Dispose();
		}

		public string getTextureEntry(Plugins.Configuration configuration, string relativeBasePath = null, bool flipped = false)
		{
			return ImageDescription.GetImageDescriptionAsTextureEntry(this, configuration, relativeBasePath: relativeBasePath, flipped: flipped);
		}
	}
}
