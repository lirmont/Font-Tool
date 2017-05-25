using System.Drawing;
using OpenTK.Graphics.OpenGL;

namespace FontTool
{
	public class Letter
	{
		private UnicodeCharacter character;
		private RectangleF rectangle;
		private DisplayList displayList;
		private Color geometryColor;
		private Color geometrySecondColor;
		private double advance;

		public UnicodeCharacter Character
		{
			get { return character; }
			set { character = value; }
		}

		public RectangleF Rectangle
		{
			get { return rectangle; }
			set { rectangle = value; }
		}

		public DisplayList DisplayList
		{
			get { return displayList; }
			set { displayList = value; }
		}

		public Color GeometryColor
		{
			get { return geometryColor; }
			set { geometryColor = value; }
		}

		public Color GeometrySecondColor
		{
			get { return geometrySecondColor; }
			set { geometrySecondColor = value; }
		}

		public double Advance
		{
			get { return advance; }
			set { advance = value; }
		}

		public Letter(RectangleF rectangle, DisplayList displayList, Color geometryColor, Color geometrySecondColor, double advance)
		{
			this.rectangle = rectangle;
			this.displayList = displayList;
			this.geometryColor = geometryColor;
			this.geometrySecondColor = geometrySecondColor;
			this.advance = advance;
		}

		public Letter(UnicodeCharacter character, RectangleF rectangle, DisplayList displayList, Color geometryColor, Color geometrySecondColor, double advance)
		{
			this.character = character;
			this.rectangle = rectangle;
			this.displayList = displayList;
			this.geometryColor = geometryColor;
			this.geometrySecondColor = geometrySecondColor;
			this.advance = advance;
		}

		public void Render(FontTool.VirtualKeyboard.VirtualKeyboardMode mode, float zoomFactor, float gutterY, OpenTK.GLControl control)
		{
			if (mode == FontTool.VirtualKeyboard.VirtualKeyboardMode.Images && DisplayList != null)
				drawGeometryWithTexture(zoomFactor, gutterY);
			else if (mode == FontTool.VirtualKeyboard.VirtualKeyboardMode.ImageAndPoints && DisplayList != null)
			{
				drawGeometryWithTexture(zoomFactor, gutterY);
				drawGeometryAsPoints(zoomFactor, gutterY);
			}
			else if (mode == FontTool.VirtualKeyboard.VirtualKeyboardMode.Geometry || mode == FontTool.VirtualKeyboard.VirtualKeyboardMode.Overlap)
			{
				SupportFunctions.glControl_DrawTexturedQuad(Rectangle.X, gutterY + Rectangle.Y, Rectangle.Width, Rectangle.Height, zoomFactor: zoomFactor, mode: BeginMode.LineLoop, color: GeometryColor);
				SupportFunctions.glControl_DrawTexturedQuad(Rectangle.X, gutterY + Rectangle.Y, Rectangle.Width, Rectangle.Height, zoomFactor: zoomFactor, mode: BeginMode.Quads, color: GeometrySecondColor);
			}
		}

