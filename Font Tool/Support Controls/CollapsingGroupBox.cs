using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Windows.Forms.Design;

namespace FontTool
{
	/// <summary>
	/// Represents a Windows control that displays a frame at the top of a group of controls with an optional caption and icon.
	/// </summary>
	[ToolboxItem(true)]
	[DefaultEvent("Click"), DefaultProperty("Text")]
	[System.Reflection.ObfuscationAttribute(Feature = "renaming")]
	public partial class CollapsingGroupBox : System.Windows.Forms.GroupBox
	{
		#region  Private Member Declarations

		private Pen _bottomPen;
		private Size _iconMargin;
		private Image _image;
		private Color _lineColorBottom;
		private Color _lineColorTop;
		private SolidBrush _textBrush;
		private Pen _topPen;
		private string expandText = "+";
		private string closeText = "-";
		private bool expanded = true;
		private Rectangle textRegion = Rectangle.Empty;

		#endregion  Private Member Declarations

		#region  Public Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="GroupBox"/> class.
		/// </summary>
		public CollapsingGroupBox()
		{
			_iconMargin = new Size(0, 6);
			_lineColorBottom = SystemColors.ButtonHighlight;
			_lineColorTop = SystemColors.ButtonShadow;

			this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw | ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor, true);

			this.CreateResources();
			this.MouseDown += new MouseEventHandler(CollapsingGroupBox_MouseDown);
		}

