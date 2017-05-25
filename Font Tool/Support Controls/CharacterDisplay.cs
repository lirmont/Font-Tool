using System;
using System.Windows.Forms;
using System.ComponentModel;

namespace FontTool
{
	public partial class CharacterDisplay : UserControl
	{
		public bool Shown {
			get {
				return characterViewer.Shown;
			}
		}

		public System.Drawing.Size ViewerSize {
			get { return new System.Drawing.Size(characterViewer.Size.Width - 2, characterViewer.Size.Height - 2); }
		}

		public float GutterY {
			get { return characterViewer.GutterY; }
		}

		//
		public enum Mode { Initial = 0, BaseImage = 1, EffectedImage = 2 };
		private Mode activeMode = Mode.BaseImage;

		[Browsable(false)] //disable design time support
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] //disable serialization
		public Mode ActiveMode {
			get {
				return activeMode;
			}
			set
			{
				Mode previous = activeMode;
				this.activeMode = value;
				if (previous != value)
				{
					bool initialSuppressionState = suppressSaveEvent;
					suppressSaveEvent = true;
					{
						if (value == Mode.BaseImage)
						{
							effectedImageModeButton.Checked = false;
							baseImageModeButton.Checked = true;
							OnRequestBaseImage(this, null);
						}
						else if (value == Mode.EffectedImage)
						{
							baseImageModeButton.Checked = false;
							effectedImageModeButton.Checked = true;
							OnRequestEffectedImage(this, null);
						}
						else
						{

						}
					}
					suppressSaveEvent = initialSuppressionState;
				}
			}
		}
		
		//
		private float descent = 4f;
		private float advances = 0f;
		private float ascends = 0f;
		private float characterWidth = 6f;
		private DerivedCharacter character = null;
		private ImageDescription imageDescription = null;

		// Character Viewer
		public OpenGLConfiguration OpenGLConfiguration
		{
			get { return characterViewer.oglConfiguration; }
		}

		public float ZoomFactor
		{
			get { return characterViewer.ZoomFactor; }
			set { characterViewer.ZoomFactor = value; }
		}

		public float Descent
		{
			get { return descent; }
			set
			{
				descent = Math.Max(0, value);
				int thisValue = (int)(Math.Ceiling(descent / 5.0) * 5.0);
				characterViewer.GutterY = thisValue;
			}
		}

		public float MaximumAscension
		{
			get { return characterViewer.MaximumAscension; }
			set { characterViewer.MaximumAscension = value; }
		}

		// Character Display
		public float Advances
		{
			get { return advances; }
			set {
				advances = value;
				characterViewer.Advances = value;
				if (advanceNumericUpDown.Value != (decimal)value)
					advanceNumericUpDown.Value = (decimal)value;
				triggerSaveOpportunity();
			}
		}

		public float Ascends
		{
			get { return ascends; }
			set {
				ascends = value;
				characterViewer.Ascends = value;
				if (ascendNumericUpDown.Value != (decimal)value)
					ascendNumericUpDown.Value = (decimal)value;
				triggerSaveOpportunity();
			}
		}

		public float CharacterWidth
		{
			get { return characterWidth; }
			set {
				characterWidth = value;
				characterViewer.CharacterWidth = value;
				if (widthNumericUpDown.Value != (decimal)value)
					widthNumericUpDown.Value = (decimal)value;
				triggerSaveOpportunity();
			}
		}

		public string UnicodeCode
		{
			get { return unicodeValueLabel.Text; }
			set { unicodeValueLabel.Text = value; }
		}

