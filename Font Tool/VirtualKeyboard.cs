using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using OpenTK.Graphics.OpenGL;
using System.Threading;
using OpenTK;
using OpenTK.Graphics;
using System.Collections.Specialized;

namespace FontTool
{
	public partial class VirtualKeyboard : Form
	{
		//
		Main parent = null;

		public enum VirtualKeyboardMode { Images = 1, Geometry = 2, Overlap = 4, ImageAndPoints = 8 };
		//
		private uint backgroundId = 0;
		private float zoomFactor = 1f;
		// Only generate visual proxies for the character in this variable.
		private OffsetableUnicodeCharacterCollection characters = new OffsetableUnicodeCharacterCollection();
		//
		private DisplayListCollection geometryDisplayLists = new DisplayListCollection();
		private List<ulong> charactersInString = new List<ulong>();
		private List<Letter> letters = new List<Letter>();
		private List<KeyValuePair<RectangleF, Color>> intersections = new List<KeyValuePair<RectangleF, Color>>();

		private int? previousX = null;
		private int? previousY = null;
		private int locationX = 0;
		private int locationY = 0;
		private uint distanceFromEndOfString = 0;
		private uint lineCount = 1;
		private float cursorWidth = 1;

		public float ZoomFactor
		{
			get { return zoomFactor; }
			set
			{
				if (zoomFactor != value)
				{
					zoomFactor = Math.Max(1f, value);
					forceRedraw();
				}
			}
		}

		public float GutterX
		{
			get { return (float)gutterXNumericUpDown.Value; }
			set
			{
				gutterXNumericUpDown.Value = (decimal)value;
				forceRedraw();
			}
		}

		public float GutterY
		{
			get { return (float)gutterYNumericUpDown.Value; }
			set
			{
				gutterYNumericUpDown.Value = (decimal)value;
				forceRedraw();
			}
		}

		private bool NoSubComponentsHaveFocus
		{
			get
			{
				return (!unicodeBlockComboBox.Focused && !gutterXNumericUpDown.Focused && !gutterYNumericUpDown.Focused) ? true : false;
			}
		}

		private VirtualKeyboardMode ActiveMode {
			get {
				if (imageRadioButton.Checked)
					return VirtualKeyboardMode.Images;
				else if (imageAndPointsRadioButton.Checked)
					return VirtualKeyboardMode.ImageAndPoints;
				else if (geometryRadioButton.Checked)
					return VirtualKeyboardMode.Geometry;
				else if (overlapRadioButton.Checked)
					return VirtualKeyboardMode.Overlap;
				return VirtualKeyboardMode.Images;
			}
			set {
				bool changed = (value != ActiveMode) ? true : false;
				//
				if (value == VirtualKeyboardMode.Images)
					imageRadioButton.Checked = true;
				else if (value == VirtualKeyboardMode.ImageAndPoints)
					imageAndPointsRadioButton.Checked = true;
				else if (value == VirtualKeyboardMode.Geometry)
					geometryRadioButton.Checked = true;
				else if (value == VirtualKeyboardMode.Overlap)
					overlapRadioButton.Checked = true;
				//
				if (changed)
					forceRedraw();
			}
		}

		private DisplayListCollection GeometryDisplayLists
		{
			get { return geometryDisplayLists; }
			set { geometryDisplayLists = value; }
		}

		private List<ulong> CharactersInString
		{
			get { return charactersInString; }
			set {
				bool changed = charactersInString != value;
				charactersInString = value;
				//
				if (changed)
					remakeRectangleBoundariesAndIntersections();
			}
		}

		public List<Letter> Letters
		{
			get { return letters; }
			set { letters = value; }
		}

		public List<KeyValuePair<RectangleF, Color>> Intersections
		{
			get { return intersections; }
			set { intersections = value; }
		}

		public int LocationX
		{
			get { return locationX; }
			set
			{
				if (locationX != value)
				{
					locationX = Math.Min(0, value);
					forceRedraw();
				}
			}
		}

