using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using OpenTK.Graphics.OpenGL;
using System.IO;
using System.Drawing.Imaging;

#pragma warning disable
namespace FontTool
{
	public partial class Credits : Form
	{
		/*
		 * Link to Main
		 */
		private Main parent = null;

		/*
		 * Scene Drawing Support
		 */
		private int rotateByXDegrees = 0, rotateIncrement = 5, baseRotateIncrement = 7, rotateVariance = 1;
		private int rotationMarker = 0; // 2 + 2 .. = 14 -> 2
		private int rotateX = 0, rotateY = 0, rotateZ = 0;
		private int delayCycles = 50, baseDelayCycles = 50;
		private Random r = new Random();

		/*
		 * Scene Drawing Support: Frame Buffer
		 */
		private bool useFBOAndShader = true;
		private int frameBufferObject = -1;
		private int frameBufferColorTexture = -1, frameBufferDepthTexture = -1;
		private int frameBufferTextureSize = 1024;
		private bool initialFrameBufferObjectDrawingPass = true;
		private float alphaFadePerFrameBufferObjectDrawingStep = 0.635f;

		/*
		 * Drawing Control Support
		 */
		private int scheduleRedrawSleepCycles = 0;

		/*
		 * Loaded image support variables.
		 */
		private static Bitmap iconBitmap;
		private static System.Drawing.Imaging.BitmapData backgroundData;
		private static int backgroundId;

		/*
		 * Scene Drawing Support: Shader
		 */
		private int shaderProgram = -1;
		private int shaderTextureLocationUniformInput = -1, shaderBlurSizeUniformInput = -1;
		private const float minimumBlurSize = 1.0f / 1024.0f, maximumBlurSize = 5f / 1024.0f;
		private float shaderBlurSize = minimumBlurSize;

		public Credits(Main parent)
		{
			useFBOAndShader = parent.OpenGLConfiguration.NewFrameBuffersAreSupported;
			//
			this.parent = parent;
			InitializeComponent();
			creditsRichTextBox.Rtf = @"{\rtf1\ansi\ansicpg1252\deff0\deflang1033{\fonttbl{\f0\fswiss\fprq2\fcharset0 Microsoft Sans Serif;}}{\colortbl ;\red0\green0\blue0;\red0\green0\blue255;}{\*\generator Msftedit 5.41.15.1515;}\viewkind4\uc1\pard\b\f0\fs18\par Programming: \par\b0\qc lirmont,\par\cf1 lirmont (at) gmail.com\par\cf0\par\pard\b Unifont 5.1:\par\pard\qc\b0 Unifoundry\b\par\cf2\ul\b0 http://unifoundry.com/ \par\cf0\ulnone\par\pard\b Unicode Standard & Last Resort Font:\par\pard\qc\b0 The Unicode Consortium\par\cf2\ul http://www.unicode.org/ \b\par\pard\cf0\ulnone\par Menu Icons:\par\b0\qc Icons Etc.\par\cf2\ul http://icons.mysitemyway.com/ \par\par\pard\cf1\ulnone\par}";
			versionNumberLabel.Rtf = @"{\rtf1\ansi\ansicpg1252\deff0\deflang1033{\fonttbl{\f0\fswiss\fprq2\fcharset0 Microsoft Sans Serif;}}
{\*\generator Msftedit 5.41.15.1515;}\viewkind4\uc1\pard\qc\b\f0\fs18 ver. " + SupportFunctions.GetProgramVersionString() + @"\par}";
		}

