﻿using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

#pragma warning disable
namespace ColorControl {
	public partial class ColorControl : Form
	{
		//
		bool insideProjection = false;
		bool insideValueBySaturationBox = false;
		bool insideHueBar = false;

		//
		Rectangle projectionBox = new Rectangle(0, 0, 7, 7);
		RectangleF valueBySaturationBox = new RectangleF(0.7f, 0.25f, 5f, 6.5f);
		RectangleF hueBar = new RectangleF(6.25f, 0.25f, 0.51f, 6.5f);

		//
		SizeF colorCircleSize = new SizeF(10f, 10f);
		SizeF hueValueArrowSize = new SizeF(4f, 8f);

		// Hit-test variables.
		public bool InsideProjection
		{
			get { return insideProjection; }
			set { insideProjection = value; }
		}

		public bool InsideValueBySaturationBox
		{
			get { return insideValueBySaturationBox; }
			set { insideValueBySaturationBox = value; }
		}

		public bool InsideHueBar
		{
			get { return insideHueBar; }
			set { insideHueBar = value; }
		}

		// Location and size of drawn components in custom projection coordinates.
		public Rectangle ProjectionBox
		{
			get { return projectionBox; }
			set { projectionBox = value; }
		}

		public RectangleF HueBar
		{
			get { return hueBar; }
			set { hueBar = value; }
		}

		public RectangleF ValueBySaturationBox
		{
			get { return valueBySaturationBox; }
			set { valueBySaturationBox = value; }
		}

		// Location and size of drawn components in Windows coordinates.
		public Rectangle ProjectionBoxScreenCoordinates
		{
			get
			{
				return new Rectangle(simpleOpenGlControl.Location.X, simpleOpenGlControl.Location.Y, simpleOpenGlControl.Width, simpleOpenGlControl.Height);
			}
		}

		public RectangleF HueBarWindows
		{
			get
			{
				return new RectangleF(
					ProjectionBoxScreenCoordinates.Left + ProjectionBoxScreenCoordinates.Width * HueBar.Left / (float)ProjectionBox.Width,
					ProjectionBoxScreenCoordinates.Top + ProjectionBoxScreenCoordinates.Height * HueBar.Top / (float)ProjectionBox.Height,
					ProjectionBoxScreenCoordinates.Width * HueBar.Width / (float)ProjectionBox.Width,
					ProjectionBoxScreenCoordinates.Height * HueBar.Height / (float)ProjectionBox.Height
				);
			}
		}

		public RectangleF ValueBySaturationBoxWindows
		{
			get
			{
				return new RectangleF(
					ProjectionBoxScreenCoordinates.Left + ProjectionBoxScreenCoordinates.Width * ValueBySaturationBox.Left / (float)ProjectionBox.Width,
					ProjectionBoxScreenCoordinates.Top + ProjectionBoxScreenCoordinates.Height * ValueBySaturationBox.Top / (float)ProjectionBox.Height,
					ProjectionBoxScreenCoordinates.Width * ValueBySaturationBox.Width / (float)ProjectionBox.Width,
					ProjectionBoxScreenCoordinates.Height * ValueBySaturationBox.Height / (float)ProjectionBox.Height
				);
			}
		}

		// This list is top-down, relating to the logical drawing order. However, the first item in this list is equivalent to 359.999 saturation. The last item is equivalent to 0.001 saturation.
		List<Color> defaultHueList = new List<Color> {
			Color.Red,
			Color.Magenta,
			Color.Blue,
			Color.Cyan,
			Color.Lime,
			Color.Yellow,
			Color.Red
		};

