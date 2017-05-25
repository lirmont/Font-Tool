using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using OpenTK;

namespace FontTool
{
	public partial class SupportFunctions
	{
		public static void getCompatibleMajorMinorVersion(out int major, out int minor)
		{
			// Check for the version number. There are more specific version checks, but they can't be done on the older versions of OpenGL.
			String rawVersionString = GL.GetString(OpenTK.Graphics.OpenGL.StringName.Version);
			String[] parts = rawVersionString.Split('.');
			// By default, assume the OpenGL implementation is remarkably old.
			major = 0;
			minor = 9;
			// Try to parse out the major and minor numbers.
			if (parts.Length >= 2)
			{
				int.TryParse(parts[0], out major);
				int.TryParse(parts[1], out minor);
			}
		}

		public static int GetAcceptableSquaredImageSize(List<TracedCharacter> tracedCharacters, int padding = 0)
		{
			int pixels = 0;
			foreach (TracedCharacter character in tracedCharacters)
				pixels += character.EstimatedPixelRequirement;
			// Increase the square root by a small factor.
			int square = (int)(Math.Ceiling(Math.Sqrt(pixels) + padding));
			square = (int)SupportFunctions.NextPowerOfTwo((uint)square);
			return square;
		}

		public static uint NextPowerOfTwo(uint x)
		{
			x--;
			x |= (x >> 1);
			x |= (x >> 2);
			x |= (x >> 4);
			x |= (x >> 8);
			x |= (x >> 16);
			return (x+1);
		}

		/// <summary>
		/// Context-safe frame buffer extension call.
		/// </summary>
		public static void BindFrameBuffer(uint frameBufferId = 0)
		{
			try
			{
				GL.BindFramebuffer(FramebufferTarget.Framebuffer, frameBufferId);
			}
			catch (Exception) { }
		}

		/// <summary>
		/// Context-safe frame buffer reseting extension call.
		/// </summary>
		public static void BindDefaultFrameBuffer()
		{
			BindFrameBuffer(frameBufferId: 0);
		}

		/// <summary>
		/// Loads a default projection matrix, transforming it into a virtual 2d drawing context. Must be closed by either the pop_projection_matrix or by GL.PopMatrix().
		/// </summary>
		/// <param name="left">Left edge. Typically zero.</param>
		/// <param name="right">Right edge. Typically the width of the control.</param>
		/// <param name="bottom">Bottom edge. Typically the height of the control.</param>
		/// <param name="top">Top edge. Typically zero.</param>
		/// <param name="near">Negative depth. Typically negative two times the frame width.</param>
		/// <param name="far">Positive depth. Typically two times the frame width.</param>
		public static void pushScreenCoordinateMatrix(int left, int right, int bottom, int top, int near = -1, int far = 1)
		{
			// Push to projection matrix.
			GL.MatrixMode(MatrixMode.Projection); //GL.MatrixMode(GL._PROJECTION);
			GL.PushMatrix(); //GL.PushMatrix();
			{
				GL.LoadIdentity(); //GL.LoadIdentity();
				GL.Ortho(left, right, bottom, top, near, far); //GL.Ortho(left, right, bottom, top, near, far);
				// Push to modelview matrix.
				GL.MatrixMode(MatrixMode.Modelview); //GL.MatrixMode(GL._MODELVIEW);
				GL.PushMatrix(); //GL.PushMatrix();
				{
					GL.LoadIdentity(); //GL.LoadIdentity();
				}
			}
		}

		/// <summary>
		/// Pops the projection matrix made by the pushScreenCoordinateMatrix function. Synonymous with switching to the projection matrix and calling GL.PopMatrix().
		/// </summary>
		public static void pop_projection_matrix()
		{
			// Pop from modelview matrix.
			GL.MatrixMode(MatrixMode.Modelview); //GL.MatrixMode(GL._MODELVIEW);
			GL.PopMatrix(); //GL.PopMatrix();
			// Pop from projection matrix.
			GL.MatrixMode(MatrixMode.Projection); //GL.MatrixMode(GL._PROJECTION);
			GL.PopMatrix(); //GL.PopMatrix();
		}

