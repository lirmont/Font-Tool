﻿#pragma warning disable
namespace FontTool
{
    partial class Credits
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Credits));
			this.programIconSimpleOpenGlControl = new OpenTK.GLControl();
			this.creditsRichTextBox = new System.Windows.Forms.RichTextBox();
			this.versionNumberLabel = new System.Windows.Forms.RichTextBox();
			this.scheduleRedrawTimer = new System.Windows.Forms.Timer(this.components);
			this.SuspendLayout();
			// 
			// programIconSimpleOpenGlControl
			// 
			this.programIconSimpleOpenGlControl.BackColor = System.Drawing.Color.Black;
			this.programIconSimpleOpenGlControl.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("programIconSimpleOpenGlControl.BackgroundImage")));
			this.programIconSimpleOpenGlControl.Location = new System.Drawing.Point(12, 12);
			this.programIconSimpleOpenGlControl.Name = "programIconSimpleOpenGlControl";
			this.programIconSimpleOpenGlControl.Size = new System.Drawing.Size(192, 192);
			this.programIconSimpleOpenGlControl.TabIndex = 1;
			this.programIconSimpleOpenGlControl.VSync = false;
			this.programIconSimpleOpenGlControl.Paint += new System.Windows.Forms.PaintEventHandler(this.programIconSimpleOpenGlControl_Paint);
			// 
			// creditsRichTextBox
			// 
			this.creditsRichTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.creditsRichTextBox.BackColor = System.Drawing.SystemColors.Control;
			this.creditsRichTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.creditsRichTextBox.Cursor = System.Windows.Forms.Cursors.Default;
			this.creditsRichTextBox.Location = new System.Drawing.Point(221, 9);
			this.creditsRichTextBox.Margin = new System.Windows.Forms.Padding(0);
			this.creditsRichTextBox.MaxLength = 1000;
			this.creditsRichTextBox.Name = "creditsRichTextBox";
			this.creditsRichTextBox.ReadOnly = true;
			this.creditsRichTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
			this.creditsRichTextBox.Size = new System.Drawing.Size(268, 351);
			this.creditsRichTextBox.TabIndex = 0;
			this.creditsRichTextBox.Text = resources.GetString("creditsRichTextBox.Text");
			this.creditsRichTextBox.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.creditsRichTextBox_LinkClicked);
			this.creditsRichTextBox.SelectionChanged += new System.EventHandler(this.creditsRichTextBox_SelectionChanged);
			// 
			// versionNumberLabel
			// 
			this.versionNumberLabel.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.versionNumberLabel.DetectUrls = false;
			this.versionNumberLabel.Location = new System.Drawing.Point(16, 216);
			this.versionNumberLabel.MaxLength = 10000;
			this.versionNumberLabel.Name = "versionNumberLabel";
			this.versionNumberLabel.ReadOnly = true;
			this.versionNumberLabel.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
			this.versionNumberLabel.Size = new System.Drawing.Size(185, 64);
			this.versionNumberLabel.TabIndex = 3;
			this.versionNumberLabel.TabStop = false;
			this.versionNumberLabel.Text = "";
			// 
			// scheduleRedrawTimer
			// 
			this.scheduleRedrawTimer.Enabled = true;
			this.scheduleRedrawTimer.Interval = 20;
			this.scheduleRedrawTimer.Tick += new System.EventHandler(this.scheduleRedrawTimer_Tick);
			// 
			// Credits
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(498, 370);
			this.Controls.Add(this.versionNumberLabel);
			this.Controls.Add(this.programIconSimpleOpenGlControl);
			this.Controls.Add(this.creditsRichTextBox);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Credits";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.Text = "Credits";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Credits_FormClosed);
			this.Load += new System.EventHandler(this.Credits_Load);
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox creditsRichTextBox;
        private OpenTK.GLControl programIconSimpleOpenGlControl;
        private System.Windows.Forms.RichTextBox versionNumberLabel;
		private System.Windows.Forms.Timer scheduleRedrawTimer;
    }
}