		public int LocationY
		{
			get { return locationY; }
			set
			{
				if (locationY != value)
				{
					int lower = (int)0;
					if (MultipleLines)
						if (LineHeight > 0)
							lower = (int)((lineCount - 1) * LineHeight);
						else
							lower = (int)((lineCount - 1) * LineHeight);
					locationY = Math.Min(lower, value);
					//
					forceRedraw();
				}
			}
		}

		private bool MultipleLines
		{
			get
			{
				return (lineCount > 1) ? true : false;
			}
		}

		private float LineHeight
		{
			get
			{
				if (parent != null && parent.ActiveFontInActiveSize != null)
					return parent.ActiveFontInActiveSize.TargetAscent + parent.ActiveFontInActiveSize.TargetDescent;
				else
					return 0;
			}
		}

		private int IndexInString
		{
			get
			{
				if (CharactersInString.Count > distanceFromEndOfString)
					return (int)(CharactersInString.Count - distanceFromEndOfString);
				else
					return 0;
			}
			set
			{
				if (CharactersInString.Count >= (int)value)
					distanceFromEndOfString = (uint)(CharactersInString.Count - value);
				forceRedraw();
			}
		}

		private int LineStartIndexInString
		{
			get
			{
				return getNearestNewLineToLeftOfIndex(IndexInString);
			}
		}

		private int LineEndIndexInString
		{
			get
			{
				return getNearestNewLineToRightOfIndex(LineStartIndexInString);
			}
		}

		private byte[] CharacterStringBytes
		{
			get
			{
				List<byte> characterBytes = new List<byte>();
				foreach (ulong code in CharactersInString)
					characterBytes.Add((byte)(char)code);
				return characterBytes.ToArray();
			}
		}

		private string UTF16Text
		{
			get
			{
				string s = "";
				foreach (ulong code in CharactersInString)
					s += (char)code;
				return s;
			}
		}

		public VirtualKeyboard(Main parent)
		{
			//
			this.parent = parent;
			//
			InitializeComponent();
			if (parent.ActiveFontInActiveSize != null)
			{
				GutterY = Math.Max(0, parent.ActiveFontInActiveSize.TargetDescent);
				GutterX = 5;
				cursorWidth = (int)Math.Max(1, Math.Min(2, parent.ActiveFontInActiveSize.DefaultWidth / 10));
			}
			glControl.MouseWheel += new MouseEventHandler(glControl_MouseWheel);
			blockPager.OnRangeUpdate += blockPager_OnRangeUpdate;
		}

		private int getNearestNewLineToLeftOfIndex(int index)
		{
			// Reminder: last index of is backwards.
			return (index > 0) ? Math.Max(0, charactersInString.LastIndexOf('\n', index - 1, index) + 1) : 0;
		}

		private int getNearestNewLineToRightOfIndex(int start)
		{
			int index = charactersInString.IndexOf('\n', start, CharactersInString.Count - start);
			if (index >= 0)
				return index;
			else
				return CharactersInString.Count;
		}

		private void VirtualKeyboard_Load(object sender, EventArgs e)
		{
			// Attach images.
			backspaceGlyphPictureBox.GlyphImageFile = Properties.Resources.backspace;
			spaceBarGlyphPictureBox.GlyphImageFile = Properties.Resources.space;
			leftGlyphPictureBox.GlyphImageFile = Properties.Resources.left;
			upGlyphPictureBox.GlyphImageFile = Properties.Resources.up;
			downGlyphPictureBox.GlyphImageFile = Properties.Resources.down;
			rightGlyphPictureBox.GlyphImageFile = Properties.Resources.right;
			// Make the combo box at this point so that the resulting events happen after the form's handle is made.
			SupportFunctions.ComposeUnicodeBlockComboBox(unicodeBlockComboBox);
		}

		private void forceRedraw()
		{
			if (glControl != null && glControl.IsHandleCreated)
				glControl.Invalidate();
		}

