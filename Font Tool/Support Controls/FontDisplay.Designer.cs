namespace FontTool
{
	partial class FontDisplay
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
			this.fontNameLabel = new System.Windows.Forms.Label();
			this.ascentLabel = new System.Windows.Forms.Label();
			this.descentLabel = new System.Windows.Forms.Label();
			this.boundingBoxLabel = new System.Windows.Forms.Label();
			this.boundingBoxValueLabel = new System.Windows.Forms.Label();
			this.ascentMaskedTextBox = new FontTool.MaskedTextBox();
			this.descentMaskedTextBox = new FontTool.MaskedTextBox();
			this.tableLayoutPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel
			// 
			this.tableLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel.ColumnCount = 2;
			this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel.Controls.Add(this.fontNameLabel, 0, 0);
			this.tableLayoutPanel.Controls.Add(this.ascentLabel, 0, 1);
			this.tableLayoutPanel.Controls.Add(this.descentLabel, 0, 2);
			this.tableLayoutPanel.Controls.Add(this.boundingBoxLabel, 0, 3);
			this.tableLayoutPanel.Controls.Add(this.ascentMaskedTextBox, 1, 1);
			this.tableLayoutPanel.Controls.Add(this.descentMaskedTextBox, 1, 2);
			this.tableLayoutPanel.Controls.Add(this.boundingBoxValueLabel, 1, 3);
			this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel.Name = "tableLayoutPanel";
			this.tableLayoutPanel.RowCount = 5;
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel.Size = new System.Drawing.Size(292, 93);
			this.tableLayoutPanel.TabIndex = 0;
			// 
			// fontNameLabel
			// 
			this.fontNameLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.fontNameLabel.AutoSize = true;
			this.tableLayoutPanel.SetColumnSpan(this.fontNameLabel, 2);
			this.fontNameLabel.Location = new System.Drawing.Point(111, 0);
			this.fontNameLabel.Name = "fontNameLabel";
			this.fontNameLabel.Size = new System.Drawing.Size(69, 13);
			this.fontNameLabel.TabIndex = 0;
			this.fontNameLabel.Text = "\"Font Name\"";
			// 
			// ascentLabel
			// 
			this.ascentLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.ascentLabel.AutoSize = true;
			this.ascentLabel.Location = new System.Drawing.Point(36, 19);
			this.ascentLabel.Name = "ascentLabel";
			this.ascentLabel.Size = new System.Drawing.Size(43, 13);
			this.ascentLabel.TabIndex = 1;
			this.ascentLabel.Text = "Ascent:";
			// 
			// descentLabel
			// 
			this.descentLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.descentLabel.AutoSize = true;
			this.descentLabel.Location = new System.Drawing.Point(29, 45);
			this.descentLabel.Name = "descentLabel";
			this.descentLabel.Size = new System.Drawing.Size(50, 13);
			this.descentLabel.TabIndex = 2;
			this.descentLabel.Text = "Descent:";
			// 
			// boundingBoxLabel
			// 
			this.boundingBoxLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.boundingBoxLabel.AutoSize = true;
			this.boundingBoxLabel.Location = new System.Drawing.Point(3, 65);
			this.boundingBoxLabel.Name = "boundingBoxLabel";
			this.boundingBoxLabel.Size = new System.Drawing.Size(76, 13);
			this.boundingBoxLabel.TabIndex = 3;
			this.boundingBoxLabel.Text = "Bounding Box:";
			// 
			// boundingBoxValueLabel
			// 
			this.boundingBoxValueLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.boundingBoxValueLabel.AutoSize = true;
			this.boundingBoxValueLabel.Location = new System.Drawing.Point(85, 65);
			this.boundingBoxValueLabel.Name = "boundingBoxValueLabel";
			this.boundingBoxValueLabel.Size = new System.Drawing.Size(0, 13);
			this.boundingBoxValueLabel.TabIndex = 6;
			// 
			// ascentMaskedTextBox
			// 
			this.ascentMaskedTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.ascentMaskedTextBox.Ceiling = 999D;
			this.ascentMaskedTextBox.EmptyText = "Maximum pixels above the baseline.";
			this.ascentMaskedTextBox.IsNumeric = true;
			this.ascentMaskedTextBox.Location = new System.Drawing.Point(85, 16);
			this.ascentMaskedTextBox.MarkInvalid = false;
			this.ascentMaskedTextBox.Name = "ascentMaskedTextBox";
			this.ascentMaskedTextBox.Size = new System.Drawing.Size(204, 20);
			this.ascentMaskedTextBox.TabIndex = 4;
			// 
			// descentMaskedTextBox
			// 
			this.descentMaskedTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.descentMaskedTextBox.Ceiling = 999D;
			this.descentMaskedTextBox.EmptyText = "Maximum pixels below the baseline.";
			this.descentMaskedTextBox.Increment = 5D;
			this.descentMaskedTextBox.IsNumeric = true;
			this.descentMaskedTextBox.Location = new System.Drawing.Point(85, 42);
			this.descentMaskedTextBox.MarkInvalid = false;
			this.descentMaskedTextBox.Name = "descentMaskedTextBox";
			this.descentMaskedTextBox.Size = new System.Drawing.Size(204, 20);
			this.descentMaskedTextBox.TabIndex = 5;
			// 
			// FontDisplay
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tableLayoutPanel);
			this.Name = "FontDisplay";
			this.Size = new System.Drawing.Size(292, 93);
			this.tableLayoutPanel.ResumeLayout(false);
			this.tableLayoutPanel.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
		private System.Windows.Forms.Label fontNameLabel;
		private System.Windows.Forms.Label ascentLabel;
		private System.Windows.Forms.Label descentLabel;
		private System.Windows.Forms.Label boundingBoxLabel;
		private MaskedTextBox ascentMaskedTextBox;
		private MaskedTextBox descentMaskedTextBox;
		private System.Windows.Forms.Label boundingBoxValueLabel;
	}
}
