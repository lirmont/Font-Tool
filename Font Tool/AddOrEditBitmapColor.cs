using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FontTool
{
	public partial class AddOrEditBitmapColor : Form
	{
		public List<Color> UsedColors { get; set; }

		//
		private BitmapColor replacementBitmapColor = new BitmapColor();
		public BitmapColor BitmapColor
		{
			get
			{
				return replacementBitmapColor;
			}
			set
			{
				bool changed = (value != replacementBitmapColor) ? true : false;
				replacementBitmapColor = value;
				if (changed)
					synchronizeFormWithBitmapVariant();
			}
		}

		private KeyValuePair<uint, BitmapEffect>? SelectedEffect
		{
			get
			{
				if (effectsListView.SelectedIndices.Count > 0)
				{
					uint index = (uint)effectsListView.SelectedIndices[0];
					if (replacementBitmapColor.Effects.Count > index)
						return new KeyValuePair<uint,BitmapEffect>(index, replacementBitmapColor.Effects[(int)index]);
				}
				return null;
			}
		}

		public AddOrEditBitmapColor()
		{
			InitializeComponent();
		}

		public void synchronizeFormWithBitmapVariant()
		{
			if (replacementBitmapColor != null)
			{
				nameMaskedTextBox.Text = string.Format("{0}", replacementBitmapColor.Name);
				//
				effectsListView.Items.Clear();
				uint i = 0;
				foreach (BitmapEffect effect in replacementBitmapColor.Effects)
				{
					i++;
					// Add.
					ListViewItem thisItem = new ListViewItem(new string[] {
						string.Format("{0}", i),
						string.Format("{0}", effect.Name),
						string.Format("{0}", effect.Parameters.ToString())
					});
					thisItem.UseItemStyleForSubItems = false;
					thisItem.SubItems[2].Font = new Font(FontFamily.GenericMonospace, thisItem.Font.Size, FontStyle.Regular);
					thisItem.Tag = effect.Name;
					effectsListView.Items.Add(thisItem);
				}
			}
		}

		private void addEffectToolStripMenuItem_Click(object sender, EventArgs e)
		{
			AddOrEditEffect dlg = new AddOrEditEffect(hideTracingOptions: true);
			dlg.Text = "Add New Effect";
			dlg.UsedColors = UsedColors;
			if (DialogResult.OK == dlg.ShowDialog())
			{
				dlg.Hide();
				//
				BitmapColor.Effects.Add(dlg.BitmapEffect);
				synchronizeFormWithBitmapVariant();
			}
			dlg.Dispose();
		}

		private void editEffectToolStripMenuItem_Click(object sender, EventArgs e)
		{
			AddOrEditEffect dlg = new AddOrEditEffect(hideTracingOptions: true);
			dlg.Text = "Edit Effect";
			dlg.UsedColors = UsedColors;
			int indexOfSelectedEffect = effectsListView.SelectedIndices[0];
			dlg.BitmapEffect = SelectedEffect.Value.Value;
			if (DialogResult.OK == dlg.ShowDialog())
			{
				dlg.Hide();
				//
				BitmapColor.Effects[indexOfSelectedEffect] = dlg.BitmapEffect;
				synchronizeFormWithBitmapVariant();
			}
			dlg.Dispose();
		}

		private void moveUpToolStripMenuItem_Click(object sender, EventArgs e)
		{
			uint key = SelectedEffect.Value.Key;
			if (SelectedEffect.Value.Key > 0 && replacementBitmapColor.Effects.Count > key)
			{
				uint targetKey = key - 1;
				BitmapEffect shiftedEffect = replacementBitmapColor.Effects[(int)targetKey];
				replacementBitmapColor.Effects[(int)targetKey] = SelectedEffect.Value.Value;
				replacementBitmapColor.Effects[(int)key] = shiftedEffect;
				//
				synchronizeFormWithBitmapVariant();
			}
		}

		private void moveDownToolStripMenuItem_Click(object sender, EventArgs e)
		{
			uint key = SelectedEffect.Value.Key;
			uint targetKey = key + 1;
			if (SelectedEffect.Value.Key < replacementBitmapColor.Effects.Count - 1)
			{
				BitmapEffect shiftedEffect = replacementBitmapColor.Effects[(int)targetKey];
				replacementBitmapColor.Effects[(int)targetKey] = SelectedEffect.Value.Value;
				replacementBitmapColor.Effects[(int)key] = shiftedEffect;
				//
				synchronizeFormWithBitmapVariant();
			}
		}

		private void removeEffectToolStripMenuItem_Click(object sender, EventArgs e)
		{
			uint key = SelectedEffect.Value.Key;
			if (replacementBitmapColor.Effects.Count > key)
			{
				replacementBitmapColor.Effects.RemoveAt((int)key);
				//
				synchronizeFormWithBitmapVariant();
			}
		}

		private void saveButton_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
		}

		private void nameMaskedTextBox_TextChanged(object sender, EventArgs e)
		{
			BitmapColor.Name = nameMaskedTextBox.Text;
		}

		private void effectsContextMenuStrip_Opening(object sender, CancelEventArgs e)
		{
			bool effectIsSelected = (effectsListView.SelectedIndices.Count > 0) ? true : false;
			editEffectToolStripMenuItem.Enabled = effectIsSelected;
			moveUpToolStripMenuItem.Enabled = effectIsSelected;
			moveDownToolStripMenuItem.Enabled = effectIsSelected;
			removeEffectToolStripMenuItem.Enabled = effectIsSelected;
		}
	}
}
