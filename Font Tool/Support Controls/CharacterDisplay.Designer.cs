namespace FontTool
{
	partial class CharacterDisplay
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
			this.mainTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
			this.characterViewer = new FontTool.CharacterViewer();
			this.parametersTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
			this.widthLabel = new System.Windows.Forms.Label();
			this.widthNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.ascendsLabel = new System.Windows.Forms.Label();
			this.advancesLabel = new System.Windows.Forms.Label();
			this.ascendNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.advanceNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.unicodeLabel = new System.Windows.Forms.Label();
			this.unicodeValueLabel = new System.Windows.Forms.Label();
			this.leftToolsTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
			this.paintModeButton = new System.Windows.Forms.CheckBox();
			this.colorPanel = new System.Windows.Forms.Panel();
			this.rightToolsTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
			this.baseImageModeButton = new System.Windows.Forms.RadioButton();
			this.effectedImageModeButton = new System.Windows.Forms.RadioButton();
			this.mainTableLayoutPanel.SuspendLayout();
			this.parametersTableLayoutPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.widthNumericUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ascendNumericUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.advanceNumericUpDown)).BeginInit();
			this.leftToolsTableLayoutPanel.SuspendLayout();
			this.rightToolsTableLayoutPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// mainTableLayoutPanel
			// 
			this.mainTableLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.mainTableLayoutPanel.ColumnCount = 3;
			this.mainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.mainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.mainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.mainTableLayoutPanel.Controls.Add(this.characterViewer, 1, 0);
			this.mainTableLayoutPanel.Controls.Add(this.parametersTableLayoutPanel, 1, 1);
			this.mainTableLayoutPanel.Controls.Add(this.leftToolsTableLayoutPanel, 0, 0);
			this.mainTableLayoutPanel.Controls.Add(this.rightToolsTableLayoutPanel, 2, 0);
			this.mainTableLayoutPanel.Location = new System.Drawing.Point(0, 0);
			this.mainTableLayoutPanel.Name = "mainTableLayoutPanel";
			this.mainTableLayoutPanel.RowCount = 2;
			this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 106F));
			this.mainTableLayoutPanel.Size = new System.Drawing.Size(161, 234);
			this.mainTableLayoutPanel.TabIndex = 1;
			// 
			// characterViewer
			// 
			this.characterViewer.Advances = 0F;
			this.characterViewer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.characterViewer.Ascends = 0F;
			this.characterViewer.BackColor = System.Drawing.Color.Black;
			this.characterViewer.CharacterWidth = 6F;
			this.characterViewer.GutterY = 5F;
			this.characterViewer.Location = new System.Drawing.Point(30, 0);
			this.characterViewer.Margin = new System.Windows.Forms.Padding(0);
			this.characterViewer.MaximumAscension = 10F;
			this.characterViewer.Name = "characterViewer";
			this.characterViewer.Size = new System.Drawing.Size(101, 128);
			this.characterViewer.TabIndex = 7;
			this.characterViewer.ZoomFactor = 5F;
			this.characterViewer.Enter += new System.EventHandler(this.characterViewer_Enter);
			// 
			// parametersTableLayoutPanel
			// 
			this.parametersTableLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.parametersTableLayoutPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.parametersTableLayoutPanel.ColumnCount = 2;
			this.mainTableLayoutPanel.SetColumnSpan(this.parametersTableLayoutPanel, 2);
			this.parametersTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.parametersTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.parametersTableLayoutPanel.Controls.Add(this.widthLabel, 0, 1);
			this.parametersTableLayoutPanel.Controls.Add(this.widthNumericUpDown, 1, 1);
			this.parametersTableLayoutPanel.Controls.Add(this.ascendsLabel, 0, 2);
			this.parametersTableLayoutPanel.Controls.Add(this.advancesLabel, 0, 3);
			this.parametersTableLayoutPanel.Controls.Add(this.ascendNumericUpDown, 1, 2);
			this.parametersTableLayoutPanel.Controls.Add(this.advanceNumericUpDown, 1, 3);
			this.parametersTableLayoutPanel.Controls.Add(this.unicodeLabel, 0, 0);
			this.parametersTableLayoutPanel.Controls.Add(this.unicodeValueLabel, 1, 0);
			this.parametersTableLayoutPanel.Location = new System.Drawing.Point(30, 128);
			this.parametersTableLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
			this.parametersTableLayoutPanel.Name = "parametersTableLayoutPanel";
			this.parametersTableLayoutPanel.RowCount = 4;
			this.parametersTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
			this.parametersTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
			this.parametersTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
			this.parametersTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
			this.parametersTableLayoutPanel.Size = new System.Drawing.Size(131, 106);
			this.parametersTableLayoutPanel.TabIndex = 1;
			// 
			// widthLabel
			// 
			this.widthLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.widthLabel.AutoSize = true;
			this.widthLabel.Location = new System.Drawing.Point(23, 32);
			this.widthLabel.Name = "widthLabel";
			this.widthLabel.Size = new System.Drawing.Size(38, 13);
			this.widthLabel.TabIndex = 0;
			this.widthLabel.Text = "Width:";
			// 
			// widthNumericUpDown
			// 
			this.widthNumericUpDown.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.widthNumericUpDown.Location = new System.Drawing.Point(67, 29);
			this.widthNumericUpDown.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
			this.widthNumericUpDown.Name = "widthNumericUpDown";
			this.widthNumericUpDown.Size = new System.Drawing.Size(42, 20);
			this.widthNumericUpDown.TabIndex = 1;
			this.widthNumericUpDown.Value = new decimal(new int[] {
            6,
            0,
            0,
            0});
			this.widthNumericUpDown.ValueChanged += new System.EventHandler(this.widthNumericUpDown_ValueChanged);
			// 
			// ascendsLabel
			// 
			this.ascendsLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.ascendsLabel.AutoSize = true;
			this.ascendsLabel.Location = new System.Drawing.Point(10, 58);
			this.ascendsLabel.Name = "ascendsLabel";
			this.ascendsLabel.Size = new System.Drawing.Size(51, 13);
			this.ascendsLabel.TabIndex = 2;
			this.ascendsLabel.Text = "Ascends:";
			// 
			// advancesLabel
			// 
			this.advancesLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.advancesLabel.AutoSize = true;
			this.advancesLabel.Location = new System.Drawing.Point(3, 85);
			this.advancesLabel.Name = "advancesLabel";
			this.advancesLabel.Size = new System.Drawing.Size(58, 13);
			this.advancesLabel.TabIndex = 3;
			this.advancesLabel.Text = "Advances:";
			// 
			// ascendNumericUpDown
			// 
			this.ascendNumericUpDown.Location = new System.Drawing.Point(67, 55);
			this.ascendNumericUpDown.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
			this.ascendNumericUpDown.Minimum = new decimal(new int[] {
            999999,
            0,
            0,
            -2147483648});
			this.ascendNumericUpDown.Name = "ascendNumericUpDown";
			this.ascendNumericUpDown.Size = new System.Drawing.Size(42, 20);
			this.ascendNumericUpDown.TabIndex = 4;
			this.ascendNumericUpDown.ValueChanged += new System.EventHandler(this.ascendNumericUpDown_ValueChanged);
			// 
			// advanceNumericUpDown
			// 
			this.advanceNumericUpDown.Location = new System.Drawing.Point(67, 81);
			this.advanceNumericUpDown.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
			this.advanceNumericUpDown.Minimum = new decimal(new int[] {
            999999,
            0,
            0,
            -2147483648});
			this.advanceNumericUpDown.Name = "advanceNumericUpDown";
			this.advanceNumericUpDown.Size = new System.Drawing.Size(42, 20);
			this.advanceNumericUpDown.TabIndex = 5;
			this.advanceNumericUpDown.ValueChanged += new System.EventHandler(this.advanceNumericUpDown_ValueChanged);
			// 
			// unicodeLabel
			// 
			this.unicodeLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.unicodeLabel.AutoSize = true;
			this.unicodeLabel.Location = new System.Drawing.Point(11, 6);
			this.unicodeLabel.Name = "unicodeLabel";
			this.unicodeLabel.Size = new System.Drawing.Size(50, 13);
			this.unicodeLabel.TabIndex = 6;
			this.unicodeLabel.Text = "Unicode:";
			// 
			// unicodeValueLabel
			// 
			this.unicodeValueLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.unicodeValueLabel.Location = new System.Drawing.Point(67, 0);
			this.unicodeValueLabel.Name = "unicodeValueLabel";
			this.unicodeValueLabel.Size = new System.Drawing.Size(61, 26);
			this.unicodeValueLabel.TabIndex = 7;
			this.unicodeValueLabel.Text = "U+000000";
			this.unicodeValueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// leftToolsTableLayoutPanel
			// 
			this.leftToolsTableLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.leftToolsTableLayoutPanel.AutoSize = true;
			this.leftToolsTableLayoutPanel.ColumnCount = 1;
			this.leftToolsTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.leftToolsTableLayoutPanel.Controls.Add(this.paintModeButton, 0, 1);
			this.leftToolsTableLayoutPanel.Controls.Add(this.colorPanel, 0, 2);
			this.leftToolsTableLayoutPanel.Location = new System.Drawing.Point(3, 0);
			this.leftToolsTableLayoutPanel.Margin = new System.Windows.Forms.Padding(3, 0, 3, 1);
			this.leftToolsTableLayoutPanel.Name = "leftToolsTableLayoutPanel";
			this.leftToolsTableLayoutPanel.RowCount = 3;
			this.leftToolsTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.leftToolsTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.leftToolsTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
			this.leftToolsTableLayoutPanel.Size = new System.Drawing.Size(24, 127);
			this.leftToolsTableLayoutPanel.TabIndex = 6;
			this.leftToolsTableLayoutPanel.Visible = false;
			// 
			// paintModeButton
			// 
			this.paintModeButton.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.paintModeButton.Appearance = System.Windows.Forms.Appearance.Button;
			this.paintModeButton.AutoSize = true;
			this.paintModeButton.Location = new System.Drawing.Point(0, 75);
			this.paintModeButton.Margin = new System.Windows.Forms.Padding(0);
			this.paintModeButton.Name = "paintModeButton";
			this.paintModeButton.Size = new System.Drawing.Size(24, 23);
			this.paintModeButton.TabIndex = 0;
			this.paintModeButton.Text = "P";
			this.paintModeButton.UseVisualStyleBackColor = true;
			// 
			// colorPanel
			// 
			this.colorPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.colorPanel.AutoSize = true;
			this.colorPanel.BackColor = System.Drawing.Color.Black;
			this.colorPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.colorPanel.Location = new System.Drawing.Point(0, 101);
			this.colorPanel.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
			this.colorPanel.Name = "colorPanel";
			this.colorPanel.Size = new System.Drawing.Size(24, 23);
			this.colorPanel.TabIndex = 1;
			this.colorPanel.Click += new System.EventHandler(this.colorPanel_Click);
			// 
			// rightToolsTableLayoutPanel
			// 
			this.rightToolsTableLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.rightToolsTableLayoutPanel.ColumnCount = 1;
			this.rightToolsTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.rightToolsTableLayoutPanel.Controls.Add(this.baseImageModeButton, 0, 0);
			this.rightToolsTableLayoutPanel.Controls.Add(this.effectedImageModeButton, 0, 1);
			this.rightToolsTableLayoutPanel.Location = new System.Drawing.Point(134, 0);
			this.rightToolsTableLayoutPanel.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
			this.rightToolsTableLayoutPanel.Name = "rightToolsTableLayoutPanel";
			this.rightToolsTableLayoutPanel.RowCount = 3;
			this.rightToolsTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.rightToolsTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.rightToolsTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.rightToolsTableLayoutPanel.Size = new System.Drawing.Size(24, 128);
			this.rightToolsTableLayoutPanel.TabIndex = 8;
			// 
			// baseImageModeButton
			// 
			this.baseImageModeButton.Appearance = System.Windows.Forms.Appearance.Button;
			this.baseImageModeButton.Checked = true;
			this.baseImageModeButton.Location = new System.Drawing.Point(0, 0);
			this.baseImageModeButton.Margin = new System.Windows.Forms.Padding(0);
			this.baseImageModeButton.Name = "baseImageModeButton";
			this.baseImageModeButton.Size = new System.Drawing.Size(24, 23);
			this.baseImageModeButton.TabIndex = 0;
			this.baseImageModeButton.TabStop = true;
			this.baseImageModeButton.Text = "B";
			this.baseImageModeButton.UseVisualStyleBackColor = true;
			this.baseImageModeButton.CheckedChanged += new System.EventHandler(this.baseImageModeButton_CheckedChanged);
			// 
			// effectedImageModeButton
			// 
			this.effectedImageModeButton.Appearance = System.Windows.Forms.Appearance.Button;
			this.effectedImageModeButton.Location = new System.Drawing.Point(0, 26);
			this.effectedImageModeButton.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
			this.effectedImageModeButton.Name = "effectedImageModeButton";
			this.effectedImageModeButton.Size = new System.Drawing.Size(24, 23);
			this.effectedImageModeButton.TabIndex = 1;
			this.effectedImageModeButton.Text = "E";
			this.effectedImageModeButton.UseVisualStyleBackColor = true;
			this.effectedImageModeButton.CheckedChanged += new System.EventHandler(this.effectedImageModeButton_CheckedChanged);
			// 
			// CharacterDisplay
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.mainTableLayoutPanel);
			this.Name = "CharacterDisplay";
			this.Size = new System.Drawing.Size(161, 234);
			this.Enter += new System.EventHandler(this.CharacterDisplay_Enter);
			this.mainTableLayoutPanel.ResumeLayout(false);
			this.mainTableLayoutPanel.PerformLayout();
			this.parametersTableLayoutPanel.ResumeLayout(false);
			this.parametersTableLayoutPanel.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.widthNumericUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ascendNumericUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.advanceNumericUpDown)).EndInit();
			this.leftToolsTableLayoutPanel.ResumeLayout(false);
			this.leftToolsTableLayoutPanel.PerformLayout();
			this.rightToolsTableLayoutPanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel mainTableLayoutPanel;
		private System.Windows.Forms.TableLayoutPanel leftToolsTableLayoutPanel;
		private System.Windows.Forms.CheckBox paintModeButton;
		private CharacterViewer characterViewer;
		private System.Windows.Forms.TableLayoutPanel parametersTableLayoutPanel;
		private System.Windows.Forms.Label widthLabel;
		private System.Windows.Forms.NumericUpDown widthNumericUpDown;
		private System.Windows.Forms.Label ascendsLabel;
		private System.Windows.Forms.Label advancesLabel;
		private System.Windows.Forms.NumericUpDown ascendNumericUpDown;
		private System.Windows.Forms.NumericUpDown advanceNumericUpDown;
		private System.Windows.Forms.Label unicodeLabel;
		private System.Windows.Forms.Label unicodeValueLabel;
		private System.Windows.Forms.TableLayoutPanel rightToolsTableLayoutPanel;
		private System.Windows.Forms.RadioButton baseImageModeButton;
		private System.Windows.Forms.RadioButton effectedImageModeButton;
		private System.Windows.Forms.Panel colorPanel;
	}
}
