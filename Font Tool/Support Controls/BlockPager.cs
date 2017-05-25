using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace FontTool
{
	public partial class BlockPager : UserControl
	{
		private string blockName;
		private uint offset;
		private uint limit = 120U;

		public string BlockName
		{
			get { return blockName; }
			set {
				blockName = value;
				blockLabel.Text = value;
			}
		}

		public uint Offset
		{
			get { return offset; }
			set {
				bool changed = (offset != value) ? true : false;
				if (changed)
				{
					offset = value;
					RangeUpdate();
					updateLocationText();
				}
			}
		}

		[Browsable(false)]
		public uint Limit
		{
			get { return limit; }
			set {
				bool changed = (limit != value) ? true : false;
				if (changed)
				{
					limit = value;
					RangeUpdate();
					updateLocationText();
				}
			}
		}

		public uint Total
		{
			get {
				return (list != null) ? (uint)list.Count : 0U;
			}
		}

		public uint VisibleItems
		{
			get {
				return (Offset + Limit <= Total) ? Limit : Total - Offset;
			}
		}

		private List<UnicodeCharacter> list;

		public List<UnicodeCharacter> List
		{
			get { return list; }
			set {
				bool changed = (list != value) ? true : false;
				if (changed)
				{
					list = value;
					if (Offset != 0U)
					{
						Offset = 0U;
					}
					else
					{
						updateLocationText();
						RangeUpdate();
					}
				}
			}
		}

		public BlockPager()
		{
			//
			InitializeComponent();
			//
			this.Limit = Properties.Settings.Default.pagerCount;
		}

		public delegate void RangeUpdateHandler(object sender, List<UnicodeCharacter> range);
		public event RangeUpdateHandler OnRangeUpdate;

		private void RangeUpdate()
		{
			// If nothing is listening to the event, return.
			if (OnRangeUpdate == null) return;
			// Otherwise, send back a range of things to display.
			List<UnicodeCharacter> range = (List != null) ? List.GetRange((int)Offset, (int)VisibleItems) : new List<UnicodeCharacter>();
			OnRangeUpdate(this, range);
		}

		private void updateLocationText()
		{
			locationLabel.Text = string.Format("{0} to {1} of {2}", new object[] {
				Offset + 1,
				Offset + VisibleItems,
				Total
			});
			//
			bool noOtherItemsToShow = (Offset + VisibleItems == Total) ? true : false;
			bool zeroOffset = (Offset == 0) ? true : false;
			nextLinkLabel.Enabled = !noOtherItemsToShow;
			lastLinkLabel.Enabled = !noOtherItemsToShow;
			firstLinkLabel.Enabled = !zeroOffset;
			previousLinkLabel.Enabled = !zeroOffset;
		}

		private void firstLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Application.DoEvents();
			Offset = 0U;
		}

		private void previousLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Application.DoEvents();
			Offset -= (Offset > Limit) ? Limit : Offset;
		}

		private void nextLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Application.DoEvents();
			Offset += (Offset + Limit < Total) ? Limit : 0U;
		}

		private void lastLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Application.DoEvents();
			Offset = (Total >= Limit) ? Total - Limit : 0U;
		}

		private void setPageLimitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Application.DoEvents();
			//
			FontTool.SetPageLimit dlg = new SetPageLimit(this.limit);
			if (DialogResult.OK == dlg.ShowDialog())
			{
				this.Limit = dlg.Limit;
				Properties.Settings.Default.pagerCount = dlg.Limit;
				Properties.Settings.Default.Save();
				Application.DoEvents();
			}
			dlg.Dispose();
		}
	}
}