		private void drawGeometryWithTexture(float zoomFactor, float gutterY)
		{
			GL.Color4(1f, 1f, 1f, 1f);
			// DISABLED CAP NOTE: Self-intersecting objects invariably fail the depth test. This results in missing geometry, even though it's generated correctly.
			bool depthTestActive = GL.IsEnabled(EnableCap.DepthTest);
			GL.Disable(EnableCap.DepthTest);
			GL.PushMatrix();
			{
				// Line the drawing up to the baseline.
				GL.Scale(zoomFactor, zoomFactor, zoomFactor);
				GL.Translate(0, gutterY, 0);
				GL.Enable(EnableCap.Blend);
				SupportFunctions.setBlendMode("overwrite");
				// Draw the traces.
				GL.Enable(EnableCap.Texture2D);
				{
					// Move the centered character object forward and up by half its dimensions.
					GL.Translate(DisplayList.Offset);
					// Z: rotate left/right, Y: forward/reverse.
					for (int i = 0; i < DisplayList.VertexBuffers.Count; i++)
					{
						VertexBufferOnGPU vertexBuffer = (VertexBufferOnGPU)DisplayList.VertexBuffers[i];
						ImageDescription imageDescription = DisplayList.ImageDescriptions[i];
						// Switch to texture matrix to set up texture scale.
						GL.MatrixMode(MatrixMode.Texture);
						GL.PushMatrix();
						{
							GL.LoadIdentity();
							GL.Scale(vertexBuffer.TextureScale);
							GL.Translate(vertexBuffer.TextureOffset);
							// Switch to model view to translate and show the letter model.
							GL.MatrixMode(MatrixMode.Modelview);
							{
								// Offset the vertex buffer so that it's locally centered.
								GL.Translate(vertexBuffer.Offset);
								// Render the triangles with the given texture.
								GL.BindTexture(TextureTarget.Texture2D, imageDescription.ContextId);
								{
									vertexBuffer.Render(BeginMode.Triangles);
								}
								GL.BindTexture(TextureTarget.Texture2D, 0);
								// Undo the previous local transformation.
								GL.Translate(-vertexBuffer.Offset);
							}
						}
						GL.MatrixMode(MatrixMode.Texture);
						GL.PopMatrix();
						// Switch back to model view matrix.
						GL.MatrixMode(MatrixMode.Modelview);
					}
					// Undo the previous, character-wide transformation.
					GL.Translate(-DisplayList.Offset);
				}
				GL.Disable(EnableCap.Texture2D);
				GL.Disable(EnableCap.Blend);
			}
			GL.PopMatrix();
			//inRenderDebuggingLinesPass(zoomFactor, gutterY);
			// Re-enable the depth test.
			if (depthTestActive)
				GL.Enable(EnableCap.DepthTest);
		}

		private void drawGeometryAsPoints(float zoomFactor, float gutterY)
		{
			GL.Color4(0, 0, 0, 0.6);
			GL.PointSize(0.9f + zoomFactor / 10f);
			GL.LineWidth(0.95f + zoomFactor / 50f);
			GL.Hint(HintTarget.PointSmoothHint, HintMode.Nicest);
			GL.PushMatrix();
			{
				// Line the drawing up to the baseline.
				GL.Scale(zoomFactor, zoomFactor, zoomFactor);
				GL.Translate(0, gutterY, 0);
				GL.Enable(EnableCap.LineStipple);
				GL.Enable(EnableCap.Blend);
				SupportFunctions.setBlendMode("overwrite");
				{
					// Move the centered character object forward and up by half its dimensions.
					GL.Translate(DisplayList.Offset);
					// Z: rotate left/right, Y: forward/reverse.
					for (int i = 0; i < DisplayList.VertexBuffers.Count; i++)
					{
						VertexBufferOnGPU vertexBuffer = (VertexBufferOnGPU)DisplayList.VertexBuffers[i];
						ImageDescription imageDescription = DisplayList.ImageDescriptions[i];
						// Offset the vertex buffer so that it's locally centered.
						GL.Translate(vertexBuffer.Offset);
						// Render the triangles as points (inner).
						GL.Color4(0, 0, 0, 0.6);
						vertexBuffer.Render(BeginMode.Points, start: 1, countPerStep: vertexBuffer.Length - 3, limit: vertexBuffer.Length - 2);
						// Last point (blue).
						GL.Color4(Color.FromArgb(255 - 40, Color.BlueViolet));
						vertexBuffer.Render(BeginMode.Points, start: vertexBuffer.Length - 1, countPerStep: 1);
						// First point (red).
						GL.Color4(Color.FromArgb(255 - 40, Color.IndianRed));
						vertexBuffer.Render(BeginMode.Points, start: 0, countPerStep: 1, limit: 1);
						// Render the triangles as lines.
						GL.LineStipple(1, getLineStipplePatternGivenPower(i));
						GL.Color4(0.25, 0.2, 0.2, 0.35);
						vertexBuffer.Render(BeginMode.LineLoop, start: 0, countPerStep: 3);
						// Undo the previous local transformation.
						GL.Translate(-vertexBuffer.Offset);
					}
					// Undo the previous, character-wide transformation.
					GL.Translate(-DisplayList.Offset);
				}
				GL.Disable(EnableCap.Blend);
				GL.Disable(EnableCap.LineStipple);
			}
			GL.PopMatrix();
			GL.Hint(HintTarget.PointSmoothHint, HintMode.DontCare);
			GL.LineWidth(1);
			GL.PointSize(1);
			GL.Color4(1f, 1f, 1f, 1f);
		}

