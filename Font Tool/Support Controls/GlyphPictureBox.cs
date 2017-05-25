using System.Drawing;
using System.IO;
using System;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Threading;
using System.Runtime.InteropServices;

namespace FontTool
{
	public partial class GlyphPictureBox : System.Windows.Forms.PictureBox
	{
		private OffsetableUnicodeCharacter character = null;
		private TracedCharacter tracedCharacter = null;
		//
		private Color hoverColor = Color.FromArgb(120, 199, 235);
		private Color clickColor = Color.FromArgb(64, 189, 232);
		//
		private Bitmap glyphPlaceholderBitmap = null;
		private Bitmap actualImage = null;
		//
		private static Brush GreyedOutBrush = new SolidBrush(Color.FromArgb(60, 0, 0, 0));
		//
		public delegate void SaveImageHandler(object sender, SaveImageEventArgs e);
		public event SaveImageHandler SaveImage;

		internal OffsetableUnicodeCharacter Character
		{
			get { return character; }
			set { character = value; }
		}

		public ulong Id
		{
			get {
				return (base.Tag != null) ? (ulong)base.Tag : 0;
			}
		}

		public string Glyph
		{
			get {
				return (character != null) ? character.ToString() : "";
			}
			set {
				if (Glyph != value)
					unsetStoredImages();
				// Reset Unicode ID.
				if (value != null && value.Length == 1)
					base.Tag = Convert.ToUInt64(value[0]);
				else
					base.Tag = null;
				//
				createOrUpdateGlyphImage();
			}
		}

		public TracedCharacter TracedCharacter
		{
			get
			{
				return tracedCharacter;
			}
			set {
				tracedCharacter = value;
				//
				Character = (tracedCharacter != null) ? tracedCharacter.DerivedUnicodeCharacter : null;
				GlyphImageFile = (tracedCharacter != null && tracedCharacter.PreviewImage != null) ? SupportFunctions.GetClonedBitmap(tracedCharacter.PreviewImage) : null;
			}
		}

		private void unsetStoredImages()
		{
			freeGlyphImage();
			glyphPlaceholderBitmap = null;
		}

		public void ReloadImages() {
			unsetStoredImages();
			createOrUpdateGlyphImage();
		}

		public Bitmap GlyphImageFile
		{
			get { return actualImage; }
			set {
				try
				{
					if (value == null)
						freeGlyphImage();
					else
						actualImage = value;
					//
					createOrUpdateGlyphImage();
				}
				catch { }
			}
		}

		private string UnicodeBlockName
		{
			get { return (character != null) ? UnicodeBlocks.GetBlockForCharacter(character.Id).BlockName : null; }
		}

		public string UnicodeName
		{
			get { return character != null ? character.Name : null; }
		}

		public GlyphPictureBox()
		{
			Initialize();
		}

		public GlyphPictureBox(OffsetableUnicodeCharacter character)
		{
			this.Character = character;
			base.Tag = character.Code;
			//
			Initialize();
		}

		public GlyphPictureBox(OffsetableUnicodeCharacter character, Bitmap image)
		{
			this.Character = character;
			this.GlyphImageFile = image;
			base.Tag = character.Code;
			//
			Initialize();
		}

		public GlyphPictureBox(ulong id, TracedCharacter tracedCharacter)
		{
			base.Tag = id;
			this.TracedCharacter = tracedCharacter;
			//
			Initialize();
		}

		~GlyphPictureBox()
		{
			DisposeHandle();
		}

		private void DisposeHandle()
		{
			freeGlyphImage();
		}

		private void freeGlyphImage()
		{
			if (this.actualImage != null)
				this.actualImage.Dispose();
			//
			this.actualImage = null;
		}

		private void Initialize()
		{
			base.DoubleBuffered = false;
			base.SizeMode = PictureBoxSizeMode.CenterImage;
			InitializeComponent();
			base.BackColor = Color.White;
			this.Size = (actualImage != null) ? new Size(Math.Max(35, actualImage.Width), Math.Max(35, actualImage.Height)) : new Size(35, 35);
			this.Cursor = Cursors.Hand;
			createOrUpdateGlyphImage();
		}