		public static void compileShader(int shader, string source)
		{
			GL.ShaderSource(shader, source); //GL.ShaderSource(shader, 1, new string[] { source }, new int[] { source.Length });
			GL.CompileShader(shader); //GL.CompileShader(shader);
		}

		/// <summary>
		/// Checks whether or not a specified framebuffer object is usable.
		/// </summary>
		/// <param name="fbo">The integer returned by a call to the GL.GenFramebuffersEXT function.</param>
		/// <returns>Returns a boolean value indicating the state of a given framebuffer object.</returns>
		public static bool isFrameBufferComplete(int fbo)
		{
			switch (GL.CheckFramebufferStatus(FramebufferTarget.Framebuffer)) //switch (GL.CheckFramebufferStatusEXT(GL._FRAMEBUFFER_EXT))
			{
				case FramebufferErrorCode.FramebufferComplete:
					return true;
				default:
					return false;
			}
		}

		/// <summary>
		/// Creates and initializes an OpenGL texture.
		/// </summary>
		/// <param name="width"></param>
		/// <param name="height"></param>
		/// <param name="textureId"></param>
		/// <param name="imageByteOrder"></param>
		/// <param name="byteOrder"></param>
		/// <param name="byteType"></param>
		/// <param name="textureFilter"></param>
		/// <param name="textureRepeat"></param>
		/// <param name="imageData"></param>
		public static void createTexture(int width, int height, out int textureId, PixelInternalFormat imageByteOrder = PixelInternalFormat.Rgba, PixelFormat byteOrder = PixelFormat.Bgra, PixelType byteType = PixelType.UnsignedByte, TextureMinFilter textureFilter = TextureMinFilter.Nearest, TextureWrapMode textureRepeat = TextureWrapMode.Clamp, IntPtr? imageData = null)
		{
			//
			if (imageData == null)
				imageData = IntPtr.Zero;
			//
			GL.GenTextures(1, out textureId); //GL.GenTextures(1, out textureId);
			GL.BindTexture(TextureTarget.Texture2D, textureId); //GL.BindTexture(GL._TEXTURE_2D, textureId);
			GL.TexImage2D(TextureTarget.Texture2D, 0, imageByteOrder, width, height, 0, byteOrder, byteType, imageData.Value); //GL.TexImage2D(GL._TEXTURE_2D, 0, imageByteOrder, width, height, 0, byteOrder, byteType, imageData.Value);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)textureFilter); //GL.TexParameteri(GL._TEXTURE_2D, GL._TEXTURE_MIN_FILTER, textureFilter);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)textureFilter); //GL.TexParameteri(GL._TEXTURE_2D, GL._TEXTURE_MAG_FILTER, textureFilter);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)textureRepeat); //GL.TexParameteri(GL._TEXTURE_2D, GL._TEXTURE_WRAP_S, textureRepeat);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)textureRepeat); //GL.TexParameteri(GL._TEXTURE_2D, GL._TEXTURE_WRAP_T, textureRepeat);
		}

		public static void createOrRecreateTexture(int width, int height, ref int textureId, PixelInternalFormat imageByteOrder = PixelInternalFormat.Rgba, PixelFormat byteOrder = PixelFormat.Bgra, PixelType byteType = PixelType.UnsignedByte, TextureMinFilter textureFilter = TextureMinFilter.Nearest, TextureWrapMode textureRepeat = TextureWrapMode.Clamp, IntPtr? imageData = null)
		{
			// Delete the existing texture if there's something there.
			if (textureId > 0)
				GL.DeleteTextures(1, ref textureId); //GL.DeleteTextures(1, ref textureId);
			// Push the image into the context.
			createTexture(width, height, out textureId, imageByteOrder, byteOrder, byteType, textureFilter, textureRepeat, imageData);
		}

		public static void glGetError(string prefix = null)
		{
			ErrorCode code = GL.GetError();
			if (prefix != null)
				Console.Write(prefix + " ");
			Console.WriteLine(code.ToString());
		}