		private static ushort getLineStipplePatternGivenPower(int power)
		{
			ushort value = 0;
			int minorPower = 0;
			if (power < 2)
			{
				value = System.Convert.ToUInt16("0000111100001111", 2);
				minorPower = 4;
			}
			else if (power >= 2 && power < 4)
			{
				value = System.Convert.ToUInt16("0110011001100110", 2);
				minorPower = 2;
			}
			else
				value = System.Convert.ToUInt16("0101010101010101", 2);
			value = (ushort)(value << (minorPower * power));
			return value;
		}

		private void inRenderDebuggingLinesPass(float zoomFactor, float gutterY)
		{
			// Debug.
			GL.PushMatrix();
			{
				// Line the drawing up to the baseline.
				GL.Scale(zoomFactor, zoomFactor, zoomFactor);
				GL.Translate(0, gutterY, 0);
				SupportFunctions.setBlendMode("overwrite");
				GL.Enable(EnableCap.Blend);
				GL.Enable(EnableCap.LineStipple);
				{
					// VIOLET: START OF DRAWING.
					GL.Color4(Color.Violet);
					GL.LineStipple(1, System.Convert.ToUInt16('P'));
					GL.Begin(BeginMode.Lines);
					{
						GL.Vertex2(-10, 0);
						GL.Vertex2(10, 0);
						GL.Vertex2(0, -10);
						GL.Vertex2(0, 10);
					}
					GL.End();
					// Move the centered character object forward and up by half its dimensions.
					GL.Translate(DisplayList.Offset);
					GL.LineStipple(1, System.Convert.ToUInt16('a'));
					// PALE GREEN: CENTER OF CHARACTER OBJECT.
					GL.Color4(Color.PaleGreen);
					GL.Begin(BeginMode.Lines);
					{
						GL.Vertex2(-10, 0);
						GL.Vertex2(10, 0);
						GL.Vertex2(0, -10);
						GL.Vertex2(0, 10);
					}
					GL.End();
					// Z: rotate left/right, Y: forward/reverse.
					for (int i = 0; i < DisplayList.VertexBuffers.Count; i++)
					{
						VertexBufferOnGPU vertexBuffer = (VertexBufferOnGPU)DisplayList.VertexBuffers[i];
						ImageDescription imageDescription = DisplayList.ImageDescriptions[i];
						// Offset the vertex buffer so that it's locally centered.
						GL.Translate(vertexBuffer.Offset);
						GL.LineStipple(1, System.Convert.ToUInt16('z'));
						// CRIMSON: CENTER OF VERTEX BUFFER. 1 OF MANY.
						GL.Color4(Color.Crimson);
						GL.Begin(BeginMode.Lines);
						{
							GL.Vertex2(-10, 0);
							GL.Vertex2(10, 0);
							GL.Vertex2(0, -10);
							GL.Vertex2(0, 10);
						}
						GL.End();
						// Undo the previous local transformation.
						GL.Translate(-vertexBuffer.Offset);
					}
					// Undo the previous, character-wide transformation.
					GL.Translate(-DisplayList.Offset);
				}
				GL.Disable(EnableCap.LineStipple);
				GL.Disable(EnableCap.Blend);
			}
			GL.PopMatrix();
			GL.Color4(Color.White);
		}
	}
}
