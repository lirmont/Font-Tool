namespace FontTool
{
	partial class SetPageLimit
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
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.limitLabel = new System.Windows.Forms.Label();
			this.limitNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.buttonsTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
			this.saveButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.tableLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.limitNumericUpDown)).BeginInit();
			this.buttonsTableLayoutPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75F));
			this.tableLayoutPanel1.Controls.Add(this.limitLabel, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.limitNumericUpDown, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.buttonsTableLayoutPanel, 0, 1);
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.Size = new System.Drawing.Size(256, 83);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// limitLabel
			// 
			this.limitLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.limitLabel.AutoSize = true;
			this.limitLabel.Location = new System.Drawing.Point(30, 17);
			this.limitLabel.Name = "limitLabel";
			this.limitLabel.Size = new System.Drawing.Size(31, 13);
			this.limitLabel.TabIndex = 0;
			this.limitLabel.Text = "Limit:";
			this.limitLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// limitNumericUpDown
			// 
			this.limitNumericUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.limitNumericUpDown.Location = new System.Drawing.Point(67, 14);
			this.limitNumericUpDown.Maximum = new decimal(new int[] {
            9999999,
            0,
            0,
            0});
			this.limitNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.limitNumericUpDown.Name = "limitNumericUpDown";
			this.limitNumericUpDown.Size = new System.Drawing.Size(186, 20);
			this.limitNumericUpDown.TabIndex = 1;
			this.limitNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// buttonsTableLayoutPanel
			// 
			this.buttonsTableLayoutPanel.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.buttonsTableLayoutPanel.AutoSize = true;
			this.buttonsTableLayoutPanel.ColumnCount = 2;
			this.tableLayoutPanel1.SetColumnSpan(this.buttonsTableLayoutPanel, 2);
			this.buttonsTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.buttonsTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.buttonsTableLayoutPanel.Controls.Add(this.saveButton, 0, 0);
			this.buttonsTableLayoutPanel.Controls.Add(this.cancelButton, 1, 0);
			this.buttonsTableLayoutPanel.Location = new System.Drawing.Point(47, 51);
			this.buttonsTableLayoutPanel.Name = "buttonsTableLayoutPanel";
			this.buttonsTableLayoutPanel.RowCount = 1;
			this.buttonsTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.buttonsTableLayoutPanel.Size = new System.Drawing.Size(162, 29);
			this.buttonsTableLayoutPanel.TabIndex = 2;
			// 
			// saveButton
			// 
			this.saveButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.saveButton.Location = new System.Drawing.Point(3, 3);
			this.saveButton.Name = "saveButton";
			this.saveButton.Size = new System.Drawing.Size(75, 23);
			this.saveButton.TabIndex = 0;
			this.saveButton.Text = "Save";
			this.saveButton.UseVisualStyleBackColor = true;
			this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
			// 
			// cancelButton
			// 
			this.cancelButton.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(84, 3);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 23);
			this.cancelButton.TabIndex = 1;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			// 
			// SetPageLimit
			// 
			this.AcceptButton = this.saveButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(257, 83);
			this.Controls.Add(this.tableLayoutPanel1);
			this.MinimumSize = new System.Drawing.Size(265, 110);
			this.Name = "SetPageLimit";
			this.ShowIcon = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.Text = "Set Page Limit";
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.limitNumericUpDown)).EndInit();
			this.buttonsTableLayoutPanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Label limitLabel;
		private System.Windows.Forms.NumericUpDown limitNumericUpDown;
		private System.Windows.Forms.TableLayoutPanel buttonsTableLayoutPanel;
		private System.Windows.Forms.Button saveButton;
		private System.Windows.Forms.Button cancelButton;

	}
}