using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FontTool
{
	public partial class AddUnicodeCharacter : Form
	{
		public string UnicodeBlock {
			get {
				if (unicodeBlockComboBox.SelectedItem != null)
					return (string)unicodeBlockComboBox.SelectedItem;
				return null;
			}
			set {
				unicodeBlockComboBox.SelectedItem = value;
			}
		}

		public UlongCollection CharacterCodesAlreadyInScope
		{
			get;
			set;
		}

		public UlongCollection CharacterCodes
		{
			get {
				UlongCollection list = new UlongCollection();
				if (unicodeBlockComboBox.SelectedItem != null && characterComboBox.SelectedIndex == 0)
				{
					// Do whole block.
					string blockName = (string)unicodeBlockComboBox.SelectedItem;
					List<UnicodeBlock> blocks = UnicodeBlocks.GetBlocksByName(new List<string> { blockName });
					if (blocks != null)
						foreach (UnicodeBlock block in blocks)
							foreach (UnicodeCharacter character in block.PrintableCharacters)
								if (CharacterCodesAlreadyInScope != null && !CharacterCodesAlreadyInScope.Contains(character.Id))
									list.Add(character.Id);
				}
				else {
					KeyValuePair<ulong, string> pair = (KeyValuePair<ulong, string>)characterComboBox.SelectedItem;
					list.Add(pair.Key);
				}
				return list;
			}
		}

		public AddUnicodeCharacter(UlongCollection characterCodesAlreadyInScope = null)
		{
			InitializeComponent();
			//
			characterComboBox.DisplayMember = "Value";
			characterComboBox.ValueMember = "Key";
			//
			CharacterCodesAlreadyInScope = characterCodesAlreadyInScope;
			//
			List<UnicodeBlock> list = UnicodeBlocks.GetBlocksByName();
			if (list != null)
				foreach (UnicodeBlock block in list)
					unicodeBlockComboBox.Items.Add(string.Format("{0}", block.BlockName));
			if (unicodeBlockComboBox.Items.Count > 0)
				unicodeBlockComboBox.SelectedIndex = 0;
		}

		private void unicodeBlockComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			ComboBox control = (ComboBox)sender;
			if (control != null) {
				characterComboBox.Items.Clear();
				//
				List<UnicodeBlock> list = UnicodeBlocks.GetBlocksByName((string)control.SelectedItem);
				if (list != null) {
					foreach (UnicodeBlock block in list) {
						characterComboBox.Items.Add(new KeyValuePair<ulong, string>(0, string.Format("All {0}", block.BlockName)));
						characterComboBox.SelectedIndex = 0;
						foreach (UnicodeCharacter character in block.PrintableCharacters)
						{
							if (!(CharacterCodesAlreadyInScope != null && CharacterCodesAlreadyInScope.Contains(character.Id)))
								characterComboBox.Items.Add(new KeyValuePair<ulong, string> (character.Id, string.Format("{0}: {1}", new object[]{ character.Id, SupportFunctions.TitleCaseString(character.PrimaryName) })));
						}
					}
				}
			}
		}

		private void addButton_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
		}
	}
}