		private void programIconSimpleOpenGlControl_Paint(object sender, PaintEventArgs e)
		{
			if (creditsRichTextBox.Focused)
				programIconSimpleOpenGlControl.Focus();
			// Make sure the drawing context is selected prior to drawing to it.
			programIconSimpleOpenGlControl.MakeCurrent();
			// Turn on the ability to blend to zero-alpha state.
			GL.Enable(EnableCap.Blend);
			GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
			GL.Enable(EnableCap.DepthTest);
			GL.DepthFunc(DepthFunction.Lequal);
			GL.Disable(EnableCap.CullFace);
			// Set the clearing depth so that zero-depth is white.
			GL.ClearDepth(1);
			// If using the FBO, draw in 2 passes.
			if (useFBOAndShader)
			{
				renderIconSceneToFrameBuffer();
				renderFrameBufferToContext();
			}
			// Otherwise, just draw the icon's scene once.
			else
				programIconSimpleOpenGlControl_DrawIcon();
			// Push everything to the drawing context.
			GL.Flush();
			programIconSimpleOpenGlControl.SwapBuffers();
		}

		// Render the scene into the frame buffer.
		private void renderIconSceneToFrameBuffer()
		{
			// Pick the frame buffer in place of the actual drawing context.
			SupportFunctions.BindFrameBuffer(frameBufferId: (uint)frameBufferObject);
			// Draw the scene into the buffer.
			programIconSimpleOpenGlControl_DrawIcon();
		}

		// Render the frame buffer that holds the icon's pre-rendered scene into the actual drawing context.
		private void renderFrameBufferToContext()
		{
			// Switch from render-to-framebuffer to render-to-context.
			SupportFunctions.BindDefaultFrameBuffer();
			// Push the settings.
			GL.PushAttrib(AttribMask.CurrentBit);
			{
				// Push the transformation.
				GL.PushMatrix();
				{
					// Re-do the viewport to match the size of the drawing context.
					GL.Viewport(0, 0, programIconSimpleOpenGlControl.Width, programIconSimpleOpenGlControl.Height);
					// Set the matrix mode.
					GL.MatrixMode(MatrixMode.Projection);
					// Zero out the transformation matrix.
					GL.LoadIdentity();
					// Set up the context for 2D coordinate-space drawing.
					GL.Ortho(-.25, 1.25, -.25, 1.25, -1, 1);
					// Make sure the color of the background is the color of the WinForms control.
					GL.ClearColor(Form.DefaultBackColor);
					// If this is the initial draw, clear it out. Otherwise, just draw the frame buffer over existing draws, yielding an after-image effect.
					if (initialFrameBufferObjectDrawingPass)
					{
						GL.Clear(ClearBufferMask.ColorBufferBit);
						initialFrameBufferObjectDrawingPass = false;
					}
					// Translate the matrix so that the icon is in the middle of the screen (because the method draws an object centered around the identity coordinate).
					GL.Translate(.5, .5, 0);
					// If this isn't the initial draw, turn off the shader, then draw a context-sized quadrilateral of the same color as the clear color (i.e. the WinForm's control color) but with an alpha component of less than one (emulating the function of the glClear command for the color buffer).
					if (!initialFrameBufferObjectDrawingPass)
					{
						// Turn off the shader.
						GL.UseProgram(0);
						// Draw the quadrilateral.
						SupportFunctions.render(-0.751, 0.751, 0.751, -0.751, color: Color.FromArgb((int)(255 * alphaFadePerFrameBufferObjectDrawingStep), DefaultBackColor));
					}
					// Clear out the depth buffer so that it only holds the textured frame buffer quadrilateral.
					GL.Clear(ClearBufferMask.DepthBufferBit);
					// If the shader program is available, use it on the draw-to-context quadrilateral.
					if (shaderProgram > 0)
					{
						GL.UseProgram(shaderProgram);
						GL.Uniform1(shaderTextureLocationUniformInput, 0);
						GL.Uniform1(shaderBlurSizeUniformInput, getShaderBlurSize());
					}
					// Otherwise, don't use a program.
					else
						GL.UseProgram(0);
					// Draw the quadrilateral holding the frame buffer color texture.
					SupportFunctions.render(-0.751, 0.751, 0.751, -0.751, textureId: frameBufferColorTexture);
				}
				GL.PopMatrix(); //GL.PopMatrix();
			}
			GL.PopAttrib();
		}

		// Perform easing on a value.
		private float weightedValue(float inputValue, float goal)
		{
			float percentOfGoal = inputValue / goal;
			float weightedValue = inputValue * (0.9f + percentOfGoal / 10.0f);
			return weightedValue;
		}