		private void colorSelectMode_Resize(object sender, EventArgs e)
		{
			// Recalculate width/height by margins (in pixels): 30 from left, 22 from each other, 9 from the right, 8 from top, and 8 from bottom.
			int valueBySaturationLeft = 30, between = 22, hueBarRight = 9, allTop = 8, allBottom = 8;
			int availableWidthInPixels = ProjectionBoxScreenCoordinates.Width - valueBySaturationLeft - between - hueBarRight;
			float widthOfHueBarInPixels = availableWidthInPixels / 11.0f;
			float widthOfValueBySaturationBoxInPixels = availableWidthInPixels - widthOfHueBarInPixels;
			//
			int availableHeightInPixels = ProjectionBoxScreenCoordinates.Height - allTop - allBottom;
			float top = ProjectionBox.Height * allTop / (float)ProjectionBoxScreenCoordinates.Height, bottom = ProjectionBox.Height - ProjectionBox.Height * allBottom / (float)ProjectionBoxScreenCoordinates.Height;
			float allHeight = bottom - top;
			//Rectangle projectionBox = new Rectangle(0, 0, 7, 7);
			RectangleF valueBySaturationBoxNew = new RectangleF(ProjectionBox.Width * valueBySaturationLeft / (float)ProjectionBoxScreenCoordinates.Width, top, ProjectionBox.Width * widthOfValueBySaturationBoxInPixels / (float)ProjectionBoxScreenCoordinates.Width, allHeight);
			float widthOfHueBarInProjectionCoordinates = ProjectionBox.Width * widthOfHueBarInPixels / (float)ProjectionBoxScreenCoordinates.Width;
			RectangleF hueBarNew = new RectangleF(ProjectionBox.Width - ProjectionBox.Width * hueBarRight / (float)ProjectionBoxScreenCoordinates.Width - widthOfHueBarInProjectionCoordinates, top, widthOfHueBarInProjectionCoordinates, allHeight);
			//
			ValueBySaturationBox = valueBySaturationBoxNew;
			HueBar = hueBarNew;
		}

		#region OpenGL drawing methods.
		private void drawWindowsBorder(RectangleF rectangle)
		{
			int minorAdjustment = 1, majorAdjustment = 2;
			SupportFunctions.glPushColor(SystemColors.ControlDark);
			GL.Begin(BeginMode.LineStrip);
			{
				GL.Vertex2(rectangle.Right, rectangle.Top);
				GL.Vertex2(rectangle.Left, rectangle.Top);
				GL.Vertex2(rectangle.Left, rectangle.Bottom - minorAdjustment);
			}
			GL.End();
			SupportFunctions.glPushColor(SystemColors.ControlDarkDark);
			GL.Begin(BeginMode.LineStrip);
			{
				GL.Vertex2(rectangle.Right + minorAdjustment, rectangle.Top - minorAdjustment);
				GL.Vertex2(rectangle.Left - minorAdjustment, rectangle.Top - minorAdjustment);
				GL.Vertex2(rectangle.Left - minorAdjustment, rectangle.Bottom);
			}
			GL.End();
			//
			SupportFunctions.glPushColor(SystemColors.ControlLight);
			GL.Begin(BeginMode.LineStrip);
			{
				GL.Vertex2(rectangle.Left, rectangle.Bottom);
				GL.Vertex2(rectangle.Right, rectangle.Bottom);
				GL.Vertex2(rectangle.Right, rectangle.Top - minorAdjustment);
			}
			GL.End();
			SupportFunctions.glPushColor(SystemColors.ControlLightLight);
			GL.Begin(BeginMode.LineStrip);
			{
				GL.Vertex2(rectangle.Left - minorAdjustment, rectangle.Bottom + minorAdjustment);
				GL.Vertex2(rectangle.Right + minorAdjustment, rectangle.Bottom + minorAdjustment);
				GL.Vertex2(rectangle.Right + minorAdjustment, rectangle.Top - majorAdjustment);
			}
			GL.End();
		}

		double lightnessPercent = 1;
		private void drawValueBySaturationValueCircle()
		{
			GL.Enable(EnableCap.ScissorTest);
			GL.Scissor((int)Math.Floor(ValueBySaturationBoxWindows.Left - simpleOpenGlControl.Location.X + 1), (int)Math.Floor(ProjectionBoxScreenCoordinates.Bottom - ValueBySaturationBoxWindows.Bottom + 1), (int)ValueBySaturationBoxWindows.Width, (int)ValueBySaturationBoxWindows.Height);
			{
				GL.PushMatrix();
				{
					GL.Translate(ValueBySaturationBoxWindows.Left, ValueBySaturationBoxWindows.Top, 0);
					GL.Translate(saturationZeroToOne * ValueBySaturationBoxWindows.Width, ReverseValueZeroToOne * ValueBySaturationBoxWindows.Height, 0);
					GL.Scale(colorCircleSize.Width, colorCircleSize.Height, 1);
					// A triangle.
					SupportFunctions.setBlendMode("lines");
					SupportFunctions.drawCircle(outline: Color.FromArgb(255, (int)(lightnessPercent * 255), (int)(lightnessPercent * 255), (int)(lightnessPercent * 255)), r: 0.41);
				}
				GL.PopMatrix();
			}
			GL.Disable(EnableCap.ScissorTest);
		}

