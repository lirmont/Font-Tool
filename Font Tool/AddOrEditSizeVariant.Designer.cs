namespace FontTool
{
	partial class AddOrEditSizeVariant
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
			this.nameLabel = new System.Windows.Forms.Label();
			this.effectsLabel = new System.Windows.Forms.Label();
			this.effectsListView = new System.Windows.Forms.ListView();
			this.numberColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.nameColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.parametersColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.effectsContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.addEffectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.editEffectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.moveUpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.moveDownToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.removeEffectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.nameMaskedTextBox = new FontTool.MaskedTextBox();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.saveButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.tableLayoutPanel.SuspendLayout();
			this.effectsContextMenuStrip.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
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
			this.tableLayoutPanel.Controls.Add(this.nameLabel, 0, 0);
			this.tableLayoutPanel.Controls.Add(this.effectsLabel, 0, 1);
			this.tableLayoutPanel.Controls.Add(this.effectsListView, 0, 2);
			this.tableLayoutPanel.Controls.Add(this.nameMaskedTextBox, 1, 0);
			this.tableLayoutPanel.Controls.Add(this.tableLayoutPanel1, 0, 3);
			this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel.Name = "tableLayoutPanel";
			this.tableLayoutPanel.RowCount = 4;
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel.Size = new System.Drawing.Size(476, 293);
			this.tableLayoutPanel.TabIndex = 0;
			// 
			// nameLabel
			// 
			this.nameLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.nameLabel.AutoSize = true;
			this.nameLabel.Location = new System.Drawing.Point(8, 6);
			this.nameLabel.Name = "nameLabel";
			this.nameLabel.Size = new System.Drawing.Size(38, 13);
			this.nameLabel.TabIndex = 0;
			this.nameLabel.Text = "Name:";
			// 
			// effectsLabel
			// 
			this.effectsLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.effectsLabel.AutoSize = true;
			this.effectsLabel.Location = new System.Drawing.Point(3, 26);
			this.effectsLabel.Name = "effectsLabel";
			this.effectsLabel.Size = new System.Drawing.Size(43, 13);
			this.effectsLabel.TabIndex = 1;
			this.effectsLabel.Text = "Effects:";
			// 
			// effectsListView
			// 
			this.effectsListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.effectsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.numberColumnHeader,
            this.nameColumnHeader,
            this.parametersColumnHeader});
			this.tableLayoutPanel.SetColumnSpan(this.effectsListView, 2);
			this.effectsListView.ContextMenuStrip = this.effectsContextMenuStrip;
			this.effectsListView.FullRowSelect = true;
			this.effectsListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.effectsListView.Location = new System.Drawing.Point(3, 42);
			this.effectsListView.Name = "effectsListView";
			this.effectsListView.ShowItemToolTips = true;
			this.effectsListView.Size = new System.Drawing.Size(470, 213);
			this.effectsListView.TabIndex = 2;
			this.effectsListView.UseCompatibleStateImageBehavior = false;
			this.effectsListView.View = System.Windows.Forms.View.Details;
			// 
			// numberColumnHeader
			// 
			this.numberColumnHeader.Text = "#";
			this.numberColumnHeader.Width = 32;
			// 
			// nameColumnHeader
			// 
			this.nameColumnHeader.Text = "Name";
			// 
			// parametersColumnHeader
			// 
			this.parametersColumnHeader.Text = "Parameters";
			this.parametersColumnHeader.Width = 360;
			// 
			// effectsContextMenuStrip
			// 
			this.effectsContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addEffectToolStripMenuItem,
            this.editEffectToolStripMenuItem,
            this.toolStripSeparator1,
            this.moveUpToolStripMenuItem,
            this.moveDownToolStripMenuItem,
            this.toolStripSeparator2,
            this.removeEffectToolStripMenuItem});
			this.effectsContextMenuStrip.Name = "effectsContextMenuStrip";
			this.effectsContextMenuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.effectsContextMenuStrip.Size = new System.Drawing.Size(174, 148);
			this.effectsContextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.effectsContextMenuStrip_Opening);
			// 
			// addEffectToolStripMenuItem
			// 
			this.addEffectToolStripMenuItem.Name = "addEffectToolStripMenuItem";
			this.addEffectToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
			this.addEffectToolStripMenuItem.Text = "Add Effect";
			this.addEffectToolStripMenuItem.Click += new System.EventHandler(this.addEffectToolStripMenuItem_Click);
			// 
			// editEffectToolStripMenuItem
			// 
			this.editEffectToolStripMenuItem.Name = "editEffectToolStripMenuItem";
			this.editEffectToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
			this.editEffectToolStripMenuItem.Text = "Edit Effect";
			this.editEffectToolStripMenuItem.Click += new System.EventHandler(this.editEffectToolStripMenuItem_Click);
			// 
			// moveUpToolStripMenuItem
			// 
			this.moveUpToolStripMenuItem.Name = "moveUpToolStripMenuItem";
			this.moveUpToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
			this.moveUpToolStripMenuItem.Text = "Move Effect Up";
			this.moveUpToolStripMenuItem.Click += new System.EventHandler(this.moveUpToolStripMenuItem_Click);
			// 
			// moveDownToolStripMenuItem
			// 
			this.moveDownToolStripMenuItem.Name = "moveDownToolStripMenuItem";
			this.moveDownToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
			this.moveDownToolStripMenuItem.Text = "Move Effect Down";
			this.moveDownToolStripMenuItem.Click += new System.EventHandler(this.moveDownToolStripMenuItem_Click);
			// 
			// removeEffectToolStripMenuItem
			// 
			this.removeEffectToolStripMenuItem.Name = "removeEffectToolStripMenuItem";
			this.removeEffectToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
			this.removeEffectToolStripMenuItem.Text = "Remove Effect";
			this.removeEffectToolStripMenuItem.Click += new System.EventHandler(this.removeEffectToolStripMenuItem_Click);
			// 
			// nameMaskedTextBox
			// 
			this.nameMaskedTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.nameMaskedTextBox.EmptyText = "Name of the variant (ex: Regular, Bold, Small Caps).";
			this.nameMaskedTextBox.Location = new System.Drawing.Point(52, 3);
			this.nameMaskedTextBox.MarkInvalid = false;
			this.nameMaskedTextBox.Name = "nameMaskedTextBox";
			this.nameMaskedTextBox.Size = new System.Drawing.Size(421, 20);
			this.nameMaskedTextBox.TabIndex = 3;
			this.nameMaskedTextBox.TextChanged += new System.EventHandler(this.nameMaskedTextBox_TextChanged);
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.tableLayoutPanel1.AutoSize = true;
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel.SetColumnSpan(this.tableLayoutPanel1, 2);
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Controls.Add(this.saveButton, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.cancelButton, 1, 0);
			this.tableLayoutPanel1.Location = new System.Drawing.Point(157, 261);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 1;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(162, 29);
			this.tableLayoutPanel1.TabIndex = 4;
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
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(170, 6);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(170, 6);
			// 
			// AddOrEditSizeVariant
			// 
			this.AcceptButton = this.saveButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(477, 294);
			this.Controls.Add(this.tableLayoutPanel);
			this.Name = "AddOrEditSizeVariant";
			this.ShowIcon = false;
			this.Text = "Add or Edit Size Variant";
			this.tableLayoutPanel.ResumeLayout(false);
			this.tableLayoutPanel.PerformLayout();
			this.effectsContextMenuStrip.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
		private System.Windows.Forms.Label nameLabel;
		private System.Windows.Forms.Label effectsLabel;
		private System.Windows.Forms.ListView effectsListView;
		private MaskedTextBox nameMaskedTextBox;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Button saveButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.ContextMenuStrip effectsContextMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem addEffectToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem editEffectToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem moveUpToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem moveDownToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem removeEffectToolStripMenuItem;
		private System.Windows.Forms.ColumnHeader nameColumnHeader;
		private System.Windows.Forms.ColumnHeader parametersColumnHeader;
		private System.Windows.Forms.ColumnHeader numberColumnHeader;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
	}
}