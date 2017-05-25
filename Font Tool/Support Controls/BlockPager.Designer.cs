namespace FontTool
{
	partial class BlockPager
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
			this.components = new System.ComponentModel.Container();
			this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
			this.blockLabel = new System.Windows.Forms.Label();
			this.pagesTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
			this.firstLinkLabel = new System.Windows.Forms.LinkLabel();
			this.previousLinkLabel = new System.Windows.Forms.LinkLabel();
			this.nextLinkLabel = new System.Windows.Forms.LinkLabel();
			this.lastLinkLabel = new System.Windows.Forms.LinkLabel();
			this.locationLabel = new System.Windows.Forms.Label();
			this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.setPageLimitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.tableLayoutPanel.SuspendLayout();
			this.pagesTableLayoutPanel.SuspendLayout();
			this.contextMenuStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel
			// 
			this.tableLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel.BackColor = System.Drawing.Color.Transparent;
			this.tableLayoutPanel.ColumnCount = 1;
			this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel.Controls.Add(this.blockLabel, 0, 0);
			this.tableLayoutPanel.Controls.Add(this.pagesTableLayoutPanel, 0, 1);
			this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanel.Name = "tableLayoutPanel";
			this.tableLayoutPanel.RowCount = 2;
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel.Size = new System.Drawing.Size(215, 150);
			this.tableLayoutPanel.TabIndex = 0;
			// 
			// blockLabel
			// 
			this.blockLabel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.blockLabel.AutoSize = true;
			this.blockLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.blockLabel.Location = new System.Drawing.Point(88, 118);
			this.blockLabel.Name = "blockLabel";
			this.blockLabel.Size = new System.Drawing.Size(39, 13);
			this.blockLabel.TabIndex = 0;
			this.blockLabel.Text = "Block";
			this.blockLabel.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			// 
			// pagesTableLayoutPanel
			// 
			this.pagesTableLayoutPanel.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.pagesTableLayoutPanel.AutoSize = true;
			this.pagesTableLayoutPanel.ColumnCount = 5;
			this.pagesTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.pagesTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.pagesTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.pagesTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.pagesTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.pagesTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.pagesTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.pagesTableLayoutPanel.Controls.Add(this.firstLinkLabel, 0, 0);
			this.pagesTableLayoutPanel.Controls.Add(this.previousLinkLabel, 1, 0);
			this.pagesTableLayoutPanel.Controls.Add(this.nextLinkLabel, 3, 0);
			this.pagesTableLayoutPanel.Controls.Add(this.lastLinkLabel, 4, 0);
			this.pagesTableLayoutPanel.Controls.Add(this.locationLabel, 2, 0);
			this.pagesTableLayoutPanel.Location = new System.Drawing.Point(31, 134);
			this.pagesTableLayoutPanel.Name = "pagesTableLayoutPanel";
			this.pagesTableLayoutPanel.RowCount = 1;
			this.pagesTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.pagesTableLayoutPanel.Size = new System.Drawing.Size(152, 13);
			this.pagesTableLayoutPanel.TabIndex = 1;
			// 
			// firstLinkLabel
			// 
			this.firstLinkLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.firstLinkLabel.AutoSize = true;
			this.firstLinkLabel.Location = new System.Drawing.Point(3, 0);
			this.firstLinkLabel.Name = "firstLinkLabel";
			this.firstLinkLabel.Size = new System.Drawing.Size(19, 13);
			this.firstLinkLabel.TabIndex = 0;
			this.firstLinkLabel.TabStop = true;
			this.firstLinkLabel.Text = "<<";
			this.firstLinkLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.firstLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.firstLinkLabel_LinkClicked);
			// 
			// previousLinkLabel
			// 
			this.previousLinkLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.previousLinkLabel.AutoSize = true;
			this.previousLinkLabel.Location = new System.Drawing.Point(28, 0);
			this.previousLinkLabel.Name = "previousLinkLabel";
			this.previousLinkLabel.Size = new System.Drawing.Size(13, 13);
			this.previousLinkLabel.TabIndex = 1;
			this.previousLinkLabel.TabStop = true;
			this.previousLinkLabel.Text = "<";
			this.previousLinkLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.previousLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.previousLinkLabel_LinkClicked);
			// 
			// nextLinkLabel
			// 
			this.nextLinkLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.nextLinkLabel.AutoSize = true;
			this.nextLinkLabel.Location = new System.Drawing.Point(111, 0);
			this.nextLinkLabel.Name = "nextLinkLabel";
			this.nextLinkLabel.Size = new System.Drawing.Size(13, 13);
			this.nextLinkLabel.TabIndex = 2;
			this.nextLinkLabel.TabStop = true;
			this.nextLinkLabel.Text = ">";
			this.nextLinkLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.nextLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.nextLinkLabel_LinkClicked);
			// 
			// lastLinkLabel
			// 
			this.lastLinkLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.lastLinkLabel.AutoSize = true;
			this.lastLinkLabel.Location = new System.Drawing.Point(130, 0);
			this.lastLinkLabel.Name = "lastLinkLabel";
			this.lastLinkLabel.Size = new System.Drawing.Size(19, 13);
			this.lastLinkLabel.TabIndex = 3;
			this.lastLinkLabel.TabStop = true;
			this.lastLinkLabel.Text = ">>";
			this.lastLinkLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.lastLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lastLinkLabel_LinkClicked);
			// 
			// locationLabel
			// 
			this.locationLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.locationLabel.AutoSize = true;
			this.locationLabel.Location = new System.Drawing.Point(47, 0);
			this.locationLabel.Name = "locationLabel";
			this.locationLabel.Size = new System.Drawing.Size(58, 13);
			this.locationLabel.TabIndex = 4;
			this.locationLabel.Text = "A to X of Y";
			this.locationLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// contextMenuStrip
			// 
			this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setPageLimitToolStripMenuItem});
			this.contextMenuStrip.Name = "contextMenuStrip";
			this.contextMenuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.contextMenuStrip.Size = new System.Drawing.Size(153, 26);
			// 
			// setPageLimitToolStripMenuItem
			// 
			this.setPageLimitToolStripMenuItem.Name = "setPageLimitToolStripMenuItem";
			this.setPageLimitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.setPageLimitToolStripMenuItem.Text = "Set Page Limit";
			this.setPageLimitToolStripMenuItem.Click += new System.EventHandler(this.setPageLimitToolStripMenuItem_Click);
			// 
			// BlockPager
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.ContextMenuStrip = this.contextMenuStrip;
			this.Controls.Add(this.tableLayoutPanel);
			this.Name = "BlockPager";
			this.Size = new System.Drawing.Size(215, 150);
			this.tableLayoutPanel.ResumeLayout(false);
			this.tableLayoutPanel.PerformLayout();
			this.pagesTableLayoutPanel.ResumeLayout(false);
			this.pagesTableLayoutPanel.PerformLayout();
			this.contextMenuStrip.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
		private System.Windows.Forms.Label blockLabel;
		private System.Windows.Forms.TableLayoutPanel pagesTableLayoutPanel;
		private System.Windows.Forms.LinkLabel firstLinkLabel;
		private System.Windows.Forms.LinkLabel previousLinkLabel;
		private System.Windows.Forms.LinkLabel nextLinkLabel;
		private System.Windows.Forms.LinkLabel lastLinkLabel;
		private System.Windows.Forms.Label locationLabel;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem setPageLimitToolStripMenuItem;
	}
}