		private void glControl_Load(object sender, EventArgs e)
		{
			//
			GLControl control = (GLControl)sender;
			control.MakeCurrent();
			//
			backgroundId = Texture.LoadTexture(Properties.Resources.transparent_background);
			//
			performLoadSavedStringOnString();
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
			// Prepare to track and use cursor location.
			int cursorIndex = IndexInString;
			int currentIndex = 0;
			bool cursorHasBeenDrawn = false;
			int gutterX = (int)GutterX;
			int gutterY = (int)GutterY;
			// DISABLED CAP NOTE: Self-intersecting objects invariably fail the depth test. This results in missing geometry, even though it's generated correctly.
			GL.Disable(EnableCap.DepthTest);
			GL.ClearColor(new Color4(1f, 1f, 1f, 0f));
			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
			//
			GL.MatrixMode(MatrixMode.Modelview);
			{
				GL.LoadIdentity();
				GL.Ortho(control.ClientRectangle.Left, control.ClientRectangle.Right, control.ClientRectangle.Top, control.ClientRectangle.Bottom, -1000, 1000);
				{
					GL.PushMatrix();
					{
						float zoomedControlWidth = control.ClientRectangle.Width / zoomFactor;
						float zoomedControlHeight = control.ClientRectangle.Height / zoomFactor;
						// Draw background.
						GL.Translate(0, 0, -0.4);
						SupportFunctions.glControl_DrawTexturedQuad(0f, 0f, zoomedControlWidth, zoomedControlHeight, zoomFactor, textureWidth: zoomedControlWidth * 0.5f, textureHeight: zoomedControlHeight * 0.5f, textureId: backgroundId, flipTextureCoordinatesVertically: true);
						// Draw baseline(s).
						GL.Translate(0, 0, 0.2);
						double lineHeight = LineHeight;
						drawBaselines(lineHeight, control);
						// Draw vertical gutter boundary.
						GL.Translate(zoomFactor * LocationX, 0, 0.2);
						SupportFunctions.glControl_DrawVerticalLine(control, gutterX, zoomFactor);
						GL.Translate(0, zoomFactor * LocationY, 0.2);
						// Track location.
						double totalX = 0, totalZ = 0, totalY = 0;
						GL.Translate(zoomFactor * gutterX, 0, 0);
						foreach (Letter letter in Letters)
						{
							// Draw letter glyph.
							if (letter.DisplayList.Id == '\n')
							{
								// Draw cursor.
								if (currentIndex == cursorIndex)
								{
									drawCursor();
									cursorHasBeenDrawn = true;
								}
								// Move to new line.
								GL.Translate(-totalX, -zoomFactor * lineHeight, 0);
								totalY += lineHeight;
								totalX = 0;
							}
							else
							{
								// Running depth adjustment to hint at drawing order.
								GL.Translate(0, 0, totalZ);
								{
									// Draw letter.
									letter.Render(ActiveMode, zoomFactor, gutterY, control);
									// Draw cursor.
									if (currentIndex == cursorIndex)
									{
										drawCursor();
										cursorHasBeenDrawn = true;
									}
								}
								GL.Translate(zoomFactor * letter.Advance, 0, -totalZ);
								totalX += zoomFactor * letter.Advance;
								totalZ += 0.00001;
							}
							// Update current index.
							currentIndex += 1;
						}
						// Fallback to cursor at end of text.
						if (!cursorHasBeenDrawn)
							drawCursor();
						//
						GL.Translate(-totalX, 0, 0);
						//
						if (ActiveMode == VirtualKeyboardMode.Overlap)
						{
							GL.Translate(0, gutterY * zoomFactor + zoomFactor * totalY, totalZ);
							foreach (KeyValuePair<RectangleF, Color> intersection in Intersections)
							{
								if (intersection.Key != RectangleF.Empty)
									SupportFunctions.glControl_DrawTexturedQuad(intersection.Key.X, intersection.Key.Y, intersection.Key.Width, intersection.Key.Height, zoomFactor: zoomFactor, mode: BeginMode.Quads, color: intersection.Value);
								else if (intersection.Key == RectangleF.Empty && intersection.Value == Color.Empty)
								{
									// New line.
									GL.Translate(0, -zoomFactor * lineHeight, 0);
								}
							}
							GL.Translate(0, 0, -totalZ);
						}
						GL.Translate(-gutterX, 0, 0);
					}
					GL.PopMatrix();
				}
			}
			control.SwapBuffers();
		}

