namespace FontTool
{
	partial class AddOrEditFont
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
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.buttonTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
			this.saveButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.nameLabel = new System.Windows.Forms.Label();
			this.sizesLabel = new System.Windows.Forms.Label();
			this.sizesListView = new System.Windows.Forms.ListView();
			this.sizeColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.totalPrintableCharactersColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.sizesContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.addSizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.editSizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.removeSizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.viewCoverageReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.nameMaskedTextBox = new FontTool.MaskedTextBox();
			this.colorsLabel = new System.Windows.Forms.Label();
			this.colorsListView = new System.Windows.Forms.ListView();
			this.colorColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.effectsColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colorsContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.addColorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.editColorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.removeColorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.tableLayoutPanel1.SuspendLayout();
			this.buttonTableLayoutPanel.SuspendLayout();
			this.sizesContextMenuStrip.SuspendLayout();
			this.colorsContextMenuStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.buttonTableLayoutPanel, 0, 5);
			this.tableLayoutPanel1.Controls.Add(this.nameLabel, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.sizesLabel, 0, 3);
			this.tableLayoutPanel1.Controls.Add(this.sizesListView, 0, 4);
			this.tableLayoutPanel1.Controls.Add(this.nameMaskedTextBox, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.colorsLabel, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.colorsListView, 0, 2);
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 6;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.Size = new System.Drawing.Size(344, 298);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// buttonTableLayoutPanel
			// 
			this.buttonTableLayoutPanel.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.buttonTableLayoutPanel.AutoSize = true;
			this.buttonTableLayoutPanel.ColumnCount = 2;
			this.tableLayoutPanel1.SetColumnSpan(this.buttonTableLayoutPanel, 2);
			this.buttonTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.buttonTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.buttonTableLayoutPanel.Controls.Add(this.saveButton, 0, 0);
			this.buttonTableLayoutPanel.Controls.Add(this.cancelButton, 1, 0);
			this.buttonTableLayoutPanel.Location = new System.Drawing.Point(91, 265);
			this.buttonTableLayoutPanel.Name = "buttonTableLayoutPanel";
			this.buttonTableLayoutPanel.RowCount = 1;
			this.buttonTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.buttonTableLayoutPanel.Size = new System.Drawing.Size(162, 29);
			this.buttonTableLayoutPanel.TabIndex = 0;
			// 
			// saveButton
			// 
			this.saveButton.Anchor = System.Windows.Forms.AnchorStyles.None;
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
			this.cancelButton.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(84, 3);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 23);
			this.cancelButton.TabIndex = 1;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			// 
			// nameLabel
			// 
			this.nameLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.nameLabel.AutoSize = true;
			this.nameLabel.Location = new System.Drawing.Point(4, 6);
			this.nameLabel.Name = "nameLabel";
			this.nameLabel.Size = new System.Drawing.Size(38, 13);
			this.nameLabel.TabIndex = 1;
			this.nameLabel.Text = "Name:";
			// 
			// sizesLabel
			// 
			this.sizesLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.sizesLabel.AutoSize = true;
			this.sizesLabel.Location = new System.Drawing.Point(7, 144);
			this.sizesLabel.Name = "sizesLabel";
			this.sizesLabel.Size = new System.Drawing.Size(35, 13);
			this.sizesLabel.TabIndex = 2;
			this.sizesLabel.Text = "Sizes:";
			// 
			// sizesListView
			// 
			this.sizesListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.sizesListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.sizeColumnHeader,
            this.totalPrintableCharactersColumnHeader});
			this.tableLayoutPanel1.SetColumnSpan(this.sizesListView, 2);
			this.sizesListView.ContextMenuStrip = this.sizesContextMenuStrip;
			this.sizesListView.FullRowSelect = true;
			this.sizesListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.sizesListView.Location = new System.Drawing.Point(3, 160);
			this.sizesListView.MultiSelect = false;
			this.sizesListView.Name = "sizesListView";
			this.sizesListView.ShowGroups = false;
			this.sizesListView.ShowItemToolTips = true;
			this.sizesListView.Size = new System.Drawing.Size(338, 99);
			this.sizesListView.TabIndex = 3;
			this.sizesListView.UseCompatibleStateImageBehavior = false;
			this.sizesListView.View = System.Windows.Forms.View.Details;
			// 
			// sizeColumnHeader
			// 
			this.sizeColumnHeader.Text = "Size";
			// 
			// totalPrintableCharactersColumnHeader
			// 
			this.totalPrintableCharactersColumnHeader.Text = "Total Printable Characters";
			this.totalPrintableCharactersColumnHeader.Width = 192;
			// 
			// sizesContextMenuStrip
			// 
			this.sizesContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addSizeToolStripMenuItem,
            this.editSizeToolStripMenuItem,
            this.removeSizeToolStripMenuItem,
            this.toolStripSeparator1,
            this.viewCoverageReportToolStripMenuItem});
			this.sizesContextMenuStrip.Name = "sizesContextMenuStrip";
			this.sizesContextMenuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.sizesContextMenuStrip.Size = new System.Drawing.Size(194, 120);
			this.sizesContextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.sizesContextMenuStrip_Opening);
			// 
			// addSizeToolStripMenuItem
			// 
			this.addSizeToolStripMenuItem.Name = "addSizeToolStripMenuItem";
			this.addSizeToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
			this.addSizeToolStripMenuItem.Text = "Add Size";
			this.addSizeToolStripMenuItem.Click += new System.EventHandler(this.addSizeToolStripMenuItem_Click);
			// 
			// editSizeToolStripMenuItem
			// 
			this.editSizeToolStripMenuItem.Name = "editSizeToolStripMenuItem";
			this.editSizeToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
			this.editSizeToolStripMenuItem.Text = "Edit Size";
			this.editSizeToolStripMenuItem.Click += new System.EventHandler(this.editSizeToolStripMenuItem_Click);
			// 
			// removeSizeToolStripMenuItem
			// 
			this.removeSizeToolStripMenuItem.Name = "removeSizeToolStripMenuItem";
			this.removeSizeToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
			this.removeSizeToolStripMenuItem.Text = "Remove Size";
			this.removeSizeToolStripMenuItem.Click += new System.EventHandler(this.removeSizeToolStripMenuItem_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(190, 6);
			// 
			// viewCoverageReportToolStripMenuItem
			// 
			this.viewCoverageReportToolStripMenuItem.Name = "viewCoverageReportToolStripMenuItem";
			this.viewCoverageReportToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
			this.viewCoverageReportToolStripMenuItem.Text = "View Coverage Report";
			this.viewCoverageReportToolStripMenuItem.Click += new System.EventHandler(this.viewCoverageReportToolStripMenuItem_Click);
			// 
			// nameMaskedTextBox
			// 
			this.nameMaskedTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.nameMaskedTextBox.EmptyText = "Name of the font.";
			this.nameMaskedTextBox.Location = new System.Drawing.Point(48, 3);
			this.nameMaskedTextBox.MarkInvalid = false;
			this.nameMaskedTextBox.Name = "nameMaskedTextBox";
			this.nameMaskedTextBox.Size = new System.Drawing.Size(293, 20);
			this.nameMaskedTextBox.TabIndex = 4;
			this.nameMaskedTextBox.ModifiedChanged += new System.EventHandler(this.nameMaskedTextBox_ModifiedChanged);
			this.nameMaskedTextBox.TextChanged += new System.EventHandler(this.nameMaskedTextBox_TextChanged);
			// 
			// colorsLabel
			// 
			this.colorsLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.colorsLabel.AutoSize = true;
			this.colorsLabel.Location = new System.Drawing.Point(3, 26);
			this.colorsLabel.Name = "colorsLabel";
			this.colorsLabel.Size = new System.Drawing.Size(39, 13);
			this.colorsLabel.TabIndex = 5;
			this.colorsLabel.Text = "Colors:";
			this.colorsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// colorsListView
			// 
			this.colorsListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.colorsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colorColumnHeader,
            this.effectsColumnHeader});
			this.tableLayoutPanel1.SetColumnSpan(this.colorsListView, 2);
			this.colorsListView.ContextMenuStrip = this.colorsContextMenuStrip;
			this.colorsListView.FullRowSelect = true;
			this.colorsListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.colorsListView.Location = new System.Drawing.Point(3, 42);
			this.colorsListView.MultiSelect = false;
			this.colorsListView.Name = "colorsListView";
			this.colorsListView.Size = new System.Drawing.Size(338, 99);
			this.colorsListView.TabIndex = 6;
			this.colorsListView.UseCompatibleStateImageBehavior = false;
			this.colorsListView.View = System.Windows.Forms.View.Details;
			// 
			// colorColumnHeader
			// 
			this.colorColumnHeader.Text = "Color";
			this.colorColumnHeader.Width = 97;
			// 
			// effectsColumnHeader
			// 
			this.effectsColumnHeader.Text = "Effects";
			this.effectsColumnHeader.Width = 154;
			// 
			// colorsContextMenuStrip
			// 
			this.colorsContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addColorToolStripMenuItem,
            this.editColorToolStripMenuItem,
            this.removeColorToolStripMenuItem});
			this.colorsContextMenuStrip.Name = "colorsContextMenuStrip";
			this.colorsContextMenuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.colorsContextMenuStrip.Size = new System.Drawing.Size(153, 70);
			this.colorsContextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.colorsContextMenuStrip_Opening);
			// 
			// addColorToolStripMenuItem
			// 
			this.addColorToolStripMenuItem.Name = "addColorToolStripMenuItem";
			this.addColorToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.addColorToolStripMenuItem.Text = "Add Color";
			this.addColorToolStripMenuItem.Click += new System.EventHandler(this.addColorToolStripMenuItem_Click);
			// 
			// editColorToolStripMenuItem
			// 
			this.editColorToolStripMenuItem.Name = "editColorToolStripMenuItem";
			this.editColorToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.editColorToolStripMenuItem.Text = "Edit Color";
			this.editColorToolStripMenuItem.Click += new System.EventHandler(this.editColorToolStripMenuItem_Click);
			// 
			// removeColorToolStripMenuItem
			// 
			this.removeColorToolStripMenuItem.Name = "removeColorToolStripMenuItem";
			this.removeColorToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.removeColorToolStripMenuItem.Text = "Remove Color";
			this.removeColorToolStripMenuItem.Click += new System.EventHandler(this.removeColorToolStripMenuItem_Click);
			// 
			// AddOrEditFont
			// 
			this.AcceptButton = this.saveButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(345, 299);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "AddOrEditFont";
			this.ShowIcon = false;
			this.Text = "Add or Edit Font";
			this.Shown += new System.EventHandler(this.AddOrEditFont_Shown);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.buttonTableLayoutPanel.ResumeLayout(false);
			this.sizesContextMenuStrip.ResumeLayout(false);
			this.colorsContextMenuStrip.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.TableLayoutPanel buttonTableLayoutPanel;
		private System.Windows.Forms.Button saveButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Label nameLabel;
		private System.Windows.Forms.Label sizesLabel;
		private System.Windows.Forms.ListView sizesListView;
		private MaskedTextBox nameMaskedTextBox;
		private System.Windows.Forms.ContextMenuStrip sizesContextMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem addSizeToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem editSizeToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem removeSizeToolStripMenuItem;
		private System.Windows.Forms.ColumnHeader sizeColumnHeader;
		private System.Windows.Forms.ColumnHeader totalPrintableCharactersColumnHeader;
		private System.Windows.Forms.Label colorsLabel;
		private System.Windows.Forms.ListView colorsListView;
		private System.Windows.Forms.ColumnHeader colorColumnHeader;
		private System.Windows.Forms.ColumnHeader effectsColumnHeader;
		private System.Windows.Forms.ContextMenuStrip colorsContextMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem addColorToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem editColorToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem removeColorToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem viewCoverageReportToolStripMenuItem;
	}
}