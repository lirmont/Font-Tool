using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace FontTool
{
	public partial class ExportSettings : Form
	{
		private BitmapFont bitmapFont;

		public BitmapFont BitmapFont
		{
			get { return bitmapFont; }
			set { bitmapFont = value; }
		}

		public string Path {
			get {
				return exportDirectoryMaskedTextBox.Text;
			}
			set {
				exportDirectoryMaskedTextBox.Text = value;
			}
		}

		public decimal HorizontalScale {
			get
			{
				return horizontalMultiplierNumericUpDown.Value * widthOfAbstractionNumericUpDown.Value / widthNumericUpDown.Value;
			}
		}

		public decimal VerticalScale
		{
			get {
				return verticalMultiplierNumericUpDown.Value * heightOfAbstractionNumericUpDown.Value / heightNumericUpDown.Value;
			}
		}

		public string MinificationFilter
		{
			get {
				return (minificationNearestRadioButton.Checked) ? "NEAREST" : "LINEAR";
			}
		}

		public string MagnificationFilter
		{
			get
			{
				return (magnificationNearestRadioButton.Checked) ? "NEAREST" : "LINEAR";
			}
		}

		public int AnistropicDegree
		{
			get {
				return (int)anistropicDegreeNumericUpDown.Value;
			}
		}

		public int CharactersPerFile
		{
			get {
				return (int)charactersPerFileNumericUpDown.Value;
			}
		}

		public List<BitmapColor> Colors
		{
			get {
				List<BitmapColor> list = new List<BitmapColor>();
				foreach (CheckBox box in colors)
				{
					if (box.Enabled && box.Checked) {
						BitmapColor color = (BitmapColor)box.Tag;
						if (color != null)
							list.Add(color);
					}
				}
				return list;
			}
		}

		public List<BitmapVariant> Variants
		{
			get {
				List<BitmapVariant> list = new List<BitmapVariant>();
				foreach (CheckBox box in variants)
				{
					if (box.Enabled && box.Checked)
					{
						BitmapVariant variant = (BitmapVariant)box.Tag;
						if (variant != null)
							list.Add(variant);
					}
				}
				return list;
			}
		}

		public bool WantMIPMapsToSmallerFonts
		{
			get {
				return (wantMipMapsCheckBox.Enabled) ? wantMipMapsCheckBox.Checked : false;
			}
		}

		public Plugins.Configuration Configuration
		{
			get {
				return new Plugins.Configuration(Path, HorizontalScale, VerticalScale, MinificationFilter, MagnificationFilter, AnistropicDegree, Variants, Colors, WantMIPMapsToSmallerFonts, charactersPerFile: CharactersPerFile);
			}
		}

		List<CheckBox> sizes = new List<CheckBox>();
		List<CheckBox> colors = new List<CheckBox>();
		List<CheckBox> variants = new List<CheckBox>();

		public ExportSettings(BitmapFont font)
		{
			BitmapFont = font;
			//
			InitializeComponent();
			// Set path from user configuration.
			Path = Properties.Settings.Default.lastExportDirectory;
			//
			horizontalMultiplierNumericUpDown.Value = Properties.Settings.Default.widthMultiplier;
			widthOfAbstractionNumericUpDown.Value = Properties.Settings.Default.widthOfAbstraction;
			widthNumericUpDown.Value = Properties.Settings.Default.widthOfScreen;
			//
			verticalMultiplierNumericUpDown.Value = Properties.Settings.Default.heightMultiplier;
			heightOfAbstractionNumericUpDown.Value = Properties.Settings.Default.heightOfAbstraction;
			heightNumericUpDown.Value = Properties.Settings.Default.heightOfScreen;
			//
			minificationNearestRadioButton.Checked = (Properties.Settings.Default.minificationFilter == "NEAREST") ? true : false;
			minificationLinearRadioButton.Checked = (Properties.Settings.Default.minificationFilter == "LINEAR") ? true : false;
			magnificationNearestRadioButton.Checked = (Properties.Settings.Default.magnificationFilter == "NEAREST") ? true : false;
			magnificationLinearRadioButton.Checked = (Properties.Settings.Default.magnificationFilter == "LINEAR") ? true : false;
			anistropicDegreeNumericUpDown.Value = Properties.Settings.Default.anistropicFiltering;
			charactersPerFileNumericUpDown.Value = Properties.Settings.Default.charactersPerFile;
			// Do MIP maps.
			if (font.Sizes.Count <= 1)
				wantMipMapsCheckBox.Enabled = false;
			else
			{
				wantMipMapsCheckBox.Checked = Properties.Settings.Default.wantMipMapToSmallerFonts;
				wantMipMapsCheckBox.Enabled = true;
			}
			// Generate the controls related to the font.
			foreach (FontInSize fontInSize in font.Sizes) {
				CheckBox box = new CheckBox();
				box.Text = string.Format("{0}", fontInSize.Size);
				box.Checked = true;
				box.Tag = fontInSize;
				box.AutoSize = true;
				box.Anchor = AnchorStyles.Left;
				sizesFlowLayoutPanel.Controls.Add(box);
				box.CheckedChanged += new EventHandler(sizeBox_CheckedChanged);
				//
				sizes.Add(box);
				//
				foreach (BitmapVariant variant in fontInSize.Variants)
				{
					CheckBox variantBox = new CheckBox();
					if (font.Sizes.Count > 1)
						variantBox.Text = string.Format("{0}px - {1}", new object[] { fontInSize.Size, variant.Name });
					else
						variantBox.Text = string.Format("{0}", variant.Name);
					variantBox.Checked = true;
					variantBox.Tag = variant;
					variantBox.AutoSize = true;
					variantBox.Anchor = AnchorStyles.Left;
					variantsFlowLayoutPanel.Controls.Add(variantBox);
					//
					variants.Add(variantBox);
				}
			}
			//
			foreach (BitmapColor color in font.Colors)
			{
				CheckBox box = new CheckBox();
				box.Text = string.Format("{0}", color.Name);
				box.Checked = true;
				box.Tag = color;
				box.AutoSize = true;
				box.Anchor = AnchorStyles.Left;
				colorsFlowLayoutPanel.Controls.Add(box);
				//
				colors.Add(box);
			}
		}

		void sizeBox_CheckedChanged(object sender, EventArgs e)
		{
			CheckBox box = (CheckBox)sender;
			if (box != null) {
				FontInSize size = (FontInSize)box.Tag;
				if (size != null) {
					bool noOtherSizes = sizes.Count == 1;
					foreach (CheckBox thisBox in variants) {
						BitmapVariant variant = (BitmapVariant)thisBox.Tag;
						if (noOtherSizes || size == variant.Dependency)
							thisBox.Enabled = box.Checked;
					}
					bool noSizes = sizes.TrueForAll(item => item.Checked == false);
					if (noSizes)
					{
						foreach (CheckBox thisBox in colors)
							thisBox.Enabled = false;
					}
					else {
						foreach (CheckBox thisBox in colors)
							thisBox.Enabled = true;
					}
				}
			}
		}

		private void browseButton_Click(object sender, EventArgs e)
		{
			FolderBrowserDialog dialog = new FolderBrowserDialog();
			dialog.ShowNewFolderButton = true;
			dialog.Description = string.Format("Select location to save the Panda3d font models at for the font, \"{0}\".", BitmapFont.Name);
			if (System.IO.Directory.Exists(Path))
				dialog.SelectedPath = Path;
			string targetDirectory = null;
			if (DialogResult.OK == dialog.ShowDialog())
			{
				Application.DoEvents();
				//
				targetDirectory = dialog.SelectedPath;
			}
			dialog.Dispose();
			// Make it happen.
			if (targetDirectory != null)
				Path = targetDirectory;
		}

		private void okButton_Click(object sender, EventArgs e)
		{
			if (Path == "" || !Directory.Exists(Path))
				exportDirectoryMaskedTextBox.MarkInvalid = true;
			else
			{
				// Save export directory to config.
				Properties.Settings.Default.lastExportDirectory = Path;
				//
				Properties.Settings.Default.widthMultiplier = horizontalMultiplierNumericUpDown.Value;
				Properties.Settings.Default.widthOfAbstraction = widthOfAbstractionNumericUpDown.Value;
				Properties.Settings.Default.widthOfScreen = (uint)widthNumericUpDown.Value;
				//
				Properties.Settings.Default.heightMultiplier = verticalMultiplierNumericUpDown.Value;
				Properties.Settings.Default.heightOfAbstraction = heightOfAbstractionNumericUpDown.Value;
				Properties.Settings.Default.heightOfScreen = (uint)heightNumericUpDown.Value;
				//
				Properties.Settings.Default.minificationFilter = (minificationNearestRadioButton.Checked) ? "NEAREST" : "LINEAR";
				Properties.Settings.Default.magnificationFilter = (magnificationNearestRadioButton.Checked) ? "NEAREST" : "LINEAR";
				if (wantMipMapsCheckBox.Enabled)
					Properties.Settings.Default.wantMipMapToSmallerFonts = wantMipMapsCheckBox.Checked;
				Properties.Settings.Default.anistropicFiltering = (uint)anistropicDegreeNumericUpDown.Value;
				Properties.Settings.Default.charactersPerFile = (int)charactersPerFileNumericUpDown.Value;
				//
				Properties.Settings.Default.Save();
				DialogResult = System.Windows.Forms.DialogResult.OK;
			}
		}

		private void optimizeScaleForPVIEWToolStripMenuItem_Click(object sender, EventArgs e)
		{
			horizontalMultiplierNumericUpDown.Value = 1;
			widthOfAbstractionNumericUpDown.Value = 8;
			widthNumericUpDown.Value = 180;
			verticalMultiplierNumericUpDown.Value = 1;
			heightOfAbstractionNumericUpDown.Value = 9;
			heightNumericUpDown.Value = 200;
		}

		private void optimizeScaleForScreenToolStripMenuItem_Click(object sender, EventArgs e)
		{
			horizontalMultiplierNumericUpDown.Value = 1;
			widthOfAbstractionNumericUpDown.Value = 3.5604m;
			widthNumericUpDown.Value = 1920;
			verticalMultiplierNumericUpDown.Value = 1;
			heightOfAbstractionNumericUpDown.Value = 2;
			heightNumericUpDown.Value = 1080;
		}
	}
}
