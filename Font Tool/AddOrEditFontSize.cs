using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;

namespace FontTool
{
	public partial class AddOrEditFontSize : Form
	{
		private FontInSize replacementFontInSize = new FontInSize();
		public FontInSize FontInSize {
			get {
				return replacementFontInSize;
			}
			set {
				bool changed = (value != replacementFontInSize) ? true : false;
				replacementFontInSize = value;
				if (changed)
					synchronizeFormWithFontInSize();
			}
		}

		private BitmapVariant SelectedVariant {
			get {
				if (variantsListView.SelectedIndices.Count > 0)
				{
					ListViewItem item = variantsListView.SelectedItems[0];
					string name = (string)item.Tag;
					if (replacementFontInSize.Variants.Contains(name))
						return replacementFontInSize.Variants[name];
				}
				return null;
			}
		}

		private volatile List<Color> usedColors = new List<Color>();
		public List<Color> UsedColors
		{
			get { return usedColors; }
			set { usedColors = value; }
		}

		public AddOrEditFontSize()
		{
			InitializeComponent();
		}

		public void synchronizeFormWithFontInSize()
		{
			if (replacementFontInSize != null)
			{
				sizeMaskedTextBox.Text = string.Format("{0}", replacementFontInSize.Size);
				targetAscentMaskedTextBox.Text = string.Format("{0}", replacementFontInSize.TargetAscent);
				targetDescentMaskedTextBox.Text = string.Format("{0}", replacementFontInSize.TargetDescent);
				targetWidthMaskedTextBox.Text = string.Format("{0}", replacementFontInSize.DefaultWidth);
				//
				variantsListView.Items.Clear();
				if (replacementFontInSize.Variants != null)
				{
					foreach (BitmapVariant variant in replacementFontInSize.Variants)
					{
						// Add.
						ListViewItem thisItem = new ListViewItem(new string[] {
						string.Format("{0}", variant.Name),
						string.Format("{0} effect{1} in stack.", variant.Effects.Count, variant.Effects.Count > 1 ? "s" : "")
					});
						thisItem.Tag = variant.Name;
						variantsListView.Items.Add(thisItem);
					}
				}
			}
		}

		private void sizeMaskedTextBox_TextChanged(object sender, EventArgs e)
		{
			MaskedTextBox control = (MaskedTextBox)sender;
			if (control != null)
			{
				float value = 0f;
				bool parsed = float.TryParse(control.Text, out value);
				if (parsed)
					replacementFontInSize.Size = value;
			}
		}

		private void targetAscentMaskedTextBox_TextChanged(object sender, EventArgs e)
		{
			MaskedTextBox control = (MaskedTextBox)sender;
			if (control != null)
			{
				float value = 0f;
				bool parsed = float.TryParse(control.Text, out value);
				if (parsed)
					replacementFontInSize.TargetAscent = value;
			}
		}

		private void targetDescentMaskedTextBox_TextChanged(object sender, EventArgs e)
		{
			MaskedTextBox control = (MaskedTextBox)sender;
			if (control != null)
			{
				float value = 0f;
				bool parsed = float.TryParse(control.Text, out value);
				if (parsed)
					replacementFontInSize.TargetDescent = value;
			}
		}

		private void targetWidthMaskedTextBox_TextChanged(object sender, EventArgs e)
		{
			MaskedTextBox control = (MaskedTextBox)sender;
			if (control != null)
			{
				float value = 0f;
				bool parsed = float.TryParse(control.Text, out value);
				if (parsed)
					replacementFontInSize.DefaultWidth = value;
			}
		}

		private void addVariantToolStripMenuItem_Click(object sender, EventArgs e)
		{
			AddOrEditSizeVariant dlg = new AddOrEditSizeVariant();
			dlg.Text = "Add New Variant";
			dlg.UsedColors = UsedColors;
			if (DialogResult.OK == dlg.ShowDialog())
			{
				dlg.Hide();
				//
				if (FontInSize.Variants == null)
					FontInSize.Variants = new BitmapVariantCollection();
				FontInSize.Variants.Add(dlg.BitmapVariant);
				synchronizeFormWithFontInSize();
			}
			dlg.Dispose();
		}

		private void editVariantToolStripMenuItem_Click(object sender, EventArgs e)
		{
			AddOrEditSizeVariant dlg = new AddOrEditSizeVariant();
			dlg.Text = string.Format("Edit \"{0}\" Variant", SelectedVariant.Name);
			dlg.UsedColors = UsedColors;
			string originalKey = SelectedVariant.Name;
			dlg.BitmapVariant = new BitmapVariant(SelectedVariant);
			if (DialogResult.OK == dlg.ShowDialog())
			{
				dlg.Hide();
				//
				string newKey = dlg.BitmapVariant.Name;
				// If the key already exists, remove it.
				if (replacementFontInSize.Variants.Contains(newKey))
					replacementFontInSize.Variants.Remove(newKey);
				// If the keys are different, remove the original key.
				if (originalKey != newKey && replacementFontInSize.Variants.Contains(originalKey))
					replacementFontInSize.Variants.Remove(originalKey);
				// Finally, push the data.
				replacementFontInSize.Variants.Add(dlg.BitmapVariant);
				//
				synchronizeFormWithFontInSize();
			}
			dlg.Dispose();
		}

		private void removeVariantToolStripMenuItem_Click(object sender, EventArgs e)
		{
			replacementFontInSize.Variants.Remove(SelectedVariant.Name);
			synchronizeFormWithFontInSize();
		}

		private void variantsContextMenuStrip_Opening(object sender, CancelEventArgs e)
		{
			if (SelectedVariant != null)
			{
				editVariantToolStripMenuItem.Enabled = true;
				removeVariantToolStripMenuItem.Enabled = true;
			}
			else
			{
				editVariantToolStripMenuItem.Enabled = false;
				removeVariantToolStripMenuItem.Enabled = false;
			}
		}

		private void saveButton_Click(object sender, EventArgs e)
		{
			Application.DoEvents();
			//
			DialogResult = DialogResult.OK;
		}

		private void AddOrEditFontSize_Shown(object sender, EventArgs e)
		{
			//
			if (FontInSize != null && FontInSize.Size > 0 && FontInSize.Font != null)
			{
				Thread thread = new Thread(new ThreadStart(delegate
				{
					UsedColors = replacementFontInSize.GetUsedColors();
				}));
				thread.Name = string.Format("Find Used Colors in Size ({0}px {1})", new object[] { replacementFontInSize.Size, replacementFontInSize.Font.Name });
				thread.Start();
			}
		}
	}
}
