using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FontTool
{
	public partial class SaveCopyAs : Form
	{
		public string FontName {
			get { return nameMaskedTextBox.Text; }
			set { nameMaskedTextBox.Text = value; }
		}

		public SaveCopyAs()
		{
			InitializeComponent();
		}

		private void saveButton_Click(object sender, EventArgs e)
		{
			DialogResult = System.Windows.Forms.DialogResult.OK;
		}
	}
}
