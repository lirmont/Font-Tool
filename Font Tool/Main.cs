using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

namespace FontTool
{
    public partial class Main : Form
	{
		/*
		 * Window Management
		 */
		public Credits creditsWindow = null;

		/*
		 * Font Management
		 */
		private BitmapFont activeFont = null;
		// Picks a sub-section of the active font.
		private string activeColor = "Default";
		private float? activeSize = null;
		private string activeVariant = null;
		// Only generate visual proxies for the characters in this variable.
		private OffsetableUnicodeCharacterCollection characters = new OffsetableUnicodeCharacterCollection();
		//
		private bool applicationIsSettingValues = false;

		public OpenGLConfiguration OpenGLConfiguration
		{
			get {
				return (characterDisplay != null) ? characterDisplay.OpenGLConfiguration : new OpenGLConfiguration();
			}
		}

		public BitmapFont ActiveFont
		{
			get { return activeFont; }
			set {
				BitmapFont previousFont = activeFont;
				bool changed = (value != activeFont);
				activeFont = value;
				if (changed)
				{
					// Update title.
					if (activeFont != null)
						setTitle(activeFont.Name, this);
					// Signal re-layouts.
					bool previousState = applicationIsSettingValues;
					applicationIsSettingValues = true;
					{
						this.Invoke((MethodInvoker)delegate
						{
							Properties.Settings.Default.lastSelectedFontName = activeFont.Name;
							Properties.Settings.Default.Save();
							//
							synchronizeAgainstFont(activeFont, previousFont);
						});
					}
					applicationIsSettingValues = previousState;
				}
			}
		}

		public string ActiveColor
		{
			get { return activeColor; }
			set { activeColor = value; }
		}

		FontInSize ownerOfPreviousFileSystemWatcher = null;
		Dictionary<ulong, System.Threading.Timer> fileCheckTimers = new Dictionary<ulong, System.Threading.Timer>();
		public float? ActiveSize
		{
			get { return activeSize; }
			set {
				activeSize = value;
				// Set character display.
				if (CanSaveToActiveFontSize)
				{
					characterDisplay.MaximumAscension = ActiveFontInActiveSize.TargetAscent;
					characterDisplay.Descent = ActiveFontInActiveSize.TargetDescent;
					// Unlisten for file changes.
					if (ownerOfPreviousFileSystemWatcher != ActiveFontInActiveSize)
					{
						disposeActiveFileListeners();
						fileCheckTimers = new Dictionary<ulong, System.Threading.Timer>();
						//
						ownerOfPreviousFileSystemWatcher = ActiveFontInActiveSize;
					}
				}
			}
		}

		private void disposeActiveFileListeners()
		{
			foreach (System.Threading.Timer timer in fileCheckTimers.Values)
				timer.Dispose();
		}

		public string ActiveVariant
		{
			get { return activeVariant; }
			set { activeVariant = value; }
		}

		//
		private bool CanSaveToActiveColor
		{
			get {
				return (ActiveColor != null && ActiveFont.Colors.Contains(ActiveColor));
			}
		}

		private BitmapColor ActiveColorData
		{
			get {
				return CanSaveToActiveColor ? ActiveFont.Colors[ActiveColor] : null;
			}
		}

		private bool CanSaveToActiveFontSize
		{
			get
			{
				return (ActiveFont != null && ActiveSize != null && ActiveFont.Sizes != null && ActiveFont.Sizes.Contains(ActiveSize.Value));
			}
		}

		public FontInSize ActiveFontInActiveSize
		{
			get
			{
				return CanSaveToActiveFontSize ? ActiveFont.Sizes[ActiveSize] : null;
			}
		}

		public Main()
		{
			// Default this to true. When the actual OpenGL configuration is queried for, this can be undone.
			ImageDescription.ForcePowerOfTwoDimensions = true;
			//
			InitializeComponent();
			//
			flowLayoutPanel.MouseEnter += new EventHandler(delegate (object sender, EventArgs e) {
				Control control = (Control)sender;
				control.Focus();
			});
		}

		private void glyph_Browse(object sender, EventArgs e)
		{
			UnicodeCharacter character = (UnicodeCharacter)glyphContextMenuStrip.Tag;
			if (character != null)
			{
				GlyphPictureBox control = getDisplayedGlyphPictureBoxById(character.Id);
				if (control.Character == null)
				{
					if (CanSaveToActiveFontSize)
					{
						DerivedCharacter addDerivedCharacter = new DerivedCharacter(character.Id);
						ActiveFontInActiveSize.Variants[ActiveVariant].Characters.Add(addDerivedCharacter);
						control.Character = addDerivedCharacter;
					}
				}
				control.browseToolStripMenuItem_Click(control, e);
			}
		}

		private void glyph_Edit(object sender, EventArgs e)
		{
			Application.DoEvents();
			//
			UnicodeCharacter character = (UnicodeCharacter)glyphContextMenuStrip.Tag;
			if (character != null)
			{
				GlyphPictureBox control = getDisplayedGlyphPictureBoxById(character.Id);
				if (ActiveFontInActiveSize.IsCharacterRealized(character.Id))
				{
					string glyphImageName = string.Format("{0}.png", character.Id);
					string filePath = Path.Combine(ActiveFontInActiveSize.Path, glyphImageName);
					// Set up a listener for changes to the file.
					if (!fileCheckTimers.ContainsKey(character.Id))
					{
						AutoResetEvent autoEvent = new AutoResetEvent(false);
						LastWriteChecker thisLastWriteChecker = new LastWriteChecker(character.Id, filePath);
						thisLastWriteChecker.FileChanged += delegate(object thisSender) {
							LastWriteChecker senderWriteChecker = (LastWriteChecker)thisSender;
							if (senderWriteChecker != null)
							{
								//
								GlyphPictureBox box = getDisplayedGlyphPictureBoxById(senderWriteChecker.Id);
								if (box != null)
								{
									this.Invoke((MethodInvoker)delegate()
									{
										box.ReloadImages();
										// Get image out of the traced character.
										box.TracedCharacter = generateEffectedTracedCharacterOutOfActiveFont(senderWriteChecker.Id, skipDetailedTracing: true);
									});
								}
								//
								reloadActiveCharacterImage();
							}
						};
						TimerCallback method = thisLastWriteChecker.CheckStatus;
						System.Threading.Timer timer = new System.Threading.Timer(method, autoEvent, 1000, 1000);
						fileCheckTimers.Add(character.Id, timer);
					}
					//
					System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo(filePath);
					startInfo.Verb = "edit";
					System.Diagnostics.Process.Start(startInfo);
				}
			}
		}

		private void glyph_Clear(object sender, EventArgs e)
		{
			UnicodeCharacter character = (UnicodeCharacter)glyphContextMenuStrip.Tag;
			if (character != null) 
			{
				GlyphPictureBox control = getDisplayedGlyphPictureBoxById(character.Id);
				control.clearToolStripMenuItem_Click(control, e);
			}
		}

		private void glyph_Remove(object sender, EventArgs e)
		{
			UnicodeCharacter character = (UnicodeCharacter)glyphContextMenuStrip.Tag;
			if (character != null)
			{
				GlyphPictureBox control = getDisplayedGlyphPictureBoxById(character.Id);
				// Signal that the character's image should be deleted.
				saveImageToActiveFont(character.Id, null);
				// Delete the character.
				ActiveFontInActiveSize.Characters.Remove(character.Id);
				// Trigger save.
				ActiveFont.Save();
				//
				synchronizeGlyphBoxWithActiveFont(character.Id, control, updateCharacterDisplay: true, skipDetailedTracing: true);
			}
		}