		private void CollapsingGroupBox_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == System.Windows.Forms.MouseButtons.Left) {
				if (textRegion.Contains(e.Location))
					Expanded = !Expanded;
			}
		}

		#endregion  Public Constructors

		#region  Protected Overridden Methods

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
				components.Dispose();
			this.CleanUpResources();
			base.Dispose(disposing);
		}

		/// <summary>
		/// Occurs when the control is to be painted.
		/// </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs"/> that contains the event data.</param>
		protected override void OnPaint(PaintEventArgs e)
		{
			SizeF size;
			int y;

			size = e.Graphics.MeasureString(CurrentText, this.Font);
			y = (int)(size.Height + 3) / 2;
			textRegion = new Rectangle(0, 0, (int)Math.Ceiling(size.Width), (int)Math.Ceiling(size.Height));
			if (!expanded)
			{
				MinimumSize = new System.Drawing.Size(MinimumSize.Width, textRegion.Height);
			}
			else {
				MinimumSize = new System.Drawing.Size(MinimumSize.Width, (_image != null) ? textRegion.Height + _iconMargin.Height + _image.Height : textRegion.Height);
			}

			// draw the header text and line
			e.Graphics.DrawString(CurrentText, this.Font, _textBrush, 1, 1);
			e.Graphics.DrawLine(_topPen, size.Width + 3, y, this.Width - 5, y);
			e.Graphics.DrawLine(_bottomPen, size.Width + 3, y + 1, this.Width - 5, y + 1);

			if (!expanded && !this.DesignMode)
				return;

			// draw the image
			if ((_image != null))
				e.Graphics.DrawImage(_image, this.Padding.Left + _iconMargin.Width, this.Padding.Top + (int)size.Height + _iconMargin.Height, _image.Width, _image.Height);

			//draw a designtime outline
			if (this.DesignMode)
			{
				Pen pen;
				pen = new Pen(SystemColors.ButtonShadow);
				pen.DashStyle = DashStyle.Dot;
				e.Graphics.DrawRectangle(pen, 0, 0, Width - 1, Height - 1);
				pen.Dispose();
			}
		}

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Control.SystemColorsChanged"/> event.
		/// </summary>
		/// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
		protected override void OnSystemColorsChanged(EventArgs e)
		{
			base.OnSystemColorsChanged(e);
			this.CreateResources();
			this.Invalidate();
		}

		#endregion  Protected Overridden Methods

		#region  Public Properties

		private int PaddingTop {
			get {
				return (textRegion != Rectangle.Empty) ? textRegion.Height : this.Font.Height + 3;
			}
		}

		private int PaddingLeft {
			get {
				return (_image != null) ? 3 + _iconMargin.Width + _image.Width + 3 : 0;
			}
		}

		/// <summary>
		/// Gets or sets the icon margin.
		/// </summary>
		/// <value>The icon margin.</value>
		[Category("Appearance"), DefaultValue(typeof(Size), "0, 6")]
		public Size IconMargin
		{
			get { return _iconMargin; }
			set
			{
				_iconMargin = value;
				this.Invalidate();
			}
		}

		/// <summary>
		/// Gets or sets the image to display.
		/// </summary>
		/// <value>The image to display.</value>
		[Browsable(true), Category("Appearance"), DefaultValue(typeof(Image), "")]
		public Image Image
		{
			get { return _image; }
			set
			{
				_image = value;
				this.Invalidate();
			}
		}

		/// <summary>
		/// Gets or sets the line color bottom.
		/// </summary>
		/// <value>The line color bottom.</value>
		[Browsable(true), Category("Appearance"), DefaultValue(typeof(Color), "ButtonHighlight")]
		public Color LineColorBottom
		{
			get { return _lineColorBottom; }
			set
			{
				_lineColorBottom = value;
				this.CreateResources();
				this.Invalidate();
			}
		}

		/// <summary>
		/// Gets or sets the line color top.
		/// </summary>
		/// <value>The line color top.</value>
		[Browsable(true), Category("Appearance"), DefaultValue(typeof(Color), "ButtonShadow")]
		public Color LineColorTop
		{
			get { return _lineColorTop; }
			set
			{
				_lineColorTop = value;
				this.CreateResources();
				this.Invalidate();
			}
		}

		/// <summary>
		/// Returns or sets the text displayed in this control.
		/// </summary>
		/// <value></value>
		/// <returns>
		/// The text associated with this control.
		/// </returns>
		[Browsable(true), DefaultValue("")]
		public override string Text
		{
			get { return base.Text; }
			set
			{
				base.Text = value;
				this.Invalidate();
			}
		}

		/// <summary>
		/// Returns or sets the text before the text displayed in this control, indicating ability to open.
		/// </summary>
		/// <value></value>
		/// <returns>
		/// The text associated with this control.
		/// </returns>
		[Browsable(true), DefaultValue("+")]
		public string ExpandText
		{
			get { return expandText; }
			set
			{
				expandText = value;
				this.Invalidate();
			}
		}

		/// <summary>
		/// Returns or sets the text before the text displayed in this control, indicating ability to close.
		/// </summary>
		/// <value></value>
		/// <returns>
		/// The text associated with this control.
		/// </returns>
		[Browsable(true), DefaultValue("-")]
		public string CloseText
		{
			get { return closeText; }
			set
			{
				closeText = value;
				this.Invalidate();
			}
		}

		/// <summary>
		/// Whether the control's children are visible or not.
		/// </summary>
		/// <value></value>
		/// <returns>
		/// The binary choice of child visibility.
		/// </returns>
		[Browsable(true), DefaultValue(true)]
		public bool Expanded
		{
			get { return expanded; }
			set {
				bool changed = expanded != value;
				expanded = value;
				if (changed)
				{
					foreach (Control c in this.Controls)
						c.Visible = expanded;
					Invalidate();
					OnContentExpansionChanged();
				}
			}
		}

		public delegate void ExpandedBoolean(object sender, bool expanded);
		public event ExpandedBoolean ContentExpansionChanged;

		public void OnContentExpansionChanged() {
			if (ContentExpansionChanged == null)
				return;
			ContentExpansionChanged(this, this.Expanded);
		}

		#endregion  Public Properties

		#region  Private Methods

		private string CurrentText {
			get {
				return string.Format("{0} {1}", new object[] { (expanded) ? closeText : expandText, Text });
			}
		}

		/// <summary>
		/// Cleans up GDI resources.
		/// </summary>
		private void CleanUpResources()
		{
			if (_topPen != null)
				_topPen.Dispose();

			if (_bottomPen != null)
				_bottomPen.Dispose();

			if (_textBrush != null)
				_textBrush.Dispose();
		}

		/// <summary>
		/// Creates GDI resources.
		/// </summary>
		private void CreateResources()
		{
			this.CleanUpResources();

			_topPen = new Pen(_lineColorTop);
			_bottomPen = new Pen(_lineColorBottom);
			_textBrush = new SolidBrush(this.ForeColor);
		}

		#endregion  Private Methods
	}
}