		[Browsable(false)] //disable design time support
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] //disable serialization
		public DerivedCharacter Character
		{
			get
			{
				return character;
			}
			set
			{
				character = value;
				bool previousValue = suppressSaveEvent;
				suppressSaveEvent = true;
				{
					if (character != null)
					{
						UnicodeCode = SupportFunctions.GetUPlusString(character);
						// Set adjustments to be the final observed adjustments.
						CharacterWidth = character.FinalOffset.OffsetWidth;
						Ascends = character.FinalOffset.OffsetY;
						Advances = character.FinalOffset.OffsetX;
					}
					else {
						UnicodeCode = "";
						CharacterWidth = 0;
						Ascends = 0;
						Advances = 0;
					}
				}
				suppressSaveEvent = previousValue;
			}
		}

		[Browsable(false)] //disable design time support
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] //disable serialization
		public ImageDescription ImageDescription
		{
			get { return imageDescription; }
			set {
				imageDescription = value;
				characterViewer.ImageDescription = (Character != null) ? imageDescription : null;
			}
		}

		private bool suppressSaveEvent = true;
		public bool SuppressSaveEvent
		{
			get { return suppressSaveEvent; }
			set { suppressSaveEvent = value; }
		}

		public delegate void CharacterComponentsChangedHandler(object sender, CharacterComponentsChangedEventArgs e);
		public event CharacterComponentsChangedHandler CharacterComponentsChanged;

		protected void OnCharacterComponentsChanged(object sender, CharacterComponentsChangedEventArgs e)
		{
			// Make sure there is a listener.
			if (SuppressSaveEvent || CharacterComponentsChanged == null) return;
			CharacterComponentsChanged(this, e);
		}

		private void triggerSaveOpportunity()
		{
			if (character != null && !suppressSaveEvent)
			{
				// Determine the magnitude of changes. The change is applied to the base character. NOTE: This will likely only be one component per call.
				float deltaWidth = CharacterWidth - character.FinalOffset.OffsetWidth;
				float deltaAdv = Advances - character.FinalOffset.OffsetX;
				float deltaAsc = Ascends - character.FinalOffset.OffsetY;
				//
				OnCharacterComponentsChanged(this,
					new CharacterComponentsChangedEventArgs(
					// TODO: Properly store base character (currently: stores to final offset).
						new DerivedCharacter(
							new Character(
								UnicodeBlocks.GetCharacterById(character.Id),
								character.BaseCharacter.OffsetWidth + deltaWidth,
								character.BaseCharacter.OffsetX + deltaAdv,
								character.BaseCharacter.OffsetY + deltaAsc,
								character.BaseCharacter.ScaleX,
								character.BaseCharacter.ScaleY
							),
							// This value is static.
							character.Delta
						)
					)
				);
			}
		}

		public CharacterDisplay()
		{
			InitializeComponent();
		}

		private void widthNumericUpDown_ValueChanged(object sender, EventArgs e)
		{
			NumericUpDown control = (NumericUpDown)sender;
			CharacterWidth = (float)control.Value;
		}

		private void ascendNumericUpDown_ValueChanged(object sender, EventArgs e)
		{
			NumericUpDown control = (NumericUpDown)sender;
			Ascends = (float)control.Value;
		}

		private void advanceNumericUpDown_ValueChanged(object sender, EventArgs e)
		{
			NumericUpDown control = (NumericUpDown)sender;
			Advances = (float)control.Value;
		}

		public delegate void RequestBaseImageHandler (object sender, EventArgs data);

		public event RequestBaseImageHandler RequestBaseImage;

		public delegate void RequestEffectedImageHandler(object sender, EventArgs data);

		public event RequestEffectedImageHandler RequestEffectedImage;

		protected void OnRequestBaseImage(object sender, EventArgs data)
		{
			if (RequestBaseImage != null)
				RequestBaseImage(this, data);
		}

		protected void OnRequestEffectedImage(object sender, EventArgs data)
		{
			if (RequestEffectedImage != null)
				RequestEffectedImage(this, data);
		}

		private void baseImageModeButton_CheckedChanged(object sender, EventArgs e)
		{
			if (baseImageModeButton.Checked)
				ActiveMode = Mode.BaseImage;
		}

		private void effectedImageModeButton_CheckedChanged(object sender, EventArgs e)
		{
			if (effectedImageModeButton.Checked)
				ActiveMode = Mode.EffectedImage;
		}

		public class CharacterComponentsChangedEventArgs : EventArgs
		{
			public DerivedCharacter Character { get; set; }

			public CharacterComponentsChangedEventArgs(DerivedCharacter character)
			{
				Character = character;
			}
		}

		private void colorPanel_Click(object sender, EventArgs e)
		{
			ColorControl.ColorControl dialog = new ColorControl.ColorControl();
			dialog.Color = colorPanel.BackColor;
			if (DialogResult.OK == dialog.ShowDialog())
			{
				colorPanel.BackColor = dialog.Color;
			}
			dialog.Dispose();
		}

		private void characterViewer_Enter(object sender, EventArgs e)
		{
			characterViewer.glControl_Enter(sender, e);
		}

		private void CharacterDisplay_Enter(object sender, EventArgs e)
		{
			characterViewer_Enter(this, e);
		}
	}
}