		private void glyph_Rename(object sender, EventArgs e)
		{
			UnicodeCharacter character = (UnicodeCharacter)glyphContextMenuStrip.Tag;
			if (character != null && CanSaveToActiveFontSize)
			{
				GlyphPictureBox control = getDisplayedGlyphPictureBoxById(character.Id);
				RenameGlyph dlg = new RenameGlyph();
				dlg.GlyphName = character.PrimaryName;
				if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					character.PrimaryName = dlg.GlyphName;
					bool wasUnused = false;
					if (!ActiveFontInActiveSize.Characters.Contains(character.Id))
					{
						Character addCharacter = new Character(character, ActiveFontInActiveSize.DefaultWidth, 0, 0, 1, 1);
						ActiveFontInActiveSize.SynchronizeCharacter(addCharacter);
						wasUnused = true;
					}
					ActiveFontInActiveSize.Characters[character.Id].PrimaryName = dlg.GlyphName;
					if (wasUnused)
						control.Character = ActiveFontInActiveSize.Characters[character.Id];
					// Redo title.
					if (characterDisplay.Character != null && characterDisplay.Character.Id == character.Id)
						setFontAwareTitle(SupportFunctions.TitleCaseString(dlg.GlyphName));
					ActiveFont.Save();
				}
			}
		}

		private void Main_Load(object sender, EventArgs e)
		{
		}

		private void Main_Shown(object sender, EventArgs e)
		{
			//
			Main m = this;
            setStatus("Ready.", m);
            startupServices_OnShown(sender);
		}

		private void doUnicodeBlockComboBox()
		{
			this.Invoke((MethodInvoker)delegate()
			{
				SupportFunctions.ComposeUnicodeBlockComboBox(unicodeBlockToolStripComboBox.ComboBox);
			});
		}

		private void doCharacterGlyphBoxLayoutGivenUnicodeCharacters(List<UnicodeCharacter> theseCharacters)
		{
			this.Invoke((MethodInvoker)delegate()
			{
				characters.Clear();
				SupportFunctions.AddProcessedCharactersToCharacterCollection(characters, theseCharacters, characterProvider: getModeAwareCharacterOutOfActiveFont);
				createGlyphVisualProxies();
			});
		}

		public ImageDescription getModeAwareImageDescriptionOutOfActiveFont(ulong id, CharacterDisplay.Mode? mode = null, bool recreateGlyph = false, bool skipDetailedTracing = true)
		{
			CharacterDisplay.Mode thisMode = (mode == null) ? characterDisplay.ActiveMode : mode.Value;
			if (thisMode == CharacterDisplay.Mode.BaseImage)
			{
				BitmapVariant variant = ActiveFontInActiveSize.Variants[ActiveVariant];
				// Get base image.
				if (variant != null)
				{
					if (variant.Characters.Contains(id))
					{
						Character character = variant.Characters[id];
						return variant.BaseImageProvider.GetImage(character, pushToContext: true);
					}
				}
			}
			else if (thisMode == CharacterDisplay.Mode.EffectedImage)
			{
				// If recreating the glyph is not necessary, try to get it off the glyph picture box that may hold it.
				if (!recreateGlyph)
				{
					GlyphPictureBox box = getDisplayedGlyphPictureBoxById(id);
					if (box != null)
					{
						// Get effected character image.
						if (box.TracedCharacter != null && box.TracedCharacter.PreviewImage != null && box.TracedCharacter.PreviewImage.PixelFormat != System.Drawing.Imaging.PixelFormat.Undefined)
						{
							// Clone existing image from glyph box.
							Bitmap bitmap = box.TracedCharacter.PreviewImage;
							return new ImageDescription(id, bitmap: bitmap, pushToContext: true);
						}
					}
				}
				// If recreating the glyph is necessary, make it.
				TracedCharacter retracedCharacter = generateEffectedTracedCharacterOutOfActiveFont(id, skipDetailedTracing: skipDetailedTracing);
				if (retracedCharacter != null && retracedCharacter.PreviewImage != null && retracedCharacter.PreviewImage.PixelFormat != System.Drawing.Imaging.PixelFormat.Undefined)
				{
					// Update existing box if available.
					GlyphPictureBox box = getDisplayedGlyphPictureBoxById(id);
					if (box != null)
						box.TracedCharacter = retracedCharacter;
					// Send back image description of preview image.
					return new ImageDescription(id, bitmap: retracedCharacter.PreviewImage, pushToContext: true);
				}
			}
			return new ImageDescription(id, pushToContext: true);
		}

		public DerivedCharacter getModeAwareCharacterOutOfActiveFont(ulong id, CharacterDisplay.Mode? mode = null)
		{
			CharacterDisplay.Mode thisMode = (mode == null) ? characterDisplay.ActiveMode : mode.Value;
			DerivedCharacter character = null;
			if (thisMode == CharacterDisplay.Mode.BaseImage)
				character = getCachedCharacterBaseOutOfActiveFont(id);
			else if (thisMode == CharacterDisplay.Mode.EffectedImage)
				character = getCachedEffectedCharacterOutOfActiveFont(id);
			// If character exists in cache, send it back.
			if (character != null)
				return character;
			// Fallback to cached variant.
			if (characters.Contains(id))
				return new DerivedCharacter(characters[id]);
			return null;
		}

		public DerivedCharacter getCachedCharacterBaseOutOfActiveFont(ulong id)
		{
			if (ActiveFont != null && ActiveFont.Sizes.Keys.Contains(ActiveSize))
			{
				FontInSize f = ActiveFont.Sizes[ActiveSize];
				if (f.Characters.Keys.Contains(id))
					return new DerivedCharacter(f.Characters[id]);
			}
			// Fallback to cached variant.
			if (characters.Contains(id))
				return new DerivedCharacter(characters[id]);
			return null;
		}

		public DerivedCharacter getCachedEffectedCharacterOutOfActiveFont(ulong id)
		{
			// Get box.
			GlyphPictureBox box = getDisplayedGlyphPictureBoxById(id);
			if (box != null)
			{
				// Get character.
				if (box.TracedCharacter != null)
					return box.TracedCharacter.DerivedUnicodeCharacter;
			}
			// Fallback to cached variant.
			if (characters.Contains(id))
				return new DerivedCharacter(characters[id]);
			return null;
		}

		public TracedCharacter generateModeAwareTracedCharacterOutOfActiveFont(ulong id, bool skipDetailedTracing = false)
		{
			return (characterDisplay.ActiveMode == CharacterDisplay.Mode.BaseImage) ? generateBaseTracedCharacterOutOfActiveFont(id) : generateEffectedTracedCharacterOutOfActiveFont(id, skipDetailedTracing: skipDetailedTracing);
		}

		public TracedCharacter generateBaseTracedCharacterOutOfActiveFont(ulong id)
		{
			if (ActiveFont != null && ActiveFontInActiveSize != null && ActiveFontInActiveSize.Characters != null)
			{
				// Get a simple tracing of the stored base image.
				if (ActiveFontInActiveSize.Characters.Keys.Contains(id))
					return TracedCharacter.Factory(id, ActiveFontInActiveSize.Variants[ActiveVariant], null, useEffects: false, skipDetailedTracing: true);
			}
			return null;
		}

		public TracedCharacter generateEffectedTracedCharacterOutOfActiveFont(ulong id, bool skipDetailedTracing = false)
		{
			if (ActiveFont != null && ActiveFontInActiveSize != null && ActiveFontInActiveSize.Characters != null)
			{
				if (ActiveVariant != null && ActiveFontInActiveSize.Variants != null && ActiveFontInActiveSize.Variants.Keys.Contains(ActiveVariant))
				{
					BitmapVariant v = ActiveFontInActiveSize.Variants[ActiveVariant];
					if (v.Characters != null && v.Characters.Keys.Contains(id))
						return TracedCharacter.Factory(id, ActiveFontInActiveSize.Variants[ActiveVariant], ActiveColorData, useEffects: true, skipDetailedTracing: skipDetailedTracing);
				}
			}
			return null;
		}

		public void createGlyphVisualProxies() {
			this.Invoke((MethodInvoker)delegate()
			{
				// Create an empty flow layout panel (without appropriate style and location).
				FlowLayoutPanel visualProxiesParentContainer = SupportFunctions.CreateFlowLayoutPanelForVisualProxies(contextMenuStrip: unicodeBlockContextMenuStrip);
				// Replace the flow layout panel.
				tableLayoutPanel.SuspendLayout();
				{
					if (!flowLayoutPanel.IsDisposed)
						flowLayoutPanel.Dispose();
					flowLayoutPanel = visualProxiesParentContainer;
					//
					tableLayoutPanel.Controls.Add(flowLayoutPanel, 3, 0);
					tableLayoutPanel.SetRowSpan(flowLayoutPanel, 2);
					//
					flowLayoutPanel.Show();
				}
				tableLayoutPanel.ResumeLayout();
				// Generate only the glyph picture boxes. These will be filled one at a time through threading.
				SupportFunctions.CreateAndPlaceCharacterGlyphBoxes(visualProxiesParentContainer, characterCollection: characters, contextMenuStrip: glyphContextMenuStrip, mouseDown: box_MouseDown, click: box_Click, saveImage: box_SaveImage);
			});
		}