		private void setSize(Size newSize, GlyphPictureBox m) {
			if (m != null && m.Size != newSize)
			{
				if (m.IsHandleCreated)
				{
					m.Invoke((MethodInvoker)delegate()
					{
						m.Size = newSize;
					});
				}
				else {
					m.Size = newSize;
				}
			}
		}

		private void createOrUpdateGlyphImage() {
			Size size = Size.Empty, newSize = Size.Empty;
			if (actualImage != null)
			{
				try
				{
					Image = actualImage;
					// Resize.
					if (actualImage != null && actualImage.PixelFormat != PixelFormat.Undefined)
					{
						size = actualImage.Size;
						newSize = new Size(Math.Max(35, size.Width + 2), Math.Max(35, size.Height + 2));
						setSize(newSize, this);
					}
					return;
				}
				catch { }
			}
			// Create a placeholder image (so glyph proxies aren't blank).
			if (glyphPlaceholderBitmap == null)
				glyphPlaceholderBitmap = SupportFunctions.CreateTextImage(Glyph, UnicodePrivateFontCollection.GetFontForUnicodeBlock(UnicodeBlockName), base.Size, drawingBrush: GreyedOutBrush);
			// Update size to the placeholder, preferring 35x35 where available.
			size = (glyphPlaceholderBitmap != null) ? glyphPlaceholderBitmap.Size : new Size(35, 35);
			//
			newSize = new Size(Math.Max(35, size.Width), Math.Max(35, size.Height));
			if (this.Size != newSize)
				this.Size = newSize;
			// Set the displayed image to the placeholder.
			Image = glyphPlaceholderBitmap;
		}

		protected override void OnPaint(System.Windows.Forms.PaintEventArgs pe)
		{
			try
			{
				base.OnPaint(pe);
				pe.Graphics.DrawRectangle(Pens.LightSlateGray, 0, 0, Size.Width - 1, Size.Height - 1);
			}
			catch { }
		}

		protected override void OnResize(System.EventArgs e)
		{
			base.OnResize(e);
			createOrUpdateGlyphImage();
		}

		protected override void OnMouseEnter(EventArgs e)
		{
			base.OnMouseEnter(e);
			base.BackColor = hoverColor;
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			base.BackColor = clickColor;
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			base.BackColor = hoverColor;
		}

		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
			base.BackColor = Color.White;
		}

		public bool CharacterNotRealized { 
			get {
				return (actualImage == null && glyphPlaceholderBitmap != null);
			}
			private set { }
		}

		public void realizeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// Emit background image (so that it can be saved).
			if (CharacterNotRealized)
				OnSaveImage(SupportFunctions.CreateTextImage(Glyph, UnicodePrivateFontCollection.GetFontForUnicodeBlock(UnicodeBlockName), base.Size, drawingBrush: Brushes.Black));
		}

		public void browseToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Application.DoEvents();
			//
			Thread openFileThread = new Thread(new ThreadStart(filePromptToEmitSaveImage));
			openFileThread.Name = string.Format("Browse for Replacement (U+{0:X}: {1})", character.Id, character.Name);
			openFileThread.Start();
		}

		private void filePromptToEmitSaveImage()
		{
			this.Invoke((MethodInvoker)delegate
			{
				string path = null;
				// Do open file dialog.
				DialogResult result = openFileDialog.ShowDialog();
				if (result == DialogResult.OK)
				{
					path = openFileDialog.FileName;
					// On success, emit selected file.
					if (File.Exists(path))
						OnSaveImage(new Bitmap(path));
				}
			});
		}

		public void clearToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// Emit clear.
			OnSaveImage(null);
		}

		protected void OnSaveImage(Bitmap bitmap)
		{
			// Make sure there is a listener.
			if (SaveImage == null) return;
			SaveImage(this, new SaveImageEventArgs((ulong)this.Tag, bitmap));
		}

		public class SaveImageEventArgs : EventArgs
		{
			public ulong Key { get; internal set; }
			public Bitmap Bitmap { get; internal set; }

			public SaveImageEventArgs(ulong key, Bitmap bitmap)
			{
				this.Key = key;
				this.Bitmap = bitmap;
			}
		}
	}
}
