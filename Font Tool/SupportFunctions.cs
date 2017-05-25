using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Drawing;

namespace FontTool
{
	public partial class SupportFunctions
	{
		public static GlyphPictureBox GetDisplayedGlyphPictureBoxById(ulong key, FlowLayoutPanel flowLayoutPanel)
		{
			GlyphPictureBox box = null;
			foreach (Control c in flowLayoutPanel.Controls)
			{
				if (c is GlyphPictureBox)
				{
					GlyphPictureBox thisBox = (GlyphPictureBox)c;
					if (thisBox.Id == key)
						return thisBox;
				}
			}
			return box;
		}

		public static Control GetAnyControlAt(TableLayoutPanel pp, int col, int row)
		{
			foreach (Control cc in pp.Controls)
				if (pp.GetCellPosition(cc).Column == col && pp.GetCellPosition(cc).Row == row)
					return cc;
			return null;
		}

		public static void ComposeUnicodeBlockComboBox(System.Windows.Forms.ComboBox control)
		{
			List<UnicodeBlock> list = UnicodeBlocks.GetBlocksByName();
			foreach (UnicodeBlock block in list)
				control.Items.Add(block.BlockName);
			if (control.Items.Count > 0)
				control.SelectedIndex = 0;
		}

		public static FlowLayoutPanel CreateFlowLayoutPanelForVisualProxies(ContextMenuStrip contextMenuStrip = null, string name = "flowLayoutPanel")
		{
			FlowLayoutPanel replacementFlowLayoutPanel = new FlowLayoutPanel();
			replacementFlowLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			replacementFlowLayoutPanel.AutoScroll = true;
			replacementFlowLayoutPanel.AutoScrollMargin = new System.Drawing.Size(16, 0);
			replacementFlowLayoutPanel.BackColor = System.Drawing.Color.Transparent;
			replacementFlowLayoutPanel.ContextMenuStrip = contextMenuStrip;
			replacementFlowLayoutPanel.Location = new System.Drawing.Point(382, 3);
			replacementFlowLayoutPanel.Name = name;
			replacementFlowLayoutPanel.Size = new System.Drawing.Size(299, 382);
			replacementFlowLayoutPanel.TabIndex = 0;
			//
			replacementFlowLayoutPanel.MouseEnter += new EventHandler(delegate(object sender, EventArgs e)
			{
				Control control = (Control)sender;
				control.Focus();
			});
			return replacementFlowLayoutPanel;
		}

		public delegate TracedCharacter GetTracedCharacterDelegate(ulong a);

		public static void CreateAndPlaceCharacterGlyphBoxes(FlowLayoutPanel visualProxiesParentContainer, OffsetableUnicodeCharacterCollection characterCollection = null, ContextMenuStrip contextMenuStrip = null, MouseEventHandler mouseDown = null, EventHandler click = null, GlyphPictureBox.SaveImageHandler saveImage = null, EventHandler mouseEnter = null)
		{
			if (characterCollection != null && characterCollection.Count > 0)
			{
				IEnumerator<OffsetableUnicodeCharacter> enumerator = characterCollection.GetEnumerator();
				while (enumerator.MoveNext())
				{
					OffsetableUnicodeCharacter character = enumerator.Current;
					GlyphPictureBox box = new GlyphPictureBox(character);
					box.ContextMenuStrip = contextMenuStrip;
					if (mouseDown != null)
						box.MouseDown += mouseDown;
					if (click != null)
						box.Click += click;
					if (saveImage != null)
						box.SaveImage += saveImage;
					if (mouseEnter != null)
						box.MouseEnter += mouseEnter;
					visualProxiesParentContainer.Controls.Add(box);
				}
			}
		}

		public delegate OffsetableUnicodeCharacter OffsetableUnicodeCharacterDelegate(ulong id, CharacterDisplay.Mode? mode = null);

		public static void AddProcessedCharactersToCharacterCollection(OffsetableUnicodeCharacterCollection characters, List<string> unicodeBlockNames, OffsetableUnicodeCharacterDelegate characterProvider = null)
		{
			List<UnicodeBlock> list = UnicodeBlocks.GetBlocksByName(unicodeBlockNames);
			foreach (UnicodeBlock block in list)
			{
				foreach (UnicodeCharacter character in block.Characters)
				{
					if ((!character.IsControlCharacter && !character.IsSurrogate) || character.Id == 0x0009)
					{
						OffsetableUnicodeCharacter thisCharacter = ((characterProvider != null) ? characterProvider(character.Id) : null) ?? new OffsetableUnicodeCharacter(character, 6f, 0f, 0f, 1f, 1f);
						characters.Add(thisCharacter);
					}
				}
			}
		}

		public static void AddProcessedCharactersToCharacterCollection(OffsetableUnicodeCharacterCollection characters, List<UnicodeCharacter> list, OffsetableUnicodeCharacterDelegate characterProvider = null)
		{
			foreach (UnicodeCharacter character in list)
			{
				if ((!character.IsControlCharacter && !character.IsSurrogate) || character.Id == 0x0009)
				{
					OffsetableUnicodeCharacter thisCharacter = ((characterProvider != null) ? characterProvider(character.Id) : null) ?? new OffsetableUnicodeCharacter(character, 6f, 0f, 0f, 1f, 1f);
					characters.Add(thisCharacter);
				}
			}
		}
	}
}