		private void drawCursor()
		{
			if (LineHeight > 0)
				SupportFunctions.glControl_DrawTexturedQuad(0, GutterY + 0, cursorWidth, parent.ActiveFontInActiveSize.TargetAscent, zoomFactor: zoomFactor, color: Color.Black);
			else
				SupportFunctions.glControl_DrawTexturedQuad(0, GutterY + 0, cursorWidth, (int)Math.Ceiling(parent.ActiveFontInActiveSize.Size), zoomFactor: zoomFactor, color: Color.Black);
		}

		private double drawBaselines(double lineHeight, GLControl control)
		{
			uint lineIterations = (lineHeight != 0) ? lineCount : 1;
			for (uint lineIndex = 0; lineIndex < lineCount; lineIndex++)
			{
				GL.Translate(0, zoomFactor * (LocationY - lineIndex * lineHeight), 0);
				{
					SupportFunctions.glControl_DrawHorizontalLine(control, GutterY, zoomFactor);
				}
				GL.Translate(0, -zoomFactor * (LocationY - lineIndex * lineHeight), 0);
			}
			return lineHeight;
		}

		private DisplayList createDisplayListForTracedCharacter(TracedCharacter tracedCharacter)
		{
			// Send it all back.
			return new DisplayList(tracedCharacter, pushToGraphicsContext: true, previewImage: SupportFunctions.GetClonedBitmap(tracedCharacter.PreviewImage), oglConfiguration: parent.OpenGLConfiguration);
		}

		private void remakeRectangleBoundariesAndIntersections(bool forceRedraw = true)
		{
			List<Letter> letterRectangles = remakeRectangleBoundaries();
			List<KeyValuePair<RectangleF, Color>> letterIntersections = remakeIntersections(letterRectangles);
			//
			Letters = letterRectangles;
			Intersections = letterIntersections;
			//
			if (forceRedraw)
				this.forceRedraw();
		}

		private List<Letter> remakeRectangleBoundaries()
		{
			List<Letter> letterRectangles = new List<Letter>();
			//
			int iteration = 0;
			foreach (ulong id in CharactersInString)
			{
				if (id != '\n')
				{
					Letter letter = generateBoundaryForCharacter(id, iteration);
					letterRectangles.Add(letter);
				}
				else
					letterRectangles.Add(
						new Letter(
							RectangleF.Empty,
							new DisplayList('\n', Vector3d.Zero),
							Color.Empty,
							Color.Empty,
							0
						)
					);
				//
				iteration++;
			}
			return letterRectangles;
		}

		private Letter generateBoundaryForCharacter(ulong id, int iteration)
		{
			//
			cacheLetterTraces(id);
			//
			if (GeometryDisplayLists.Contains(id))
			{
				//
				DisplayList displayList = GeometryDisplayLists[id];
				DerivedCharacter character = displayList.Character;
				//
				if (displayList != null)
				{
					float x = (displayList.Minimum != null) ? (float)displayList.Minimum.Value.X : 0;
					float y = (displayList.Minimum != null) ? (float)displayList.Minimum.Value.Y : 0;
					RectangleF rectangle = new RectangleF(character.FinalOffset.OffsetX + x, 0 + character.FinalOffset.OffsetY + y, (float)displayList.Dimensions.X, (float)displayList.Dimensions.Y);
					Color uniqueColor = Color.FromArgb(-(65355 / 2 / CharactersInString.Count) * iteration - (65355 / 2));
					Letter letter = new Letter(character, rectangle, displayList, uniqueColor, Color.FromArgb(80, Color.DarkGray), character.FinalOffset.OffsetWidth);
					return letter;
				}
			}
			double defaultAdvance = (parent.ActiveFontInActiveSize != null) ? parent.ActiveFontInActiveSize.DefaultWidth : 6;
			return new Letter(
				UnicodeBlocks.GetCharacterById(id),
				RectangleF.Empty,
				new DisplayList(id, Vector3d.Zero),
				Color.White,
				Color.FromArgb(80, Color.DarkGray),
				defaultAdvance
			);
		}