		// Get the blur size for the shader, calculated based on the rotational component.
		private float getShaderBlurSize()
		{
			float midPointDegree = 90.0f;
			// Calculate the percentage based on 90 being a midpoint. There are 2 mid-points per animation cycle.
			float percentage = (rotateByXDegrees % (int)midPointDegree) / midPointDegree;
			// Reset the percentage if the animation is through.
			if (rotateByXDegrees >= 360)
				percentage = 0;
			// Reverse the percentage if the animation is not through but is at or past the halfway point.
			else if (midPointDegree * 4 > rotateByXDegrees && rotateByXDegrees >= midPointDegree * 3)
				percentage = 1 - percentage;
			else if (midPointDegree * 2 > rotateByXDegrees && rotateByXDegrees >= midPointDegree)
				percentage = 1 - percentage;
			// Calculate the blur size of the shader.
			float blurSize = minimumBlurSize + (maximumBlurSize - minimumBlurSize) * percentage;
			// Adjust the blur size for easing.
			float adjustedBlurSize = weightedValue(blurSize, maximumBlurSize);
			// Send it back.
			return adjustedBlurSize;
		}

		private void drawQuadWithTexture(int textureId = -1, float halfWidth = 0.025f, float size = 0.5f)
		{
			// Choose whether to draw it with a texture or not.
			if (textureId >= 0)
			{
				// Set the texture to the specified texture.
				GL.BindTexture(TextureTarget.Texture2D, textureId);
				// Enable texturing.
				GL.Enable(EnableCap.Texture2D);
			}
			else
			{
				// Reset the bound texture to zero, so that it is in a default state.
				GL.BindTexture(TextureTarget.Texture2D, 0);
				// Disable the texturing.
				GL.Disable(EnableCap.Texture2D);
			}
			// Forward-facing.
			SupportFunctions.render(size, -size, -size, size, depth: halfWidth, s: 1, S: 0, t: 0, T: 1, textureId: textureId);
			//
			if (halfWidth > 0)
			{
				// Left-facing.
				SupportFunctions.render(-size, -size, -size, size, depth: -halfWidth, depthChange: halfWidth * 2, s: 0, S: 0, t: 0, T: 1, textureId: textureId);
				// Right-facing.
				SupportFunctions.render(size, size, -size, size, depth: halfWidth, depthChange: -halfWidth * 2, s: 1, S: 1, t: 0, T: 1, textureId: textureId);
				// Top-facing.
				SupportFunctions.render(
					points: new List<Shapes.Point> {
						new Shapes.Point(-size, size,  halfWidth),
						new Shapes.Point(-size, size, -halfWidth),
						new Shapes.Point( size, size, -halfWidth),
						new Shapes.Point( size, size,  halfWidth),
					},
					textureCoordinates: new List<Shapes.Point> {
						new Shapes.Point(0, 0),
						new Shapes.Point(0, 0),
						new Shapes.Point(1, 0),
						new Shapes.Point(1, 0)
					},
					textureId: textureId
				);
				// Bottom-facing.
				SupportFunctions.render(
					points: new List<Shapes.Point> {
						new Shapes.Point(-size, -size, -halfWidth),
						new Shapes.Point(-size, -size,  halfWidth),
						new Shapes.Point( size, -size,  halfWidth),
						new Shapes.Point( size, -size, -halfWidth),
					},
					textureCoordinates: new List<Shapes.Point> {
						new Shapes.Point(0, 1),
						new Shapes.Point(0, 1),
						new Shapes.Point(1, 1),
						new Shapes.Point(1, 1)
					},
					textureId: textureId
				);
				// Rear-facing.
				SupportFunctions.render(-size, size, -size, size, depth: -halfWidth, s: 0, S: 1, t: 0, T: 1, textureId: textureId);
			}
		}

