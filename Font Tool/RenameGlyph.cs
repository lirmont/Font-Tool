using System.Windows.Forms;

namespace FontTool
{
	public partial class RenameGlyph : Form
	{
		public string GlyphName {
			get { return nameMaskedTextBox.Text; }
			set { nameMaskedTextBox.Text = value; }
		}

		public RenameGlyph()
		{
			InitializeComponent();
		}

		private void saveButton_Click(object sender, System.EventArgs e)
		{
			DialogResult = System.Windows.Forms.DialogResult.OK;
		}
	}
}