		private static List<KeyValuePair<RectangleF, Color>> remakeIntersections(List<Letter> letterRectangles)
		{
			List<KeyValuePair<RectangleF, Color>> letterIntersections = new List<KeyValuePair<RectangleF, Color>>();
			float advance = 0;
			for (int i = 0; i + 1 < letterRectangles.Count; i++)
			{
				Letter recordA = letterRectangles[i], recordB = letterRectangles[i + 1];
				RectangleF A = recordA.Rectangle, B = recordB.Rectangle;
				//
				if (recordA.DisplayList.Id == '\n')
				{
					advance = 0;
					letterIntersections.Add(new KeyValuePair<RectangleF, Color>(RectangleF.Empty, Color.Empty));
				}
				else
				{
					float nextAdvance = (float)recordA.Advance;
					A.Offset(advance, 0);
					B.Offset(advance + nextAdvance, 0);
					//
					if (recordB.DisplayList.Id != '\n' && A.IntersectsWith(B))
					{
						A.Intersect(B);
						RectangleF rectangle = A;
						letterIntersections.Add(new KeyValuePair<RectangleF, Color>(A, Color.DarkRed));
					}
					else
						letterIntersections.Add(new KeyValuePair<RectangleF, Color>(RectangleF.Empty, Color.DarkRed));
					//
					advance += nextAdvance;
				}
			}
			return letterIntersections;
		}

		private void box_MouseUp(object sender, EventArgs e)
		{
			GlyphPictureBox box = (GlyphPictureBox)sender;
			if (box != null)
				addCharacterToString(box.Id);
		}

		private void cacheLetterTraces(ulong id)
		{
			if (!GeometryDisplayLists.Contains(id))
			{
				TracedCharacter tracedCharacter = generateModeAwareTracedCharacterOutOfActiveFont(id);
				if (tracedCharacter != null)
					GeometryDisplayLists.Add(createDisplayListForTracedCharacter(tracedCharacter));
			}
		}

		private void addCharacterToString(char character)
		{
			addCharacterToString((ulong)character);
		}

		private void addCharacterToString(ulong id)
		{
			if (glControl != null && glControl.IsHandleCreated)
			{
				glControl.MakeCurrent();
				int oldIndex = IndexInString;
				bool wasBlank = CharactersInString.Count == 0;
				// Add character reference (id) to ordered list of characters in string.
				CharactersInString.Insert(IndexInString, id);
				//
				IndexInString = oldIndex;
				if (IndexInString < Letters.Count)
					Letters.Insert(IndexInString, generateBoundaryForCharacter(id, IndexInString));
				else
					Letters.Add(generateBoundaryForCharacter(id, IndexInString));
				//
				Intersections = remakeIntersections(Letters);
				// Increase the line count.
				if (id == '\n')
					lineCount += 1;
				//
				IndexInString += 1;
				//
				forceRedraw();
			}
		}

		private void box_MouseEnter(object sender, EventArgs e)
		{
			GlyphPictureBox box = (GlyphPictureBox)sender;
			if (box != null && box.UnicodeName != null)
				glyphToolTip.SetToolTip(box, box.UnicodeName);
		}