		private void programIconSimpleOpenGlControl_DrawIcon(bool advanceAnimations = true)
		{
			//
			GL.PushAttrib(AttribMask.CurrentBit); //GL.PushAttrib(Gl.GL_CURRENT_BIT);
			{
				//
				if (useFBOAndShader)
				{
					GL.UseProgram(0);
					GL.Viewport(0, 0, frameBufferTextureSize, frameBufferTextureSize);
				}
				else
					GL.Viewport(0, 0, programIconSimpleOpenGlControl.Width, programIconSimpleOpenGlControl.Height); //GL.Viewport(0, 0, programIconSimpleOpenGlControl.Width, programIconSimpleOpenGlControl.Height);
				//
				GL.MatrixMode(MatrixMode.Projection); //GL.MatrixMode(Gl.GL_PROJECTION);
				GL.LoadIdentity(); //GL.LoadIdentity();
				GL.Ortho(-.25, 1.25, -.25, 1.25, -1, 1); //GL.Ortho(-.25, 1.25, -.25, 1.25, -1, 1);
				// If this is being drawn to a frame buffer, make the background completely transparent.
				if (useFBOAndShader)
					GL.ClearColor(0, 0, 0, 0);
				// Otherwise, use the WinForm's control color.
				else
					GL.ClearColor(Form.DefaultBackColor.R / 255f, Form.DefaultBackColor.G / 255f, Form.DefaultBackColor.B / 255f, Form.DefaultBackColor.A / 255f);
				// 
				GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
				//
				GL.Color4(1, 1, 1, 1);
				GL.PushMatrix();
				{
					// Apply the centering translation.
					GL.Translate(.5, .5, 0);
					// Apply the rotation.
					GL.Rotate(Math.Min(360, rotateByXDegrees), rotateX, rotateY, rotateZ);
					// Update the rotation if the object has rotated past the 360 degree mark.
					if (scheduleRedrawSleepCycles <= 0 && rotateByXDegrees >= 360)
						updateRotation(rotateX, rotateY, rotateZ, rotateIncrement, delayCycles);
					// Advance animations only if the task shouldn't be sleeping (i.e. drawing, but remaining stationary).
					if (scheduleRedrawSleepCycles <= 0 && advanceAnimations)
						rotateByXDegrees += rotateIncrement;
					// Otherwise, decrement the sleep cycles, approaching 0.
					else
						scheduleRedrawSleepCycles -= 1;
					// Draw the icon.
					drawQuadWithTexture(textureId: backgroundId);
				}
				GL.PopMatrix();
			}
			GL.PopAttrib();
			// If this is being drawn to a frame buffer, push everything to the frame buffer for this pass.
			if (useFBOAndShader)
				GL.Flush();
		}

		private void updateRotation(int previousX = 0, int previousY = 0, int previousZ = 0, int previousIncrement = 5, int previousDelayCycles = 50)
		{
			int varianceParity = (r.Next(2) % 2 == 0) ? 1 : -1;
			// Get a rotational increment, varying after each run.
			rotateIncrement = baseRotateIncrement + (varianceParity * r.Next(rotateVariance));
			// Set the rotation back to 0.
			rotateByXDegrees = 0;
			// Create a value at random, up to 14.
			rotationMarker = r.Next(7) * 2;
			// Pick axes based on whether or not the binary number has a significant bit at the location (2nd bit, 4th bit, or 8th bit).
			rotateX = ((rotationMarker & 2) > 0) ? 1 : 0;
			rotateY = ((rotationMarker & 4) > 0) ? 1 : 0;
			rotateZ = ((rotationMarker & 8) > 0) ? 1 : 0;
			// Apply a random parity.
			rotateX *= (r.Next(2) == 1) ? 1 : -1;
			rotateY *= (r.Next(2) == 1) ? 1 : -1;
			rotateZ *= (r.Next(2) == 1) ? 1 : -1;
			// If the variables are the same after calculating everything out, treat that as a cue to build the effect faster (up to a continuous cycle if it repeats enough times).
			if (rotateX == previousX && rotateY == previousY && rotateZ == previousZ)
			{
				int maxIncrement = 15;
				rotateIncrement = Math.Min(maxIncrement, previousIncrement + 2);
				scheduleRedrawSleepCycles = delayCycles = (int)(previousDelayCycles * (1 - rotateIncrement / maxIncrement));
			}
			else
				scheduleRedrawSleepCycles = delayCycles = baseDelayCycles;
		}

