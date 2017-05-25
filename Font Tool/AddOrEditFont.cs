using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace FontTool
{
	public partial class AddOrEditFont : Form
	{
		private BitmapFont replacementFont = new BitmapFont();

		public new BitmapFont Font
		{
			get
			{
				return replacementFont;
			}
			set
			{
				BitmapFont oldValue = replacementFont;
				replacementFont = value;
				if (oldValue != value)
					synchronizeFormWithFont();
			}
		}

		public string FontName {
			get {
				return (replacementFont != null) ? replacementFont.Name : null;
			}
			set {
				replacementFont.Name = value;
				synchronizeFormWithFont();
			}
		}

		public SizeCollection Sizes {
			get
			{
				return replacementFont.Sizes;
			}
			set {
				replacementFont.Sizes = value;
				synchronizeFormWithFont();
			}
		}

		public BitmapColorCollection Colors {
			get
			{
				return replacementFont.Colors;
			}
			set {
				replacementFont.Colors = value;
				synchronizeFormWithFont();
			}
		}

		private FontInSize SelectedSize {
			get {
				if (sizesListView.SelectedIndices.Count > 0 && replacementFont.Sizes.Count > 0)
				{
					ListViewItem item = sizesListView.SelectedItems[0];
					float? value = (float?)item.Tag;
					if (replacementFont.Sizes.Contains(value))
						return replacementFont.Sizes[value];
				}
				return null;
			}
		}

		private BitmapColor SelectedColor {
			get {
				if (colorsListView.SelectedIndices.Count > 0 && replacementFont.Colors.Count > 0)
				{
					ListViewItem item = colorsListView.SelectedItems[0];
					string value = (string)item.Tag;
					if (replacementFont.Colors.Contains(value))
						return replacementFont.Colors[value];
				}
				return null;
			}
		}

		private void synchronizeFormWithFont()
		{
			nameMaskedTextBox.Text = FontName;
			//
			colorsListView.Items.Clear();
			if (Colors != null)
			{
				foreach (BitmapColor color in Colors)
				{
					ListViewItem thisItem = new ListViewItem(new string[] { color.Name, string.Format("{0} {1}.", (color.Effects != null) ? color.Effects.Count : 0, (color.Effects != null && color.Effects.Count == 1) ? "effect" : "effects") });
					thisItem.Tag = color.Name;
					colorsListView.Items.Add(thisItem);
				}
			}
			//
			sizesListView.Items.Clear();
			if (Sizes != null)
			{
				foreach (FontInSize size in Sizes)
				{
					// Add.
					int count = (size.Characters != null) ? size.Characters.Count : 0;
					string word = (count == 1) ? "Character" : "Characters";
					ListViewItem thisItem = new ListViewItem(new string[] { 
						string.Format("{0}", size.Size),
						string.Format("{0} {1}", new object[] { count, word})
					});
					thisItem.Tag = size.Size;
					sizesListView.Items.Add(thisItem);
				}
			}
		}

		//
		private volatile List<Color> usedColors = new List<Color>();
		public List<Color> UsedColors
		{
			get { return usedColors; }
			set { usedColors = value; }
		}

		public AddOrEditFont()
		{
			InitializeComponent();
		}

		public AddOrEditFont(string fontName) : this() {
			Font = BitmapFont.Construct(fontName);
		}

		private void AddOrEditFont_Shown(object sender, EventArgs e)
		{
			//
			if (Sizes != null)
			{
				Thread thread = new Thread(new ThreadStart(delegate
				{
					foreach (FontInSize size in Sizes)
					{
						List<Color> thisUsedColors = size.GetUsedColors();
						foreach (Color color in thisUsedColors)
							if (!UsedColors.Contains(color))
								UsedColors.Add(color);
					}
					foreach (BitmapColor bitmapColor in Colors) {
						List<Color> thisUsedColors = bitmapColor.GetUsedColors();
						foreach (Color color in thisUsedColors)
							if (!UsedColors.Contains(color))
								UsedColors.Add(color);
					}
				}));
				thread.Name = string.Format("Find Used Colors for Font ({0})", new object[] { replacementFont.Name });
				thread.Start();
			}
		}

		private void colorsContextMenuStrip_Opening(object sender, CancelEventArgs e)
		{
			removeColorToolStripMenuItem.Enabled = editColorToolStripMenuItem.Enabled = (colorsListView.SelectedItems.Count > 0) ? true : false;
		}

		private void addColorToolStripMenuItem_Click(object sender, EventArgs e)
		{
			AddOrEditBitmapColor dlg = new AddOrEditBitmapColor();
			dlg.Text = "Add New Color";
			dlg.UsedColors = UsedColors;
			if (DialogResult.OK == dlg.ShowDialog())
			{
				dlg.Hide();
				//
				if (Colors == null)
					Colors = new BitmapColorCollection();
				Colors.Add(dlg.BitmapColor);
				synchronizeFormWithFont();
			}
			dlg.Dispose();
		}

		private void editColorToolStripMenuItem_Click(object sender, EventArgs e)
		{
			AddOrEditBitmapColor dlg = new AddOrEditBitmapColor();
			dlg.Text = "Edit Color";
			dlg.UsedColors = UsedColors;
			string originalKey = SelectedColor.Name;
			dlg.BitmapColor = new BitmapColor(SelectedColor);
			if (DialogResult.OK == dlg.ShowDialog())
			{
				dlg.Hide();
				//
				string newKey = dlg.BitmapColor.Name;
				// If the key already exists, remove it.
				if (replacementFont.Colors.Contains(newKey))
					replacementFont.Colors.Remove(newKey);
				// If the keys are different, remove the original key.
				if (originalKey != newKey && replacementFont.Colors.Contains(originalKey))
					replacementFont.Colors.Remove(originalKey);
				// Finally, push the data.
				replacementFont.Colors.Add(dlg.BitmapColor);
				//
				synchronizeFormWithFont();
			}
			dlg.Dispose();
		}

		private void removeColorToolStripMenuItem_Click(object sender, EventArgs e)
		{
			replacementFont.Colors.Remove(SelectedColor.Name);
			synchronizeFormWithFont();
		}

		private void sizesContextMenuStrip_Opening(object sender, CancelEventArgs e)
		{
			removeSizeToolStripMenuItem.Enabled = editSizeToolStripMenuItem.Enabled = viewCoverageReportToolStripMenuItem.Enabled = (sizesListView.SelectedItems.Count > 0) ? true : false;
		}

		private void addSizeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			AddOrEditFontSize dlg = new AddOrEditFontSize();
			dlg.Text = "Add New Font Size";
			if (DialogResult.OK == dlg.ShowDialog())
			{
				dlg.Hide();
				if (replacementFont.Sizes == null)
					replacementFont.Sizes = new SizeCollection();
				//
				float newKey = dlg.FontInSize.Size;
				// If the key already exists, remove it.
				if (replacementFont.Sizes.Contains(newKey))
					replacementFont.Sizes.Remove(newKey);
				// Push the data.
				replacementFont.Sizes.Add(dlg.FontInSize);
				// Finally, try to look for existing images based on the name.
				if (replacementFont.Name != null)
				{
					FontInSize fontInSize = replacementFont.Sizes[newKey];
					fontInSize.Characters = fontInSize.GetCharactersFromImageNames();
					System.Console.WriteLine(replacementFont.Sizes[newKey].Characters.Count);
				}
				//
				synchronizeFormWithFont();
			}
			dlg.Dispose();
		}

		private void editSizeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			AddOrEditFontSize dlg = new AddOrEditFontSize();
			dlg.Text = "Edit Font Size";
			float originalKey = SelectedSize.Size;
			dlg.FontInSize = new FontInSize(SelectedSize);
			if (DialogResult.OK == dlg.ShowDialog())
			{
				dlg.Hide();
				//
				float newKey = dlg.FontInSize.Size;
				string possibleExistingPath = Path.Combine(dlg.FontInSize.Font.Path, string.Format("{0}", originalKey));
				string destinationPath = dlg.FontInSize.Path;
				// If the key already exists, remove it.
				if (replacementFont.Sizes.Contains(newKey))
					replacementFont.Sizes.Remove(newKey);
				// If the keys are different, remove the original key.
				if (originalKey != newKey && replacementFont.Sizes.Contains(originalKey))
				{
					replacementFont.Sizes.Remove(originalKey);
					// If it exists, rename font directory.
					if (Directory.Exists(possibleExistingPath))
					{
						if (Directory.Exists(destinationPath))
							Directory.Delete(destinationPath, true);
						Directory.Move(possibleExistingPath, destinationPath);
					}
				}
				// Finally, push the data.
				replacementFont.Sizes.Add(dlg.FontInSize);
				//
				synchronizeFormWithFont();
			}
			dlg.Dispose();
		}

		private void removeSizeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			replacementFont.Sizes.Remove(SelectedSize.Size);
			synchronizeFormWithFont();
		}

		private void saveButton_Click(object sender, EventArgs e)
		{
			Application.DoEvents();
			//
			DialogResult = DialogResult.OK;
		}

		private void nameMaskedTextBox_TextChanged(object sender, EventArgs e)
		{
			MaskedTextBox control = (MaskedTextBox)sender;
			if (control != null)
				FontName = control.Text;
		}

		private void viewCoverageReportToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Application.DoEvents();
			//
			UnicodeCoverageReportViewer dlg = new UnicodeCoverageReportViewer(SelectedSize.Characters.Keys);
			if (dlg.ShowDialog() == DialogResult.OK) {
				
			}
		}

		private void nameMaskedTextBox_ModifiedChanged(object sender, EventArgs e)
		{
			replacementFont.Name = nameMaskedTextBox.Text;
		}
	}
}
