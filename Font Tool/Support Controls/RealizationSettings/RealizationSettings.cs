using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace TextAnalyzer
{
	public partial class RealizationSettings : Form
	{
		public List<FontStyle> wantStyles = new List<FontStyle>();

		private Bitmap clippedBitmap;
		private int? extraAscentMarker = null;

		public Bitmap ClippedBitmap
		{
			get { return clippedBitmap; }
			set { clippedBitmap = value; }
		}

		public int? ExtraAscentMarker
		{
			get { return extraAscentMarker; }
			set { extraAscentMarker = value; }
		}

		public string ReferenceText
		{
			get { return referenceTextBox.Text; }
			set { referenceTextBox.Text = value; }
		}

		public RealizationSettings()
		{
			InitializeComponent();
			//
			wantStyles = new List<FontStyle> {
				FontStyle.Regular,
				FontStyle.Bold,
				FontStyle.Italic,
				FontStyle.Strikeout,
				FontStyle.Underline
			};
			fontComboBox.DataSource = FontFamily.Families;
			fontComboBox.DisplayMember = "Name";
			//
			linesPictureBox.BackColor = referenceColorPanel.BackColor;
		}

		public RealizationSettings(int? extraAscentMarker = null, float? size = null, Color? antiAliasMatteColor = null)
			: this()
		{
			//
			ExtraAscentMarker = extraAscentMarker;
			if (size != null)
				sizeNumericUpDown.Value = (decimal)size.Value;
			if (antiAliasMatteColor != null)
				matteColorPanel.BackColor = antiAliasMatteColor.Value;
		}

		public void SetFontByName(string name)
		{
			bool exists = Array.Exists<FontFamily>(FontFamily.Families, item => item.Name == name);
			if (exists)
			{
				FontFamily f = new FontFamily(name);
				int index = fontComboBox.Items.IndexOf(f);
				if (index >= 0)
					fontComboBox.SelectedIndex = index;
			}
			else {
				fontComboBox.SelectedIndex = 0;
			}
		}

		private void fontComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			FontFamily family = (FontFamily)fontComboBox.SelectedItem;
			List<FontStyle> haveStyles = new List<FontStyle>();
			foreach (FontStyle style in wantStyles) {
				if (family.IsStyleAvailable(style))
					haveStyles.Add(style);
			}
			List<FontStyle> oldDataSource = (List<FontStyle>)variantComboBox.DataSource;
			if (oldDataSource == null || oldDataSource.Count != haveStyles.Count || !oldDataSource.TrueForAll(item => haveStyles.Contains(item) == true))
			{
				variantComboBox.DataSource = haveStyles;
			}
			else {
				synchronizeImages();
			}
		}

		private void variantComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			synchronizeImages();
		}

		public RealizationConfiguration Configuration
		{
			get {
				FontFamily family = (FontFamily)fontComboBox.SelectedItem;
				float emSizeInGU = (float)sizeNumericUpDown.Value;
				FontStyle style = (FontStyle)variantComboBox.SelectedItem;
				GraphicsUnit unit = GraphicsUnit.Pixel;
				Font font = new Font(family, emSizeInGU, style, unit);
				//
				return new RealizationConfiguration(font, antialiased: antialiasedCheckBox.Checked, alternateLayout: alternateCheckBox.Checked, antialiasMatteColor: matteColorPanel.BackColor);
			}
		}

		private void synchronizeImages() {
			string controlText = referenceTextBox.Text;
			RealizationConfiguration config = Configuration;
			Bitmap left = config.GetAnnotatedCharacterImageFromText(controlText, extraAscentMarker: extraAscentMarker);
			Image previousImage = linesPictureBox.Image;
			linesPictureBox.Image = left;
			linesPictureBox.Size = linesPictureBox.Image.Size;
			if (previousImage != null)
				previousImage.Dispose();
		}

		private void alternateCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			synchronizeImages();
		}

		private void sizeNumericUpDown_ValueChanged(object sender, EventArgs e)
		{
			synchronizeImages();
		}

		private void antialiasedCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			synchronizeImages();
		}

		private void matteColorPanel_Click(object sender, EventArgs e)
		{
			ColorControl.ColorControl dlg = new ColorControl.ColorControl();
			dlg.Color = matteColorPanel.BackColor;
			if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				matteColorPanel.BackColor = dlg.Color;
				if (antialiasedCheckBox.Checked)
					synchronizeImages();
			}
		}

		private void referenceColorPanel_Click(object sender, EventArgs e)
		{
			ColorControl.ColorControl dlg = new ColorControl.ColorControl();
			dlg.Color = referenceColorPanel.BackColor;
			if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				referenceColorPanel.BackColor = dlg.Color;
				linesPictureBox.BackColor = dlg.Color;
			}
		}

		private void fromPointsButton_Click(object sender, EventArgs e)
		{
			PointsToPixels dlg = new PointsToPixels(pixels: (int)sizeNumericUpDown.Value);
			if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				sizeNumericUpDown.Value = dlg.Pixels;
		}

		private void referenceTextBox_TextChanged(object sender, EventArgs e)
		{
			synchronizeImages();
		}

		private void okButton_Click(object sender, EventArgs e)
		{
			DialogResult = System.Windows.Forms.DialogResult.OK;
		}
	}
}
