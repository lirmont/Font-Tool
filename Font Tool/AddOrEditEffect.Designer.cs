namespace FontTool
{
	partial class AddOrEditEffect
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
			this.mainTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
			this.nameLabel = new System.Windows.Forms.Label();
			this.effectNameComboBox = new System.Windows.Forms.ComboBox();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.saveButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.parametersLabel = new System.Windows.Forms.Label();
			this.parametersPanel = new System.Windows.Forms.Panel();
			this.parametersTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
			this.scopeLabel = new System.Windows.Forms.Label();
			this.scopeComboBox = new System.Windows.Forms.ComboBox();
			this.scopeListView = new System.Windows.Forms.ListView();
			this.characterCodeColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.primaryNameColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.blockColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.scopeListViewContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.addCharacterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.removeCharacterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.traceMethodLabel = new System.Windows.Forms.Label();
			this.traceMethodTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
			this.imageBoundaryRadioButton = new System.Windows.Forms.RadioButton();
			this.imageRadioButton = new System.Windows.Forms.RadioButton();
			this.passThroughRadioButton = new System.Windows.Forms.RadioButton();
			this.traceSettingsTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
			this.dilationLabel = new System.Windows.Forms.Label();
			this.lineInterpolationLabel = new System.Windows.Forms.Label();
			this.pixelSmoothingCheckBox = new System.Windows.Forms.CheckBox();
			this.lineSmoothingCheckBox = new System.Windows.Forms.CheckBox();
			this.dilationNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.lineInterpolationNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.dilationPixelsLabel = new System.Windows.Forms.Label();
			this.lineInterploatePixelsLabel = new System.Windows.Forms.Label();
			this.solidificationLabel = new System.Windows.Forms.Label();
			this.solidifyByNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.label1 = new System.Windows.Forms.Label();
			this.invertedNormalsCheckBox = new System.Windows.Forms.CheckBox();
			this.xyzAdjustmentLabel = new System.Windows.Forms.Label();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.zAdjustmentNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.yAdjustmentNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.xAdjustmentNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.fillEdgesCheckBox = new System.Windows.Forms.CheckBox();
			this.mainTableLayoutPanel.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.parametersPanel.SuspendLayout();
			this.scopeListViewContextMenuStrip.SuspendLayout();
			this.traceMethodTableLayoutPanel.SuspendLayout();
			this.traceSettingsTableLayoutPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dilationNumericUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.lineInterpolationNumericUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.solidifyByNumericUpDown)).BeginInit();
			this.tableLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.zAdjustmentNumericUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.yAdjustmentNumericUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.xAdjustmentNumericUpDown)).BeginInit();
			this.SuspendLayout();
			// 
			// mainTableLayoutPanel
			// 
			this.mainTableLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.mainTableLayoutPanel.BackColor = System.Drawing.Color.Transparent;
			this.mainTableLayoutPanel.ColumnCount = 2;
			this.mainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.mainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.mainTableLayoutPanel.Controls.Add(this.nameLabel, 0, 0);
			this.mainTableLayoutPanel.Controls.Add(this.effectNameComboBox, 1, 0);
			this.mainTableLayoutPanel.Controls.Add(this.tableLayoutPanel2, 0, 7);
			this.mainTableLayoutPanel.Controls.Add(this.parametersLabel, 0, 1);
			this.mainTableLayoutPanel.Controls.Add(this.parametersPanel, 0, 2);
			this.mainTableLayoutPanel.Controls.Add(this.scopeLabel, 0, 5);
			this.mainTableLayoutPanel.Controls.Add(this.scopeComboBox, 1, 5);
			this.mainTableLayoutPanel.Controls.Add(this.scopeListView, 0, 6);
			this.mainTableLayoutPanel.Controls.Add(this.traceMethodLabel, 0, 3);
			this.mainTableLayoutPanel.Controls.Add(this.traceMethodTableLayoutPanel, 1, 3);
			this.mainTableLayoutPanel.Controls.Add(this.traceSettingsTableLayoutPanel, 1, 4);
			this.mainTableLayoutPanel.Location = new System.Drawing.Point(0, 0);
			this.mainTableLayoutPanel.Name = "mainTableLayoutPanel";
			this.mainTableLayoutPanel.RowCount = 8;
			this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.mainTableLayoutPanel.Size = new System.Drawing.Size(491, 493);
			this.mainTableLayoutPanel.TabIndex = 0;
			// 
			// nameLabel
			// 
			this.nameLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.nameLabel.AutoSize = true;
			this.nameLabel.Location = new System.Drawing.Point(42, 7);
			this.nameLabel.Name = "nameLabel";
			this.nameLabel.Size = new System.Drawing.Size(38, 13);
			this.nameLabel.TabIndex = 0;
			this.nameLabel.Text = "Name:";
			// 
			// effectNameComboBox
			// 
			this.effectNameComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.effectNameComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.effectNameComboBox.FormattingEnabled = true;
			this.effectNameComboBox.Location = new System.Drawing.Point(86, 3);
			this.effectNameComboBox.Name = "effectNameComboBox";
			this.effectNameComboBox.Size = new System.Drawing.Size(402, 21);
			this.effectNameComboBox.TabIndex = 1;
			this.effectNameComboBox.SelectedIndexChanged += new System.EventHandler(this.effectNameComboBox_SelectedIndexChanged);
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.tableLayoutPanel2.AutoSize = true;
			this.tableLayoutPanel2.ColumnCount = 2;
			this.mainTableLayoutPanel.SetColumnSpan(this.tableLayoutPanel2, 2);
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel2.Controls.Add(this.saveButton, 0, 0);
			this.tableLayoutPanel2.Controls.Add(this.cancelButton, 1, 0);
			this.tableLayoutPanel2.Location = new System.Drawing.Point(164, 461);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 1;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(162, 29);
			this.tableLayoutPanel2.TabIndex = 2;
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
			// parametersLabel
			// 
			this.parametersLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.parametersLabel.AutoSize = true;
			this.parametersLabel.Location = new System.Drawing.Point(17, 27);
			this.parametersLabel.Name = "parametersLabel";
			this.parametersLabel.Size = new System.Drawing.Size(63, 13);
			this.parametersLabel.TabIndex = 3;
			this.parametersLabel.Text = "Parameters:";
			this.parametersLabel.Visible = false;
			// 
			// parametersPanel
			// 
			this.parametersPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.parametersPanel.AutoScroll = true;
			this.parametersPanel.AutoSize = true;
			this.parametersPanel.BackColor = System.Drawing.Color.Transparent;
			this.mainTableLayoutPanel.SetColumnSpan(this.parametersPanel, 2);
			this.parametersPanel.Controls.Add(this.parametersTableLayoutPanel);
			this.parametersPanel.Location = new System.Drawing.Point(35, 43);
			this.parametersPanel.Margin = new System.Windows.Forms.Padding(35, 3, 3, 3);
			this.parametersPanel.Name = "parametersPanel";
			this.parametersPanel.Size = new System.Drawing.Size(453, 125);
			this.parametersPanel.TabIndex = 4;
			this.parametersPanel.Visible = false;
			// 
			// parametersTableLayoutPanel
			// 
			this.parametersTableLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.parametersTableLayoutPanel.ColumnCount = 2;
			this.parametersTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.parametersTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.parametersTableLayoutPanel.Location = new System.Drawing.Point(0, 0);
			this.parametersTableLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
			this.parametersTableLayoutPanel.Name = "parametersTableLayoutPanel";
			this.parametersTableLayoutPanel.RowCount = 6;
			this.parametersTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.parametersTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.parametersTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.parametersTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.parametersTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.parametersTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.parametersTableLayoutPanel.Size = new System.Drawing.Size(453, 125);
			this.parametersTableLayoutPanel.TabIndex = 5;
			// 
			// scopeLabel
			// 
			this.scopeLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.scopeLabel.AutoSize = true;
			this.scopeLabel.Location = new System.Drawing.Point(39, 392);
			this.scopeLabel.Name = "scopeLabel";
			this.scopeLabel.Size = new System.Drawing.Size(41, 13);
			this.scopeLabel.TabIndex = 5;
			this.scopeLabel.Text = "Scope:";
			this.scopeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// scopeComboBox
			// 
			this.scopeComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.scopeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.scopeComboBox.FormattingEnabled = true;
			this.scopeComboBox.Items.AddRange(new object[] {
            "Generic",
            "Specific"});
			this.scopeComboBox.Location = new System.Drawing.Point(86, 388);
			this.scopeComboBox.Name = "scopeComboBox";
			this.scopeComboBox.Size = new System.Drawing.Size(402, 21);
			this.scopeComboBox.TabIndex = 6;
			this.scopeComboBox.SelectedIndexChanged += new System.EventHandler(this.scopeComboBox_SelectedIndexChanged);
			// 
			// scopeListView
			// 
			this.scopeListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.scopeListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.characterCodeColumnHeader,
            this.primaryNameColumnHeader,
            this.blockColumnHeader});
			this.mainTableLayoutPanel.SetColumnSpan(this.scopeListView, 2);
			this.scopeListView.ContextMenuStrip = this.scopeListViewContextMenuStrip;
			this.scopeListView.Enabled = false;
			this.scopeListView.FullRowSelect = true;
			this.scopeListView.GridLines = true;
			this.scopeListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.scopeListView.Location = new System.Drawing.Point(35, 415);
			this.scopeListView.Margin = new System.Windows.Forms.Padding(35, 3, 3, 3);
			this.scopeListView.Name = "scopeListView";
			this.scopeListView.ShowItemToolTips = true;
			this.scopeListView.Size = new System.Drawing.Size(453, 40);
			this.scopeListView.TabIndex = 7;
			this.scopeListView.UseCompatibleStateImageBehavior = false;
			this.scopeListView.View = System.Windows.Forms.View.Details;
			// 
			// characterCodeColumnHeader
			// 
			this.characterCodeColumnHeader.Text = "Character Code";
			this.characterCodeColumnHeader.Width = 91;
			// 
			// primaryNameColumnHeader
			// 
			this.primaryNameColumnHeader.Text = "Primary Name";
			this.primaryNameColumnHeader.Width = 144;
			// 
			// blockColumnHeader
			// 
			this.blockColumnHeader.Text = "Block";
			this.blockColumnHeader.Width = 125;
			// 
			// scopeListViewContextMenuStrip
			// 
			this.scopeListViewContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addCharacterToolStripMenuItem,
            this.removeCharacterToolStripMenuItem});
			this.scopeListViewContextMenuStrip.Name = "scopeListViewContextMenuStrip";
			this.scopeListViewContextMenuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.scopeListViewContextMenuStrip.Size = new System.Drawing.Size(176, 48);
			this.scopeListViewContextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.scopeListViewContextMenuStrip_Opening);
			// 
			// addCharacterToolStripMenuItem
			// 
			this.addCharacterToolStripMenuItem.Name = "addCharacterToolStripMenuItem";
			this.addCharacterToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
			this.addCharacterToolStripMenuItem.Text = "Add Character";
			this.addCharacterToolStripMenuItem.Click += new System.EventHandler(this.addCharacterToolStripMenuItem_Click);
			// 
			// removeCharacterToolStripMenuItem
			// 
			this.removeCharacterToolStripMenuItem.Name = "removeCharacterToolStripMenuItem";
			this.removeCharacterToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
			this.removeCharacterToolStripMenuItem.Text = "Remove Character";
			this.removeCharacterToolStripMenuItem.Click += new System.EventHandler(this.removeCharacterToolStripMenuItem_Click);
			// 
			// traceMethodLabel
			// 
			this.traceMethodLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.traceMethodLabel.AutoSize = true;
			this.traceMethodLabel.Location = new System.Drawing.Point(3, 179);
			this.traceMethodLabel.Name = "traceMethodLabel";
			this.traceMethodLabel.Size = new System.Drawing.Size(77, 13);
			this.traceMethodLabel.TabIndex = 8;
			this.traceMethodLabel.Text = "Trace Method:";
			this.traceMethodLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// traceMethodTableLayoutPanel
			// 
			this.traceMethodTableLayoutPanel.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.traceMethodTableLayoutPanel.AutoSize = true;
			this.traceMethodTableLayoutPanel.ColumnCount = 3;
			this.traceMethodTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.traceMethodTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.traceMethodTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.traceMethodTableLayoutPanel.Controls.Add(this.imageBoundaryRadioButton, 1, 0);
			this.traceMethodTableLayoutPanel.Controls.Add(this.imageRadioButton, 2, 0);
			this.traceMethodTableLayoutPanel.Controls.Add(this.passThroughRadioButton, 0, 0);
			this.traceMethodTableLayoutPanel.Location = new System.Drawing.Point(86, 174);
			this.traceMethodTableLayoutPanel.Name = "traceMethodTableLayoutPanel";
			this.traceMethodTableLayoutPanel.RowCount = 1;
			this.traceMethodTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.traceMethodTableLayoutPanel.Size = new System.Drawing.Size(265, 23);
			this.traceMethodTableLayoutPanel.TabIndex = 9;
			// 
			// imageBoundaryRadioButton
			// 
			this.imageBoundaryRadioButton.AutoSize = true;
			this.imageBoundaryRadioButton.Location = new System.Drawing.Point(100, 3);
			this.imageBoundaryRadioButton.Name = "imageBoundaryRadioButton";
			this.imageBoundaryRadioButton.Size = new System.Drawing.Size(102, 17);
			this.imageBoundaryRadioButton.TabIndex = 0;
			this.imageBoundaryRadioButton.Text = "Image Boundary";
			this.imageBoundaryRadioButton.UseVisualStyleBackColor = true;
			this.imageBoundaryRadioButton.CheckedChanged += new System.EventHandler(this.imageBoundaryRadioButton_CheckedChanged);
			// 
			// imageRadioButton
			// 
			this.imageRadioButton.AutoSize = true;
			this.imageRadioButton.Location = new System.Drawing.Point(208, 3);
			this.imageRadioButton.Name = "imageRadioButton";
			this.imageRadioButton.Size = new System.Drawing.Size(54, 17);
			this.imageRadioButton.TabIndex = 1;
			this.imageRadioButton.Text = "Image";
			this.imageRadioButton.UseVisualStyleBackColor = true;
			this.imageRadioButton.CheckedChanged += new System.EventHandler(this.imageRadioButton_CheckedChanged);
			// 
			// passThroughRadioButton
			// 
			this.passThroughRadioButton.AutoSize = true;
			this.passThroughRadioButton.Checked = true;
			this.passThroughRadioButton.Location = new System.Drawing.Point(3, 3);
			this.passThroughRadioButton.Name = "passThroughRadioButton";
			this.passThroughRadioButton.Size = new System.Drawing.Size(91, 17);
			this.passThroughRadioButton.TabIndex = 2;
			this.passThroughRadioButton.TabStop = true;
			this.passThroughRadioButton.Text = "Pass-Through";
			this.passThroughRadioButton.UseVisualStyleBackColor = true;
			this.passThroughRadioButton.CheckedChanged += new System.EventHandler(this.passThroughRadioButton_CheckedChanged);
			// 
			// traceSettingsTableLayoutPanel
			// 
			this.traceSettingsTableLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.traceSettingsTableLayoutPanel.AutoSize = true;
			this.traceSettingsTableLayoutPanel.ColumnCount = 4;
			this.traceSettingsTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.traceSettingsTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.traceSettingsTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.traceSettingsTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.traceSettingsTableLayoutPanel.Controls.Add(this.dilationLabel, 0, 0);
			this.traceSettingsTableLayoutPanel.Controls.Add(this.lineInterpolationLabel, 0, 3);
			this.traceSettingsTableLayoutPanel.Controls.Add(this.pixelSmoothingCheckBox, 1, 1);
			this.traceSettingsTableLayoutPanel.Controls.Add(this.lineSmoothingCheckBox, 1, 2);
			this.traceSettingsTableLayoutPanel.Controls.Add(this.dilationNumericUpDown, 1, 0);
			this.traceSettingsTableLayoutPanel.Controls.Add(this.lineInterpolationNumericUpDown, 1, 3);
			this.traceSettingsTableLayoutPanel.Controls.Add(this.dilationPixelsLabel, 2, 0);
			this.traceSettingsTableLayoutPanel.Controls.Add(this.lineInterploatePixelsLabel, 2, 3);
			this.traceSettingsTableLayoutPanel.Controls.Add(this.solidificationLabel, 0, 4);
			this.traceSettingsTableLayoutPanel.Controls.Add(this.solidifyByNumericUpDown, 1, 4);
			this.traceSettingsTableLayoutPanel.Controls.Add(this.label1, 2, 4);
			this.traceSettingsTableLayoutPanel.Controls.Add(this.invertedNormalsCheckBox, 1, 5);
			this.traceSettingsTableLayoutPanel.Controls.Add(this.xyzAdjustmentLabel, 0, 6);
			this.traceSettingsTableLayoutPanel.Controls.Add(this.tableLayoutPanel1, 1, 6);
			this.traceSettingsTableLayoutPanel.Controls.Add(this.fillEdgesCheckBox, 3, 4);
			this.traceSettingsTableLayoutPanel.Enabled = false;
			this.traceSettingsTableLayoutPanel.Location = new System.Drawing.Point(86, 203);
			this.traceSettingsTableLayoutPanel.Name = "traceSettingsTableLayoutPanel";
			this.traceSettingsTableLayoutPanel.RowCount = 7;
			this.traceSettingsTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.traceSettingsTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.traceSettingsTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.traceSettingsTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.traceSettingsTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.traceSettingsTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.traceSettingsTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.traceSettingsTableLayoutPanel.Size = new System.Drawing.Size(402, 179);
			this.traceSettingsTableLayoutPanel.TabIndex = 10;
			// 
			// dilationLabel
			// 
			this.dilationLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.dilationLabel.AutoSize = true;
			this.dilationLabel.Location = new System.Drawing.Point(106, 6);
			this.dilationLabel.Name = "dilationLabel";
			this.dilationLabel.Size = new System.Drawing.Size(42, 13);
			this.dilationLabel.TabIndex = 0;
			this.dilationLabel.Text = "Dilation";
			this.dilationLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lineInterpolationLabel
			// 
			this.lineInterpolationLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.lineInterpolationLabel.AutoSize = true;
			this.lineInterpolationLabel.Location = new System.Drawing.Point(3, 78);
			this.lineInterpolationLabel.Name = "lineInterpolationLabel";
			this.lineInterpolationLabel.Size = new System.Drawing.Size(145, 13);
			this.lineInterpolationLabel.TabIndex = 1;
			this.lineInterpolationLabel.Text = "Interpolate Sharp Corners By:";
			this.lineInterpolationLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// pixelSmoothingCheckBox
			// 
			this.pixelSmoothingCheckBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.pixelSmoothingCheckBox.AutoSize = true;
			this.traceSettingsTableLayoutPanel.SetColumnSpan(this.pixelSmoothingCheckBox, 2);
			this.pixelSmoothingCheckBox.Location = new System.Drawing.Point(154, 29);
			this.pixelSmoothingCheckBox.Name = "pixelSmoothingCheckBox";
			this.pixelSmoothingCheckBox.Size = new System.Drawing.Size(101, 17);
			this.pixelSmoothingCheckBox.TabIndex = 2;
			this.pixelSmoothingCheckBox.Text = "Pixel Smoothing";
			this.pixelSmoothingCheckBox.UseVisualStyleBackColor = true;
			this.pixelSmoothingCheckBox.CheckedChanged += new System.EventHandler(this.pixelSmoothingCheckBox_CheckedChanged);
			// 
			// lineSmoothingCheckBox
			// 
			this.lineSmoothingCheckBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.lineSmoothingCheckBox.AutoSize = true;
			this.traceSettingsTableLayoutPanel.SetColumnSpan(this.lineSmoothingCheckBox, 2);
			this.lineSmoothingCheckBox.Location = new System.Drawing.Point(154, 52);
			this.lineSmoothingCheckBox.Name = "lineSmoothingCheckBox";
			this.lineSmoothingCheckBox.Size = new System.Drawing.Size(99, 17);
			this.lineSmoothingCheckBox.TabIndex = 3;
			this.lineSmoothingCheckBox.Text = "Line Smoothing";
			this.lineSmoothingCheckBox.UseVisualStyleBackColor = true;
			this.lineSmoothingCheckBox.CheckedChanged += new System.EventHandler(this.lineSmoothingCheckBox_CheckedChanged);
			// 
			// dilationNumericUpDown
			// 
			this.dilationNumericUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.dilationNumericUpDown.AutoSize = true;
			this.dilationNumericUpDown.Location = new System.Drawing.Point(154, 3);
			this.dilationNumericUpDown.Name = "dilationNumericUpDown";
			this.dilationNumericUpDown.Size = new System.Drawing.Size(72, 20);
			this.dilationNumericUpDown.TabIndex = 4;
			this.dilationNumericUpDown.ValueChanged += new System.EventHandler(this.dilationNumericUpDown_ValueChanged);
			// 
			// lineInterpolationNumericUpDown
			// 
			this.lineInterpolationNumericUpDown.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.lineInterpolationNumericUpDown.AutoSize = true;
			this.lineInterpolationNumericUpDown.DecimalPlaces = 2;
			this.lineInterpolationNumericUpDown.Location = new System.Drawing.Point(154, 75);
			this.lineInterpolationNumericUpDown.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
			this.lineInterpolationNumericUpDown.Name = "lineInterpolationNumericUpDown";
			this.lineInterpolationNumericUpDown.Size = new System.Drawing.Size(72, 20);
			this.lineInterpolationNumericUpDown.TabIndex = 5;
			this.lineInterpolationNumericUpDown.ValueChanged += new System.EventHandler(this.lineInterpolationNumericUpDown_ValueChanged);
			// 
			// dilationPixelsLabel
			// 
			this.dilationPixelsLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.dilationPixelsLabel.AutoSize = true;
			this.dilationPixelsLabel.Location = new System.Drawing.Point(232, 6);
			this.dilationPixelsLabel.Name = "dilationPixelsLabel";
			this.dilationPixelsLabel.Size = new System.Drawing.Size(33, 13);
			this.dilationPixelsLabel.TabIndex = 6;
			this.dilationPixelsLabel.Text = "pixels";
			// 
			// lineInterploatePixelsLabel
			// 
			this.lineInterploatePixelsLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.lineInterploatePixelsLabel.AutoSize = true;
			this.lineInterploatePixelsLabel.Location = new System.Drawing.Point(232, 78);
			this.lineInterploatePixelsLabel.Name = "lineInterploatePixelsLabel";
			this.lineInterploatePixelsLabel.Size = new System.Drawing.Size(33, 13);
			this.lineInterploatePixelsLabel.TabIndex = 7;
			this.lineInterploatePixelsLabel.Text = "pixels";
			// 
			// solidificationLabel
			// 
			this.solidificationLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.solidificationLabel.AutoSize = true;
			this.solidificationLabel.Location = new System.Drawing.Point(90, 104);
			this.solidificationLabel.Name = "solidificationLabel";
			this.solidificationLabel.Size = new System.Drawing.Size(58, 13);
			this.solidificationLabel.TabIndex = 8;
			this.solidificationLabel.Text = "Solidify By:";
			this.solidificationLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// solidifyByNumericUpDown
			// 
			this.solidifyByNumericUpDown.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.solidifyByNumericUpDown.AutoSize = true;
			this.solidifyByNumericUpDown.DecimalPlaces = 2;
			this.solidifyByNumericUpDown.Location = new System.Drawing.Point(154, 101);
			this.solidifyByNumericUpDown.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
			this.solidifyByNumericUpDown.Name = "solidifyByNumericUpDown";
			this.solidifyByNumericUpDown.Size = new System.Drawing.Size(72, 20);
			this.solidifyByNumericUpDown.TabIndex = 9;
			this.solidifyByNumericUpDown.ValueChanged += new System.EventHandler(this.solidifyByNumericUpDown_ValueChanged);
			// 
			// label1
			// 
			this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(232, 104);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(33, 13);
			this.label1.TabIndex = 10;
			this.label1.Text = "pixels";
			// 
			// invertedNormalsCheckBox
			// 
			this.invertedNormalsCheckBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.invertedNormalsCheckBox.AutoSize = true;
			this.traceSettingsTableLayoutPanel.SetColumnSpan(this.invertedNormalsCheckBox, 2);
			this.invertedNormalsCheckBox.Location = new System.Drawing.Point(154, 127);
			this.invertedNormalsCheckBox.Name = "invertedNormalsCheckBox";
			this.invertedNormalsCheckBox.Size = new System.Drawing.Size(94, 17);
			this.invertedNormalsCheckBox.TabIndex = 11;
			this.invertedNormalsCheckBox.Text = "Invert Normals";
			this.invertedNormalsCheckBox.UseVisualStyleBackColor = true;
			this.invertedNormalsCheckBox.CheckedChanged += new System.EventHandler(this.invertedNormalsCheckBox_CheckedChanged);
			// 
			// xyzAdjustmentLabel
			// 
			this.xyzAdjustmentLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.xyzAdjustmentLabel.AutoSize = true;
			this.xyzAdjustmentLabel.Location = new System.Drawing.Point(105, 156);
			this.xyzAdjustmentLabel.Name = "xyzAdjustmentLabel";
			this.xyzAdjustmentLabel.Size = new System.Drawing.Size(43, 13);
			this.xyzAdjustmentLabel.TabIndex = 12;
			this.xyzAdjustmentLabel.Text = "X, Y, Z:";
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.AutoSize = true;
			this.tableLayoutPanel1.ColumnCount = 3;
			this.traceSettingsTableLayoutPanel.SetColumnSpan(this.tableLayoutPanel1, 3);
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.Controls.Add(this.zAdjustmentNumericUpDown, 2, 0);
			this.tableLayoutPanel1.Controls.Add(this.yAdjustmentNumericUpDown, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.xAdjustmentNumericUpDown, 0, 0);
			this.tableLayoutPanel1.Location = new System.Drawing.Point(154, 150);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 1;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.Size = new System.Drawing.Size(234, 26);
			this.tableLayoutPanel1.TabIndex = 14;
			// 
			// zAdjustmentNumericUpDown
			// 
			this.zAdjustmentNumericUpDown.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.zAdjustmentNumericUpDown.AutoSize = true;
			this.zAdjustmentNumericUpDown.DecimalPlaces = 2;
			this.zAdjustmentNumericUpDown.Location = new System.Drawing.Point(159, 3);
			this.zAdjustmentNumericUpDown.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
			this.zAdjustmentNumericUpDown.Minimum = new decimal(new int[] {
            99999,
            0,
            0,
            -2147483648});
			this.zAdjustmentNumericUpDown.Name = "zAdjustmentNumericUpDown";
			this.zAdjustmentNumericUpDown.Size = new System.Drawing.Size(72, 20);
			this.zAdjustmentNumericUpDown.TabIndex = 13;
			this.zAdjustmentNumericUpDown.ValueChanged += new System.EventHandler(this.zAdjustmentNumericUpDown_ValueChanged);
			// 
			// yAdjustmentNumericUpDown
			// 
			this.yAdjustmentNumericUpDown.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.yAdjustmentNumericUpDown.AutoSize = true;
			this.yAdjustmentNumericUpDown.DecimalPlaces = 2;
			this.yAdjustmentNumericUpDown.Location = new System.Drawing.Point(81, 3);
			this.yAdjustmentNumericUpDown.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
			this.yAdjustmentNumericUpDown.Minimum = new decimal(new int[] {
            99999,
            0,
            0,
            -2147483648});
			this.yAdjustmentNumericUpDown.Name = "yAdjustmentNumericUpDown";
			this.yAdjustmentNumericUpDown.Size = new System.Drawing.Size(72, 20);
			this.yAdjustmentNumericUpDown.TabIndex = 14;
			this.yAdjustmentNumericUpDown.ValueChanged += new System.EventHandler(this.yNumericUpDown_ValueChanged);
			// 
			// xAdjustmentNumericUpDown
			// 
			this.xAdjustmentNumericUpDown.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.xAdjustmentNumericUpDown.AutoSize = true;
			this.xAdjustmentNumericUpDown.DecimalPlaces = 2;
			this.xAdjustmentNumericUpDown.Location = new System.Drawing.Point(3, 3);
			this.xAdjustmentNumericUpDown.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
			this.xAdjustmentNumericUpDown.Minimum = new decimal(new int[] {
            99999,
            0,
            0,
            -2147483648});
			this.xAdjustmentNumericUpDown.Name = "xAdjustmentNumericUpDown";
			this.xAdjustmentNumericUpDown.Size = new System.Drawing.Size(72, 20);
			this.xAdjustmentNumericUpDown.TabIndex = 15;
			this.xAdjustmentNumericUpDown.ValueChanged += new System.EventHandler(this.xNumericUpDown_ValueChanged);
			// 
			// fillEdgesCheckBox
			// 
			this.fillEdgesCheckBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.fillEdgesCheckBox.AutoSize = true;
			this.fillEdgesCheckBox.Location = new System.Drawing.Point(271, 102);
			this.fillEdgesCheckBox.Name = "fillEdgesCheckBox";
			this.fillEdgesCheckBox.Size = new System.Drawing.Size(71, 17);
			this.fillEdgesCheckBox.TabIndex = 15;
			this.fillEdgesCheckBox.Text = "Fill Edges";
			this.fillEdgesCheckBox.UseVisualStyleBackColor = true;
			this.fillEdgesCheckBox.CheckedChanged += new System.EventHandler(this.fillEdgesCheckBox_CheckedChanged);
			// 
			// AddOrEditEffect
			// 
			this.AcceptButton = this.saveButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(492, 494);
			this.Controls.Add(this.mainTableLayoutPanel);
			this.MinimumSize = new System.Drawing.Size(500, 27);
			this.Name = "AddOrEditEffect";
			this.ShowIcon = false;
			this.Text = "Add or Edit Effect";
			this.mainTableLayoutPanel.ResumeLayout(false);
			this.mainTableLayoutPanel.PerformLayout();
			this.tableLayoutPanel2.ResumeLayout(false);
			this.parametersPanel.ResumeLayout(false);
			this.scopeListViewContextMenuStrip.ResumeLayout(false);
			this.traceMethodTableLayoutPanel.ResumeLayout(false);
			this.traceMethodTableLayoutPanel.PerformLayout();
			this.traceSettingsTableLayoutPanel.ResumeLayout(false);
			this.traceSettingsTableLayoutPanel.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.dilationNumericUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.lineInterpolationNumericUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.solidifyByNumericUpDown)).EndInit();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.zAdjustmentNumericUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.yAdjustmentNumericUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.xAdjustmentNumericUpDown)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel mainTableLayoutPanel;
		private System.Windows.Forms.Label nameLabel;
		private System.Windows.Forms.ComboBox effectNameComboBox;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.Button saveButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Label parametersLabel;
		private System.Windows.Forms.Panel parametersPanel;
		private System.Windows.Forms.TableLayoutPanel parametersTableLayoutPanel;
		private System.Windows.Forms.Label scopeLabel;
		private System.Windows.Forms.ComboBox scopeComboBox;
		private System.Windows.Forms.ListView scopeListView;
		private System.Windows.Forms.ColumnHeader characterCodeColumnHeader;
		private System.Windows.Forms.ColumnHeader primaryNameColumnHeader;
		private System.Windows.Forms.ContextMenuStrip scopeListViewContextMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem addCharacterToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem removeCharacterToolStripMenuItem;
		private System.Windows.Forms.ColumnHeader blockColumnHeader;
		private System.Windows.Forms.Label traceMethodLabel;
		private System.Windows.Forms.TableLayoutPanel traceMethodTableLayoutPanel;
		private System.Windows.Forms.RadioButton imageBoundaryRadioButton;
		private System.Windows.Forms.RadioButton imageRadioButton;
		private System.Windows.Forms.TableLayoutPanel traceSettingsTableLayoutPanel;
		private System.Windows.Forms.Label dilationLabel;
		private System.Windows.Forms.Label lineInterpolationLabel;
		private System.Windows.Forms.CheckBox pixelSmoothingCheckBox;
		private System.Windows.Forms.CheckBox lineSmoothingCheckBox;
		private System.Windows.Forms.NumericUpDown dilationNumericUpDown;
		private System.Windows.Forms.NumericUpDown lineInterpolationNumericUpDown;
		private System.Windows.Forms.Label dilationPixelsLabel;
		private System.Windows.Forms.Label lineInterploatePixelsLabel;
		private System.Windows.Forms.RadioButton passThroughRadioButton;
		private System.Windows.Forms.Label solidificationLabel;
		private System.Windows.Forms.NumericUpDown solidifyByNumericUpDown;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox invertedNormalsCheckBox;
		private System.Windows.Forms.Label xyzAdjustmentLabel;
		private System.Windows.Forms.NumericUpDown zAdjustmentNumericUpDown;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.NumericUpDown yAdjustmentNumericUpDown;
		private System.Windows.Forms.NumericUpDown xAdjustmentNumericUpDown;
		private System.Windows.Forms.CheckBox fillEdgesCheckBox;
	}
}