		void box_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == System.Windows.Forms.MouseButtons.Right)
			{
				GlyphPictureBox box = (GlyphPictureBox)sender;
				OffsetableUnicodeCharacter character = (box.Character != null) ? box.Character : new OffsetableUnicodeCharacter(box.Id);
				glyphContextMenuStrip.Tag = realizeGlyphContextMenuStrip.Tag = character;
			}
		}
		
		void box_SaveImage(object sender, GlyphPictureBox.SaveImageEventArgs e)
		{
			GlyphPictureBox box = (GlyphPictureBox)sender;
			saveImageToActiveFont(e.Key, e.Bitmap);
			updateCharaterInActiveFontGivenImage(e.Key, e.Bitmap);
			synchronizeGlyphBoxWithActiveFont(e.Key, box, updateCharacterDisplay: true, skipDetailedTracing: true);
		}

		private bool saveImageToActiveFont(ulong key, Bitmap bitmap)
		{
			bool succeeded = false;
			if (CanSaveToActiveFontSize)
			{
				if (!Directory.Exists(ActiveFontInActiveSize.Path))
				{
					try
					{
						Directory.CreateDirectory(ActiveFontInActiveSize.Path);
					}
					catch { }
				}
				string file = Path.Combine(ActiveFontInActiveSize.Path, string.Format("{0}.png", key));
				succeeded = true;
				if (bitmap != null)
				{
					// Try to save.
					try { bitmap.Save(file); }
					catch { succeeded = false; }
				}
				else
				{
					// Try to delete.
					if (File.Exists(file))
					{
						try { File.Delete(file); }
						catch { succeeded = false; }
					}
				}
			}
			return succeeded;
		}

		private bool saveImageDescriptionToActiveFont(ulong key, ImageDescription description)
		{
			if (description != null)
			{
				Bitmap bitmap = (description != null) ? description.GetClonedBitmap() : null;
				bool result = false;
				if (bitmap != null)
					result = saveImageToActiveFont(key, bitmap);
				return result;
			}
			else
				return false;
		}

		private void updateCharaterInActiveFontGivenImage(ulong key, Bitmap bitmap, bool suppressAutoSave = false)
		{
			// If there isn't a bitmap, pretend like it's the size of a space . Otherwise, add 1 pixel.
			float width = (bitmap != null) ? bitmap.Size.Width : ActiveFontInActiveSize.DefaultWidth;
			if (!ActiveFontInActiveSize.Characters.Contains(key))
				ActiveFontInActiveSize.Characters.Add(new Character(UnicodeBlocks.GetCharacterById(key), width + 1, 0, 0, 1f, 1f));
			else 
				ActiveFontInActiveSize.Characters[key].OffsetWidth = width + 1;
			// Save it out.
			if (!suppressAutoSave)
				saveActiveFont();
		}

		private void synchronizeGlyphBoxWithActiveFont(ulong key, GlyphPictureBox box = null, bool updateCharacterDisplay = false, bool skipDetailedTracing = false)
		{
			if (box == null)
				box = getDisplayedGlyphPictureBoxById(key);
			if (box != null)
			{
				box.TracedCharacter = generateEffectedTracedCharacterOutOfActiveFont(key, skipDetailedTracing: skipDetailedTracing);
				if (updateCharacterDisplay)
					updateCharacterDisplayFromGlyphPictureBox(box);
			}
		}

		private GlyphPictureBox getDisplayedGlyphPictureBoxById(ulong key)
		{
			return SupportFunctions.GetDisplayedGlyphPictureBoxById(key, flowLayoutPanel);
		}

		private void saveActiveFont(bool reloadComboAfter = false)
		{
			if (ActiveFont != null)
			{
				Thread saveThread = new Thread(new ThreadStart(delegate {
					ActiveFont.Save();
					if (reloadComboAfter)
						reloadFontComboBox(this);
				}));
				saveThread.Name = string.Format("Save Font ({0})", ActiveFont.Name);
				saveThread.Start();
			}
		}

		private void box_Click(object sender, System.EventArgs e)
		{
			characterDisplay.SuppressSaveEvent = true;
			{
				GlyphPictureBox box = (GlyphPictureBox)sender;
				updateCharacterDisplayFromGlyphPictureBox(box);
			}
			characterDisplay.SuppressSaveEvent = false;
		}

		private void updateCharacterDisplayFromGlyphPictureBox(GlyphPictureBox box)
		{
			bool previousState = characterDisplay.SuppressSaveEvent;
			characterDisplay.SuppressSaveEvent = true;
			{
				ulong key = (ulong)box.Tag;
				TracedCharacter tracedCharacter = generateModeAwareTracedCharacterOutOfActiveFont(key, skipDetailedTracing: true);
				DerivedCharacter character = (tracedCharacter != null) ? tracedCharacter.DerivedUnicodeCharacter : new DerivedCharacter(key);
				string characterName = (character != null) ? character.Name : UnicodeBlocks.GetCharacterById(key).PrimaryName;
				setFontAwareTitle(characterName);
				characterDisplay.Character = character;
				characterDisplay.ImageDescription = new ImageDescription(key, bitmap: (tracedCharacter != null && tracedCharacter.PreviewImage != null) ? (Bitmap)tracedCharacter.PreviewImage.Clone() : null, pushToContext: true);
			}
			characterDisplay.SuppressSaveEvent = previousState;
		}

		private void setFontAwareTitle(string characterName)
		{
			string name = (ActiveFont != null) ? string.Format("{0} - {1}", ActiveFont.Name, characterName) : characterName;
			setTitle(name, this);
		}

		private void unicodeBlockToolStripComboBox_DropDown(object sender, EventArgs e)
		{
			ToolStripComboBox senderComboBox = (ToolStripComboBox)sender;
			int width = senderComboBox.DropDownWidth;
			Graphics g = senderComboBox.ComboBox.CreateGraphics();
			Font font = senderComboBox.ComboBox.Font;
			//
			int vertScrollBarWidth = (senderComboBox.Items.Count > senderComboBox.MaxDropDownItems) ? SystemInformation.VerticalScrollBarWidth : 0;
			foreach (string s in senderComboBox.Items)
			{
				int newWidth = (int)g.MeasureString(s, font).Width + vertScrollBarWidth;
				if (width < newWidth)
					width = newWidth;
			}
			senderComboBox.DropDownWidth = width;
		}

		private void characterDisplay_RequestBaseImage(object sender, EventArgs data)
		{
			reloadActiveCharacterImage();
			reloadActiveCharacter();
		}

		private void characterDisplay_RequestEffectedImage(object sender, EventArgs data)
		{
			reloadActiveCharacterImage();
			reloadActiveCharacter();
		}

		private void reloadActiveCharacterImage(bool recreateGlyph = false)
		{
			if (characterDisplay.Character != null)
			{
				// Use base character image OR effected character image.
				ulong key = characterDisplay.Character.Id;
				characterDisplay.ImageDescription = getModeAwareImageDescriptionOutOfActiveFont(key, recreateGlyph: recreateGlyph);
			}
		}

		private void reloadActiveCharacter()
		{
			if (characterDisplay.Character != null)
				characterDisplay.Character = getModeAwareCharacterOutOfActiveFont(characterDisplay.Character.Id);
		}

		private void realizeGlyphContextMenuStrip_Opening(object sender, System.ComponentModel.CancelEventArgs e)
		{
			ContextMenuStrip contextMenu = (ContextMenuStrip)sender;
			createOrUpdateRealizeGlyphOrBlockContextMenuStrip(contextMenu, defaultRealizeHandler: defaultRealizeGlyphAliased_Click, advancedRealizeHandler: realizeGlyphAdvanced_Click);
		}

		private void realizeBlockContextMenuStrip_Opening(object sender, System.ComponentModel.CancelEventArgs e)
		{
			ContextMenuStrip contextMenu = (ContextMenuStrip)sender;
			createOrUpdateRealizeGlyphOrBlockContextMenuStrip(contextMenu, defaultRealizeHandler: realizeBlockAliased_Click, advancedRealizeHandler: realizeAdvanced_Click);
		}

		private void realizeGlyphAdvanced_Click(object sender, EventArgs e)
		{
			ToolStripItem control = (ToolStripItem)sender;
			string requestedUnicodeBlock = unicodeBlockToolStripComboBox.Text;
			List<UnicodeCharacter> characters = getCharactersGivenCharacterOrBlock(character: (UnicodeCharacter)control.Owner.Tag);
			realizeAdvanced(characters);
		}

		private void realizeAdvanced_Click(object sender, EventArgs e)
		{
			string requestedUnicodeBlock = unicodeBlockToolStripComboBox.Text;
			List<UnicodeCharacter> characters = getCharactersGivenCharacterOrBlock(requestedUnicodeBlock: requestedUnicodeBlock);
			realizeAdvanced(characters);
		}

		Color? lastAntiAliasMatteColor = null;
		private void realizeAdvanced(List<UnicodeCharacter> characters)
		{
			TextAnalyzer.RealizationSettings dlg = new TextAnalyzer.RealizationSettings(extraAscentMarker: (int)Math.Ceiling(ActiveFontInActiveSize.TargetAscent), size: ActiveSize, antiAliasMatteColor: lastAntiAliasMatteColor);
			if (characters.Count == 1)
				dlg.ReferenceText = Char.ConvertFromUtf32((int)characters[0].Id);
			//
			dlg.SetFontByName(ActiveFont.Name);
			if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				TextAnalyzer.RealizationConfiguration configuration = dlg.Configuration;
				lastAntiAliasMatteColor = configuration.AntialiasMatteColor;
				Thread thread = new Thread((ThreadStart)delegate
				{
					realizeGlyphs(configuration, characters);
				});
				thread.Name = "Realize Glyphs";
				thread.Start();
			}
		}

		private void createOrUpdateRealizeGlyphOrBlockContextMenuStrip(ContextMenuStrip contextMenu, EventHandler defaultRealizeHandler = null, EventHandler advancedRealizeHandler = null)
		{
			if (contextMenu.Items.Count == 0)
			{
				ToolStripMenuItem realizeDefaultItem = new ToolStripMenuItem("Default", null, defaultRealizeHandler);
				contextMenu.Items.Add(realizeDefaultItem);
				contextMenu.Items.Add(new ToolStripSeparator());
				//
				ToolStripMenuItem realizeAdvancedItem = new ToolStripMenuItem("Advanced", null, advancedRealizeHandler);
				contextMenu.Items.Add(realizeAdvancedItem);
				contextMenu.Items.Add(new ToolStripSeparator());
				// Come up with groupings.
				List<List<string>> groups = new List<List<string>>();
				{
					List<string> thisGroup = new List<string>();
					for (uint i = 0; i < 10; i++)
						thisGroup.Add(i.ToString());
					groups.Add(thisGroup);
				}
				uint requestedGroupSize = 2;
				for (char c = 'A'; c <= 'Z'; )
				{
					List<string> thisGroup = new List<string>();
					for (char thisChar = c; thisChar < (char)(Math.Min('Z' + 1, c + requestedGroupSize)); thisChar++)
					{
						thisGroup.Add(thisChar.ToString());
					}
					groups.Add(thisGroup);
					c = (char)(c + requestedGroupSize);
				}
				// Compose menu items based on those groupings.
				foreach (List<string> group in groups)
				{
					string first = null, last = null;
					if (group.Count > 0)
						first = group[0];
					if (group.Count > 1)
						last = group[group.Count - 1];
					string menuItemText = null;
					if (first != null && last != null)
						menuItemText = string.Format("{0} - {1}", first, last);
					else if (first != null)
						menuItemText = first;
					if (menuItemText != null)
					{
						ToolStripMenuItem thisGroupMenuItem = new ToolStripMenuItem(menuItemText);
						thisGroupMenuItem.DropDownItems.Add(new ToolStripMenuItem("No fonts."));
						thisGroupMenuItem.DropDownOpening += new EventHandler(fontGroupMenuItem_DropDownOpening);
						thisGroupMenuItem.Tag = group;
						contextMenu.Items.Add(thisGroupMenuItem);
					}
				}
			}
		}

		void fontGroupMenuItem_DropDownOpening(object sender, EventArgs e)
		{
			ToolStripMenuItem thisMenuItem = (ToolStripMenuItem)sender;
			thisMenuItem.DropDownItems.Clear();
			List<string> group = thisMenuItem.Tag as List<string>;
			if (thisMenuItem.Owner == unicodeBlockContextMenuStrip)
				composeFontFamilyMenuItems(thisMenuItem, group, realizeBlockAliased_Click, realizeBlockAntiAliased_Click);
			else
				composeFontFamilyMenuItems(thisMenuItem, group, realizeGlyphAliased_Click, realizeGlyphAntiAliased_Click);
		}

		private void composeFontFamilyMenuItems(ToolStripMenuItem thisMenuItem, List<string> group, EventHandler realizeAliased = null, EventHandler realizeAntiAliased = null)
		{
			if (group != null && group.Count > 0)
			{
				Dictionary<string, ToolStripMenuItem> items = new Dictionary<string, ToolStripMenuItem>();
				foreach (string name in group)
				{
					ToolStripMenuItem thisGroupMenuItem = new ToolStripMenuItem(name);
					thisGroupMenuItem.Tag = name;
					items.Add(name, thisGroupMenuItem);
				}
				// Create font list.
				InstalledFontCollection installedFontCollection = new InstalledFontCollection();
				// Get the array of FontFamily objects.
				foreach (FontFamily family in installedFontCollection.Families)
				{
					string thisName = family.Name;
					string foundItem = group.Find(item => item != null && string.Compare(item[0].ToString(), thisName[0].ToString(), true) == 0);
					if (foundItem != null)
					{
						// If the menu item is displayable, add it. It may not be, considering it may not actually implement a style.
						try
						{
							ToolStripMenuItem thisFontMenuItem = new ToolStripMenuItem(thisName);
							FontStyle style = SupportFunctions.GetFirstAvailableFontStyle(family);
							float size = (ActiveSize != null) ? ActiveSize.Value : items[foundItem].Font.Size;
							Font font = new Font(family, size, style);
							if (font.GetHeight() > size * 2.5)
								font = new Font(FontFamily.GenericSerif, size, style);
							thisFontMenuItem.Font = font;
							thisFontMenuItem.ToolTipText = thisName;
							thisFontMenuItem.AutoSize = true;
							thisFontMenuItem.Tag = thisName;
							//
							ToolStripMenuItem aliasedMenuItem = new ToolStripMenuItem("Aliased", null, realizeAliased);
							aliasedMenuItem.Font = items[foundItem].Font;
							aliasedMenuItem.Tag = thisName;
							ToolStripMenuItem antiAliasedMenuItem = new
							ToolStripMenuItem("Anti-Aliased", null, realizeAntiAliased);
							antiAliasedMenuItem.Font = items[foundItem].Font;
							antiAliasedMenuItem.Tag = thisName;
							//
							thisFontMenuItem.DropDownItems.Add(aliasedMenuItem);
							thisFontMenuItem.DropDownItems.Add(antiAliasedMenuItem);
							//
							items[foundItem].DropDownItems.Add(thisFontMenuItem);
						}
						catch { }
					}
				}
				//
				foreach (string name in group)
					thisMenuItem.DropDownItems.Add(items[name]);
			}
		}

		void defaultRealizeGlyphAliased_Click(object sender, EventArgs e)
		{
			ToolStripItem control = (ToolStripItem)sender;
			realizeGlyphOrBlock(control, antiAliased: false, character: (UnicodeCharacter)control.Owner.Tag);
		}

		void realizeGlyphAliased_Click(object sender, EventArgs e)
		{
			ToolStripItem control = (ToolStripItem)sender;
			realizeGlyphOrBlock(control, antiAliased: false, character: (UnicodeCharacter)control.OwnerItem.OwnerItem.OwnerItem.Owner.Tag);
		}

		void realizeGlyphAntiAliased_Click(object sender, EventArgs e)
		{
			ToolStripItem control = (ToolStripItem)sender;
			realizeGlyphOrBlock(control, antiAliased: true, character: (UnicodeCharacter)control.OwnerItem.OwnerItem.OwnerItem.Owner.Tag);
		}

		void realizeBlockAliased_Click(object sender, EventArgs e)
		{
			realizeGlyphOrBlock((ToolStripItem)sender, antiAliased: false);
		}

		void realizeBlockAntiAliased_Click(object sender, EventArgs e)
		{
			realizeGlyphOrBlock((ToolStripItem)sender, antiAliased: true);
		}

		private void realizeGlyphOrBlock(ToolStripItem control, bool antiAliased = false, UnicodeCharacter character = null)
		{
			string requestedUnicodeBlock = unicodeBlockToolStripComboBox.Text;
			Thread thread = new Thread((ThreadStart)delegate
			{
				List<UnicodeCharacter> characters = getCharactersGivenCharacterOrBlock(character: character, requestedUnicodeBlock: requestedUnicodeBlock);
				TextAnalyzer.RealizationConfiguration configuration = getConfigurationFromToolStripItem(control, characters, antiAliased: antiAliased);
				realizeGlyphs(configuration, characters);
			});
			thread.Name = "Realize Glyphs";
			thread.Start();
		}

		private TextAnalyzer.RealizationConfiguration getConfigurationFromToolStripItem(ToolStripItem control, List<UnicodeCharacter> characters, bool antiAliased = false) {

			bool hasFontFamilyTag = (control != null && control.Tag != null) ? true : false;
			Font font;
			int size = 12;
			if (!hasFontFamilyTag)
			{
				UnicodeCharacter exemplarCharacter = (characters.Count > 0) ? characters[0] : null;
				font = UnicodePrivateFontCollection.GetFontForUnicodeCharacter(exemplarCharacter);
				size = (int)Math.Ceiling(font.SizeInPoints);
				antiAliased = !UnicodePrivateFontCollection.GetAliasedDefaultFontNatureForUnicodeCharacter(exemplarCharacter);
			}
			else
			{
				FontFamily family = new FontFamily((string)control.Tag);
				size = (int)Math.Ceiling(ActiveFontInActiveSize.Size);
				font = new Font(family, ActiveFontInActiveSize.Size, SupportFunctions.GetFirstAvailableFontStyle(family));
			}
			//
			TextAnalyzer.RealizationConfiguration config = new TextAnalyzer.RealizationConfiguration(font, antialiased: antiAliased);
			return config;
		}

		private List<UnicodeCharacter> getCharactersGivenCharacterOrBlock(UnicodeCharacter character = null, string requestedUnicodeBlock = null)
		{
			// Get a list of characters to realize.
			List<UnicodeCharacter> list = new List<UnicodeCharacter>();
			if (character != null)
				list.Add(character);
			else
			{
				foreach (UnicodeBlock block in UnicodeBlocks.GetBlocksByName(requestedUnicodeBlock))
				{
					foreach (UnicodeCharacter thisCharacter in block.Characters)
					{
						// If the active font in this size does not contain a definition for the character, realize it.
						if (!thisCharacter.IsControlCharacter && !thisCharacter.IsSurrogate && !ActiveFontInActiveSize.IsCharacterRealized(thisCharacter.Id))
							list.Add(thisCharacter);
					}
				}
			}
			return list;
		}

		private void realizeGlyphs(TextAnalyzer.RealizationConfiguration config, List<UnicodeCharacter> characters)
		{
			if (CanSaveToActiveFontSize)
			{
				int row = 0;
				int count = characters.Count;
				characters.ForEach(delegate(UnicodeCharacter thisCharacter)
				{
					row++;
					setStatus(string.Format("Realizing Glyphs: {0}/{1}.", new object[] { row, count }), this);
					//
					int ascent = 0;
					Bitmap glyphImage = config.GetClippedBitmap(thisCharacter.ToString(), out ascent);
					if (glyphImage != null)
					{
						bool success = false;
						ImageDescription description = new ImageDescription(thisCharacter.Id, bitmap: glyphImage, pushToContext: false);
						//
						success = saveImageDescriptionToActiveFont(thisCharacter.Id, description);
						if (success)
						{
							if (ActiveFontInActiveSize.Characters == null)
								ActiveFontInActiveSize.Characters = new CharacterCollection();
							// Add the character (and synch it to variants).
							ActiveFontInActiveSize.SynchronizeCharacter(new Character(thisCharacter, description.SignificantWidth + 1, 0, ascent, 1f, 1f));
							synchronizeGlyphBoxWithActiveFont(thisCharacter.Id, skipDetailedTracing: true);
						}
					}
				});
				//
				setStatus("Ready.", this);
				saveActiveFont();
			}
		}

		private void clearBlock_Click(object sender, EventArgs e)
		{
			Application.DoEvents();
			//
			if (CanSaveToActiveFontSize)
			{
				List<UnicodeBlock> blocks = UnicodeBlocks.GetBlocksByName(unicodeBlockToolStripComboBox.Text);
				foreach (UnicodeBlock block in blocks)
				{
					foreach (UnicodeCharacter character in block.Characters)
					{
						if (ActiveFontInActiveSize.Characters.Contains(character.Id))
						{
							bool success = saveImageToActiveFont(character.Id, null);
							updateCharaterInActiveFontGivenImage(character.Id, null, suppressAutoSave: true);
							ActiveFontInActiveSize.Characters.Remove(character.Id);
							synchronizeGlyphBoxWithActiveFont(character.Id, updateCharacterDisplay: (characterDisplay.Character != null && characterDisplay.Character.Id == character.Id) ? true : false, skipDetailedTracing: true);
						}
					}
				}
				//
				saveActiveFont();
			}
		}

		private void characterDisplay_CharacterComponentsChanged(object sender, CharacterDisplay.CharacterComponentsChangedEventArgs e)
		{
			// Save font.
			if (CanSaveToActiveFontSize)
			{
				ActiveFontInActiveSize.SynchronizeCharacter(e.Character.Id, e.Character.BaseCharacter);
				// Update values on cached traced character to avoid needing to re-trace.
				GlyphPictureBox box = getDisplayedGlyphPictureBoxById(e.Character.Id);
				if (box != null && box.TracedCharacter != null)
					box.TracedCharacter.DerivedUnicodeCharacter.BaseCharacter = e.Character.BaseCharacter;
				//
				bool recreateAndReloadGlyph = false;
				if (ActiveFontInActiveSize.Variants != null && ActiveFontInActiveSize.Variants.Contains(ActiveVariant))
				{
					BitmapVariant v = ActiveFontInActiveSize.Variants[ActiveVariant];
					recreateAndReloadGlyph = (v.Effects != null && v.Effects.Exists(item => item.RedrawOnCharacterOffsetChanged == true)) ? true : recreateAndReloadGlyph;
					if (!recreateAndReloadGlyph && ActiveColor != null && ActiveFont.Colors != null && ActiveFont.Colors.Contains(ActiveColor))
					{
						BitmapColor c = ActiveFont.Colors[ActiveColor];
						recreateAndReloadGlyph = (c.Effects != null && c.Effects.Exists(item => item.RedrawOnCharacterOffsetChanged == true)) ? true : recreateAndReloadGlyph;
					}
				}
				if (recreateAndReloadGlyph)
					reloadActiveCharacterImage(recreateGlyph: true);
				saveActiveFont();
			}
		}

		private void editToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Application.DoEvents();
			//
			if (ActiveFont != null)
			{
				AddOrEditFont dlg = new AddOrEditFont(ActiveFont.Name);
				dlg.Text = string.Format("Edit {0} Font", ActiveFont.Name);
				if (DialogResult.OK == dlg.ShowDialog())
				{
					Application.DoEvents();
					//
					bool activeFontIsntNewFont = ActiveFont != dlg.Font;
					// Save over it.
					ActiveFont = dlg.Font;
					saveActiveFont(reloadComboAfter: activeFontIsntNewFont);
					//
					if (activeFontIsntNewFont) {
						// Redo all the glyphs anyway (something may have changed).
						synchronizeAgainstFont(ActiveFont, forceSynchronize: true);
					}
				}
			}
		}

		private void newFontToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Application.DoEvents();
			//
			AddOrEditFont dlg = new AddOrEditFont();
			dlg.Text = "Add New Font";
			if (DialogResult.OK == dlg.ShowDialog())
			{
				Application.DoEvents();
				//
				bool activeFontIsntNewFont = ActiveFont != dlg.Font;
				// Save over it.
				ActiveFont = dlg.Font;
				saveActiveFont(reloadComboAfter: activeFontIsntNewFont);
			}
		}

		private void synchronizeAgainstFont(BitmapFont font, BitmapFont oldFont = null, bool forceSynchronize = false)
		{
			// Figure out relationship between the two provided fonts.
			bool HasOldFont = oldFont != null ? true : false;
			bool FontChanged = (HasOldFont && font.Name == oldFont.Name) ? false : true;
			// Colors
			bool reloadColorComboBox = !(font != null && colorsToolStripComboBox != null && font.Colors != null && font.Colors.Count == colorsToolStripComboBox.Items.Count && font.Colors.Keys.TrueForAll(item => colorsToolStripComboBox.Items.Contains(item))) ? true : false;
			bool PickingNewColor = (reloadColorComboBox || FontChanged) ? synchronizeColorsComboBoxAgainstFont(font) : false; // Reload the colors combobox, if necessary.
			// Sizes
			bool NoFontInSize = (ActiveSize == null || font.Sizes == null || !font.Sizes.Contains(ActiveSize)) ? true : false;
			bool reloadSizeComboBox = !(font != null && sizesToolStripComboBox != null && font.Sizes != null && font.Sizes.Count == sizesToolStripComboBox.Items.Count && font.Sizes.Keys.TrueForAll(item => sizesToolStripComboBox.Items.Contains(item.Value))) ? true : false;
			bool PickingNewSize = reloadSizeComboBox ? synchronizeSizesComboBoxAgainstFont(font) : false; // Reload the sizes combobox, if necessary.
			NoFontInSize = (ActiveSize == null || font.Sizes == null || !font.Sizes.Contains(ActiveSize)) ? true : false;
			FontInSize fontInSize = !NoFontInSize ? font.Sizes[ActiveSize] : null;
			// Variants
			bool NoActiveVariant = (NoFontInSize || ActiveVariant == null || fontInSize.Variants == null || !fontInSize.Variants.Contains(ActiveVariant)) ? true : false;
			BitmapVariant activeVariant = !NoActiveVariant ? fontInSize.Variants[ActiveVariant] : null;
			BitmapVariant oldActiveVariant = (HasOldFont && oldFont.Sizes.Contains(ActiveSize) && oldFont.Sizes[ActiveSize].Variants.Contains(ActiveVariant)) ? oldFont.Sizes[ActiveSize].Variants[ActiveVariant] : null;
			bool reloadVariantsComboBox = !(!NoFontInSize && variantToolStripComboBox != null && fontInSize.Variants != null && fontInSize.Variants.Count == variantToolStripComboBox.Items.Count && fontInSize.Variants.Keys.TrueForAll(item => variantToolStripComboBox.Items.Contains(item)));
			bool PickingNewVariant = reloadVariantsComboBox ? synchronizeVariantsComboBoxAgainstFont(fontInSize) : false; // Reload the variants combobox, if necessary.
			bool VariantEffectsHaveChanged = (!NoActiveVariant && oldActiveVariant != null && activeVariant.Equals(oldActiveVariant)) ? false : true;
			//
			bool synchronizeVisualProxies = (FontChanged || PickingNewColor || PickingNewSize || PickingNewVariant || VariantEffectsHaveChanged || forceSynchronize) ? true : false;
			if (synchronizeVisualProxies) 
				synchronizeAllGlyphBoxesWithActiveFont();
			// Synch the character display to the font in size data.
			characterDisplay.MaximumAscension = (!NoFontInSize) ? fontInSize.TargetAscent : 0f;
			characterDisplay.Descent = (!NoFontInSize) ? fontInSize.TargetDescent : 0f;
			// Do best-fit sizing for the character display size.
			double verticalNeeds = characterDisplay.MaximumAscension + characterDisplay.GutterY + 1;
			double horizontalNeeds = characterDisplay.CharacterWidth;
			double verticalPixelsAvailable = characterDisplay.ViewerSize.Height / (characterDisplay.ZoomFactor);
			double horizontalPixelsAvailable = characterDisplay.ViewerSize.Width / (characterDisplay.ZoomFactor);
			if (!(horizontalPixelsAvailable >= horizontalNeeds && verticalPixelsAvailable >= verticalNeeds))
			{
				double bestHorizontalFit = characterDisplay.ViewerSize.Width / horizontalNeeds;
				double bestVerticalFit = characterDisplay.ViewerSize.Height / verticalNeeds;
				int zoomWithBestFit = (int)Math.Max(1, Math.Floor(Math.Min(bestHorizontalFit, bestVerticalFit)));
				characterDisplay.ZoomFactor = zoomWithBestFit;
			}
		}

		private bool synchronizeColorsComboBoxAgainstFont(BitmapFont font)
		{
			bool PickingNewColor = false;
			//
			colorsToolStripComboBox.Items.Clear();
			if (font.Colors != null)
				foreach (string color in font.Colors.Keys)
					colorsToolStripComboBox.Items.Add(color);
			//
			if (colorsToolStripComboBox.Items.Count > 0 && (ActiveColor == null || !colorsToolStripComboBox.Items.Contains(ActiveColor)))
			{
				colorsToolStripComboBox.Enabled = true;
				PickingNewColor = true;
				colorsToolStripComboBox.SelectedIndex = 0;
				ActiveColor = (string)colorsToolStripComboBox.SelectedItem;
			}
			else if (colorsToolStripComboBox.Items.Count == 0)
				colorsToolStripComboBox.Enabled = false;
			else if (colorsToolStripComboBox.Items.IndexOf(ActiveColor) >= 0)
			{
				colorsToolStripComboBox.Enabled = true;
				PickingNewColor = true;
				colorsToolStripComboBox.SelectedIndex = colorsToolStripComboBox.Items.IndexOf(ActiveColor);
			}
			//
			if (colorsToolStripComboBox.Enabled && colorsToolStripComboBox.SelectedIndex < 0)
			{
				PickingNewColor = true;
				colorsToolStripComboBox.SelectedIndex = 0;
			}
			return PickingNewColor;
		}

		private void enableBlockPager(bool value, Main m = null)
		{
			if (m != null && m.IsHandleCreated)
			{
				m.Invoke((MethodInvoker)delegate()
				{
					m.blockPager.Enabled = value;
				});
			}
			else
				m.blockPager.Enabled = value;
		}

		private void synchronizeAllGlyphBoxesWithActiveFont()
		{
			bool previousValue = blockPager.Enabled;
			// Freeze pager to prevent character change. Unfreeze pager afterwards if applicable.
			enableBlockPager(false, m: this);
			{
				// Try to process pager enabledness state change.
				Application.DoEvents();
				// Update glyph boxes for each character.
				foreach (OffsetableUnicodeCharacter character in characters)
				{
					bool characterExists = characterDisplay.Character != null ? true : false,
						characterIsCurrentlySelected = characterExists && character.Id == characterDisplay.Character.Id ? true : false,
						updateThisCharacterInDisplay = (characterIsCurrentlySelected) ? true : false;
					// TODO: Make mesher thread-safe so the following call can be threaded.
					// Update the box(es) on the right.
					synchronizeGlyphBoxWithActiveFont(character.Id, updateCharacterDisplay: updateThisCharacterInDisplay, skipDetailedTracing: true);
					//
					Application.DoEvents();
				}
			}
			enableBlockPager(previousValue, m: this);
		}

		private bool synchronizeVariantsComboBoxAgainstFont(FontInSize fontInSize)
		{
			bool PickingNewVariant = false;
			//
			variantToolStripComboBox.Items.Clear();
			if (fontInSize != null && fontInSize.Variants != null)
				foreach (string variant in fontInSize.Variants.Keys)
					variantToolStripComboBox.Items.Add(variant);
			if (variantToolStripComboBox.Items.Count > 0 && (ActiveVariant == null || !variantToolStripComboBox.Items.Contains(ActiveVariant)))
			{
				variantToolStripComboBox.Enabled = true;
				PickingNewVariant = true;
				variantToolStripComboBox.SelectedIndex = 0;
				ActiveVariant = (string)variantToolStripComboBox.SelectedItem;
			}
			else if (variantToolStripComboBox.Items.Count == 0)
				variantToolStripComboBox.Enabled = false;
			return PickingNewVariant;
		}

		private bool synchronizeSizesComboBoxAgainstFont(BitmapFont font)
		{
			bool PickingNewSize = false;
			//
			sizesToolStripComboBox.Items.Clear();
			if (font != null && font.Sizes != null)
				foreach (float size in font.Sizes.Keys)
					sizesToolStripComboBox.Items.Add(size);
			if (sizesToolStripComboBox.Items.Count > 0 && (ActiveSize == null || !sizesToolStripComboBox.Items.Contains(ActiveSize)))
			{
				sizesToolStripComboBox.Enabled = true;
				PickingNewSize = true;
				sizesToolStripComboBox.SelectedIndex = 0;
				ActiveSize = (float?)sizesToolStripComboBox.SelectedItem;
			} else if (sizesToolStripComboBox.Items.Count == 0)
				sizesToolStripComboBox.Enabled = false;
			return PickingNewSize;
		}

		private void sizesToolStripComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!applicationIsSettingValues)
			{
				// Let the combo box close first.
				this.Focus();
				Application.DoEvents();
				//
				ToolStripComboBox control = (ToolStripComboBox)sender;
				if (control.SelectedItem != null)
				{
					string selectedSize = control.SelectedItem.ToString();
					bool parsed = false;
					float size = 0f;
					parsed = float.TryParse(selectedSize, out size);
					if (parsed)
						ActiveSize = size;
					synchronizeAgainstFont(activeFont, activeFont, forceSynchronize: true);
				}
			}
		}

		private void variantToolStripComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!applicationIsSettingValues)
			{
				// Let the combo box close first.
				this.Focus();
				Application.DoEvents();
				//
				ToolStripComboBox control = (ToolStripComboBox)sender;
				if (control.SelectedItem != null)
				{
					string selectedVariant = control.SelectedItem.ToString();
					ActiveVariant = selectedVariant;
					synchronizeAgainstFont(activeFont, activeFont, forceSynchronize: true);
				}
			}
		}

		private void colorsToolStripComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!applicationIsSettingValues)
			{
				// Let the combo box close first.
				this.Focus();
				Application.DoEvents();
				//
				ToolStripComboBox control = (ToolStripComboBox)sender;
				if (control.SelectedItem != null)
				{
					string selectedColor = control.SelectedItem.ToString();
					ActiveColor = selectedColor;
					synchronizeAgainstFont(activeFont, activeFont, forceSynchronize: true);
				}
			}
		}

		private void unicodeBlockToolStripComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			Application.DoEvents();
			//
			ToolStripComboBox control = (ToolStripComboBox)sender;
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
				//
				doCharacterGlyphBoxLayoutGivenUnicodeCharacters(range);
				synchronizeAllGlyphBoxesWithActiveFont();
			}));
			doLayout.Name = "Do Unicode Glyph Layout";
			doLayout.Start();
		}

		private void helpToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Application.DoEvents();
			Help.ShowHelp(this, "Documentation.chm");
		}

		private void creditsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Application.DoEvents();
			if (creditsWindow == null)
			{
				creditsWindow = new Credits(this);
				creditsWindow.Show(this);
			}
			else
				creditsWindow.Focus();
		}

		public void setStatus(string s, Main m)
		{
			if (m != null)
			{
				try
				{
					m.Invoke((MethodInvoker)delegate
					{
						m.toolStripStatusLabel.Text = s;
					});
				}
				catch { }
			}
		}

		public void setTitle(string s, Main m)
		{
			if (m != null)
			{
				try
				{
					m.Invoke((MethodInvoker)delegate
					{
						m.Text = string.Format("Unicode Font Tool - {0}", s);
					});
				}
				catch { }
			}
		}

		public void startupServices_OnShown(object sender)
		{
			Application.DoEvents();
			//
			Main m = sender as Main;
			//
			blockPager.OnRangeUpdate += blockPager_OnRangeUpdate;
			// Load fonts.
			reloadFontComboBox(m, forcedSelection: Properties.Settings.Default.lastSelectedFontName);
			//
			m.Invoke((MethodInvoker)delegate
			{
				m.Enabled = true;
			});
			Thread doUnicodeBlockListAndLayout = new Thread(new ThreadStart(delegate()
			{
				doUnicodeBlockComboBox();
			}));
			doUnicodeBlockListAndLayout.Name = "Do Unicode Block List and Layout";
			doUnicodeBlockListAndLayout.Start();
			//
			createOrUpdateRealizeGlyphOrBlockContextMenuStrip(realizeBlockContextMenuStrip, defaultRealizeHandler: realizeBlockAliased_Click, advancedRealizeHandler: realizeAdvanced_Click);
		}

		private void reloadFontComboBox(Main m, string forcedSelection = null)
		{
			m.Invoke((MethodInvoker)delegate
			{
				string previouslySelectedFontName = (forcedSelection == null && fontToolStripComboBox.Items.Count > 0) ? (string)fontToolStripComboBox.SelectedItem : forcedSelection;
				clearFontsInComboBox(m);
				string path = SupportFunctions.Combine("fonts");
				if (Directory.Exists(path))
				{
					string[] potentialFonts = Directory.GetDirectories(path);
					foreach (string fontPath in potentialFonts)
					{
						string shortName = Path.GetFileName(fontPath);
						string xmlPath = Path.Combine(fontPath, "font.xml");
						if (File.Exists(xmlPath))
						{
							// Using a FileStream, create an XmlTextReader.
							using (Stream fs = new FileStream(xmlPath, FileMode.Open))
							{
								XmlReader reader = new XmlTextReader(fs);
								try
								{
									if (SupportFunctions.BitmapFontSerializer.CanDeserialize(reader))
									{
										m.Invoke((MethodInvoker)delegate
										{
											fontToolStripComboBox.Items.Add(shortName);
										});
									}
								}
								catch (XmlException) { }
								finally
								{
									reader.Close();
									fs.Close();
								}
							}
						}
					}
				}
				selectOrReselectFontInComboBox(m, previousSelection: previouslySelectedFontName);
			});
		}

		private void clearFontsInComboBox(Main m)
		{
			m.Invoke((MethodInvoker)delegate
			{
				fontToolStripComboBox.Items.Clear();
			});
		}

		private void selectOrReselectFontInComboBox(Main m, string previousSelection = null)
		{
			m.Invoke((MethodInvoker)delegate
			{
				fontToolStripComboBox.Enabled = (fontToolStripComboBox.Items.Count > 0) ? true : false;
				if (previousSelection != null && fontToolStripComboBox.Items.Contains(previousSelection))
					fontToolStripComboBox.SelectedItem = previousSelection;
				else if (fontToolStripComboBox.Items.Count > 0)
					fontToolStripComboBox.SelectedItem = fontToolStripComboBox.Items[0];
			});
		}        

		private void closeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		private void fontToolStripComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			//
			ToolStripComboBox control = (ToolStripComboBox)sender;
			if (control.SelectedItem != null)
			{
				string fontName = control.SelectedItem.ToString();
				Thread synchronizeToActiveFont = new Thread(new ThreadStart(delegate()
				{
					//
					this.Invoke((MethodInvoker)delegate
					{
						this.Focus();
						Application.DoEvents();
					});
					//
					ActiveFont = BitmapFont.Construct(fontName);
				}));
				synchronizeToActiveFont.Name = "Synchronize to Active Font";
				synchronizeToActiveFont.Start();
			}
		}

		SaveCopyAs saveCopyAsDialog = new SaveCopyAs();
		private void saveCopyAsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			saveCopyAsDialog.FontName = "";
			if (DialogResult.OK == saveCopyAsDialog.ShowDialog())
			{
				saveCopyAsDialog.Hide();
				Application.DoEvents();
				//
				string saveFontAs = saveCopyAsDialog.FontName;
				string fileName = SupportFunctions.Combine("fonts", saveFontAs);
				bool validFilename = false;
				try
				{
					new System.IO.FileInfo(fileName);
					validFilename = true;
				}
				catch (ArgumentException) { }
				catch (System.IO.PathTooLongException) { }
				catch (NotSupportedException) { }
				//
				if (validFilename && saveFontAs != ActiveFont.Name)
				{
					if (Directory.Exists(fileName))
					{
						try
						{
							Directory.Delete(fileName);
						}
						catch { }
					}
					SupportFunctions.CopyDirectory(ActiveFont.Path, fileName);
					// Load XML in.
					BitmapFont copiedFont = BitmapFont.Construct(saveFontAs);
					if (copiedFont != null)
					{
						// Rewrite name.
						copiedFont.Name = saveFontAs;
						// Save XML.
						copiedFont.Save();
						// Dispose.
						copiedFont = null;
					}
					//
					reloadFontComboBox(this);
				}
			}
		}

		private void exportZIPArchiveToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Application.DoEvents();
			//
			if (ActiveFont != null)
			{
				SaveFileDialog dialog = new SaveFileDialog();
				dialog.CheckFileExists = false;
				dialog.CheckPathExists = true;
				dialog.DefaultExt = "zip";
				dialog.FileName = string.Format("{0}.zip", ActiveFont.Name);
				dialog.Filter = "Zip Archives (*.zip)|*.zip";
				dialog.Title = "Save Font As Zip Archive As...";
				dialog.ValidateNames = true;
				if (DialogResult.OK == dialog.ShowDialog())
				{
					Application.DoEvents();
					//
					string targetFile = dialog.FileName;
					Plugins.ZippedFont zippedFont = new Plugins.ZippedFont(ActiveFont);
					Plugins.IPlugin plugin = (Plugins.IPlugin)zippedFont;
					string temporaryFile = plugin.Save();
					if (temporaryFile != null && File.Exists(temporaryFile))
					{
						setStatus(string.Format("Exporting \"{0}\" to ZIP file: {1}", new object[] { ActiveFont.Name, targetFile }), this);
						try
						{
							File.Move(temporaryFile, targetFile);
						}
						catch { }
						setStatus("Ready.", this);
					}
				}
			}
		}

		private void importZIPArchiveToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Application.DoEvents();
			//
			OpenFileDialog dialog = new OpenFileDialog();
			dialog.CheckFileExists = false;
			dialog.CheckPathExists = true;
			dialog.DefaultExt = "zip";
			dialog.Filter = "Zip Archives (*.zip)|*.zip";
			dialog.Multiselect = false;
			dialog.Title = "Save Zip Archive As...";
			if (DialogResult.OK == dialog.ShowDialog())
			{
				Application.DoEvents();
				//
				string sourceFile = dialog.FileName;
				Plugins.ZippedFont zippedFont = new Plugins.ZippedFont(sourceFile);
				Plugins.IPlugin plugin = (Plugins.IPlugin)zippedFont;
				BitmapFont font = plugin.Acquire();
				reloadFontComboBox(this, forcedSelection: font.Name);
				//selectOrReselectFontInComboBox(this, previousSelection: font.Name);
			}
		}

		private void exportPanda3DEGGToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Application.DoEvents();
			//
			Plugins.EGGFont eggFont = new Plugins.EGGFont(ActiveFont);
			launchPlugin((Plugins.IPlugin)eggFont);
		}


		private void textureBasedPanda3DEGGToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Application.DoEvents();
			//
			Plugins.TextureBasedEGGFont eggFont = new Plugins.TextureBasedEGGFont(ActiveFont);
			launchPlugin((Plugins.IPlugin)eggFont);
		}

		private void embeddedTexturePanda3DBAMToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Application.DoEvents();
			//
			Plugins.EmbeddedTextureBAMFont bamFont = new Plugins.EmbeddedTextureBAMFont(ActiveFont);
			launchPlugin((Plugins.IPlugin)bamFont, threadName: "Export Font to BAM");
		}

		private void launchPlugin(Plugins.IPlugin plugin, string threadName = "Export Font to EGG")
		{
			//
			if (ActiveFont != null)
			{
				ExportSettings dialog = new ExportSettings(ActiveFont);
				Plugins.Configuration configuration = Plugins.Configuration.Empty;
				if (DialogResult.OK == dialog.ShowDialog())
				{
					Application.DoEvents();
					//
					configuration = dialog.Configuration;
				}
				dialog.Dispose();
				// Make it happen.
				if (configuration.Path != null)
				{
					Main m = this;
					Thread exportThread = new Thread(new ThreadStart(delegate()
					{
						//
						setStatus("Exporting fonts...", m);
						//
						SupportFunctions.ProgressUpdater updater = delegate(string s)
						{
							setStatus(s, m);
						};
						string temporaryDirectory = plugin.Save(configuration, updater);
						// Copy.
						setStatus("Copying exported fonts to output directory...", m);
						SupportFunctions.CopyDirectory(temporaryDirectory, configuration.Path);
						// Delete.
						setStatus("Deleting temporary workspace folder...", m);
						SupportFunctions.TryDelete(temporaryDirectory);
						//
						setStatus("Ready.", m);
					}));
					exportThread.Name = threadName;
					exportThread.Start();
				}
			}
		}

		private void importPanda3dEGGToolStripMenuItem_Click(object sender, EventArgs e)
		{
		}

		private void proofingToolToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Application.DoEvents();
			//
			VirtualKeyboard dialog = new VirtualKeyboard(this);
			dialog.ShowDialog(this);
			dialog.Dispose();
		}

		private void Main_FormClosing(object sender, FormClosingEventArgs e)
		{
			disposeActiveFileListeners();
		}

		private void guidedToolStripMenuItem_Click(object sender, EventArgs e)
		{
			BitmapFont font = null;
			if (font == null)
			{
				GuidedFontCreation dlg = new GuidedFontCreation();
				if (DialogResult.OK == dlg.ShowDialog())
				{
					Application.DoEvents();
					//
					font = new BitmapFont();
					font.Name = dlg.FontName;
					font.AddSize(dlg.DesiredLineHeight);
					FontInSize size = font.Sizes[0];
					size.TargetAscent = (int)Math.Ceiling(0.8 * dlg.DesiredLineHeight);
					size.TargetDescent = (int)Math.Ceiling(0.2 * dlg.DesiredLineHeight);
					size.DefaultWidth = (int)Math.Ceiling(0.6 * dlg.DesiredLineHeight);
					//
					System.Collections.Specialized.OrderedDictionary variants = dlg.Variants;
					foreach (System.Collections.DictionaryEntry entry in variants)
					{
						BitmapVariant variant = size.AddVariant((string)entry.Key);
						BitmapEffect effect = (BitmapEffect)entry.Value;
						if (effect != null)
							variant.Effects.Add(effect);
					}
					//
					System.Collections.Specialized.OrderedDictionary colors = dlg.Colors;
					foreach (System.Collections.DictionaryEntry entry in colors)
					{
						BitmapColor color = font.AddColor((string)entry.Key);
						BitmapEffect effect = (BitmapEffect)entry.Value;
						if (effect != null)
							color.Effects.Add(effect);
					}
				}
				dlg.Dispose();
			}
			// Add new font dialog if font is not null. Otherwise, nothing.
			if (font != null) {
				//
				AddOrEditFont dlg = new AddOrEditFont();
				dlg.Text = "Add New Font";
				dlg.Font = font;
				if (DialogResult.OK == dlg.ShowDialog())
				{
					Application.DoEvents();
					//
					bool activeFontIsntNewFont = true;
					// Save over it.
					ActiveFont = dlg.Font;
					saveActiveFont(reloadComboAfter: activeFontIsntNewFont);
				}
			}
		}

		private void glyphContextMenuStrip_Opening(object sender, System.ComponentModel.CancelEventArgs e)
		{
            glyphContextMenuStrip.Enabled = (ActiveFont != null) ? true : false;
            renameToolStripMenuItem.Enabled = (CanSaveToActiveFontSize) ? true : false;
		}

        private void unicodeBlockContextMenuStrip_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            unicodeBlockContextMenuStrip.Enabled = (ActiveFont != null) ? true : false;
        }
    }
}
