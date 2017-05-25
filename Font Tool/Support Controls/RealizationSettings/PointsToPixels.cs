using System;
using System.Windows.Forms;

namespace TextAnalyzer
{
	public partial class PointsToPixels : Form
	{
		public int Pixels
		{
			get {
				return (int)Math.Ceiling(pointsNumericUpDown.Value * dpiNumericUpDown.Value / 72.0m);
			}
			set {
				pointsNumericUpDown.Value = value * 72m / dpiNumericUpDown.Value;
			}
		}

		public PointsToPixels()
		{
			InitializeComponent();
		}

		public PointsToPixels(int? pixels = null)
		{
			InitializeComponent();
			if (pixels != null)
				Pixels = pixels.Value;
		}

		private void okButton_Click(object sender, EventArgs e)
		{
			DialogResult = System.Windows.Forms.DialogResult.OK;
		}
	}
}
