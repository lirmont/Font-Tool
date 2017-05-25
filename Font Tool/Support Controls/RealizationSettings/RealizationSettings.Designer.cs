namespace TextAnalyzer
{
	partial class RealizationSettings
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
			System.Windows.Forms.Label label1;
			System.Windows.Forms.Label label2;
			System.Windows.Forms.Label label5;
			System.Windows.Forms.Label label6;
			System.Windows.Forms.Label label7;
			this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
			this.pictureBoxTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
			this.linesPictureBox = new System.Windows.Forms.PictureBox();
			this.fontComboBox = new System.Windows.Forms.ComboBox();
			this.variantComboBox = new System.Windows.Forms.ComboBox();
			this.alternateCheckBox = new System.Windows.Forms.CheckBox();
			this.antialiasedCheckBox = new System.Windows.Forms.CheckBox();
			this.matteTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
			this.matteColorPanel = new System.Windows.Forms.Panel();
			this.label3 = new System.Windows.Forms.Label();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.referenceColorPanel = new System.Windows.Forms.Panel();
			this.label4 = new System.Windows.Forms.Label();
			this.sizeTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
			this.sizeNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.fromPointsButton = new System.Windows.Forms.Button();
			this.referenceTextBox = new System.Windows.Forms.TextBox();
			this.buttonsTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
			this.okButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			label1 = new System.Windows.Forms.Label();
			label2 = new System.Windows.Forms.Label();
			label5 = new System.Windows.Forms.Label();
			label6 = new System.Windows.Forms.Label();
			label7 = new System.Windows.Forms.Label();
			this.tableLayoutPanel.SuspendLayout();
			this.pictureBoxTableLayoutPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.linesPictureBox)).BeginInit();
			this.matteTableLayoutPanel.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.sizeTableLayoutPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.sizeNumericUpDown)).BeginInit();
			this.buttonsTableLayoutPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(56, 7);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(31, 13);
			label1.TabIndex = 3;
			label1.Text = "Font:";
			// 
			// label2
			// 
			label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(44, 63);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(43, 13);
			label2.TabIndex = 7;
			label2.Text = "Variant:";
			// 
			// label5
			// 
			label5.Anchor = System.Windows.Forms.AnchorStyles.Right;
			label5.AutoSize = true;
			label5.Location = new System.Drawing.Point(3, 150);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(84, 13);
			label5.TabIndex = 14;
			label5.Text = "Reference Text:";
			// 
			// label6
			// 
			label6.Anchor = System.Windows.Forms.AnchorStyles.Right;
			label6.AutoSize = true;
			label6.Location = new System.Drawing.Point(57, 35);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(30, 13);
			label6.TabIndex = 17;
			label6.Text = "Size:";
			// 
			// label7
			// 
			label7.AutoSize = true;
			label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			label7.Location = new System.Drawing.Point(3, 193);
			label7.Name = "label7";
			label7.Size = new System.Drawing.Size(56, 13);
			label7.TabIndex = 18;
			label7.Text = "Preview:";
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
			this.tableLayoutPanel.Controls.Add(this.pictureBoxTableLayoutPanel, 0, 9);
			this.tableLayoutPanel.Controls.Add(label1, 0, 0);
			this.tableLayoutPanel.Controls.Add(this.fontComboBox, 1, 0);
			this.tableLayoutPanel.Controls.Add(label2, 0, 2);
			this.tableLayoutPanel.Controls.Add(this.variantComboBox, 1, 2);
			this.tableLayoutPanel.Controls.Add(this.alternateCheckBox, 1, 7);
			this.tableLayoutPanel.Controls.Add(this.antialiasedCheckBox, 1, 3);
			this.tableLayoutPanel.Controls.Add(this.matteTableLayoutPanel, 1, 4);
			this.tableLayoutPanel.Controls.Add(this.tableLayoutPanel1, 1, 5);
			this.tableLayoutPanel.Controls.Add(this.sizeTableLayoutPanel, 1, 1);
			this.tableLayoutPanel.Controls.Add(label5, 0, 6);
			this.tableLayoutPanel.Controls.Add(this.referenceTextBox, 1, 6);
			this.tableLayoutPanel.Controls.Add(this.buttonsTableLayoutPanel, 0, 10);
			this.tableLayoutPanel.Controls.Add(label6, 0, 1);
			this.tableLayoutPanel.Controls.Add(label7, 0, 8);
			this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel.Name = "tableLayoutPanel";
			this.tableLayoutPanel.RowCount = 11;
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel.Size = new System.Drawing.Size(355, 323);
			this.tableLayoutPanel.TabIndex = 0;
			// 
			// pictureBoxTableLayoutPanel
			// 
			this.pictureBoxTableLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.pictureBoxTableLayoutPanel.AutoScroll = true;
			this.pictureBoxTableLayoutPanel.AutoScrollMargin = new System.Drawing.Size(4, 0);
			this.pictureBoxTableLayoutPanel.AutoSize = true;
			this.pictureBoxTableLayoutPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.pictureBoxTableLayoutPanel.BackColor = System.Drawing.SystemColors.Control;
			this.pictureBoxTableLayoutPanel.ColumnCount = 1;
			this.tableLayoutPanel.SetColumnSpan(this.pictureBoxTableLayoutPanel, 2);
			this.pictureBoxTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.pictureBoxTableLayoutPanel.Controls.Add(this.linesPictureBox, 0, 0);
			this.pictureBoxTableLayoutPanel.Location = new System.Drawing.Point(0, 206);
			this.pictureBoxTableLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
			this.pictureBoxTableLayoutPanel.Name = "pictureBoxTableLayoutPanel";
			this.pictureBoxTableLayoutPanel.Padding = new System.Windows.Forms.Padding(0, 0, 4, 0);
			this.pictureBoxTableLayoutPanel.RowCount = 1;
			this.pictureBoxTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.pictureBoxTableLayoutPanel.Size = new System.Drawing.Size(355, 82);
			this.pictureBoxTableLayoutPanel.TabIndex = 2;
			// 
			// linesPictureBox
			// 
			this.linesPictureBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.linesPictureBox.BackColor = System.Drawing.SystemColors.Control;
			this.linesPictureBox.Location = new System.Drawing.Point(125, 3);
			this.linesPictureBox.Name = "linesPictureBox";
			this.linesPictureBox.Size = new System.Drawing.Size(100, 50);
			this.linesPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.linesPictureBox.TabIndex = 0;
			this.linesPictureBox.TabStop = false;
			// 
			// fontComboBox
			// 
			this.fontComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.fontComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.fontComboBox.FormattingEnabled = true;
			this.fontComboBox.Location = new System.Drawing.Point(93, 3);
			this.fontComboBox.Name = "fontComboBox";
			this.fontComboBox.Size = new System.Drawing.Size(259, 21);
			this.fontComboBox.TabIndex = 4;
			this.fontComboBox.SelectedIndexChanged += new System.EventHandler(this.fontComboBox_SelectedIndexChanged);
			// 
			// variantComboBox
			// 
			this.variantComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.variantComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.variantComboBox.FormattingEnabled = true;
			this.variantComboBox.Location = new System.Drawing.Point(93, 59);
			this.variantComboBox.Name = "variantComboBox";
			this.variantComboBox.Size = new System.Drawing.Size(259, 21);
			this.variantComboBox.TabIndex = 8;
			this.variantComboBox.SelectedIndexChanged += new System.EventHandler(this.variantComboBox_SelectedIndexChanged);
			// 
			// alternateCheckBox
			// 
			this.alternateCheckBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.alternateCheckBox.AutoSize = true;
			this.alternateCheckBox.Location = new System.Drawing.Point(93, 173);
			this.alternateCheckBox.Name = "alternateCheckBox";
			this.alternateCheckBox.Size = new System.Drawing.Size(117, 17);
			this.alternateCheckBox.TabIndex = 9;
			this.alternateCheckBox.Text = "Alternate Alignment";
			this.alternateCheckBox.UseVisualStyleBackColor = true;
			this.alternateCheckBox.CheckedChanged += new System.EventHandler(this.alternateCheckBox_CheckedChanged);
			// 
			// antialiasedCheckBox
			// 
			this.antialiasedCheckBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.antialiasedCheckBox.AutoSize = true;
			this.antialiasedCheckBox.Location = new System.Drawing.Point(93, 86);
			this.antialiasedCheckBox.Name = "antialiasedCheckBox";
			this.antialiasedCheckBox.Size = new System.Drawing.Size(81, 17);
			this.antialiasedCheckBox.TabIndex = 10;
			this.antialiasedCheckBox.Text = "Anti-Aliased";
			this.antialiasedCheckBox.UseVisualStyleBackColor = true;
			this.antialiasedCheckBox.CheckedChanged += new System.EventHandler(this.antialiasedCheckBox_CheckedChanged);
			// 
			// matteTableLayoutPanel
			// 
			this.matteTableLayoutPanel.AutoSize = true;
			this.matteTableLayoutPanel.ColumnCount = 2;
			this.matteTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.matteTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.matteTableLayoutPanel.Controls.Add(this.matteColorPanel, 0, 0);
			this.matteTableLayoutPanel.Controls.Add(this.label3, 1, 0);
			this.matteTableLayoutPanel.Location = new System.Drawing.Point(90, 106);
			this.matteTableLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
			this.matteTableLayoutPanel.Name = "matteTableLayoutPanel";
			this.matteTableLayoutPanel.RowCount = 1;
			this.matteTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.matteTableLayoutPanel.Size = new System.Drawing.Size(105, 19);
			this.matteTableLayoutPanel.TabIndex = 11;
			// 
			// matteColorPanel
			// 
			this.matteColorPanel.BackColor = System.Drawing.Color.White;
			this.matteColorPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.matteColorPanel.Location = new System.Drawing.Point(3, 3);
			this.matteColorPanel.Name = "matteColorPanel";
			this.matteColorPanel.Size = new System.Drawing.Size(13, 13);
			this.matteColorPanel.TabIndex = 12;
			this.matteColorPanel.Click += new System.EventHandler(this.matteColorPanel_Click);
			// 
			// label3
			// 
			this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(22, 3);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(80, 13);
			this.label3.TabIndex = 13;
			this.label3.Text = "Anti-Alias Matte";
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.AutoSize = true;
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.Controls.Add(this.referenceColorPanel, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.label4, 1, 0);
			this.tableLayoutPanel1.Location = new System.Drawing.Point(90, 125);
			this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 1;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(143, 19);
			this.tableLayoutPanel1.TabIndex = 12;
			// 
			// referenceColorPanel
			// 
			this.referenceColorPanel.BackColor = System.Drawing.Color.White;
			this.referenceColorPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.referenceColorPanel.Location = new System.Drawing.Point(3, 3);
			this.referenceColorPanel.Name = "referenceColorPanel";
			this.referenceColorPanel.Size = new System.Drawing.Size(13, 13);
			this.referenceColorPanel.TabIndex = 12;
			this.referenceColorPanel.Click += new System.EventHandler(this.referenceColorPanel_Click);
			// 
			// label4
			// 
			this.label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(22, 3);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(118, 13);
			this.label4.TabIndex = 13;
			this.label4.Text = "Reference Background";
			// 
			// sizeTableLayoutPanel
			// 
			this.sizeTableLayoutPanel.AutoSize = true;
			this.sizeTableLayoutPanel.ColumnCount = 2;
			this.sizeTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.sizeTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.sizeTableLayoutPanel.Controls.Add(this.sizeNumericUpDown, 0, 0);
			this.sizeTableLayoutPanel.Controls.Add(this.fromPointsButton, 1, 0);
			this.sizeTableLayoutPanel.Location = new System.Drawing.Point(90, 27);
			this.sizeTableLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
			this.sizeTableLayoutPanel.Name = "sizeTableLayoutPanel";
			this.sizeTableLayoutPanel.RowCount = 1;
			this.sizeTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.sizeTableLayoutPanel.Size = new System.Drawing.Size(144, 29);
			this.sizeTableLayoutPanel.TabIndex = 13;
			// 
			// sizeNumericUpDown
			// 
			this.sizeNumericUpDown.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.sizeNumericUpDown.AutoSize = true;
			this.sizeNumericUpDown.Location = new System.Drawing.Point(3, 4);
			this.sizeNumericUpDown.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
			this.sizeNumericUpDown.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
			this.sizeNumericUpDown.Name = "sizeNumericUpDown";
			this.sizeNumericUpDown.Size = new System.Drawing.Size(57, 20);
			this.sizeNumericUpDown.TabIndex = 5;
			this.sizeNumericUpDown.Value = new decimal(new int[] {
            24,
            0,
            0,
            0});
			this.sizeNumericUpDown.ValueChanged += new System.EventHandler(this.sizeNumericUpDown_ValueChanged);
			// 
			// fromPointsButton
			// 
			this.fromPointsButton.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.fromPointsButton.AutoSize = true;
			this.fromPointsButton.Location = new System.Drawing.Point(66, 3);
			this.fromPointsButton.Name = "fromPointsButton";
			this.fromPointsButton.Size = new System.Drawing.Size(75, 23);
			this.fromPointsButton.TabIndex = 6;
			this.fromPointsButton.Text = "From Points";
			this.fromPointsButton.UseVisualStyleBackColor = true;
			this.fromPointsButton.Click += new System.EventHandler(this.fromPointsButton_Click);
			// 
			// referenceTextBox
			// 
			this.referenceTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.referenceTextBox.Location = new System.Drawing.Point(93, 147);
			this.referenceTextBox.Name = "referenceTextBox";
			this.referenceTextBox.Size = new System.Drawing.Size(259, 20);
			this.referenceTextBox.TabIndex = 15;
			this.referenceTextBox.Text = "+/-Yy.";
			this.referenceTextBox.TextChanged += new System.EventHandler(this.referenceTextBox_TextChanged);
			// 
			// buttonsTableLayoutPanel
			// 
			this.buttonsTableLayoutPanel.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.buttonsTableLayoutPanel.AutoSize = true;
			this.buttonsTableLayoutPanel.ColumnCount = 2;
			this.tableLayoutPanel.SetColumnSpan(this.buttonsTableLayoutPanel, 2);
			this.buttonsTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.buttonsTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.buttonsTableLayoutPanel.Controls.Add(this.okButton, 0, 0);
			this.buttonsTableLayoutPanel.Controls.Add(this.cancelButton, 1, 0);
			this.buttonsTableLayoutPanel.Location = new System.Drawing.Point(96, 291);
			this.buttonsTableLayoutPanel.Name = "buttonsTableLayoutPanel";
			this.buttonsTableLayoutPanel.RowCount = 1;
			this.buttonsTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.buttonsTableLayoutPanel.Size = new System.Drawing.Size(162, 29);
			this.buttonsTableLayoutPanel.TabIndex = 16;
			// 
			// okButton
			// 
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
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(84, 3);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 23);
			this.cancelButton.TabIndex = 1;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			// 
			// RealizationSettings
			// 
			this.AcceptButton = this.okButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(356, 324);
			this.Controls.Add(this.tableLayoutPanel);
			this.Name = "RealizationSettings";
			this.ShowIcon = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.Text = "Realization Settings";
			this.tableLayoutPanel.ResumeLayout(false);
			this.tableLayoutPanel.PerformLayout();
			this.pictureBoxTableLayoutPanel.ResumeLayout(false);
			this.pictureBoxTableLayoutPanel.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.linesPictureBox)).EndInit();
			this.matteTableLayoutPanel.ResumeLayout(false);
			this.matteTableLayoutPanel.PerformLayout();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.sizeTableLayoutPanel.ResumeLayout(false);
			this.sizeTableLayoutPanel.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.sizeNumericUpDown)).EndInit();
			this.buttonsTableLayoutPanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
		private System.Windows.Forms.TableLayoutPanel pictureBoxTableLayoutPanel;
		private System.Windows.Forms.PictureBox linesPictureBox;
		private System.Windows.Forms.ComboBox fontComboBox;
		private System.Windows.Forms.NumericUpDown sizeNumericUpDown;
		private System.Windows.Forms.ComboBox variantComboBox;
		private System.Windows.Forms.CheckBox alternateCheckBox;
		private System.Windows.Forms.CheckBox antialiasedCheckBox;
		private System.Windows.Forms.TableLayoutPanel matteTableLayoutPanel;
		private System.Windows.Forms.Panel matteColorPanel;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Panel referenceColorPanel;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TableLayoutPanel sizeTableLayoutPanel;
		private System.Windows.Forms.Button fromPointsButton;
		private System.Windows.Forms.TextBox referenceTextBox;
		private System.Windows.Forms.TableLayoutPanel buttonsTableLayoutPanel;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Button cancelButton;

	}
}