		/// <summary>
		/// Render a four-point shape with optional color, texture, and depth.
		/// </summary>
		/// <param name="left">Left edge. Typically negative.</param>
		/// <param name="right">Right edge. Typically positive.</param>
		/// <param name="bottom">Bottom edge. Typically negative.</param>
		/// <param name="top">Top edge. Typically positive.</param>
		/// <param name="depth">Screen depth. Postive is into the screen; negative is back from the screen.</param>
		/// <param name="depthChange">Screen depth change.</param>
		/// <param name="points">List of Shapes.Point objects that represent the polygon.</param>
		/// <param name="s">Left edge of texture. Typically 0.</param>
		/// <param name="t">Top edge of texture. Typically 0.</param>
		/// <param name="S">Right edge of texture. Typically 1.</param>
		/// <param name="T">Bottom edge of texture. Typically 1.</param>
		/// <param name="textureId">OpenGL texture ID.</param>
		/// <param name="textureCoordinates">List of Shapes.Point objects, matched with the provided points.</param>
		/// <param name="color">Single System.Drawing.Color to draw the polygon as.</param>
		/// <param name="colors">List of System.Drawing.Color objects, matched with provided points.</param>
		/// <param name="blendMode">String representation of the blending mode. Default: "overwrite".</param>
		/// <param name="drawingMode">OpenGL drawing mode. Default: GL._QUADS.</param>
		/// <param name="lineWidth">Line width, for use with line-related drawing modes.</param>
		public static void render(double left = 0, double right = 0, double bottom = 0, double top = 0, double depth = 0, double depthChange = 0, List<Shapes.Point> points = null, double s = 0, double t = 0, double S = 1, double T = 1, int textureId = 0, List<Shapes.Point> textureCoordinates = null, Color? color = null, Color[] colors = null, string blendMode = "overwrite", BeginMode drawingMode = BeginMode.Quads, float lineWidth = 1, int overrideFilter = 0, Vector2? textureScale = null)
		{
			// Assemble the 4 points that will be used.
			if (points == null)
			{
				points = new List<Shapes.Point> {
					new Shapes.Point(left, top, depth),
					new Shapes.Point(right, top, depth + depthChange),
					new Shapes.Point(right, bottom, depth + depthChange),
					new Shapes.Point(left, bottom, depth),
				};
			}
			// If there are things to draw, do so.
			if (points.Count > 0)
			{
				List<Shapes.Color> colorList = new List<Shapes.Color>();
				// Assemble the 4 colors that will be used.
				if (colors == null)
				{
					if (color == null)
						color = Color.White;
					// Initialize the color array to only include the default color.
					colors = new Color[4] { color.Value, color.Value, color.Value, color.Value };
					colorList = new List<Shapes.Color> {
						new Shapes.Color(color: color),
						new Shapes.Color(color: color),
						new Shapes.Color(color: color),
						new Shapes.Color(color: color)
					};
				}
				else
				{
					foreach (Color c in colors)
						colorList.Add(new Shapes.Color(color: c));
				}
				// Assemble the 4 texture coordinates that will be used.
				if (textureCoordinates == null)
					textureCoordinates = new List<Shapes.Point> {
						new Shapes.Point(s, t),
						new Shapes.Point(S, t),
						new Shapes.Point(S, T),
						new Shapes.Point(s, T)
					};
				//
				if (textureScale != null && textureCoordinates != null) {
					foreach (Shapes.Point point in textureCoordinates)
					{
						point.X *= textureScale.Value.X;
						point.Y *= textureScale.Value.Y;
					}
				}
				// If there's a texture, enable the texturing feature.
				if (textureId > 0)
					GL.Enable(EnableCap.Texture2D); //GL.Enable(GL._TEXTURE_2D);
				// If the mode is line-related, set the line width.
				if (drawingMode == BeginMode.LineLoop || drawingMode == BeginMode.Lines)
					GL.LineWidth(lineWidth);
				// Bind the texture regardless (zero is equivalent to unbinding).
				GL.BindTexture(TextureTarget.Texture2D, textureId);
				if (overrideFilter != 0)
				{
					GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, overrideFilter);
					GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, overrideFilter);
				}
				// Set the blend mode.
				SupportFunctions.setBlendMode(blendMode);
				// Render the primitive.
				GL.Begin(drawingMode);
				{
					for (int i = 0; i < points.Count; i++)
					{
						textureCoordinates[i].RenderAsTextureCoordinate();
						colorList[i].Render();
						points[i].Render();
					}
				}
				GL.End();
				// Disable the texturing feature.
				GL.Disable(EnableCap.Texture2D);
			}
		}


		/// <summary>
		/// Controls the color blending function.
		/// </summary>
		/// <param name="blend">Abstracted name of blending mode. Intentionally avoids using features past OpenGL 1.1.</param>
		public static void setBlendMode(string blend = "overwrite")
		{
			// ex: Red = (Red-Incoming * Red-Incoming Blend Factor + Red-Existing * Red-Existing Blend Factor)
			switch (blend)
			{
				default:
					GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
					GL.Hint(HintTarget.LineSmoothHint, HintMode.Nicest);
					break;
			}
		}

		public enum StipplePatterns { Flat = 0xFFFF, Dotted = 0xAAAA }
		public static void glControl_DrawHorizontalLine(GLControl control, float height, float zoomFactor = 1f, StipplePatterns stipple = StipplePatterns.Flat, Color? color = null)
		{
			if (color == null)
				color = Color.Blue;
			GL.Color3(color.Value);
			GL.PushMatrix();
			{
				GL.Scale(zoomFactor, zoomFactor, 1);
				GL.Translate(0, height, 0);
				GL.Enable(EnableCap.LineStipple);
				GL.LineStipple(1, (ushort)stipple);
				GL.Begin(BeginMode.Lines);
				{
					GL.Vertex2(0, 0);
					GL.Vertex2(control.ClientRectangle.Width / zoomFactor, 0);
				}
				GL.End();
				GL.Disable(EnableCap.LineStipple);
			}
			GL.PopMatrix();
		}

		public static void glControl_DrawVerticalLine(GLControl control, float width, float zoomFactor = 1f, StipplePatterns stipple = StipplePatterns.Flat)
		{
			GL.Color3(Color.Blue);
			GL.PushMatrix();
			{
				GL.Scale(zoomFactor, zoomFactor, 1);
				GL.Translate(width, 0, 0);
				GL.Enable(EnableCap.LineStipple);
				GL.LineStipple(1, (ushort)stipple);
				GL.Begin(BeginMode.Lines);
				{
					GL.Vertex2(0, 0);
					GL.Vertex2(0, control.ClientRectangle.Height / zoomFactor);
				}
				GL.End();
				GL.Disable(EnableCap.LineStipple);
			}
			GL.PopMatrix();
		}

		public static void glControl_DrawTexturedQuad(float x, float y, float width, float height, float zoomFactor = 1f, float textureWidth = 1f, float textureHeight = 1f, uint textureId = 0, bool flipTextureCoordinatesVertically = false, BeginMode mode = BeginMode.Quads, Color? color = null)
		{
			if (color == null)
				color = Color.White;
			//
			GL.Color4(color.Value);
			GL.PushMatrix();
			{
				GL.Scale(zoomFactor, zoomFactor, 1f);
				GL.Translate(x, y, 0f);
				GL.Scale(width, height, 1f);
				//
				GL.Enable(EnableCap.Texture2D);
				GL.BindTexture(TextureTarget.Texture2D, textureId);
				//
				GL.Enable(EnableCap.Blend);
				GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
				//
				if (!flipTextureCoordinatesVertically)
				{
					GL.Begin(mode);
					{
						GL.TexCoord2(0f, textureHeight);
						GL.Vertex2(0, 0);
						GL.TexCoord2(0f, 0f);
						GL.Vertex2(0, 1);
						GL.TexCoord2(textureWidth, 0f);
						GL.Vertex2(1, 1);
						GL.TexCoord2(textureWidth, textureHeight);
						GL.Vertex2(1, 0);
					}
					GL.End();
				}
				else
				{
					GL.Begin(mode);
					{
						GL.TexCoord2(0f, 0f);
						GL.Vertex2(0, 0);
						GL.TexCoord2(0f, textureHeight);
						GL.Vertex2(0, 1);
						GL.TexCoord2(textureWidth, textureHeight);
						GL.Vertex2(1, 1);
						GL.TexCoord2(textureWidth, 0f);
						GL.Vertex2(1, 0);
					}
					GL.End();
				}
				//
				GL.BindTexture(TextureTarget.Texture2D, 0);
				GL.Disable(EnableCap.Texture2D);
				//
				GL.Disable(EnableCap.Blend);
			}
			GL.PopMatrix();
		}
	}
}
