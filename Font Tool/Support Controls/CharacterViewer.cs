using System;
using System.Drawing;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System.ComponentModel;

namespace FontTool
{
	public partial class CharacterViewer : UserControl
	{
		//
		private bool shown = false;

		[Browsable(false)] //disable design time support
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] //disable serialization
		public bool Shown
		{
			get { return shown; }
			set { shown = value; }
		}

		//
		private float zoomFactor = 5f;
		private float gutterY = 5f;
		private float characterWidth = 6f;
		private float advances = 0f;
		private float ascends = 0f;
		private float maximumAscension = 10f;
		//
		private uint TextureId {
			get {
				return (imageDescription != null) ? (uint)imageDescription.ContextId : 0U;
			}
			set { }
		}

		public float ZoomFactor
		{
			get { return zoomFactor; }
			set {
				zoomFactor = Math.Max(1, value);
				forceRedraw();
			}
		}

		public float GutterY
		{
			get { return gutterY; }
			set {
				gutterY = value;
				forceRedraw();
			}
		}

		public float CharacterWidth
		{
			get { return characterWidth; }
			set {
				characterWidth = value;
				forceRedraw();
			}
		}

		public float Advances
		{
			get { return advances; }
			set {
				advances = value;
				forceRedraw();
			}
		}

		public float Ascends
		{
			get { return ascends; }
			set {
				ascends = value;
				forceRedraw();
			}
		}

		public float MaximumAscension
		{
			get { return maximumAscension; }
			set {
				maximumAscension = value;
				forceRedraw();
			}
		}

		private ImageDescription imageDescription = null;

		[Browsable(false)] //disable design time support
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] //disable serialization
		public ImageDescription ImageDescription
		{
			get { return imageDescription; }
			set {
				imageDescription = value;
				if (this.glControl.IsHandleCreated)
					forceRedraw();
			}
		}

		[Browsable(false)] //disable design time support
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] //disable serialization
		public float CenterLineX
		{
			get {
				return (int)Math.Round(
					(ClientRectangle.Width - CharacterWidth * ZoomFactor) / ZoomFactor / 2f,
					MidpointRounding.AwayFromZero
				);
			}
		}

		public CharacterViewer()
		{
			if (DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime || System.Diagnostics.Process.GetCurrentProcess().ProcessName == "devenv")
				return;
			//
			InitializeComponent();
			glControl.Resize += glControl_Resize;
			glControl.Paint += glControl_Paint;
			glControl.Load += glControl_Load;
			glControl.MouseWheel += new MouseEventHandler(glControl_MouseWheel);
		}

		private void forceRedraw()
		{
			if (glControl != null && glControl.IsHandleCreated)
				glControl.Invalidate();
		}

		uint backgroundId = 0;
		public OpenGLConfiguration oglConfiguration = new OpenGLConfiguration();
		private void glControl_Load(object sender, EventArgs e)
		{
			//
			GLControl control = (GLControl)sender;
			control.MakeCurrent();
			// Get version.
			int major, minor;
			SupportFunctions.getCompatibleMajorMinorVersion(out major, out minor);
			oglConfiguration.setMajorMinorVersion(major, minor);
			// Get texture size.
			int textureSize;
			GL.Enable(EnableCap.Texture2D);
			GL.GetInteger(GetPName.MaxTextureSize, out textureSize);
			GL.Disable(EnableCap.Texture2D);
			oglConfiguration.MaximumTextureSize = textureSize;
			// Force power of two. Disable the OpenGL frame renderer.
			if (major < 3)
				ImageDescription.ForcePowerOfTwoDimensions = true;
			else
				ImageDescription.ForcePowerOfTwoDimensions = false;
			//
			backgroundId = Texture.LoadTexture(Properties.Resources.transparent_background);
		}

		private void glControl_Resize(object sender, EventArgs e)
		{
			//
			GLControl control = (GLControl)sender;
			control.MakeCurrent();
			GL.Viewport(control.ClientRectangle);
		}

		private void glControl_Paint(object sender, PaintEventArgs e)
		{
			//
			GLControl control = (GLControl)sender;
			control.MakeCurrent();
			//
			GL.Enable(EnableCap.DepthTest);
			GL.ClearColor(new Color4(1f, 1f, 1f, 0f));
			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
			//
			GL.MatrixMode(MatrixMode.Modelview);
			{
				GL.LoadIdentity();
				GL.Ortho(control.ClientRectangle.Left, control.ClientRectangle.Right, control.ClientRectangle.Top, control.ClientRectangle.Bottom, -10, 10);
				{
					GL.PushMatrix();
					{
						float zoomedControlWidth = control.ClientRectangle.Width / zoomFactor;
						float zoomedControlHeight = control.ClientRectangle.Height / zoomFactor;
						GL.Translate(0, 0, 0.2);
						SupportFunctions.glControl_DrawHorizontalLine(control, gutterY, zoomFactor);
						SupportFunctions.glControl_DrawHorizontalLine(control, gutterY + maximumAscension, zoomFactor, SupportFunctions.StipplePatterns.Dotted);
						SupportFunctions.glControl_DrawVerticalLine(control, CenterLineX, zoomFactor);
						SupportFunctions.glControl_DrawVerticalLine(control, (float)(CenterLineX + characterWidth), zoomFactor);
						GL.Translate(0, 0, -0.4);
						SupportFunctions.glControl_DrawTexturedQuad(0f, 0f, zoomedControlWidth, zoomedControlHeight, zoomFactor, textureWidth: zoomedControlWidth * 0.5f, textureHeight: zoomedControlHeight * 0.5f, textureId: backgroundId, flipTextureCoordinatesVertically: true);
						bool hasValidTexture = (TextureId > 0U) ? true : false;
						if (hasValidTexture)
						{
							// For OpenGL implementations less than 2.0, this is required. Depth testing is already disabled in the virtual keyboard for self-intersections.
							if (oglConfiguration.MajorVersion < 2)
								GL.Disable(EnableCap.DepthTest);
							//
							GL.Translate(0, 0, 0.2);
							SupportFunctions.glControl_DrawTexturedQuad(
								CenterLineX + Advances, gutterY + Ascends,
								imageDescription.SignificantWidth, imageDescription.SignificantHeight,
								zoomFactor,
								textureId: TextureId,
								textureWidth: imageDescription.TextureScale.X, textureHeight: imageDescription.TextureScale.Y
							);
						}
					}
					GL.PopMatrix();
				}
			}
			GL.Disable(EnableCap.DepthTest);
			//
			if (Shown != true)
				Shown = true;
			control.SwapBuffers();
		}

		public void glControl_Enter(object sender, EventArgs e)
		{
			glControl.Focus();
		}

		void glControl_MouseWheel(object sender, MouseEventArgs e)
		{
			ZoomFactor += Math.Sign(e.Delta);
		}
	}
}
