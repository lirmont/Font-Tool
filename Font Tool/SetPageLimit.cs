using System.Windows.Forms;

namespace FontTool
{
	public partial class SetPageLimit : Form
	{
		public uint Limit {
			get {
				return (uint)this.limitNumericUpDown.Value;
			}
		}

		public SetPageLimit()
		{
			InitializeComponent();
		}

		public SetPageLimit(uint limit)
		{
			InitializeComponent();
			this.limitNumericUpDown.Value = limit;
		}

		private void saveButton_Click(object sender, System.EventArgs e)
		{
			DialogResult = System.Windows.Forms.DialogResult.OK;
		}
	}
}
