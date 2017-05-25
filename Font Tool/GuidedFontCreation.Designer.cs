namespace FontTool
{
	partial class GuidedFontCreation
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
			this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
			this.buttonsTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
			this.okButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.nameLabel = new System.Windows.Forms.Label();
			this.desiredLineHeightLabel = new System.Windows.Forms.Label();
			this.defaultVariantsLabel = new System.Windows.Forms.Label();
			this.defaultColorsLabel = new System.Windows.Forms.Label();
			this.nameMaskedTextBox = new FontTool.MaskedTextBox();
			this.desiredLineHeightMaskedTextBox = new FontTool.MaskedTextBox();
			this.variantsFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
			this.colorsFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
			this.regularVariantCheckBox = new System.Windows.Forms.CheckBox();
			this.italicVariantCheckBox = new System.Windows.Forms.CheckBox();
			this.boldVariantCheckBox = new System.Windows.Forms.CheckBox();
			this.regularColorCheckBox = new System.Windows.Forms.CheckBox();
			this.whiteColorCheckBox = new System.Windows.Forms.CheckBox();
			this.redColorCheckBox = new System.Windows.Forms.CheckBox();
			this.orangeColorCheckBox = new System.Windows.Forms.CheckBox();
			this.yellowColorCheckBox = new System.Windows.Forms.CheckBox();
			this.pinkColorCheckBox = new System.Windows.Forms.CheckBox();
			this.blueColorCheckBox = new System.Windows.Forms.CheckBox();
			this.purpleColorCheckBox = new System.Windows.Forms.CheckBox();
			this.greenColorCheckBox = new System.Windows.Forms.CheckBox();
			this.tableLayoutPanel.SuspendLayout();
			this.buttonsTableLayoutPanel.SuspendLayout();
			this.variantsFlowLayoutPanel.SuspendLayout();
			this.colorsFlowLayoutPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel
			// 
			this.tableLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel.BackColor = System.Drawing.Color.Transparent;
			this.tableLayoutPanel.ColumnCount = 2;
			this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel.Controls.Add(this.buttonsTableLayoutPanel, 0, 6);
			this.tableLayoutPanel.Controls.Add(this.nameLabel, 0, 0);
			this.tableLayoutPanel.Controls.Add(this.desiredLineHeightLabel, 0, 1);
			this.tableLayoutPanel.Controls.Add(this.defaultVariantsLabel, 0, 2);
			this.tableLayoutPanel.Controls.Add(this.defaultColorsLabel, 0, 4);
			this.tableLayoutPanel.Controls.Add(this.nameMaskedTextBox, 1, 0);
			this.tableLayoutPanel.Controls.Add(this.desiredLineHeightMaskedTextBox, 1, 1);
			this.tableLayoutPanel.Controls.Add(this.variantsFlowLayoutPanel, 0, 3);
			this.tableLayoutPanel.Controls.Add(this.colorsFlowLayoutPanel, 0, 5);
			this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel.Name = "tableLayoutPanel";
			this.tableLayoutPanel.RowCount = 7;
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel.Size = new System.Drawing.Size(288, 272);
			this.tableLayoutPanel.TabIndex = 0;
			// 
			// buttonsTableLayoutPanel
			// 
			this.buttonsTableLayoutPanel.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.buttonsTableLayoutPanel.AutoSize = true;
			this.buttonsTableLayoutPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.buttonsTableLayoutPanel.ColumnCount = 2;
			this.tableLayoutPanel.SetColumnSpan(this.buttonsTableLayoutPanel, 2);
			this.buttonsTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.buttonsTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.buttonsTableLayoutPanel.Controls.Add(this.okButton, 0, 0);
			this.buttonsTableLayoutPanel.Controls.Add(this.cancelButton, 1, 0);
			this.buttonsTableLayoutPanel.Location = new System.Drawing.Point(97, 240);
			this.buttonsTableLayoutPanel.Name = "buttonsTableLayoutPanel";
			this.buttonsTableLayoutPanel.RowCount = 1;
			this.buttonsTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.buttonsTableLayoutPanel.Size = new System.Drawing.Size(94, 29);
			this.buttonsTableLayoutPanel.TabIndex = 0;
			// 
			// okButton
			// 
			this.okButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.okButton.AutoSize = true;
			this.okButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.okButton.Location = new System.Drawing.Point(3, 3);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(32, 23);
			this.okButton.TabIndex = 0;
			this.okButton.Text = "OK";
			this.okButton.UseVisualStyleBackColor = true;
			this.okButton.Click += new System.EventHandler(this.okButton_Click);
			// 
			// cancelButton
			// 
			this.cancelButton.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.cancelButton.AutoSize = true;
			this.cancelButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.cancelButton.Location = new System.Drawing.Point(41, 3);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(50, 23);
			this.cancelButton.TabIndex = 1;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			// 
			// nameLabel
			// 
			this.nameLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.nameLabel.AutoSize = true;
			this.nameLabel.Location = new System.Drawing.Point(68, 6);
			this.nameLabel.Name = "nameLabel";
			this.nameLabel.Size = new System.Drawing.Size(38, 13);
			this.nameLabel.TabIndex = 1;
			this.nameLabel.Text = "Name:";
			this.nameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// desiredLineHeightLabel
			// 
			this.desiredLineHeightLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.desiredLineHeightLabel.AutoSize = true;
			this.desiredLineHeightLabel.Location = new System.Drawing.Point(3, 32);
			this.desiredLineHeightLabel.Name = "desiredLineHeightLabel";
			this.desiredLineHeightLabel.Size = new System.Drawing.Size(103, 13);
			this.desiredLineHeightLabel.TabIndex = 2;
			this.desiredLineHeightLabel.Text = "Desired Line Height:";
			this.desiredLineHeightLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// defaultVariantsLabel
			// 
			this.defaultVariantsLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.defaultVariantsLabel.AutoSize = true;
			this.defaultVariantsLabel.Location = new System.Drawing.Point(21, 52);
			this.defaultVariantsLabel.Name = "defaultVariantsLabel";
			this.defaultVariantsLabel.Size = new System.Drawing.Size(85, 13);
			this.defaultVariantsLabel.TabIndex = 3;
			this.defaultVariantsLabel.Text = "Default Variants:";
			this.defaultVariantsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// defaultColorsLabel
			// 
			this.defaultColorsLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.defaultColorsLabel.AutoSize = true;
			this.defaultColorsLabel.Location = new System.Drawing.Point(30, 94);
			this.defaultColorsLabel.Name = "defaultColorsLabel";
			this.defaultColorsLabel.Size = new System.Drawing.Size(76, 13);
			this.defaultColorsLabel.TabIndex = 4;
			this.defaultColorsLabel.Text = "Default Colors:";
			this.defaultColorsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// nameMaskedTextBox
			// 
			this.nameMaskedTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.nameMaskedTextBox.EmptyText = "Name of font.";
			this.nameMaskedTextBox.Location = new System.Drawing.Point(112, 3);
			this.nameMaskedTextBox.MarkInvalid = false;
			this.nameMaskedTextBox.Name = "nameMaskedTextBox";
			this.nameMaskedTextBox.Size = new System.Drawing.Size(173, 20);
			this.nameMaskedTextBox.TabIndex = 5;
			// 
			// desiredLineHeightMaskedTextBox
			// 
			this.desiredLineHeightMaskedTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.desiredLineHeightMaskedTextBox.Ceiling = 100000D;
			this.desiredLineHeightMaskedTextBox.EmptyText = "Line height in pixels.";
			this.desiredLineHeightMaskedTextBox.Floor = 2D;
			this.desiredLineHeightMaskedTextBox.Interval = 1D;
			this.desiredLineHeightMaskedTextBox.IsNumeric = true;
			this.desiredLineHeightMaskedTextBox.Location = new System.Drawing.Point(112, 29);
			this.desiredLineHeightMaskedTextBox.MarkInvalid = false;
			this.desiredLineHeightMaskedTextBox.Name = "desiredLineHeightMaskedTextBox";
			this.desiredLineHeightMaskedTextBox.Size = new System.Drawing.Size(173, 20);
			this.desiredLineHeightMaskedTextBox.TabIndex = 6;
			// 
			// variantsFlowLayoutPanel
			// 
			this.variantsFlowLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.variantsFlowLayoutPanel.AutoSize = true;
			this.variantsFlowLayoutPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel.SetColumnSpan(this.variantsFlowLayoutPanel, 2);
			this.variantsFlowLayoutPanel.Controls.Add(this.regularVariantCheckBox);
			this.variantsFlowLayoutPanel.Controls.Add(this.italicVariantCheckBox);
			this.variantsFlowLayoutPanel.Controls.Add(this.boldVariantCheckBox);
			this.variantsFlowLayoutPanel.Location = new System.Drawing.Point(3, 68);
			this.variantsFlowLayoutPanel.Name = "variantsFlowLayoutPanel";
			this.variantsFlowLayoutPanel.Size = new System.Drawing.Size(282, 23);
			this.variantsFlowLayoutPanel.TabIndex = 7;
			// 
			// colorsFlowLayoutPanel
			// 
			this.colorsFlowLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.colorsFlowLayoutPanel.AutoSize = true;
			this.colorsFlowLayoutPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel.SetColumnSpan(this.colorsFlowLayoutPanel, 2);
			this.colorsFlowLayoutPanel.Controls.Add(this.regularColorCheckBox);
			this.colorsFlowLayoutPanel.Controls.Add(this.whiteColorCheckBox);
			this.colorsFlowLayoutPanel.Controls.Add(this.redColorCheckBox);
			this.colorsFlowLayoutPanel.Controls.Add(this.orangeColorCheckBox);
			this.colorsFlowLayoutPanel.Controls.Add(this.yellowColorCheckBox);
			this.colorsFlowLayoutPanel.Controls.Add(this.greenColorCheckBox);
			this.colorsFlowLayoutPanel.Controls.Add(this.blueColorCheckBox);
			this.colorsFlowLayoutPanel.Controls.Add(this.purpleColorCheckBox);
			this.colorsFlowLayoutPanel.Controls.Add(this.pinkColorCheckBox);
			this.colorsFlowLayoutPanel.Location = new System.Drawing.Point(3, 110);
			this.colorsFlowLayoutPanel.Name = "colorsFlowLayoutPanel";
			this.colorsFlowLayoutPanel.Size = new System.Drawing.Size(282, 69);
			this.colorsFlowLayoutPanel.TabIndex = 8;
			// 
			// regularVariantCheckBox
			// 
			this.regularVariantCheckBox.AutoSize = true;
			this.regularVariantCheckBox.Location = new System.Drawing.Point(3, 3);
			this.regularVariantCheckBox.Name = "regularVariantCheckBox";
			this.regularVariantCheckBox.Size = new System.Drawing.Size(63, 17);
			this.regularVariantCheckBox.TabIndex = 0;
			this.regularVariantCheckBox.Text = "Regular";
			this.regularVariantCheckBox.UseVisualStyleBackColor = true;
			// 
			// italicVariantCheckBox
			// 
			this.italicVariantCheckBox.AutoSize = true;
			this.italicVariantCheckBox.Location = new System.Drawing.Point(72, 3);
			this.italicVariantCheckBox.Name = "italicVariantCheckBox";
			this.italicVariantCheckBox.Size = new System.Drawing.Size(48, 17);
			this.italicVariantCheckBox.TabIndex = 1;
			this.italicVariantCheckBox.Text = "Italic";
			this.italicVariantCheckBox.UseVisualStyleBackColor = true;
			// 
			// boldVariantCheckBox
			// 
			this.boldVariantCheckBox.AutoSize = true;
			this.boldVariantCheckBox.Location = new System.Drawing.Point(126, 3);
			this.boldVariantCheckBox.Name = "boldVariantCheckBox";
			this.boldVariantCheckBox.Size = new System.Drawing.Size(47, 17);
			this.boldVariantCheckBox.TabIndex = 2;
			this.boldVariantCheckBox.Text = "Bold";
			this.boldVariantCheckBox.UseVisualStyleBackColor = true;
			// 
			// regularColorCheckBox
			// 
			this.regularColorCheckBox.AutoSize = true;
			this.regularColorCheckBox.Location = new System.Drawing.Point(3, 3);
			this.regularColorCheckBox.Name = "regularColorCheckBox";
			this.regularColorCheckBox.Size = new System.Drawing.Size(63, 17);
			this.regularColorCheckBox.TabIndex = 0;
			this.regularColorCheckBox.Text = "Regular";
			this.regularColorCheckBox.UseVisualStyleBackColor = true;
			// 
			// whiteColorCheckBox
			// 
			this.whiteColorCheckBox.AutoSize = true;
			this.whiteColorCheckBox.Location = new System.Drawing.Point(72, 3);
			this.whiteColorCheckBox.Name = "whiteColorCheckBox";
			this.whiteColorCheckBox.Size = new System.Drawing.Size(54, 17);
			this.whiteColorCheckBox.TabIndex = 1;
			this.whiteColorCheckBox.Text = "White";
			this.whiteColorCheckBox.UseVisualStyleBackColor = true;
			// 
			// redColorCheckBox
			// 
			this.redColorCheckBox.AutoSize = true;
			this.redColorCheckBox.Location = new System.Drawing.Point(132, 3);
			this.redColorCheckBox.Name = "redColorCheckBox";
			this.redColorCheckBox.Size = new System.Drawing.Size(46, 17);
			this.redColorCheckBox.TabIndex = 2;
			this.redColorCheckBox.Text = "Red";
			this.redColorCheckBox.UseVisualStyleBackColor = true;
			// 
			// orangeColorCheckBox
			// 
			this.orangeColorCheckBox.AutoSize = true;
			this.orangeColorCheckBox.Location = new System.Drawing.Point(184, 3);
			this.orangeColorCheckBox.Name = "orangeColorCheckBox";
			this.orangeColorCheckBox.Size = new System.Drawing.Size(61, 17);
			this.orangeColorCheckBox.TabIndex = 3;
			this.orangeColorCheckBox.Text = "Orange";
			this.orangeColorCheckBox.UseVisualStyleBackColor = true;
			// 
			// yellowColorCheckBox
			// 
			this.yellowColorCheckBox.AutoSize = true;
			this.yellowColorCheckBox.Location = new System.Drawing.Point(3, 26);
			this.yellowColorCheckBox.Name = "yellowColorCheckBox";
			this.yellowColorCheckBox.Size = new System.Drawing.Size(57, 17);
			this.yellowColorCheckBox.TabIndex = 4;
			this.yellowColorCheckBox.Text = "Yellow";
			this.yellowColorCheckBox.UseVisualStyleBackColor = true;
			// 
			// pinkColorCheckBox
			// 
			this.pinkColorCheckBox.AutoSize = true;
			this.pinkColorCheckBox.Location = new System.Drawing.Point(3, 49);
			this.pinkColorCheckBox.Name = "pinkColorCheckBox";
			this.pinkColorCheckBox.Size = new System.Drawing.Size(47, 17);
			this.pinkColorCheckBox.TabIndex = 5;
			this.pinkColorCheckBox.Text = "Pink";
			this.pinkColorCheckBox.UseVisualStyleBackColor = true;
			// 
			// blueColorCheckBox
			// 
			this.blueColorCheckBox.AutoSize = true;
			this.blueColorCheckBox.Location = new System.Drawing.Point(127, 26);
			this.blueColorCheckBox.Name = "blueColorCheckBox";
			this.blueColorCheckBox.Size = new System.Drawing.Size(47, 17);
			this.blueColorCheckBox.TabIndex = 6;
			this.blueColorCheckBox.Text = "Blue";
			this.blueColorCheckBox.UseVisualStyleBackColor = true;
			// 
			// purpleColorCheckBox
			// 
			this.purpleColorCheckBox.AutoSize = true;
			this.purpleColorCheckBox.Location = new System.Drawing.Point(180, 26);
			this.purpleColorCheckBox.Name = "purpleColorCheckBox";
			this.purpleColorCheckBox.Size = new System.Drawing.Size(56, 17);
			this.purpleColorCheckBox.TabIndex = 7;
			this.purpleColorCheckBox.Text = "Purple";
			this.purpleColorCheckBox.UseVisualStyleBackColor = true;
			// 
			// greenColorCheckBox
			// 
			this.greenColorCheckBox.AutoSize = true;
			this.greenColorCheckBox.Location = new System.Drawing.Point(66, 26);
			this.greenColorCheckBox.Name = "greenColorCheckBox";
			this.greenColorCheckBox.Size = new System.Drawing.Size(55, 17);
			this.greenColorCheckBox.TabIndex = 8;
			this.greenColorCheckBox.Text = "Green";
			this.greenColorCheckBox.UseVisualStyleBackColor = true;
			// 
			// GuidedFontCreation
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(289, 273);
			this.Controls.Add(this.tableLayoutPanel);
			this.Name = "GuidedFontCreation";
			this.ShowIcon = false;
			this.Text = "Guided Font Creation";
			this.tableLayoutPanel.ResumeLayout(false);
			this.tableLayoutPanel.PerformLayout();
			this.buttonsTableLayoutPanel.ResumeLayout(false);
			this.buttonsTableLayoutPanel.PerformLayout();
			this.variantsFlowLayoutPanel.ResumeLayout(false);
			this.variantsFlowLayoutPanel.PerformLayout();
			this.colorsFlowLayoutPanel.ResumeLayout(false);
			this.colorsFlowLayoutPanel.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
		private System.Windows.Forms.TableLayoutPanel buttonsTableLayoutPanel;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Label nameLabel;
		private System.Windows.Forms.Label desiredLineHeightLabel;
		private System.Windows.Forms.Label defaultVariantsLabel;
		private System.Windows.Forms.Label defaultColorsLabel;
		private MaskedTextBox nameMaskedTextBox;
		private MaskedTextBox desiredLineHeightMaskedTextBox;
		private System.Windows.Forms.FlowLayoutPanel variantsFlowLayoutPanel;
		private System.Windows.Forms.CheckBox regularVariantCheckBox;
		private System.Windows.Forms.CheckBox italicVariantCheckBox;
		private System.Windows.Forms.CheckBox boldVariantCheckBox;
		private System.Windows.Forms.FlowLayoutPanel colorsFlowLayoutPanel;
		private System.Windows.Forms.CheckBox regularColorCheckBox;
		private System.Windows.Forms.CheckBox whiteColorCheckBox;
		private System.Windows.Forms.CheckBox redColorCheckBox;
		private System.Windows.Forms.CheckBox orangeColorCheckBox;
		private System.Windows.Forms.CheckBox yellowColorCheckBox;
		private System.Windows.Forms.CheckBox greenColorCheckBox;
		private System.Windows.Forms.CheckBox blueColorCheckBox;
		private System.Windows.Forms.CheckBox purpleColorCheckBox;
		private System.Windows.Forms.CheckBox pinkColorCheckBox;
	}
}