		private void unicodeBlockComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			Application.DoEvents();
			//
			ComboBox control = (ComboBox)sender;
			if (control.SelectedItem != null)
			{
				string unicodeBlock = control.SelectedItem.ToString();
				blockPager.BlockName = unicodeBlock;
				UnicodeBlock block = UnicodeBlocks.GetBlockByName(unicodeBlock);
				blockPager.List = block.PrintableCharacters;
			}
		}

		private void blockPager_OnRangeUpdate(object sender, List<UnicodeCharacter> range)
		{
			Thread doLayout = new Thread(new ThreadStart(delegate()
			{
				doCharacterGlyphBoxLayoutGivenUnicodeCharacters(range);
			}));
			doLayout.Name = "Do Unicode Glyph Layout";
			doLayout.Start();
		}

		private void doCharacterGlyphBoxLayoutGivenUnicodeCharacters(List<UnicodeCharacter> theseCharacters)
		{
			if (this.IsHandleCreated)
			{
				this.Invoke((MethodInvoker)delegate()
				{
					characters.Clear();
					SupportFunctions.AddProcessedCharactersToCharacterCollection(characters, theseCharacters, characterProvider: parent.getModeAwareCharacterOutOfActiveFont);
					createGlyphVisualProxies();
					//
					Application.DoEvents();
					//
					foreach (UnicodeCharacter character in theseCharacters)
					{
						synchronizeGlyphBoxWithActiveFont(character.Id);
						// Update with the image of the character as soon as it's created.
						Application.DoEvents();
					}
				});
			}
		}

		private void synchronizeGlyphBoxWithActiveFont(ulong key, GlyphPictureBox box = null)
		{
			if (box == null)
				box = getDisplayedGlyphPictureBoxById(key);
			if (box != null)
				box.TracedCharacter = generateModeAwareTracedCharacterOutOfActiveFont(key);
		}

		private TracedCharacter generateModeAwareTracedCharacterOutOfActiveFont(ulong id, bool skipDetailedTracing = false)
		{
			// Favor an effected version. Fallback to a base image.
			return parent.generateEffectedTracedCharacterOutOfActiveFont(id, skipDetailedTracing: skipDetailedTracing) ?? parent.generateBaseTracedCharacterOutOfActiveFont(id);
		}

		private GlyphPictureBox getDisplayedGlyphPictureBoxById(ulong key)
		{
			return SupportFunctions.GetDisplayedGlyphPictureBoxById(key, flowLayoutPanel);
		}

		private void createGlyphVisualProxies()
		{
			if (this.IsHandleCreated)
			{
				this.Invoke((MethodInvoker)delegate()
				{
					FlowLayoutPanel visualProxiesParentContainer = SupportFunctions.CreateFlowLayoutPanelForVisualProxies(contextMenuStrip: null);
					SupportFunctions.CreateAndPlaceCharacterGlyphBoxes(visualProxiesParentContainer, characterCollection: characters, contextMenuStrip: null, mouseDown: box_MouseUp, click: null, saveImage: null, mouseEnter: box_MouseEnter);
					// Replace the flow layout panel.
					keyboardTableLayoutPanel.SuspendLayout();
					{
						if (!flowLayoutPanel.IsDisposed)
							flowLayoutPanel.Dispose();
						flowLayoutPanel = visualProxiesParentContainer;
						//
						keyboardTableLayoutPanel.Controls.Add(flowLayoutPanel, 0, 1);
						//
						flowLayoutPanel.Show();
					}
					keyboardTableLayoutPanel.ResumeLayout();
				});
			}
		}

