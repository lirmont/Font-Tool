namespace FontTool
{
	partial class AddUnicodeCharacter
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
			this.addButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.unicodeBlockLabel = new System.Windows.Forms.Label();
			this.characterLabel = new System.Windows.Forms.Label();
			this.unicodeBlockComboBox = new System.Windows.Forms.ComboBox();
			this.characterComboBox = new System.Windows.Forms.ComboBox();
			this.tableLayoutPanel.SuspendLayout();
			this.buttonsTableLayoutPanel.SuspendLayout();
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
			this.tableLayoutPanel.Controls.Add(this.buttonsTableLayoutPanel, 0, 3);
			this.tableLayoutPanel.Controls.Add(this.unicodeBlockLabel, 0, 0);
			this.tableLayoutPanel.Controls.Add(this.characterLabel, 0, 1);
			this.tableLayoutPanel.Controls.Add(this.unicodeBlockComboBox, 1, 0);
			this.tableLayoutPanel.Controls.Add(this.characterComboBox, 1, 1);
			this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel.Name = "tableLayoutPanel";
			this.tableLayoutPanel.RowCount = 4;
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel.Size = new System.Drawing.Size(288, 101);
			this.tableLayoutPanel.TabIndex = 0;
			// 
			// buttonsTableLayoutPanel
			// 
			this.buttonsTableLayoutPanel.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.buttonsTableLayoutPanel.AutoSize = true;
			this.buttonsTableLayoutPanel.ColumnCount = 2;
			this.tableLayoutPanel.SetColumnSpan(this.buttonsTableLayoutPanel, 2);
			this.buttonsTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.buttonsTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.buttonsTableLayoutPanel.Controls.Add(this.addButton, 0, 0);
			this.buttonsTableLayoutPanel.Controls.Add(this.cancelButton, 1, 0);
			this.buttonsTableLayoutPanel.Location = new System.Drawing.Point(63, 69);
			this.buttonsTableLayoutPanel.Name = "buttonsTableLayoutPanel";
			this.buttonsTableLayoutPanel.RowCount = 1;
			this.buttonsTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.buttonsTableLayoutPanel.Size = new System.Drawing.Size(162, 29);
			this.buttonsTableLayoutPanel.TabIndex = 0;
			// 
			// addButton
			// 
			this.addButton.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.addButton.Location = new System.Drawing.Point(3, 3);
			this.addButton.Name = "addButton";
			this.addButton.Size = new System.Drawing.Size(75, 23);
			this.addButton.TabIndex = 0;
			this.addButton.Text = "Add";
			this.addButton.UseVisualStyleBackColor = true;
			this.addButton.Click += new System.EventHandler(this.addButton_Click);
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
			// unicodeBlockLabel
			// 
			this.unicodeBlockLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.unicodeBlockLabel.AutoSize = true;
			this.unicodeBlockLabel.Location = new System.Drawing.Point(3, 7);
			this.unicodeBlockLabel.Name = "unicodeBlockLabel";
			this.unicodeBlockLabel.Size = new System.Drawing.Size(80, 13);
			this.unicodeBlockLabel.TabIndex = 1;
			this.unicodeBlockLabel.Text = "Unicode Block:";
			this.unicodeBlockLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// characterLabel
			// 
			this.characterLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.characterLabel.AutoSize = true;
			this.characterLabel.Location = new System.Drawing.Point(27, 34);
			this.characterLabel.Name = "characterLabel";
			this.characterLabel.Size = new System.Drawing.Size(56, 13);
			this.characterLabel.TabIndex = 2;
			this.characterLabel.Text = "Character:";
			this.characterLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// unicodeBlockComboBox
			// 
			this.unicodeBlockComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.unicodeBlockComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.unicodeBlockComboBox.FormattingEnabled = true;
			this.unicodeBlockComboBox.Location = new System.Drawing.Point(89, 3);
			this.unicodeBlockComboBox.Name = "unicodeBlockComboBox";
			this.unicodeBlockComboBox.Size = new System.Drawing.Size(196, 21);
			this.unicodeBlockComboBox.TabIndex = 3;
			this.unicodeBlockComboBox.SelectedIndexChanged += new System.EventHandler(this.unicodeBlockComboBox_SelectedIndexChanged);
			// 
			// characterComboBox
			// 
			this.characterComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.characterComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.characterComboBox.DropDownWidth = 500;
			this.characterComboBox.FormattingEnabled = true;
			this.characterComboBox.Location = new System.Drawing.Point(89, 30);
			this.characterComboBox.Name = "characterComboBox";
			this.characterComboBox.Size = new System.Drawing.Size(196, 21);
			this.characterComboBox.TabIndex = 4;
			// 
			// AddUnicodeCharacter
			// 
			this.AcceptButton = this.addButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(289, 102);
			this.Controls.Add(this.tableLayoutPanel);
			this.Name = "AddUnicodeCharacter";
			this.ShowIcon = false;
			this.Text = "Add Unicode Character";
			this.tableLayoutPanel.ResumeLayout(false);
			this.tableLayoutPanel.PerformLayout();
			this.buttonsTableLayoutPanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
		private System.Windows.Forms.TableLayoutPanel buttonsTableLayoutPanel;
		private System.Windows.Forms.Button addButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Label unicodeBlockLabel;
		private System.Windows.Forms.Label characterLabel;
		private System.Windows.Forms.ComboBox unicodeBlockComboBox;
		private System.Windows.Forms.ComboBox characterComboBox;
	}
}