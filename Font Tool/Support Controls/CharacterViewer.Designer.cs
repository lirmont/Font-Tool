namespace FontTool
{
	partial class CharacterViewer
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
			this.glControl = new OpenTK.GLControl();
			this.SuspendLayout();
			// 
			// glControl
			// 
			this.glControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.glControl.BackColor = System.Drawing.Color.Black;
			this.glControl.Location = new System.Drawing.Point(1, 1);
			this.glControl.Margin = new System.Windows.Forms.Padding(0);
			this.glControl.Name = "glControl";
			this.glControl.Size = new System.Drawing.Size(148, 148);
			this.glControl.TabIndex = 9;
			this.glControl.VSync = true;
			this.glControl.Enter += new System.EventHandler(this.glControl_Enter);
			// 
			// CharacterViewer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Black;
			this.Controls.Add(this.glControl);
			this.Margin = new System.Windows.Forms.Padding(0);
			this.Name = "CharacterViewer";
			this.Padding = new System.Windows.Forms.Padding(1);
			this.ResumeLayout(false);

		}

		#endregion

		private OpenTK.GLControl glControl;
	}
}