		private void splitContainer_Paint(object sender, PaintEventArgs e)
		{
			SplitContainer s = (SplitContainer)sender;
			if (s != null)
			{
				Point pointA = Point.Empty, pointB = Point.Empty;
				Point pointC = Point.Empty, pointD = Point.Empty;
				int padding = 5;
				Point verticalOffset = new Point(0, 1);
				Point horizontalOffset = new Point(1, 0);
				if (s.Orientation == Orientation.Vertical)
				{
					pointA = new Point(s.SplitterDistance, padding);
					pointB = new Point(pointA.X, s.Height - padding);
					pointC = pointA;
					pointD = pointB;
					pointC.Offset(horizontalOffset);
					pointD.Offset(horizontalOffset);
				}
				else {
					pointA = new Point(padding, s.SplitterDistance);
					pointB = new Point(s.Width - padding, pointA.Y);
					pointC = pointA;
					pointD = pointB;
					pointC.Offset(verticalOffset);
					pointD.Offset(verticalOffset);
				}
				e.Graphics.DrawLine(SystemPens.ControlDark, pointA, pointB);
				e.Graphics.DrawLine(SystemPens.ControlLightLight, pointC, pointD);
			}
		}

		private void splitContainer_SplitterMoved(object sender, SplitterEventArgs e)
		{
			flowLayoutPanel.Focus();
		}

