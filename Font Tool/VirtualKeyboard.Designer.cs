namespace FontTool
{
	partial class VirtualKeyboard
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VirtualKeyboard));
			this.splitContainer = new System.Windows.Forms.SplitContainer();
			this.displayTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
			this.glControl = new OpenTK.GLControl();
			this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.defaultToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.uTF16ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.uTFUEscapeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.uTFXEscapeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.defaultPasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.utf16PasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.utfPasteUEscapeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.utfPasteXEscapeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.exportToPNGToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.leftDisplayToolsTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
			this.zoomInButton = new System.Windows.Forms.Button();
			this.zoomOutButton = new System.Windows.Forms.Button();
			this.displayRightToolsTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
			this.imageRadioButton = new System.Windows.Forms.RadioButton();
			this.geometryRadioButton = new System.Windows.Forms.RadioButton();
			this.overlapRadioButton = new System.Windows.Forms.RadioButton();
			this.imageAndPointsRadioButton = new System.Windows.Forms.RadioButton();
			this.keyboardTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
			this.unicodeBlockTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
			this.unicodeBlockComboBox = new System.Windows.Forms.ComboBox();
			this.backspaceGlyphPictureBox = new FontTool.GlyphPictureBox();
			this.blockPager = new FontTool.BlockPager();
			this.spaceBarTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
			this.spaceBarGlyphPictureBox = new FontTool.GlyphPictureBox();
			this.leftGlyphPictureBox = new FontTool.GlyphPictureBox();
			this.upGlyphPictureBox = new FontTool.GlyphPictureBox();
			this.downGlyphPictureBox = new FontTool.GlyphPictureBox();
			this.rightGlyphPictureBox = new FontTool.GlyphPictureBox();
			this.flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
			this.glyphToolTip = new System.Windows.Forms.ToolTip(this.components);
			this.gutterXNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.gutterYNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.splitContainer.Panel1.SuspendLayout();
			this.splitContainer.Panel2.SuspendLayout();
			this.splitContainer.SuspendLayout();
			this.displayTableLayoutPanel.SuspendLayout();
			this.contextMenuStrip.SuspendLayout();
			this.leftDisplayToolsTableLayoutPanel.SuspendLayout();
			this.displayRightToolsTableLayoutPanel.SuspendLayout();
			this.keyboardTableLayoutPanel.SuspendLayout();
			this.unicodeBlockTableLayoutPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.backspaceGlyphPictureBox)).BeginInit();
			this.spaceBarTableLayoutPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.spaceBarGlyphPictureBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.leftGlyphPictureBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.upGlyphPictureBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.downGlyphPictureBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.rightGlyphPictureBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gutterXNumericUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gutterYNumericUpDown)).BeginInit();
			this.SuspendLayout();
			// 
			// splitContainer
			// 
			this.splitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.splitContainer.BackColor = System.Drawing.Color.Transparent;
			this.splitContainer.Location = new System.Drawing.Point(0, 0);
			this.splitContainer.Name = "splitContainer";
			this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer.Panel1
			// 
			this.splitContainer.Panel1.Controls.Add(this.displayTableLayoutPanel);
			this.splitContainer.Panel1MinSize = 125;
			// 
			// splitContainer.Panel2
			// 
			this.splitContainer.Panel2.Controls.Add(this.keyboardTableLayoutPanel);
			this.splitContainer.Size = new System.Drawing.Size(662, 531);
			this.splitContainer.SplitterDistance = 136;
			this.splitContainer.TabIndex = 1;
			this.splitContainer.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainer_SplitterMoved);
			this.splitContainer.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer_Paint);
			// 
			// displayTableLayoutPanel
			// 
			this.displayTableLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.displayTableLayoutPanel.ColumnCount = 3;
			this.displayTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.displayTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.displayTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.displayTableLayoutPanel.Controls.Add(this.glControl, 1, 0);
			this.displayTableLayoutPanel.Controls.Add(this.leftDisplayToolsTableLayoutPanel, 0, 0);
			this.displayTableLayoutPanel.Controls.Add(this.displayRightToolsTableLayoutPanel, 2, 0);
			this.displayTableLayoutPanel.Location = new System.Drawing.Point(0, 0);
			this.displayTableLayoutPanel.Name = "displayTableLayoutPanel";
			this.displayTableLayoutPanel.RowCount = 1;
			this.displayTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.displayTableLayoutPanel.Size = new System.Drawing.Size(662, 136);
			this.displayTableLayoutPanel.TabIndex = 1;
			// 
			// glControl
			// 
			this.glControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.glControl.BackColor = System.Drawing.Color.Black;
			this.glControl.ContextMenuStrip = this.contextMenuStrip;
			this.glControl.Location = new System.Drawing.Point(66, 0);
			this.glControl.Margin = new System.Windows.Forms.Padding(0);
			this.glControl.Name = "glControl";
			this.glControl.Size = new System.Drawing.Size(562, 136);
			this.glControl.TabIndex = 0;
			this.glControl.VSync = false;
			this.glControl.Load += new System.EventHandler(this.glControl_Load);
			this.glControl.Paint += new System.Windows.Forms.PaintEventHandler(this.glControl_Paint);
			this.glControl.MouseEnter += new System.EventHandler(this.glControl_MouseEnter);
			this.glControl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.glControl_MouseMove);
			this.glControl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.glControl_MouseUp);
			this.glControl.Resize += new System.EventHandler(this.glControl_Resize);
			// 
			// contextMenuStrip
			// 
			this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.toolStripSeparator4,
            this.exportToPNGToolStripMenuItem,
            this.toolStripSeparator1,
            this.clearToolStripMenuItem});
			this.contextMenuStrip.Name = "contextMenuStrip";
			this.contextMenuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.contextMenuStrip.Size = new System.Drawing.Size(154, 126);
			this.contextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip_Opening);
			// 
			// copyToolStripMenuItem
			// 
			this.copyToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.defaultToolStripMenuItem,
            this.toolStripSeparator2,
            this.uTF16ToolStripMenuItem,
            this.uTFUEscapeToolStripMenuItem,
            this.uTFXEscapeToolStripMenuItem});
			this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
			this.copyToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
			this.copyToolStripMenuItem.Text = "Copy";
			// 
			// defaultToolStripMenuItem
			// 
			this.defaultToolStripMenuItem.Name = "defaultToolStripMenuItem";
			this.defaultToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.defaultToolStripMenuItem.Text = "Default";
			this.defaultToolStripMenuItem.Click += new System.EventHandler(this.defaultToolStripMenuItem_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(149, 6);
			// 
			// uTF16ToolStripMenuItem
			// 
			this.uTF16ToolStripMenuItem.Name = "uTF16ToolStripMenuItem";
			this.uTF16ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.uTF16ToolStripMenuItem.Text = "UTF-16";
			this.uTF16ToolStripMenuItem.Click += new System.EventHandler(this.utf16ToolStripMenuItem_Click);
			// 
			// uTFUEscapeToolStripMenuItem
			// 
			this.uTFUEscapeToolStripMenuItem.Name = "uTFUEscapeToolStripMenuItem";
			this.uTFUEscapeToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.uTFUEscapeToolStripMenuItem.Text = "UTF U-Escape";
			this.uTFUEscapeToolStripMenuItem.Click += new System.EventHandler(this.utfUEscapeToolStripMenuItem_Click);
			// 
			// uTFXEscapeToolStripMenuItem
			// 
			this.uTFXEscapeToolStripMenuItem.Name = "uTFXEscapeToolStripMenuItem";
			this.uTFXEscapeToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.uTFXEscapeToolStripMenuItem.Text = "UTF X-Escape";
			this.uTFXEscapeToolStripMenuItem.Click += new System.EventHandler(this.utfXEscapeToolStripMenuItem_Click);
			// 
			// pasteToolStripMenuItem
			// 
			this.pasteToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.defaultPasteToolStripMenuItem,
            this.toolStripSeparator3,
            this.utf16PasteToolStripMenuItem,
            this.utfPasteUEscapeToolStripMenuItem,
            this.utfPasteXEscapeToolStripMenuItem});
			this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
			this.pasteToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
			this.pasteToolStripMenuItem.Text = "Paste";
			this.pasteToolStripMenuItem.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
			// 
			// defaultPasteToolStripMenuItem
			// 
			this.defaultPasteToolStripMenuItem.Name = "defaultPasteToolStripMenuItem";
			this.defaultPasteToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.defaultPasteToolStripMenuItem.Text = "Default";
			this.defaultPasteToolStripMenuItem.Click += new System.EventHandler(this.defaultPasteToolStripMenuItem_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(149, 6);
			// 
			// utf16PasteToolStripMenuItem
			// 
			this.utf16PasteToolStripMenuItem.Name = "utf16PasteToolStripMenuItem";
			this.utf16PasteToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.utf16PasteToolStripMenuItem.Text = "UTF-16";
			this.utf16PasteToolStripMenuItem.Click += new System.EventHandler(this.utf16PasteToolStripMenuItem_Click);
			// 
			// utfPasteUEscapeToolStripMenuItem
			// 
			this.utfPasteUEscapeToolStripMenuItem.Name = "utfPasteUEscapeToolStripMenuItem";
			this.utfPasteUEscapeToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.utfPasteUEscapeToolStripMenuItem.Text = "UTF U-Escape";
			this.utfPasteUEscapeToolStripMenuItem.Click += new System.EventHandler(this.utfPasteUEscapeToolStripMenuItem_Click);
			// 
			// utfPasteXEscapeToolStripMenuItem
			// 
			this.utfPasteXEscapeToolStripMenuItem.Name = "utfPasteXEscapeToolStripMenuItem";
			this.utfPasteXEscapeToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.utfPasteXEscapeToolStripMenuItem.Text = "UTF X-Escape";
			this.utfPasteXEscapeToolStripMenuItem.Click += new System.EventHandler(this.utfPasteXEscapeToolStripMenuItem_Click);
			// 
			// saveToolStripMenuItem
			// 
			this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
			this.saveToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
			this.saveToolStripMenuItem.Text = "Save";
			this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(150, 6);
			// 
			// exportToPNGToolStripMenuItem
			// 
			this.exportToPNGToolStripMenuItem.Name = "exportToPNGToolStripMenuItem";
			this.exportToPNGToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
			this.exportToPNGToolStripMenuItem.Text = "Export to PNG";
			this.exportToPNGToolStripMenuItem.Click += new System.EventHandler(this.exportToPNGToolStripMenuItem_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(150, 6);
			// 
			// clearToolStripMenuItem
			// 
			this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
			this.clearToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
			this.clearToolStripMenuItem.Text = "Clear";
			this.clearToolStripMenuItem.Click += new System.EventHandler(this.clearToolStripMenuItem_Click);
			// 
			// leftDisplayToolsTableLayoutPanel
			// 
			this.leftDisplayToolsTableLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.leftDisplayToolsTableLayoutPanel.AutoSize = true;
			this.leftDisplayToolsTableLayoutPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.leftDisplayToolsTableLayoutPanel.ColumnCount = 1;
			this.leftDisplayToolsTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.leftDisplayToolsTableLayoutPanel.Controls.Add(this.zoomInButton, 0, 0);
			this.leftDisplayToolsTableLayoutPanel.Controls.Add(this.zoomOutButton, 0, 1);
			this.leftDisplayToolsTableLayoutPanel.Controls.Add(this.gutterXNumericUpDown, 0, 2);
			this.leftDisplayToolsTableLayoutPanel.Controls.Add(this.gutterYNumericUpDown, 0, 3);
			this.leftDisplayToolsTableLayoutPanel.Location = new System.Drawing.Point(3, 3);
			this.leftDisplayToolsTableLayoutPanel.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
			this.leftDisplayToolsTableLayoutPanel.Name = "leftDisplayToolsTableLayoutPanel";
			this.leftDisplayToolsTableLayoutPanel.RowCount = 5;
			this.leftDisplayToolsTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.leftDisplayToolsTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.leftDisplayToolsTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.leftDisplayToolsTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.leftDisplayToolsTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.leftDisplayToolsTableLayoutPanel.Size = new System.Drawing.Size(63, 130);
			this.leftDisplayToolsTableLayoutPanel.TabIndex = 1;
			// 
			// zoomInButton
			// 
			this.zoomInButton.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.zoomInButton.AutoSize = true;
			this.zoomInButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.zoomInButton.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.zoomInButton.Location = new System.Drawing.Point(19, 3);
			this.zoomInButton.Name = "zoomInButton";
			this.zoomInButton.Size = new System.Drawing.Size(24, 24);
			this.zoomInButton.TabIndex = 0;
			this.zoomInButton.Text = "+";
			this.zoomInButton.UseVisualStyleBackColor = true;
			this.zoomInButton.Click += new System.EventHandler(this.zoomInButton_Click);
			// 
			// zoomOutButton
			// 
			this.zoomOutButton.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.zoomOutButton.AutoSize = true;
			this.zoomOutButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.zoomOutButton.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.zoomOutButton.Location = new System.Drawing.Point(19, 30);
			this.zoomOutButton.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
			this.zoomOutButton.Name = "zoomOutButton";
			this.zoomOutButton.Size = new System.Drawing.Size(24, 24);
			this.zoomOutButton.TabIndex = 1;
			this.zoomOutButton.Text = "-";
			this.zoomOutButton.UseVisualStyleBackColor = true;
			this.zoomOutButton.Click += new System.EventHandler(this.zoomOutButton_Click);
			// 
			// displayRightToolsTableLayoutPanel
			// 
			this.displayRightToolsTableLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.displayRightToolsTableLayoutPanel.AutoSize = true;
			this.displayRightToolsTableLayoutPanel.ColumnCount = 1;
			this.displayRightToolsTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.displayRightToolsTableLayoutPanel.Controls.Add(this.imageRadioButton, 0, 0);
			this.displayRightToolsTableLayoutPanel.Controls.Add(this.geometryRadioButton, 0, 2);
			this.displayRightToolsTableLayoutPanel.Controls.Add(this.overlapRadioButton, 0, 3);
			this.displayRightToolsTableLayoutPanel.Controls.Add(this.imageAndPointsRadioButton, 0, 1);
			this.displayRightToolsTableLayoutPanel.Location = new System.Drawing.Point(628, 3);
			this.displayRightToolsTableLayoutPanel.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
			this.displayRightToolsTableLayoutPanel.Name = "displayRightToolsTableLayoutPanel";
			this.displayRightToolsTableLayoutPanel.RowCount = 5;
			this.displayRightToolsTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.displayRightToolsTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.displayRightToolsTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.displayRightToolsTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.displayRightToolsTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.displayRightToolsTableLayoutPanel.Size = new System.Drawing.Size(31, 130);
			this.displayRightToolsTableLayoutPanel.TabIndex = 2;
			// 
			// imageRadioButton
			// 
			this.imageRadioButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.imageRadioButton.Appearance = System.Windows.Forms.Appearance.Button;
			this.imageRadioButton.AutoSize = true;
			this.imageRadioButton.Checked = true;
			this.imageRadioButton.Location = new System.Drawing.Point(3, 3);
			this.imageRadioButton.Name = "imageRadioButton";
			this.imageRadioButton.Size = new System.Drawing.Size(25, 23);
			this.imageRadioButton.TabIndex = 0;
			this.imageRadioButton.TabStop = true;
			this.imageRadioButton.Text = "I";
			this.imageRadioButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.imageRadioButton.UseVisualStyleBackColor = true;
			this.imageRadioButton.CheckedChanged += new System.EventHandler(this.imageRadioButton_CheckedChanged);
			// 
			// geometryRadioButton
			// 
			this.geometryRadioButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.geometryRadioButton.Appearance = System.Windows.Forms.Appearance.Button;
			this.geometryRadioButton.AutoSize = true;
			this.geometryRadioButton.Location = new System.Drawing.Point(3, 55);
			this.geometryRadioButton.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
			this.geometryRadioButton.Name = "geometryRadioButton";
			this.geometryRadioButton.Size = new System.Drawing.Size(25, 23);
			this.geometryRadioButton.TabIndex = 1;
			this.geometryRadioButton.Text = "G";
			this.geometryRadioButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.geometryRadioButton.UseVisualStyleBackColor = true;
			this.geometryRadioButton.CheckedChanged += new System.EventHandler(this.geometryRadioButton_CheckedChanged);
			// 
			// overlapRadioButton
			// 
			this.overlapRadioButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.overlapRadioButton.Appearance = System.Windows.Forms.Appearance.Button;
			this.overlapRadioButton.AutoSize = true;
			this.overlapRadioButton.Location = new System.Drawing.Point(3, 81);
			this.overlapRadioButton.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
			this.overlapRadioButton.Name = "overlapRadioButton";
			this.overlapRadioButton.Size = new System.Drawing.Size(25, 23);
			this.overlapRadioButton.TabIndex = 2;
			this.overlapRadioButton.Text = "O";
			this.overlapRadioButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.overlapRadioButton.UseVisualStyleBackColor = true;
			this.overlapRadioButton.CheckedChanged += new System.EventHandler(this.overlapRadioButton_CheckedChanged);
			// 
			// imageAndPointsRadioButton
			// 
			this.imageAndPointsRadioButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.imageAndPointsRadioButton.Appearance = System.Windows.Forms.Appearance.Button;
			this.imageAndPointsRadioButton.AutoSize = true;
			this.imageAndPointsRadioButton.Location = new System.Drawing.Point(3, 29);
			this.imageAndPointsRadioButton.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
			this.imageAndPointsRadioButton.Name = "imageAndPointsRadioButton";
			this.imageAndPointsRadioButton.Size = new System.Drawing.Size(25, 23);
			this.imageAndPointsRadioButton.TabIndex = 3;
			this.imageAndPointsRadioButton.TabStop = true;
			this.imageAndPointsRadioButton.Text = "P";
			this.imageAndPointsRadioButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.imageAndPointsRadioButton.UseVisualStyleBackColor = true;
			this.imageAndPointsRadioButton.CheckedChanged += new System.EventHandler(this.imageAndPointsRadioButton_CheckedChanged);
			// 
			// keyboardTableLayoutPanel
			// 
			this.keyboardTableLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.keyboardTableLayoutPanel.BackColor = System.Drawing.Color.Transparent;
			this.keyboardTableLayoutPanel.ColumnCount = 1;
			this.keyboardTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.keyboardTableLayoutPanel.Controls.Add(this.unicodeBlockTableLayoutPanel, 0, 0);
			this.keyboardTableLayoutPanel.Controls.Add(this.spaceBarTableLayoutPanel, 0, 2);
			this.keyboardTableLayoutPanel.Controls.Add(this.flowLayoutPanel, 0, 1);
			this.keyboardTableLayoutPanel.Location = new System.Drawing.Point(0, 0);
			this.keyboardTableLayoutPanel.Name = "keyboardTableLayoutPanel";
			this.keyboardTableLayoutPanel.RowCount = 3;
			this.keyboardTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.keyboardTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.keyboardTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.keyboardTableLayoutPanel.Size = new System.Drawing.Size(662, 391);
			this.keyboardTableLayoutPanel.TabIndex = 2;
			// 
			// unicodeBlockTableLayoutPanel
			// 
			this.unicodeBlockTableLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.unicodeBlockTableLayoutPanel.AutoSize = true;
			this.unicodeBlockTableLayoutPanel.ColumnCount = 3;
			this.unicodeBlockTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.unicodeBlockTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.unicodeBlockTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.unicodeBlockTableLayoutPanel.Controls.Add(this.unicodeBlockComboBox, 0, 0);
			this.unicodeBlockTableLayoutPanel.Controls.Add(this.backspaceGlyphPictureBox, 2, 0);
			this.unicodeBlockTableLayoutPanel.Controls.Add(this.blockPager, 1, 0);
			this.unicodeBlockTableLayoutPanel.Location = new System.Drawing.Point(3, 3);
			this.unicodeBlockTableLayoutPanel.Name = "unicodeBlockTableLayoutPanel";
			this.unicodeBlockTableLayoutPanel.RowCount = 1;
			this.unicodeBlockTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.unicodeBlockTableLayoutPanel.Size = new System.Drawing.Size(656, 41);
			this.unicodeBlockTableLayoutPanel.TabIndex = 0;
			// 
			// unicodeBlockComboBox
			// 
			this.unicodeBlockComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.unicodeBlockComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.unicodeBlockComboBox.FormattingEnabled = true;
			this.unicodeBlockComboBox.Location = new System.Drawing.Point(3, 10);
			this.unicodeBlockComboBox.Name = "unicodeBlockComboBox";
			this.unicodeBlockComboBox.Size = new System.Drawing.Size(301, 21);
			this.unicodeBlockComboBox.TabIndex = 0;
			this.unicodeBlockComboBox.SelectedIndexChanged += new System.EventHandler(this.unicodeBlockComboBox_SelectedIndexChanged);
			// 
			// backspaceGlyphPictureBox
			// 
			this.backspaceGlyphPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.backspaceGlyphPictureBox.BackColor = System.Drawing.Color.White;
			this.backspaceGlyphPictureBox.Cursor = System.Windows.Forms.Cursors.Hand;
			this.backspaceGlyphPictureBox.Glyph = "";
			this.backspaceGlyphPictureBox.GlyphImageFile = null;
			this.backspaceGlyphPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("backspaceGlyphPictureBox.Image")));
			this.backspaceGlyphPictureBox.Location = new System.Drawing.Point(618, 3);
			this.backspaceGlyphPictureBox.Name = "backspaceGlyphPictureBox";
			this.backspaceGlyphPictureBox.Size = new System.Drawing.Size(35, 35);
			this.backspaceGlyphPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.backspaceGlyphPictureBox.TabIndex = 1;
			this.backspaceGlyphPictureBox.TabStop = false;
			this.backspaceGlyphPictureBox.Tag = ((ulong)(0ul));
			this.backspaceGlyphPictureBox.TracedCharacter = null;
			this.backspaceGlyphPictureBox.Click += new System.EventHandler(this.backspaceGlyphPictureBox_Click);
			// 
			// blockPager
			// 
			this.blockPager.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.blockPager.AutoSize = true;
			this.blockPager.BlockName = null;
			this.blockPager.Limit = ((uint)(120u));
			this.blockPager.List = null;
			this.blockPager.Location = new System.Drawing.Point(310, 3);
			this.blockPager.Name = "blockPager";
			this.blockPager.Offset = ((uint)(0u));
			this.blockPager.Size = new System.Drawing.Size(215, 35);
			this.blockPager.TabIndex = 2;
			// 
			// spaceBarTableLayoutPanel
			// 
			this.spaceBarTableLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.spaceBarTableLayoutPanel.AutoSize = true;
			this.spaceBarTableLayoutPanel.BackColor = System.Drawing.Color.Transparent;
			this.spaceBarTableLayoutPanel.ColumnCount = 6;
			this.spaceBarTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
			this.spaceBarTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.spaceBarTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 45F));
			this.spaceBarTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 45F));
			this.spaceBarTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 45F));
			this.spaceBarTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 45F));
			this.spaceBarTableLayoutPanel.Controls.Add(this.spaceBarGlyphPictureBox, 1, 0);
			this.spaceBarTableLayoutPanel.Controls.Add(this.leftGlyphPictureBox, 2, 0);
			this.spaceBarTableLayoutPanel.Controls.Add(this.upGlyphPictureBox, 3, 0);
			this.spaceBarTableLayoutPanel.Controls.Add(this.downGlyphPictureBox, 4, 0);
			this.spaceBarTableLayoutPanel.Controls.Add(this.rightGlyphPictureBox, 5, 0);
			this.spaceBarTableLayoutPanel.Location = new System.Drawing.Point(3, 347);
			this.spaceBarTableLayoutPanel.Name = "spaceBarTableLayoutPanel";
			this.spaceBarTableLayoutPanel.RowCount = 1;
			this.spaceBarTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.spaceBarTableLayoutPanel.Size = new System.Drawing.Size(656, 41);
			this.spaceBarTableLayoutPanel.TabIndex = 1;
			// 
			// spaceBarGlyphPictureBox
			// 
			this.spaceBarGlyphPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
			this.spaceBarGlyphPictureBox.BackColor = System.Drawing.Color.White;
			this.spaceBarGlyphPictureBox.Cursor = System.Windows.Forms.Cursors.Hand;
			this.spaceBarGlyphPictureBox.Glyph = "";
			this.spaceBarGlyphPictureBox.GlyphImageFile = null;
			this.spaceBarGlyphPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("spaceBarGlyphPictureBox.Image")));
			this.spaceBarGlyphPictureBox.Location = new System.Drawing.Point(260, 3);
			this.spaceBarGlyphPictureBox.Name = "spaceBarGlyphPictureBox";
			this.spaceBarGlyphPictureBox.Size = new System.Drawing.Size(35, 35);
			this.spaceBarGlyphPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.spaceBarGlyphPictureBox.TabIndex = 0;
			this.spaceBarGlyphPictureBox.TabStop = false;
			this.spaceBarGlyphPictureBox.Tag = ((ulong)(0ul));
			this.spaceBarGlyphPictureBox.TracedCharacter = null;
			this.spaceBarGlyphPictureBox.Click += new System.EventHandler(this.spaceBarGlyphPictureBox_Click);
			// 
			// leftGlyphPictureBox
			// 
			this.leftGlyphPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.leftGlyphPictureBox.BackColor = System.Drawing.Color.White;
			this.leftGlyphPictureBox.Cursor = System.Windows.Forms.Cursors.Hand;
			this.leftGlyphPictureBox.Glyph = "";
			this.leftGlyphPictureBox.GlyphImageFile = null;
			this.leftGlyphPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("leftGlyphPictureBox.Image")));
			this.leftGlyphPictureBox.Location = new System.Drawing.Point(479, 3);
			this.leftGlyphPictureBox.Name = "leftGlyphPictureBox";
			this.leftGlyphPictureBox.Size = new System.Drawing.Size(35, 35);
			this.leftGlyphPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.leftGlyphPictureBox.TabIndex = 1;
			this.leftGlyphPictureBox.TabStop = false;
			this.leftGlyphPictureBox.Tag = ((ulong)(0ul));
			this.leftGlyphPictureBox.TracedCharacter = null;
			this.leftGlyphPictureBox.Click += new System.EventHandler(this.leftGlyphPictureBox_Click);
			// 
			// upGlyphPictureBox
			// 
			this.upGlyphPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.upGlyphPictureBox.BackColor = System.Drawing.Color.White;
			this.upGlyphPictureBox.Cursor = System.Windows.Forms.Cursors.Hand;
			this.upGlyphPictureBox.Glyph = "";
			this.upGlyphPictureBox.GlyphImageFile = null;
			this.upGlyphPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("upGlyphPictureBox.Image")));
			this.upGlyphPictureBox.Location = new System.Drawing.Point(524, 3);
			this.upGlyphPictureBox.Name = "upGlyphPictureBox";
			this.upGlyphPictureBox.Size = new System.Drawing.Size(35, 35);
			this.upGlyphPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.upGlyphPictureBox.TabIndex = 2;
			this.upGlyphPictureBox.TabStop = false;
			this.upGlyphPictureBox.Tag = ((ulong)(0ul));
			this.upGlyphPictureBox.TracedCharacter = null;
			this.upGlyphPictureBox.Click += new System.EventHandler(this.upGlyphPictureBox_Click);
			// 
			// downGlyphPictureBox
			// 
			this.downGlyphPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.downGlyphPictureBox.BackColor = System.Drawing.Color.White;
			this.downGlyphPictureBox.Cursor = System.Windows.Forms.Cursors.Hand;
			this.downGlyphPictureBox.Glyph = "";
			this.downGlyphPictureBox.GlyphImageFile = null;
			this.downGlyphPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("downGlyphPictureBox.Image")));
			this.downGlyphPictureBox.Location = new System.Drawing.Point(569, 3);
			this.downGlyphPictureBox.Name = "downGlyphPictureBox";
			this.downGlyphPictureBox.Size = new System.Drawing.Size(35, 35);
			this.downGlyphPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.downGlyphPictureBox.TabIndex = 3;
			this.downGlyphPictureBox.TabStop = false;
			this.downGlyphPictureBox.Tag = ((ulong)(0ul));
			this.downGlyphPictureBox.TracedCharacter = null;
			this.downGlyphPictureBox.Click += new System.EventHandler(this.downGlyphPictureBox_Click);
			// 
			// rightGlyphPictureBox
			// 
			this.rightGlyphPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.rightGlyphPictureBox.BackColor = System.Drawing.Color.White;
			this.rightGlyphPictureBox.Cursor = System.Windows.Forms.Cursors.Hand;
			this.rightGlyphPictureBox.Glyph = "";
			this.rightGlyphPictureBox.GlyphImageFile = null;
			this.rightGlyphPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("rightGlyphPictureBox.Image")));
			this.rightGlyphPictureBox.Location = new System.Drawing.Point(614, 3);
			this.rightGlyphPictureBox.Name = "rightGlyphPictureBox";
			this.rightGlyphPictureBox.Size = new System.Drawing.Size(35, 35);
			this.rightGlyphPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.rightGlyphPictureBox.TabIndex = 4;
			this.rightGlyphPictureBox.TabStop = false;
			this.rightGlyphPictureBox.Tag = ((ulong)(0ul));
			this.rightGlyphPictureBox.TracedCharacter = null;
			this.rightGlyphPictureBox.Click += new System.EventHandler(this.rightGlyphPictureBox_Click);
			// 
			// flowLayoutPanel
			// 
			this.flowLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.flowLayoutPanel.Location = new System.Drawing.Point(3, 50);
			this.flowLayoutPanel.Name = "flowLayoutPanel";
			this.flowLayoutPanel.Size = new System.Drawing.Size(656, 291);
			this.flowLayoutPanel.TabIndex = 2;
			// 
			// glyphToolTip
			// 
			this.glyphToolTip.AutomaticDelay = 250;
			this.glyphToolTip.UseAnimation = false;
			this.glyphToolTip.UseFading = false;
			// 
			// gutterXNumericUpDown
			// 
			this.gutterXNumericUpDown.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.gutterXNumericUpDown.AutoSize = true;
			this.gutterXNumericUpDown.Location = new System.Drawing.Point(3, 62);
			this.gutterXNumericUpDown.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.gutterXNumericUpDown.Name = "gutterXNumericUpDown";
			this.gutterXNumericUpDown.Size = new System.Drawing.Size(57, 20);
			this.gutterXNumericUpDown.TabIndex = 2;
			this.gutterXNumericUpDown.ValueChanged += new System.EventHandler(this.gutterXNumericUpDown_ValueChanged);
			// 
			// gutterYNumericUpDown
			// 
			this.gutterYNumericUpDown.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.gutterYNumericUpDown.AutoSize = true;
			this.gutterYNumericUpDown.Location = new System.Drawing.Point(3, 92);
			this.gutterYNumericUpDown.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.gutterYNumericUpDown.Name = "gutterYNumericUpDown";
			this.gutterYNumericUpDown.Size = new System.Drawing.Size(57, 20);
			this.gutterYNumericUpDown.TabIndex = 3;
			this.gutterYNumericUpDown.ValueChanged += new System.EventHandler(this.gutterYNumericUpDown_ValueChanged);
			// 
			// VirtualKeyboard
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(665, 533);
			this.Controls.Add(this.splitContainer);
			this.KeyPreview = true;
			this.Name = "VirtualKeyboard";
			this.ShowIcon = false;
			this.Text = "Proofing Tool";
			this.Load += new System.EventHandler(this.VirtualKeyboard_Load);
			this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.VirtualKeyboard_KeyPress);
			this.splitContainer.Panel1.ResumeLayout(false);
			this.splitContainer.Panel2.ResumeLayout(false);
			this.splitContainer.ResumeLayout(false);
			this.displayTableLayoutPanel.ResumeLayout(false);
			this.displayTableLayoutPanel.PerformLayout();
			this.contextMenuStrip.ResumeLayout(false);
			this.leftDisplayToolsTableLayoutPanel.ResumeLayout(false);
			this.leftDisplayToolsTableLayoutPanel.PerformLayout();
			this.displayRightToolsTableLayoutPanel.ResumeLayout(false);
			this.displayRightToolsTableLayoutPanel.PerformLayout();
			this.keyboardTableLayoutPanel.ResumeLayout(false);
			this.keyboardTableLayoutPanel.PerformLayout();
			this.unicodeBlockTableLayoutPanel.ResumeLayout(false);
			this.unicodeBlockTableLayoutPanel.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.backspaceGlyphPictureBox)).EndInit();
			this.spaceBarTableLayoutPanel.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.spaceBarGlyphPictureBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.leftGlyphPictureBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.upGlyphPictureBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.downGlyphPictureBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.rightGlyphPictureBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gutterXNumericUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gutterYNumericUpDown)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainer;
		private System.Windows.Forms.TableLayoutPanel displayTableLayoutPanel;
		private OpenTK.GLControl glControl;
		private System.Windows.Forms.TableLayoutPanel leftDisplayToolsTableLayoutPanel;
		private System.Windows.Forms.Button zoomInButton;
		private System.Windows.Forms.Button zoomOutButton;
		private System.Windows.Forms.TableLayoutPanel displayRightToolsTableLayoutPanel;
		private System.Windows.Forms.RadioButton imageRadioButton;
		private System.Windows.Forms.RadioButton geometryRadioButton;
		private System.Windows.Forms.RadioButton overlapRadioButton;
		private System.Windows.Forms.TableLayoutPanel keyboardTableLayoutPanel;
		private System.Windows.Forms.TableLayoutPanel unicodeBlockTableLayoutPanel;
		private System.Windows.Forms.ComboBox unicodeBlockComboBox;
		private GlyphPictureBox backspaceGlyphPictureBox;
		private System.Windows.Forms.TableLayoutPanel spaceBarTableLayoutPanel;
		private GlyphPictureBox spaceBarGlyphPictureBox;
		private GlyphPictureBox leftGlyphPictureBox;
		private GlyphPictureBox upGlyphPictureBox;
		private GlyphPictureBox downGlyphPictureBox;
		private GlyphPictureBox rightGlyphPictureBox;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel;
		private System.Windows.Forms.ToolTip glyphToolTip;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem defaultToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem uTF16ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem uTFUEscapeToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem uTFXEscapeToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem defaultPasteToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripMenuItem utf16PasteToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem utfPasteUEscapeToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem utfPasteXEscapeToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ToolStripMenuItem exportToPNGToolStripMenuItem;
		private BlockPager blockPager;
		private System.Windows.Forms.RadioButton imageAndPointsRadioButton;
		private System.Windows.Forms.NumericUpDown gutterXNumericUpDown;
		private System.Windows.Forms.NumericUpDown gutterYNumericUpDown;
	}
}