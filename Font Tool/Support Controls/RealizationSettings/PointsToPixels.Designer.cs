namespace TextAnalyzer
{
	partial class PointsToPixels
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
			System.Windows.Forms.Label label2;
			this.label1 = new System.Windows.Forms.Label();
			this.buttonsTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
			this.okButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.pointsNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.dpiNumericUpDown = new System.Windows.Forms.NumericUpDown();
			tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
			label2 = new System.Windows.Forms.Label();
			tableLayoutPanel.SuspendLayout();
			this.buttonsTableLayoutPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pointsNumericUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dpiNumericUpDown)).BeginInit();
			this.SuspendLayout();
			// 
			// tableLayoutPanel
			// 
			tableLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			tableLayoutPanel.BackColor = System.Drawing.Color.Transparent;
			tableLayoutPanel.ColumnCount = 2;
			tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			tableLayoutPanel.Controls.Add(this.label1, 0, 0);
			tableLayoutPanel.Controls.Add(label2, 0, 1);
			tableLayoutPanel.Controls.Add(this.buttonsTableLayoutPanel, 0, 3);
			tableLayoutPanel.Controls.Add(this.pointsNumericUpDown, 1, 0);
			tableLayoutPanel.Controls.Add(this.dpiNumericUpDown, 1, 1);
			tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
			tableLayoutPanel.Name = "tableLayoutPanel";
			tableLayoutPanel.RowCount = 4;
			tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			tableLayoutPanel.Size = new System.Drawing.Size(288, 111);
			tableLayoutPanel.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(90, 6);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(73, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Size in Points:";
			// 
			// label2
			// 
			label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(3, 32);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(160, 13);
			label2.TabIndex = 1;
			label2.Text = "Device Vertical Dots/Inch (DPI):";
			// 
			// buttonsTableLayoutPanel
			// 
			this.buttonsTableLayoutPanel.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.buttonsTableLayoutPanel.AutoSize = true;
			this.buttonsTableLayoutPanel.ColumnCount = 2;
			tableLayoutPanel.SetColumnSpan(this.buttonsTableLayoutPanel, 2);
			this.buttonsTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.buttonsTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.buttonsTableLayoutPanel.Controls.Add(this.okButton, 0, 0);
			this.buttonsTableLayoutPanel.Controls.Add(this.cancelButton, 1, 0);
			this.buttonsTableLayoutPanel.Location = new System.Drawing.Point(63, 79);
			this.buttonsTableLayoutPanel.Name = "buttonsTableLayoutPanel";
			this.buttonsTableLayoutPanel.RowCount = 1;
			this.buttonsTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.buttonsTableLayoutPanel.Size = new System.Drawing.Size(162, 29);
			this.buttonsTableLayoutPanel.TabIndex = 2;
			// 
			// okButton
			// 
			this.okButton.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.okButton.Location = new System.Drawing.Point(3, 3);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(75, 23);
			this.okButton.TabIndex = 0;
			this.okButton.Text = "OK";
			this.okButton.UseVisualStyleBackColor = true;
			this.okButton.Click += new System.EventHandler(this.okButton_Click);
			// 
			// cancelButton
			// 
			this.cancelButton.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(84, 3);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 23);
			this.cancelButton.TabIndex = 1;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			// 
			// pointsNumericUpDown
			// 
			this.pointsNumericUpDown.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.pointsNumericUpDown.AutoSize = true;
			this.pointsNumericUpDown.DecimalPlaces = 2;
			this.pointsNumericUpDown.Location = new System.Drawing.Point(169, 3);
			this.pointsNumericUpDown.Name = "pointsNumericUpDown";
			this.pointsNumericUpDown.Size = new System.Drawing.Size(60, 20);
			this.pointsNumericUpDown.TabIndex = 3;
			this.pointsNumericUpDown.Value = new decimal(new int[] {
            24,
            0,
            0,
            0});
			// 
			// dpiNumericUpDown
			// 
			this.dpiNumericUpDown.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.dpiNumericUpDown.AutoSize = true;
			this.dpiNumericUpDown.DecimalPlaces = 2;
			this.dpiNumericUpDown.Location = new System.Drawing.Point(169, 29);
			this.dpiNumericUpDown.Name = "dpiNumericUpDown";
			this.dpiNumericUpDown.Size = new System.Drawing.Size(60, 20);
			this.dpiNumericUpDown.TabIndex = 4;
			this.dpiNumericUpDown.Value = new decimal(new int[] {
            72,
            0,
            0,
            0});
			// 
			// PointsToPixels
			// 
			this.AcceptButton = this.okButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(290, 112);
			this.Controls.Add(tableLayoutPanel);
			this.MinimumSize = new System.Drawing.Size(298, 139);
			this.Name = "PointsToPixels";
			this.ShowIcon = false;
			this.Text = "Points to Pixels";
			tableLayoutPanel.ResumeLayout(false);
			tableLayoutPanel.PerformLayout();
			this.buttonsTableLayoutPanel.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pointsNumericUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dpiNumericUpDown)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TableLayoutPanel buttonsTableLayoutPanel;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.NumericUpDown pointsNumericUpDown;
		private System.Windows.Forms.NumericUpDown dpiNumericUpDown;

	}
}