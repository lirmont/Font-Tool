namespace FontTool
{
	partial class AddOrEditFontSize
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
			this.components = new System.ComponentModel.Container();
			this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
			this.targetAscentLabel = new System.Windows.Forms.Label();
			this.targetDescentLabel = new System.Windows.Forms.Label();
			this.targetWidthLabel = new System.Windows.Forms.Label();
			this.variantsLabel = new System.Windows.Forms.Label();
			this.variantsListView = new System.Windows.Forms.ListView();
			this.variantColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.effectsColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.variantsContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.addVariantToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.editVariantToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.removeVariantToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.buttonsTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
			this.saveButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.targetAscentMaskedTextBox = new FontTool.MaskedTextBox();
			this.targetDescentMaskedTextBox = new FontTool.MaskedTextBox();
			this.targetWidthMaskedTextBox = new FontTool.MaskedTextBox();
			this.sizeLabel = new System.Windows.Forms.Label();
			this.sizeMaskedTextBox = new FontTool.MaskedTextBox();
			this.tableLayoutPanel.SuspendLayout();
			this.variantsContextMenuStrip.SuspendLayout();
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
			this.tableLayoutPanel.Controls.Add(this.targetAscentLabel, 0, 1);
			this.tableLayoutPanel.Controls.Add(this.targetDescentLabel, 0, 2);
			this.tableLayoutPanel.Controls.Add(this.targetWidthLabel, 0, 3);
			this.tableLayoutPanel.Controls.Add(this.variantsLabel, 0, 4);
			this.tableLayoutPanel.Controls.Add(this.variantsListView, 0, 5);
			this.tableLayoutPanel.Controls.Add(this.buttonsTableLayoutPanel, 0, 6);
			this.tableLayoutPanel.Controls.Add(this.targetAscentMaskedTextBox, 1, 1);
			this.tableLayoutPanel.Controls.Add(this.targetDescentMaskedTextBox, 1, 2);
			this.tableLayoutPanel.Controls.Add(this.targetWidthMaskedTextBox, 1, 3);
			this.tableLayoutPanel.Controls.Add(this.sizeLabel, 0, 0);
			this.tableLayoutPanel.Controls.Add(this.sizeMaskedTextBox, 1, 0);
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
			this.tableLayoutPanel.Size = new System.Drawing.Size(389, 248);
			this.tableLayoutPanel.TabIndex = 0;
			// 
			// targetAscentLabel
			// 
			this.targetAscentLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.targetAscentLabel.AutoSize = true;
			this.targetAscentLabel.Location = new System.Drawing.Point(10, 32);
			this.targetAscentLabel.Name = "targetAscentLabel";
			this.targetAscentLabel.Size = new System.Drawing.Size(77, 13);
			this.targetAscentLabel.TabIndex = 0;
			this.targetAscentLabel.Text = "Target Ascent:";
			// 
			// targetDescentLabel
			// 
			this.targetDescentLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.targetDescentLabel.AutoSize = true;
			this.targetDescentLabel.Location = new System.Drawing.Point(3, 58);
			this.targetDescentLabel.Name = "targetDescentLabel";
			this.targetDescentLabel.Size = new System.Drawing.Size(84, 13);
			this.targetDescentLabel.TabIndex = 1;
			this.targetDescentLabel.Text = "Target Descent:";
			// 
			// targetWidthLabel
			// 
			this.targetWidthLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.targetWidthLabel.AutoSize = true;
			this.targetWidthLabel.Location = new System.Drawing.Point(15, 84);
			this.targetWidthLabel.Name = "targetWidthLabel";
			this.targetWidthLabel.Size = new System.Drawing.Size(72, 13);
			this.targetWidthLabel.TabIndex = 2;
			this.targetWidthLabel.Text = "Target Width:";
			// 
			// variantsLabel
			// 
			this.variantsLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.variantsLabel.AutoSize = true;
			this.variantsLabel.Location = new System.Drawing.Point(3, 104);
			this.variantsLabel.Name = "variantsLabel";
			this.variantsLabel.Size = new System.Drawing.Size(48, 13);
			this.variantsLabel.TabIndex = 3;
			this.variantsLabel.Text = "Variants:";
			// 
			// variantsListView
			// 
			this.variantsListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.variantsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.variantColumnHeader,
            this.effectsColumnHeader});
			this.tableLayoutPanel.SetColumnSpan(this.variantsListView, 2);
			this.variantsListView.ContextMenuStrip = this.variantsContextMenuStrip;
			this.variantsListView.FullRowSelect = true;
			this.variantsListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.variantsListView.Location = new System.Drawing.Point(3, 120);
			this.variantsListView.MultiSelect = false;
			this.variantsListView.Name = "variantsListView";
			this.variantsListView.ShowGroups = false;
			this.variantsListView.Size = new System.Drawing.Size(383, 90);
			this.variantsListView.Sorting = System.Windows.Forms.SortOrder.Ascending;
			this.variantsListView.TabIndex = 5;
			this.variantsListView.UseCompatibleStateImageBehavior = false;
			this.variantsListView.View = System.Windows.Forms.View.Details;
			// 
			// variantColumnHeader
			// 
			this.variantColumnHeader.Text = "Variant";
			this.variantColumnHeader.Width = 87;
			// 
			// effectsColumnHeader
			// 
			this.effectsColumnHeader.Text = "Effects";
			this.effectsColumnHeader.Width = 282;
			// 
			// variantsContextMenuStrip
			// 
			this.variantsContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addVariantToolStripMenuItem,
            this.editVariantToolStripMenuItem,
            this.removeVariantToolStripMenuItem});
			this.variantsContextMenuStrip.Name = "variantsContextMenuStrip";
			this.variantsContextMenuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.variantsContextMenuStrip.Size = new System.Drawing.Size(151, 70);
			this.variantsContextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.variantsContextMenuStrip_Opening);
			// 
			// addVariantToolStripMenuItem
			// 
			this.addVariantToolStripMenuItem.Name = "addVariantToolStripMenuItem";
			this.addVariantToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.addVariantToolStripMenuItem.Text = "Add Variant";
			this.addVariantToolStripMenuItem.Click += new System.EventHandler(this.addVariantToolStripMenuItem_Click);
			// 
			// editVariantToolStripMenuItem
			// 
			this.editVariantToolStripMenuItem.Name = "editVariantToolStripMenuItem";
			this.editVariantToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.editVariantToolStripMenuItem.Text = "Edit Variant";
			this.editVariantToolStripMenuItem.Click += new System.EventHandler(this.editVariantToolStripMenuItem_Click);
			// 
			// removeVariantToolStripMenuItem
			// 
			this.removeVariantToolStripMenuItem.Name = "removeVariantToolStripMenuItem";
			this.removeVariantToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.removeVariantToolStripMenuItem.Text = "Remove Variant";
			this.removeVariantToolStripMenuItem.Click += new System.EventHandler(this.removeVariantToolStripMenuItem_Click);
			// 
			// buttonsTableLayoutPanel
			// 
			this.buttonsTableLayoutPanel.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.buttonsTableLayoutPanel.AutoSize = true;
			this.buttonsTableLayoutPanel.ColumnCount = 2;
			this.tableLayoutPanel.SetColumnSpan(this.buttonsTableLayoutPanel, 2);
			this.buttonsTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.buttonsTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.buttonsTableLayoutPanel.Controls.Add(this.saveButton, 0, 0);
			this.buttonsTableLayoutPanel.Controls.Add(this.cancelButton, 1, 0);
			this.buttonsTableLayoutPanel.Location = new System.Drawing.Point(113, 216);
			this.buttonsTableLayoutPanel.Name = "buttonsTableLayoutPanel";
			this.buttonsTableLayoutPanel.RowCount = 1;
			this.buttonsTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.buttonsTableLayoutPanel.Size = new System.Drawing.Size(162, 29);
			this.buttonsTableLayoutPanel.TabIndex = 5;
			// 
			// saveButton
			// 
			this.saveButton.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.saveButton.Location = new System.Drawing.Point(3, 3);
			this.saveButton.Name = "saveButton";
			this.saveButton.Size = new System.Drawing.Size(75, 23);
			this.saveButton.TabIndex = 5;
			this.saveButton.Text = "Save";
			this.saveButton.UseVisualStyleBackColor = true;
			this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
			// 
			// cancelButton
			// 
			this.cancelButton.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(84, 3);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 23);
			this.cancelButton.TabIndex = 6;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			// 
			// targetAscentMaskedTextBox
			// 
			this.targetAscentMaskedTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.targetAscentMaskedTextBox.Ceiling = 99999D;
			this.targetAscentMaskedTextBox.EmptyText = "Maximum vertical ascent, an interface-only setting.";
			this.targetAscentMaskedTextBox.IsNumeric = true;
			this.targetAscentMaskedTextBox.Location = new System.Drawing.Point(93, 29);
			this.targetAscentMaskedTextBox.MarkInvalid = false;
			this.targetAscentMaskedTextBox.Name = "targetAscentMaskedTextBox";
			this.targetAscentMaskedTextBox.Size = new System.Drawing.Size(293, 20);
			this.targetAscentMaskedTextBox.TabIndex = 2;
			this.targetAscentMaskedTextBox.TextChanged += new System.EventHandler(this.targetAscentMaskedTextBox_TextChanged);
			// 
			// targetDescentMaskedTextBox
			// 
			this.targetDescentMaskedTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.targetDescentMaskedTextBox.Ceiling = 99999D;
			this.targetDescentMaskedTextBox.EmptyText = "Maximum vertical descent, an interface-only setting.";
			this.targetDescentMaskedTextBox.IsNumeric = true;
			this.targetDescentMaskedTextBox.Location = new System.Drawing.Point(93, 55);
			this.targetDescentMaskedTextBox.MarkInvalid = false;
			this.targetDescentMaskedTextBox.Name = "targetDescentMaskedTextBox";
			this.targetDescentMaskedTextBox.Size = new System.Drawing.Size(293, 20);
			this.targetDescentMaskedTextBox.TabIndex = 3;
			this.targetDescentMaskedTextBox.TextChanged += new System.EventHandler(this.targetDescentMaskedTextBox_TextChanged);
			// 
			// targetWidthMaskedTextBox
			// 
			this.targetWidthMaskedTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.targetWidthMaskedTextBox.Ceiling = 99999D;
			this.targetWidthMaskedTextBox.EmptyText = "Default character width, an interface-only setting.";
			this.targetWidthMaskedTextBox.IsNumeric = true;
			this.targetWidthMaskedTextBox.Location = new System.Drawing.Point(93, 81);
			this.targetWidthMaskedTextBox.MarkInvalid = false;
			this.targetWidthMaskedTextBox.Name = "targetWidthMaskedTextBox";
			this.targetWidthMaskedTextBox.Size = new System.Drawing.Size(293, 20);
			this.targetWidthMaskedTextBox.TabIndex = 4;
			this.targetWidthMaskedTextBox.TextChanged += new System.EventHandler(this.targetWidthMaskedTextBox_TextChanged);
			// 
			// sizeLabel
			// 
			this.sizeLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.sizeLabel.AutoSize = true;
			this.sizeLabel.Location = new System.Drawing.Point(57, 6);
			this.sizeLabel.Name = "sizeLabel";
			this.sizeLabel.Size = new System.Drawing.Size(30, 13);
			this.sizeLabel.TabIndex = 9;
			this.sizeLabel.Text = "Size:";
			// 
			// sizeMaskedTextBox
			// 
			this.sizeMaskedTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.sizeMaskedTextBox.Ceiling = 99999D;
			this.sizeMaskedTextBox.EmptyText = "Bounding box height, a primarily organizational setting.";
			this.sizeMaskedTextBox.Floor = 1D;
			this.sizeMaskedTextBox.IsNumeric = true;
			this.sizeMaskedTextBox.Location = new System.Drawing.Point(93, 3);
			this.sizeMaskedTextBox.MarkInvalid = false;
			this.sizeMaskedTextBox.Name = "sizeMaskedTextBox";
			this.sizeMaskedTextBox.Size = new System.Drawing.Size(293, 20);
			this.sizeMaskedTextBox.TabIndex = 1;
			this.sizeMaskedTextBox.TextChanged += new System.EventHandler(this.sizeMaskedTextBox_TextChanged);
			// 
			// AddOrEditFontSize
			// 
			this.AcceptButton = this.saveButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(390, 249);
			this.Controls.Add(this.tableLayoutPanel);
			this.Name = "AddOrEditFontSize";
			this.ShowIcon = false;
			this.Text = "Add or Edit Font Size";
			this.Shown += new System.EventHandler(this.AddOrEditFontSize_Shown);
			this.tableLayoutPanel.ResumeLayout(false);
			this.tableLayoutPanel.PerformLayout();
			this.variantsContextMenuStrip.ResumeLayout(false);
			this.buttonsTableLayoutPanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
		private System.Windows.Forms.Label targetAscentLabel;
		private System.Windows.Forms.Label targetDescentLabel;
		private System.Windows.Forms.Label targetWidthLabel;
		private System.Windows.Forms.Label variantsLabel;
		private System.Windows.Forms.ListView variantsListView;
		private System.Windows.Forms.TableLayoutPanel buttonsTableLayoutPanel;
		private System.Windows.Forms.Button saveButton;
		private System.Windows.Forms.Button cancelButton;
		private MaskedTextBox targetAscentMaskedTextBox;
		private MaskedTextBox targetDescentMaskedTextBox;
		private MaskedTextBox targetWidthMaskedTextBox;
		private System.Windows.Forms.ColumnHeader variantColumnHeader;
		private System.Windows.Forms.ContextMenuStrip variantsContextMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem addVariantToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem editVariantToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem removeVariantToolStripMenuItem;
		private System.Windows.Forms.Label sizeLabel;
		private MaskedTextBox sizeMaskedTextBox;
		private System.Windows.Forms.ColumnHeader effectsColumnHeader;
	}
}