		private void drawHueValueArrow()
		{
			GL.PushMatrix();
			{
				GL.Translate(HueBarWindows.Left, HueBarWindows.Top, 0);
				GL.Translate(-hueValueArrowSize.Width, (360 - hueDegree) / 360.0 * HueBarWindows.Height, 0);
				GL.Scale(hueValueArrowSize.Width, hueValueArrowSize.Height, 1);
				// A triangle.
				SupportFunctions.setBlendMode("lines");
				GL.Begin(BeginMode.LineStrip);
				{
					SupportFunctions.glPushColor(Color.Black);
					GL.Vertex2(-0.5, 0.5);
					GL.Vertex2(0.5, 0);
					GL.Vertex2(-0.5, -0.5);
					GL.Vertex2(-0.5, 0.5);
				}
				GL.End();
			}
			GL.PopMatrix();
		}

		private Color getPrimaryHueColorFromHueDegree()
		{
			double percent = (360 - hueDegree) / 360.0;
			double doubleIndex = percent * (defaultHueList.Count - 1);
			int startIndex = (int)doubleIndex, endIndex = SupportFunctions.Clamp<int>((int)Math.Ceiling(doubleIndex), 0, defaultHueList.Count - 1);
			Color a = defaultHueList[startIndex];
			Color b = defaultHueList[endIndex];
			double bNot = doubleIndex - startIndex;
			double aNot = 1 - bNot;
			return Color.FromArgb(SupportFunctions.Clamp<int>((int)Math.Round(aNot * a.R + bNot * b.R), 0, 255), SupportFunctions.Clamp<int>((int)Math.Round(aNot * a.G + bNot * b.G), 0, 255), SupportFunctions.Clamp<int>((int)Math.Round(aNot * a.B + bNot * b.B), 0, 255));
		}

		private void drawHuePolygon(List<Color> colorList = null)
		{
			if (colorList == null)
				colorList = defaultHueList;
			//
			PointF startPoint = new PointF(0, 0.5f);
			float advancementIncrement = (1f / ((float)colorList.Count - 1));
			//
			GL.PushMatrix();
			{
				GL.Translate(HueBar.X + HueBar.Width / 2.0, ProjectionBox.Bottom - HueBar.Y - HueBar.Height / 2.0, 0);
				GL.Scale(HueBar.Width, HueBar.Height, 1);
				SupportFunctions.setBlendMode("default");
				GL.Begin(BeginMode.QuadStrip);
				{
					int i = 0;
					foreach (Color color in colorList)
					{
						PointF currentPoint = new PointF(startPoint.X, startPoint.Y - i * advancementIncrement);
						drawColorLine(color, startPoint: currentPoint, reverse: false);
						i++;
					}
				}
				GL.End();
			}
			GL.PopMatrix();
		}

		private static void drawColorLine(Color color, PointF? startPoint = null, bool reverse = false)
		{
			if (startPoint == null)
				startPoint = PointF.Empty;
			//
			SupportFunctions.glPushColor(color);
			if (!reverse)
			{
				GL.Vertex2(-0.5 + startPoint.Value.X, startPoint.Value.Y);
				GL.Vertex2(0.5 + startPoint.Value.X, startPoint.Value.Y);
			}
			else
			{
				GL.Vertex2(0.5 + startPoint.Value.X, startPoint.Value.Y);
				GL.Vertex2(-0.5 + startPoint.Value.X, startPoint.Value.Y);
			}
		}

		private void drawSaturationAndValuePolygon(Color? hue = null)
		{
			// Default to red.
			if (hue == null)
				hue = getPrimaryHueColorFromHueDegree();
			//
			GL.PushMatrix();
			{
				GL.Translate(ValueBySaturationBox.X + ValueBySaturationBox.Width / 2.0, ProjectionBox.Bottom - ValueBySaturationBox.Y - ValueBySaturationBox.Height / 2.0, 0);
				GL.Scale(ValueBySaturationBox.Width, ValueBySaturationBox.Height, 1);
				// Value Gradient (white at top, black at bottom).
				SupportFunctions.setBlendMode("default");
				GL.Begin(BeginMode.Quads);
				{
					SupportFunctions.glPushColor(Color.White);
					GL.Vertex2(-0.5, 0.5);
					GL.Vertex2(0.5, 0.5);
					SupportFunctions.glPushColor(Color.Black);
					GL.Vertex2(0.5, -0.5);
					GL.Vertex2(-0.5, -0.5);
				}
				GL.End();
				// Saturation Pass (multiplies value gradient by hue, approaching the value gradient as it approaches left).
				SupportFunctions.setBlendMode("multiply");
				GL.Begin(BeginMode.Quads);
				{
					SupportFunctions.glPushColor(Color.White);
					GL.Vertex2(-0.5, 0.5);
					SupportFunctions.glPushColor(hue);
					GL.Vertex2(0.5, 0.5);
					GL.Vertex2(0.5, -0.5);
					SupportFunctions.glPushColor(Color.White);
					GL.Vertex2(-0.5, -0.5);
				}
				GL.End();
			}
			GL.PopMatrix();
		}
		#endregion