		private void Credits_FormClosed(object sender, FormClosedEventArgs e)
		{
			parent.creditsWindow = null;
		}

		private void creditsRichTextBox_LinkClicked(object sender, LinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start(e.LinkText);
		}

		private void creditsRichTextBox_SelectionChanged(object sender, EventArgs e)
		{
			creditsRichTextBox.SelectionStart = creditsRichTextBox.Text.Length;
			programIconSimpleOpenGlControl.Focus();
		}

		private void creditsRichTextBox_TextChanged(object sender, EventArgs e)
		{
			programIconSimpleOpenGlControl.Focus();
		}

		private void Credits_Load(object sender, EventArgs e)
		{
			// Obtain the icon image.
			iconBitmap = global::FontTool.Properties.Resources.LogoImage;
			backgroundData = iconBitmap.LockBits(new Rectangle(0, 0, iconBitmap.Width, iconBitmap.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
			//
			afterInitialize();
		}

		private void scheduleRedrawTimer_Tick(object sender, EventArgs e)
		{
			// If there is no texture in the resource file, load the one in the Resources directory.
			if (backgroundData == null)
			{
				string path = SupportFunctions.Combine("images", "logo.png");
				if (File.Exists(path))
				{
					iconBitmap = (Bitmap)Image.FromFile(path);
					backgroundData = iconBitmap.LockBits(new Rectangle(0, 0, iconBitmap.Width, iconBitmap.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
				}
			}
			programIconSimpleOpenGlControl.Refresh();
		}

		private void afterInitialize()
		{
			//
			programIconSimpleOpenGlControl.MakeCurrent();
			// Set the pixel storage format.
			GL.PixelStore(PixelStoreParameter.UnpackAlignment, 1);
			// TODO: Temporary fix.
			//Gl.ReloadFunctions();
			// Prepare OpenGL texture bindings to fill out shader uniform requests.
			if (useFBOAndShader)
				GL.ActiveTexture(TextureUnit.Texture0);
			// Load the icon image into a texture.
			SupportFunctions.createTexture(iconBitmap.Width, iconBitmap.Height, out backgroundId, imageData: backgroundData.Scan0);
			// If the control variable is true, attempt to make the FBO (and subsequently use it).
			if (useFBOAndShader)
			{
				// Color texture.
				SupportFunctions.createTexture(frameBufferTextureSize, frameBufferTextureSize, out frameBufferColorTexture, textureFilter: TextureMinFilter.Linear);
				// Depth texture.
				SupportFunctions.createTexture(frameBufferTextureSize, frameBufferTextureSize, out frameBufferDepthTexture, imageByteOrder: PixelInternalFormat.DepthComponent, byteOrder: OpenTK.Graphics.OpenGL.PixelFormat.DepthComponent, textureFilter: TextureMinFilter.Linear);
				// Create frame buffer.
				GL.GenFramebuffers(1, out frameBufferObject);
				// Bind frame buffer object for setup.
				SupportFunctions.BindFrameBuffer(frameBufferId: (uint)frameBufferObject);
				// Attach the two textures to the frame buffer object.
				GL.FramebufferTexture2D(FramebufferTarget.DrawFramebuffer, FramebufferAttachment.ColorAttachment0, TextureTarget.Texture2D, frameBufferColorTexture, 0);
				GL.FramebufferTexture2D(FramebufferTarget.DrawFramebuffer, FramebufferAttachment.DepthAttachment, TextureTarget.Texture2D, frameBufferDepthTexture, 0);
				// Check to make sure that the FBO is drawable.
				useFBOAndShader = SupportFunctions.isFrameBufferComplete(frameBufferObject);
			}
			//
			if (useFBOAndShader)
				generateShaders();
			//
			if (shaderProgram > -1)
			{
				// Load the shader program into the slot.
				GL.UseProgram(shaderProgram);
				// Attach the texture.
				shaderTextureLocationUniformInput = GL.GetUniformLocation(shaderProgram, "textures[0]");
				if (shaderTextureLocationUniformInput >= 0)
					GL.Uniform1(shaderTextureLocationUniformInput, frameBufferColorTexture);
				// Attach the blur size.
				shaderBlurSizeUniformInput = GL.GetUniformLocation(shaderProgram, "blurSize");
				if (shaderBlurSizeUniformInput >= 0)
					GL.Uniform1(shaderBlurSizeUniformInput, shaderBlurSize);
			}
			//
			if (useFBOAndShader)
			{
				// Disable the shader.
				GL.UseProgram(0);
				// Disable rendering into the FBO.
				SupportFunctions.BindDefaultFrameBuffer();
			}
		}

		private void generateShaders()
		{
			shaderProgram = GL.CreateProgram();
			int vertex = GL.CreateShader(ShaderType.VertexShader);
			int fragment = GL.CreateShader(ShaderType.FragmentShader);
			String vertexSource = @"
void main(void)
{
	// Get the standard transform.
	gl_Position = ftransform();
	// Send the texture coordinate of TEXTURE0 to the fragment.
	gl_TexCoord[0] = gl_MultiTexCoord0;
	// Pass the glColor through to the fragment shader (in the form of gl_Color).
	gl_FrontColor = gl_Color;
}
			";
			String fragmentSource = @"
uniform sampler2D textures[1];
uniform float blurSize = " + shaderBlurSize + @";

void main(void)
{
	// Initialize the default color summation to 0 (the color black).
	vec4 sum = vec4(0.0);
	// Take nine samples, with the distance blurSize between them.
	sum += texture2D(textures[0], vec2(gl_TexCoord[0].x - 4.0 * blurSize, gl_TexCoord[0].y - 4.0 * blurSize)) * 0.05;
	sum += texture2D(textures[0], vec2(gl_TexCoord[0].x - 3.0 * blurSize, gl_TexCoord[0].y - 3.0 * blurSize)) * 0.09;
	sum += texture2D(textures[0], vec2(gl_TexCoord[0].x - 2.0 * blurSize, gl_TexCoord[0].y - 2.0 * blurSize)) * 0.12;
	sum += texture2D(textures[0], vec2(gl_TexCoord[0].x - blurSize, gl_TexCoord[0].y - blurSize)) * 0.15;
	sum += texture2D(textures[0], vec2(gl_TexCoord[0].x, gl_TexCoord[0].y)) * 0.16;
	sum += texture2D(textures[0], vec2(gl_TexCoord[0].x + blurSize, gl_TexCoord[0].y + blurSize)) * 0.15;
	sum += texture2D(textures[0], vec2(gl_TexCoord[0].x + 2.0 * blurSize, gl_TexCoord[0].y + 2.0 * blurSize)) * 0.12;
	sum += texture2D(textures[0], vec2(gl_TexCoord[0].x + 3.0 * blurSize, gl_TexCoord[0].y + 3.0 * blurSize)) * 0.09;
	sum += texture2D(textures[0], vec2(gl_TexCoord[0].x + 4.0 * blurSize, gl_TexCoord[0].y + 4.0 * blurSize)) * 0.05;
	//
	gl_FragColor = sum * gl_Color;
}
			";
			// Compile shaders.
			SupportFunctions.compileShader(vertex, vertexSource);
			SupportFunctions.compileShader(fragment, fragmentSource);
			// Attach shaders and link the program.
			GL.AttachShader(shaderProgram, fragment);
			GL.AttachShader(shaderProgram, vertex);
			GL.LinkProgram(shaderProgram);
			// Clean up the intermediate objects.
			if (fragment != 0)
				GL.DeleteShader(fragment);
			if (vertex != 0)
				GL.DeleteShader(vertex);
		}
	}
}