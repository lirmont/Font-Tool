using System.Collections.Specialized;
using System.Drawing;
using System.Windows.Forms;
using System;

namespace FontTool
{
	public partial class GuidedFontCreation : Form
	{
		public GuidedFontCreation()
		{
			InitializeComponent();
			// Variants.
			regularVariantCheckBox.Tag = null;
			italicVariantCheckBox.Tag = new ItalicizeEffect(3);
			boldVariantCheckBox.Tag = new WidenEffect(1U);
			// Colors.
			regularColorCheckBox.Tag = null;
			whiteColorCheckBox.Tag = new ReplaceColorEffect(Color.Black, Color.GhostWhite);
			redColorCheckBox.Tag = new ReplaceColorEffect(Color.Black, Color.Red);
			orangeColorCheckBox.Tag = new ReplaceColorEffect(Color.Black, Color.Orange);
			yellowColorCheckBox.Tag = new ReplaceColorEffect(Color.Black, Color.Yellow);
			greenColorCheckBox.Tag = new ReplaceColorEffect(Color.Black, Color.LimeGreen);
			blueColorCheckBox.Tag = new ReplaceColorEffect(Color.Black, Color.DodgerBlue);
			purpleColorCheckBox.Tag = new ReplaceColorEffect(Color.Black, Color.Purple);
			pinkColorCheckBox.Tag = new ReplaceColorEffect(Color.Black, Color.Pink);
		}

		public string FontName
		{
			get {
				return (nameMaskedTextBox.Text != null && nameMaskedTextBox.Text.Trim() != "") ? nameMaskedTextBox.Text.Trim() : "";
			}
		}

		public int DesiredLineHeight
		{
			get {
				return (desiredLineHeightMaskedTextBox.Text != null && desiredLineHeightMaskedTextBox.Text != "" && Convert.ToInt32(desiredLineHeightMaskedTextBox.Text, 10) > 0) ? Convert.ToInt32(desiredLineHeightMaskedTextBox.Text, 10) : 0;
			}
		}

		public OrderedDictionary Variants
		{
			get
			{
				// Track requested variants. Entries are in form of: string, BitmapEffect.
				OrderedDictionary variants = new OrderedDictionary();
				foreach (Control child in variantsFlowLayoutPanel.Controls)
				{
					CheckBox box = (CheckBox)child;
					if (box != null && box.Checked)
					{
						BitmapEffect effect = box.Tag as BitmapEffect;
						variants.Add(box.Text, effect);
					}
				}
				return variants;
			}
		}

		public OrderedDictionary Colors
		{
			get
			{
				// Track requested colors. Entries are in form of: string, BitmapEffect.
				OrderedDictionary colors = new OrderedDictionary();
				foreach (Control child in colorsFlowLayoutPanel.Controls)
				{
					CheckBox box = (CheckBox)child;
					if (box != null && box.Checked)
					{
						BitmapEffect effect = box.Tag as BitmapEffect;
						colors.Add(box.Text, effect);
					}
				}
				return colors;
			}
		}

		private void okButton_Click(object sender, System.EventArgs e)
		{
			bool nameIsValidDirectoryName = (FontName != "") ? true : false;
			bool lineHeightIsValid = (DesiredLineHeight > 0) ? true : false;
			nameMaskedTextBox.MarkInvalid = !nameIsValidDirectoryName;
			desiredLineHeightMaskedTextBox.MarkInvalid = !lineHeightIsValid;
			if (nameMaskedTextBox.MarkInvalid || desiredLineHeightMaskedTextBox.MarkInvalid)
				DialogResult = System.Windows.Forms.DialogResult.None;
			else
				DialogResult = System.Windows.Forms.DialogResult.OK;
		}
	}
}