		// Context menu handlers.
		private void storeColorForComparisonToolStripMenuItem_Click(object sender, EventArgs e)
		{
			PreviousColor = Color;
		}

		private void clearComparisonColorToolStripMenuItem_Click(object sender, EventArgs e)
		{
			PreviousColor = null;
		}

		// Drawing.
		private void colorSelectMode_Draw()
		{
			GL.PushAttrib(AttribMask.CurrentBit);
			{
				GL.PushMatrix();
				{
					// Draw color geometry.
					SupportFunctions.pushScreenCoordinateMatrix(ProjectionBox.Left, ProjectionBox.Right, ProjectionBox.Top, ProjectionBox.Bottom, -10, 10);
					{
						drawSaturationAndValuePolygon();
						drawHuePolygon();
					}
					SupportFunctions.pop_projection_matrix();
					// Push a pixel projection to draw cursors and 1px borders.
					SupportFunctions.pushScreenCoordinateMatrix(ProjectionBoxScreenCoordinates.Left, ProjectionBoxScreenCoordinates.Right, ProjectionBoxScreenCoordinates.Bottom, ProjectionBoxScreenCoordinates.Top);
					{
						drawHueValueArrow();
						drawValueBySaturationValueCircle();
						//
						drawWindowsBorder(ValueBySaturationBoxWindows);
						drawWindowsBorder(HueBarWindows);
					}
					SupportFunctions.pop_projection_matrix();
				}
				GL.PopMatrix();
			}
			GL.PopAttrib();
		}

		//
		private void colorSelectMode_MouseMove(MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				Shapes.Point point = new Shapes.Point(e.X + simpleOpenGlControl.Location.X, e.Y + simpleOpenGlControl.Location.Y);
				//
				if (InsideProjection)
				{
					if (InsideValueBySaturationBox)
					{
						// Move circle by moving value and saturation.
						ignoreTextChanges = true;
						{
							double saturation = SupportFunctions.Clamp<double>((point.X - ValueBySaturationBoxWindows.Left) / ValueBySaturationBoxWindows.Width, 0, 1);
							double value = SupportFunctions.Clamp<double>(1 - (point.Y - ValueBySaturationBoxWindows.Top) / ValueBySaturationBoxWindows.Height, 0, 1);
							SaturationZeroToOne = saturation;
							ValueZeroToOne = value;
						}
						ignoreTextChanges = false;
						updateColorSpaceComponents();
					}
					else if (InsideHueBar)
					{
						// Move arrow by setting the hue degree.
						int value = (int)SupportFunctions.Clamp<double>(360 - 360 * (point.Y - HueBarWindows.Top) / HueBarWindows.Height, 0.001, 359.999);
						HueDegree = value;
					}
				}
			}
		}

		private void colorSelectMode_MouseDown(MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				Shapes.Point point = new Shapes.Point(e.X + simpleOpenGlControl.Location.X, e.Y + simpleOpenGlControl.Location.Y);
				//
				insideProjection = SupportFunctions.PointInRectangle(ProjectionBoxScreenCoordinates, point);
				insideValueBySaturationBox = SupportFunctions.PointInRectangle(ValueBySaturationBoxWindows, point);
				insideHueBar = SupportFunctions.PointInRectangle(HueBarWindows, point);
				//
				colorSelectMode_MouseMove(e);
			}
		}

		private void colorSelectMode_MouseUp()
		{
			insideProjection = insideValueBySaturationBox = insideHueBar = false;
		}

		void colorSelectMode_MouseWheel(object sender, MouseEventArgs e) { }

		private void switchToPaletteModeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SelectedMode = Mode.Palette;
		}
	}
}