		private void VirtualKeyboard_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (NoSubComponentsHaveFocus)
				addCharacterToString(e.KeyChar);
		}

		private void performBackspaceOnString()
		{
			deleteCharacterAtIndex(IndexInString - 1);
		}

		private void performDeleteOnString()
		{
			deleteCharacterAtIndex(IndexInString);
		}

		private void deleteCharacterAtIndex(int indexToDelete)
		{
			if (indexToDelete >= 0 && indexToDelete < CharactersInString.Count && CharactersInString.Count > 0)
			{
				int actualIndex = indexToDelete;
				// Remove line.
				if (charactersInString[indexToDelete] == '\n')
					lineCount -= 1;
				//
				CharactersInString.RemoveAt(indexToDelete);
				indexToDelete = actualIndex;
				Letters.RemoveAt(indexToDelete);
				IndexInString = actualIndex;
				Intersections = remakeIntersections(Letters);
				forceRedraw();
			}
		}

		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			if (NoSubComponentsHaveFocus)
			{
				if (keyData == (Keys.Control | Keys.V))
				{
					performDefaultPasteOnString();
					return true;
				} else if (keyData == (Keys.Control | Keys.C))
				{
					performDefaultCopyOnString();
					return true;
				}
				else if (keyData == (Keys.Control | Keys.S))
				{
					performSaveOnString();
					return true;
				}
				else if (keyData == Keys.Left)
				{
					performLeftOnString();
					return true;
				}
				else if (keyData == Keys.Right)
				{
					performRightOnString();
					return true;
				}
				else if (keyData == Keys.Up)
				{
					performUpOnString();
					return true;
				}
				else if (keyData == Keys.Down)
				{
					performDownOnString();
					return true;
				}
				else if (keyData == Keys.Delete)
				{
					performDeleteOnString();
					return true;
				}
				else if (keyData == Keys.End)
				{
					IndexInString = LineEndIndexInString;
					return true;
				}
				else if (keyData == Keys.Home)
				{
					IndexInString = LineStartIndexInString;
					return true;
				}
				else if (keyData == Keys.Space)
				{
					performSpaceOnString();
					return true;
				}
				else if (keyData == Keys.Back)
				{
					performBackspaceOnString();
					return true;
				}
				else if (keyData == Keys.Enter)
				{
					addCharacterToString('\n');
					return true;
				}
			}
			return base.ProcessCmdKey(ref msg, keyData);
		}

		private void performLoadSavedStringOnString()
		{
			System.Collections.Specialized.StringCollection collection = Properties.Settings.Default.storedProofingText;
			List<ulong> newList = new List<ulong>();
			if (collection != null && collection.Count > 0)
			{
				foreach (string character in collection)
				{
					ulong thisCharacter = Convert.ToUInt64(character, 16);
					if (thisCharacter == '\n')
						lineCount += 1;
					newList.Add(thisCharacter);
				}
			}
			CharactersInString = newList;
		}

		private void performSaveOnString()
		{
			System.Collections.Specialized.StringCollection collection = new StringCollection();
			if (CharactersInString.Count > 0)
			{
				foreach (ulong character in CharactersInString)
					collection.Add(string.Format("{0:X}", character));
			}
			Properties.Settings.Default.storedProofingText = collection;
			Properties.Settings.Default.Save();
		}

		private void performDefaultCopyOnString()
		{
			Clipboard.SetText(UTF16Text, TextDataFormat.UnicodeText);
		}

		private void performDefaultPasteOnString()
		{
			string s = Clipboard.GetText(TextDataFormat.UnicodeText);
			foreach (char c in s)
				addCharacterToString(c);
		}

		private void performSpaceOnString()
		{
			addCharacterToString(' ');
		}

		private void performDownOnString()
		{
			int start = LineStartIndexInString;
			int end = LineEndIndexInString;
			if (end < charactersInString.Count - 1)
			{
				int indexOfStartOfNextLine = getNearestNewLineToRightOfIndex(end) + 1;
				int indexOfEndOfNextLine = getNearestNewLineToRightOfIndex(indexOfStartOfNextLine);
				int lengthOfNextLine = indexOfEndOfNextLine - indexOfStartOfNextLine;
				int distanceFromCurrentLine = IndexInString - start;
				IndexInString = Math.Min(indexOfStartOfNextLine + lengthOfNextLine, indexOfStartOfNextLine + distanceFromCurrentLine);
			} else 
				IndexInString = charactersInString.Count;
		}

		private void performUpOnString()
		{
			int start = LineStartIndexInString;
			if (start > 0)
			{
				int indexOfStartOfPreviousLine = getNearestNewLineToLeftOfIndex(start - 1);
				int distanceToNextLine = start - indexOfStartOfPreviousLine - 1;
				int distanceFromCurrentLine = IndexInString - start;
				IndexInString = Math.Min(indexOfStartOfPreviousLine + distanceToNextLine, indexOfStartOfPreviousLine + distanceFromCurrentLine);
			} else
				IndexInString = 0;
		}

		private void performRightOnString()
		{
			IndexInString += 1;
		}

		private void performLeftOnString()
		{
			IndexInString -= 1;
		}

		private void backspaceGlyphPictureBox_Click(object sender, EventArgs e)
		{
			performBackspaceOnString();
		}

		private void spaceBarGlyphPictureBox_Click(object sender, EventArgs e)
		{
			performSpaceOnString();
		}

		private void leftGlyphPictureBox_Click(object sender, EventArgs e)
		{
			performLeftOnString();
		}

		private void upGlyphPictureBox_Click(object sender, EventArgs e)
		{
			performUpOnString();
		}

		private void downGlyphPictureBox_Click(object sender, EventArgs e)
		{
			performDownOnString();
		}

		private void rightGlyphPictureBox_Click(object sender, EventArgs e)
		{
			performRightOnString();
		}

		private void glControl_MouseEnter(object sender, EventArgs e)
		{
			GLControl control = (GLControl)sender;
			if (control != null)
				control.Focus();
		}

		private void glControl_MouseMove(object sender, MouseEventArgs e)
		{
			GLControl control = (GLControl)sender;
			if (control != null && e.Button == System.Windows.Forms.MouseButtons.Middle)
			{
				if (previousX == null)
					previousX = e.X;
				if (previousY == null)
					previousY = e.Y;
				LocationX += previousX.Value - e.X;
				LocationY -= previousY.Value - e.Y;
				previousX = e.X;
				previousY = e.Y;
			}
		}

		private void glControl_MouseUp(object sender, MouseEventArgs e)
		{
			previousX = null;
			previousY = null;
		}

		private  void glControl_MouseWheel(object sender, MouseEventArgs e)
		{
			GLControl control = (GLControl)sender;
			if (control != null)
				ZoomFactor += Math.Sign(e.Delta) * 1f;
		}
	}
}
