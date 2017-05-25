namespace FontTool
{
	partial class UnicodeCoverageReportViewer
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
			System.Windows.Forms.Label sectionCountLabel;
			System.Windows.Forms.Label loadingDataLabel;
			System.Windows.Forms.Label label1;
			this.toolStripContainer = new System.Windows.Forms.ToolStripContainer();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
			this.scriptsTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
			this.languagesTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
			this.scriptsCollapsingGroupBox = new FontTool.CollapsingGroupBox();
			this.territoriesCollapsingGroupBox = new FontTool.CollapsingGroupBox();
			this.sectionCountValueLabel = new System.Windows.Forms.LinkLabel();
			sectionCountLabel = new System.Windows.Forms.Label();
			loadingDataLabel = new System.Windows.Forms.Label();
			label1 = new System.Windows.Forms.Label();
			this.toolStripContainer.BottomToolStripPanel.SuspendLayout();
			this.toolStripContainer.ContentPanel.SuspendLayout();
			this.toolStripContainer.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.tableLayoutPanel.SuspendLayout();
			this.scriptsTableLayoutPanel.SuspendLayout();
			this.languagesTableLayoutPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// sectionCountLabel
			// 
			sectionCountLabel.AutoSize = true;
			sectionCountLabel.Location = new System.Drawing.Point(3, 0);
			sectionCountLabel.Name = "sectionCountLabel";
			sectionCountLabel.Size = new System.Drawing.Size(85, 13);
			sectionCountLabel.TabIndex = 1;
			sectionCountLabel.Text = "Unicode Blocks:";
			// 
			// loadingDataLabel
			// 
			loadingDataLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
			loadingDataLabel.AutoSize = true;
			loadingDataLabel.Enabled = false;
			loadingDataLabel.Location = new System.Drawing.Point(206, 91);
			loadingDataLabel.Name = "loadingDataLabel";
			loadingDataLabel.Size = new System.Drawing.Size(80, 13);
			loadingDataLabel.TabIndex = 0;
			loadingDataLabel.Text = "(Loading Data.)";
			loadingDataLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label1
			// 
			label1.Anchor = System.Windows.Forms.AnchorStyles.None;
			label1.AutoSize = true;
			label1.Enabled = false;
			label1.Location = new System.Drawing.Point(205, 88);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(80, 13);
			label1.TabIndex = 1;
			label1.Text = "(Loading Data.)";
			label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// toolStripContainer
			// 
			this.toolStripContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			// 
			// toolStripContainer.BottomToolStripPanel
			// 
			this.toolStripContainer.BottomToolStripPanel.Controls.Add(this.statusStrip1);
			this.toolStripContainer.BottomToolStripPanel.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			// 
			// toolStripContainer.ContentPanel
			// 
			this.toolStripContainer.ContentPanel.Controls.Add(this.tableLayoutPanel);
			this.toolStripContainer.ContentPanel.Size = new System.Drawing.Size(513, 454);
			// 
			// toolStripContainer.LeftToolStripPanel
			// 
			this.toolStripContainer.LeftToolStripPanel.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.toolStripContainer.Location = new System.Drawing.Point(0, 0);
			this.toolStripContainer.Name = "toolStripContainer";
			// 
			// toolStripContainer.RightToolStripPanel
			// 
			this.toolStripContainer.RightToolStripPanel.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.toolStripContainer.Size = new System.Drawing.Size(513, 476);
			this.toolStripContainer.TabIndex = 0;
			this.toolStripContainer.Text = "toolStripContainer1";
			// 
			// toolStripContainer.TopToolStripPanel
			// 
			this.toolStripContainer.TopToolStripPanel.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.toolStripContainer.TopToolStripPanelVisible = false;
			// 
			// statusStrip1
			// 
			this.statusStrip1.Dock = System.Windows.Forms.DockStyle.None;
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
			this.statusStrip1.Location = new System.Drawing.Point(0, 0);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(513, 22);
			this.statusStrip1.TabIndex = 0;
			// 
			// toolStripStatusLabel
			// 
			this.toolStripStatusLabel.Name = "toolStripStatusLabel";
			this.toolStripStatusLabel.Size = new System.Drawing.Size(42, 17);
			this.toolStripStatusLabel.Text = "Ready.";
			// 
			// tableLayoutPanel
			// 
			this.tableLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel.ColumnCount = 2;
			this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel.Controls.Add(this.scriptsTableLayoutPanel, 0, 2);
			this.tableLayoutPanel.Controls.Add(sectionCountLabel, 0, 0);
			this.tableLayoutPanel.Controls.Add(this.languagesTableLayoutPanel, 0, 4);
			this.tableLayoutPanel.Controls.Add(this.scriptsCollapsingGroupBox, 0, 1);
			this.tableLayoutPanel.Controls.Add(this.territoriesCollapsingGroupBox, 0, 3);
			this.tableLayoutPanel.Controls.Add(this.sectionCountValueLabel, 1, 0);
			this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel.Name = "tableLayoutPanel";
			this.tableLayoutPanel.RowCount = 5;
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel.Size = new System.Drawing.Size(513, 454);
			this.tableLayoutPanel.TabIndex = 1;
			// 
			// scriptsTableLayoutPanel
			// 
			this.scriptsTableLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.scriptsTableLayoutPanel.BackColor = System.Drawing.SystemColors.Control;
			this.scriptsTableLayoutPanel.ColumnCount = 1;
			this.tableLayoutPanel.SetColumnSpan(this.scriptsTableLayoutPanel, 2);
			this.scriptsTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.scriptsTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.scriptsTableLayoutPanel.Controls.Add(loadingDataLabel, 0, 0);
			this.scriptsTableLayoutPanel.Location = new System.Drawing.Point(20, 38);
			this.scriptsTableLayoutPanel.Margin = new System.Windows.Forms.Padding(20, 0, 0, 0);
			this.scriptsTableLayoutPanel.MinimumSize = new System.Drawing.Size(100, 50);
			this.scriptsTableLayoutPanel.Name = "scriptsTableLayoutPanel";
			this.scriptsTableLayoutPanel.RowCount = 1;
			this.scriptsTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.scriptsTableLayoutPanel.Size = new System.Drawing.Size(493, 195);
			this.scriptsTableLayoutPanel.TabIndex = 10;
			// 
			// languagesTableLayoutPanel
			// 
			this.languagesTableLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.languagesTableLayoutPanel.AutoScroll = true;
			this.languagesTableLayoutPanel.AutoSize = true;
			this.languagesTableLayoutPanel.ColumnCount = 1;
			this.tableLayoutPanel.SetColumnSpan(this.languagesTableLayoutPanel, 2);
			this.languagesTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.languagesTableLayoutPanel.Controls.Add(label1, 0, 0);
			this.languagesTableLayoutPanel.Location = new System.Drawing.Point(20, 261);
			this.languagesTableLayoutPanel.Margin = new System.Windows.Forms.Padding(20, 3, 3, 3);
			this.languagesTableLayoutPanel.Name = "languagesTableLayoutPanel";
			this.languagesTableLayoutPanel.RowCount = 1;
			this.languagesTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.languagesTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 195F));
			this.languagesTableLayoutPanel.Size = new System.Drawing.Size(490, 190);
			this.languagesTableLayoutPanel.TabIndex = 6;
			// 
			// scriptsCollapsingGroupBox
			// 
			this.scriptsCollapsingGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.scriptsCollapsingGroupBox.AutoSize = true;
			this.tableLayoutPanel.SetColumnSpan(this.scriptsCollapsingGroupBox, 2);
			this.scriptsCollapsingGroupBox.Location = new System.Drawing.Point(3, 16);
			this.scriptsCollapsingGroupBox.MinimumSize = new System.Drawing.Size(0, 14);
			this.scriptsCollapsingGroupBox.Name = "scriptsCollapsingGroupBox";
			this.scriptsCollapsingGroupBox.Size = new System.Drawing.Size(507, 19);
			this.scriptsCollapsingGroupBox.TabIndex = 11;
			this.scriptsCollapsingGroupBox.TabStop = false;
			this.scriptsCollapsingGroupBox.Text = "Scripts";
			this.scriptsCollapsingGroupBox.ContentExpansionChanged += new FontTool.CollapsingGroupBox.ExpandedBoolean(this.scriptsCollapsingGroupBox_ContentExpansionChanged);
			// 
			// territoriesCollapsingGroupBox
			// 
			this.territoriesCollapsingGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.territoriesCollapsingGroupBox.AutoSize = true;
			this.tableLayoutPanel.SetColumnSpan(this.territoriesCollapsingGroupBox, 2);
			this.territoriesCollapsingGroupBox.Location = new System.Drawing.Point(3, 236);
			this.territoriesCollapsingGroupBox.MinimumSize = new System.Drawing.Size(0, 14);
			this.territoriesCollapsingGroupBox.Name = "territoriesCollapsingGroupBox";
			this.territoriesCollapsingGroupBox.Size = new System.Drawing.Size(507, 19);
			this.territoriesCollapsingGroupBox.TabIndex = 12;
			this.territoriesCollapsingGroupBox.TabStop = false;
			this.territoriesCollapsingGroupBox.Text = "Territories";
			this.territoriesCollapsingGroupBox.ContentExpansionChanged += new FontTool.CollapsingGroupBox.ExpandedBoolean(this.languagesCollapsingGroupBox_ContentExpansionChanged);
			// 
			// sectionCountValueLabel
			// 
			this.sectionCountValueLabel.AutoSize = true;
			this.sectionCountValueLabel.Enabled = false;
			this.sectionCountValueLabel.Location = new System.Drawing.Point(259, 0);
			this.sectionCountValueLabel.Name = "sectionCountValueLabel";
			this.sectionCountValueLabel.Size = new System.Drawing.Size(13, 13);
			this.sectionCountValueLabel.TabIndex = 13;
			this.sectionCountValueLabel.TabStop = true;
			this.sectionCountValueLabel.Text = "0";
			this.sectionCountValueLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.sectionCountValueLabel_LinkClicked);
			// 
			// UnicodeCoverageReportViewer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(513, 476);
			this.Controls.Add(this.toolStripContainer);
			this.Name = "UnicodeCoverageReportViewer";
			this.ShowIcon = false;
			this.Text = "Unicode Coverage Report Viewer";
			this.Shown += new System.EventHandler(this.UnicodeCoverageReportViewer_Shown);
			this.toolStripContainer.BottomToolStripPanel.ResumeLayout(false);
			this.toolStripContainer.BottomToolStripPanel.PerformLayout();
			this.toolStripContainer.ContentPanel.ResumeLayout(false);
			this.toolStripContainer.ResumeLayout(false);
			this.toolStripContainer.PerformLayout();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.tableLayoutPanel.ResumeLayout(false);
			this.tableLayoutPanel.PerformLayout();
			this.scriptsTableLayoutPanel.ResumeLayout(false);
			this.scriptsTableLayoutPanel.PerformLayout();
			this.languagesTableLayoutPanel.ResumeLayout(false);
			this.languagesTableLayoutPanel.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ToolStripContainer toolStripContainer;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
		private System.Windows.Forms.TableLayoutPanel languagesTableLayoutPanel;
		private System.Windows.Forms.TableLayoutPanel scriptsTableLayoutPanel;
		private CollapsingGroupBox scriptsCollapsingGroupBox;
		private CollapsingGroupBox territoriesCollapsingGroupBox;
		private System.Windows.Forms.LinkLabel sectionCountValueLabel;

	}
}