using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

namespace FontTool
{
	public partial class AddOrEditEffect : Form
	{
		public List<Color> UsedColors { get; set; }

		//
		private BitmapEffect replacementEffect = new BitmapEffect();
		public BitmapEffect BitmapEffect
		{
			get
			{
				return replacementEffect;
			}
			set
			{
				bool changed = (value != replacementEffect) ? true : false;
				replacementEffect = value;
				if (changed)
					synchronizeFormWithBitmapEffect();
			}
		}

		public bool HasEffect {
			get {
				return (BitmapEffect != null) ? true : false;
			}
		}

		public AddOrEditEffect(bool hideTracingOptions = false)
		{
			InitializeComponent();
			//
			scopeComboBox.SelectedIndex = 0;
			//
			effectNameComboBox.Items.Clear();
			effectNameComboBox.DisplayMember = "Key";
			effectNameComboBox.ValueMember = "Value";
			int effectLength = "Effect".Length;
			foreach (Type type in SupportFunctions.GetInheritingTypes(typeof(BitmapEffect)))
			{
				string name = type.Name.Substring(0, type.Name.Length - effectLength);
				KeyValuePair<string, Type> thisEntry = new KeyValuePair<string, Type>(name, type);
				effectNameComboBox.Items.Add(thisEntry);
			}
			// To facilitate colors (which defer tracing), allow hiding of tracing options and settings.
			if (hideTracingOptions)
			{
				traceMethodLabel.Hide();
				traceMethodTableLayoutPanel.Hide();
				traceSettingsTableLayoutPanel.Hide();
			}
		}

		private void synchronizeFormWithBitmapEffect()
		{
			//
			if (HasEffect)
			{
				int? selectedIndex = getIndexOfNameInComboBox(replacementEffect.Name);
				if (selectedIndex != null)
					effectNameComboBox.SelectedIndex = selectedIndex.Value;
				// Show/Hide parameters based on whether there are any.
				parametersTableLayoutPanel.Visible = parametersLabel.Visible = (BitmapEffect.Parameters.Count > 0) ? true : false;
				// Signal re-synch of tracing settings.
				synchronizeTracingWithBitmapEffect();
				// Signal re-synch of scope list view.
				synchronizeScopeWithBitmapEffect();
			}
		}

		private void synchronizeTracingWithBitmapEffect()
		{
			traceSettingsTableLayoutPanel.Enabled = (BitmapEffect.Tracer != null) ? BitmapEffect.Tracer.HasConfiguration : false;
			if (BitmapEffect.Tracer is BitmapTracer)
			{
				BitmapTracer tracer = BitmapEffect.Tracer as BitmapTracer;
				dilationNumericUpDown.Value = tracer.Configuration.Dilation;
				pixelSmoothingCheckBox.Checked = tracer.Configuration.PixelSmoothing;
				lineSmoothingCheckBox.Checked = tracer.Configuration.LineSmoothing;
				lineInterpolationNumericUpDown.Value = (decimal)tracer.Configuration.InterpolateSharpCornersBy;
				imageRadioButton.Checked = true;
				solidifyByNumericUpDown.Value = (decimal)tracer.Configuration.Solidification;
				fillEdgesCheckBox.Checked = tracer.Configuration.FillEdges;
				invertedNormalsCheckBox.Checked = tracer.Configuration.InvertedSolid;
				xAdjustmentNumericUpDown.Value = (decimal)tracer.Configuration.XAdjustment;
				yAdjustmentNumericUpDown.Value = (decimal)tracer.Configuration.YAdjustment;
				zAdjustmentNumericUpDown.Value = (decimal)tracer.Configuration.ZAdjustment;
			}
			else if (BitmapEffect.Tracer is BitmapBoundaryTracer)
				imageBoundaryRadioButton.Checked = true;
			else
				passThroughRadioButton.Checked = true;
		}

		private void synchronizeScopeWithBitmapEffect()
		{
			if (HasEffect && BitmapEffect.Scope == FontTool.BitmapEffect.ScopeType.Specific)
			{
				scopeListView.SuspendLayout();
				{
					scopeListView.Items.Clear();
					foreach (ulong key in BitmapEffect.ApplyToCharacters)
					{
						UnicodeCharacter character = UnicodeBlocks.GetCharacterById(key);
						ListViewItem item = new ListViewItem(new string[] { SupportFunctions.GetUPlusString(character), SupportFunctions.TitleCaseString(character.PrimaryName), character.Block.BlockName });
						item.Tag = character.Id;
						scopeListView.Items.Add(item);
					}
					scopeComboBox.SelectedItem = "Specific";
				}
				scopeListView.ResumeLayout();
			}
			else
				scopeComboBox.SelectedItem = "Generic";
		}

		private int? getIndexOfNameInComboBox(string name)
		{
			uint i = 0;
			foreach (KeyValuePair<string, Type> entry in effectNameComboBox.Items)
			{
				if (entry.Key == name)
					return (int)i;
				i++;
			}
			return null;
		}

		private void saveButton_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
		}

		private void effectNameComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			// Signal resynch of parameters.
			Type type = ((KeyValuePair<string, Type>)effectNameComboBox.SelectedItem).Value;
			BitmapEffect = (HasEffect && BitmapEffect.GetType() == type) ? BitmapEffect : (type != null) ? (BitmapEffect)Activator.CreateInstance(type) : null;
			if (HasEffect)
			{
				TableLayoutPanel replacementParametersTableLayoutPanel = new TableLayoutPanel();
				replacementParametersTableLayoutPanel.Margin = new System.Windows.Forms.Padding(0, 0, 35, 0);
				replacementParametersTableLayoutPanel.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
				replacementParametersTableLayoutPanel.AutoSize = true;
				replacementParametersTableLayoutPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
				replacementParametersTableLayoutPanel.RowCount = BitmapEffect.Parameters.Count;
				for (int i = 0; i < replacementParametersTableLayoutPanel.RowCount; i++)
					replacementParametersTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
				replacementParametersTableLayoutPanel.ColumnCount = 2;
				replacementParametersTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
				replacementParametersTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
				//
				int entry = 0;
				foreach (EffectParameter parameter in BitmapEffect.Parameters)
				{
					Label label = new Label();
					label.Text = string.Format("{0}:", SupportFunctions.TitleCaseString(parameter.Name));
					label.Anchor = AnchorStyles.Right;
					label.AutoSize = true;
					Control valueControl = BitmapEffect.getValidatingControl(parameter.Name, parameter.Value);
					if (valueControl is Panel)
					{
						valueControl.Click += new EventHandler(delegate(object thisSender, EventArgs args) {
							Control control = (Control)thisSender;
							ColorControl.ColorControl colorControl = new ColorControl.ColorControl();
							colorControl.colorListToPalette(UsedColors);
							colorControl.Color = control.BackColor;
							if (DialogResult.OK == colorControl.ShowDialog(this))
							{
								control.BackColor = colorControl.Color;
								XmlColor thisColor = new XmlColor(colorControl.Color);
								control.Text = thisColor.ToString();
							}
							colorControl.Dispose();
						});
						valueControl.TextChanged += new EventHandler(delegate(object thisSender, EventArgs args)
						{
							Control control = (Control)thisSender;
							string parameterName = (string)control.Tag;
							// Type Coerce
							EffectParameter thisParameter = BitmapEffect.Parameters[parameterName];
							Type typeOfParameter = thisParameter.Value.GetType();
							object thisValue = (thisParameter.Value is XmlColor || thisParameter.Value is System.Drawing.Color) ? new XmlColor(control.Text) : Convert.ChangeType(control.Text, typeOfParameter);
							BitmapEffect.Parameters[parameterName].Value = thisValue;
						});
					}
					else if (valueControl is ComboBox) {
						ComboBox box = (ComboBox)valueControl;
						box.SelectedIndexChanged += new EventHandler(delegate(object thisSender, EventArgs args) {
							ComboBox thisBox = (ComboBox)thisSender;
							string parameterName = (string)thisBox.Tag;
							// Type Coerce
							EffectParameter thisParameter = BitmapEffect.Parameters[parameterName];
							Type typeOfParameter = thisParameter.Value.GetType();
							if (thisBox.Items[thisBox.SelectedIndex] is KeyValuePair<string, ColorDirection>)
							{
								KeyValuePair<string, ColorDirection> obj = (KeyValuePair<string, ColorDirection>)thisBox.Items[thisBox.SelectedIndex];
								BitmapEffect.Parameters[parameterName].Value = obj.Value;
							}
						});
					}
					else if (valueControl is CheckBox) {
						CheckBox box = (CheckBox)valueControl;
						box.CheckedChanged += new EventHandler(delegate(object thisSender, EventArgs args) {
							CheckBox thisBox = (CheckBox)thisSender;
							string parameterName = (string)thisBox.Tag;
							// Type Coerce
							EffectParameter thisParameter = BitmapEffect.Parameters[parameterName];
							thisParameter.Value = (bool)thisBox.Checked;
						});
					}
					else
					{
						valueControl.TextChanged += new EventHandler(delegate(object thisSender, EventArgs args)
						{
							Control control = (Control)thisSender;
							string parameterName = (string)control.Tag;
							// Type Coerce
							EffectParameter thisParameter = BitmapEffect.Parameters[parameterName];
							Type typeOfParameter = thisParameter.Value.GetType();
							object thisValue = Convert.ChangeType(control.Text, typeOfParameter);
							BitmapEffect.Parameters[parameterName].Value = thisValue;
						});
					}
					//
					replacementParametersTableLayoutPanel.Controls.Add(label, 0, entry);
					replacementParametersTableLayoutPanel.Controls.Add(valueControl, 1, entry);
					//
					entry++;
				}
				parametersTableLayoutPanel.Dispose();
				//
				parametersPanel.Controls.Add(replacementParametersTableLayoutPanel);
				replacementParametersTableLayoutPanel.Location = new Point(0, 0);
				parametersTableLayoutPanel = replacementParametersTableLayoutPanel;
				parametersLabel.Visible = true;
				parametersPanel.Visible = true;
				parametersPanel.AutoScrollMinSize = new Size(0, replacementParametersTableLayoutPanel.Height);
			}
			else
				parametersTableLayoutPanel.Dispose();
		}

		private void scopeComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			ComboBox control = (ComboBox)sender;
			scopeListView.Enabled = (control != null && (string)control.SelectedItem == "Specific") ? true : false;
		}

		private void scopeListViewContextMenuStrip_Opening(object sender, CancelEventArgs e)
		{
			removeCharacterToolStripMenuItem.Enabled = (scopeListView.SelectedIndices.Count > 0) ? true : false;
		}

		string lastUsedBlock = null;
		private void addCharacterToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Application.DoEvents();
			//
			AddUnicodeCharacter dialog = new AddUnicodeCharacter(characterCodesAlreadyInScope: BitmapEffect.ApplyToCharacters);
			if (lastUsedBlock != null)
				dialog.UnicodeBlock = lastUsedBlock;
			if (DialogResult.OK == dialog.ShowDialog())
			{
				lastUsedBlock = dialog.UnicodeBlock;
				UlongCollection characters = dialog.CharacterCodes;
				foreach (ulong item in characters)
					BitmapEffect.ApplyToCharacters.Add(item);
				//
				synchronizeFormWithBitmapEffect();
			}
			dialog.Dispose();
		}

		private void removeCharacterToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Application.DoEvents();
			//
			if (scopeListView.SelectedIndices.Count > 0)
			{
				if (BitmapEffect != null && BitmapEffect.Scope == FontTool.BitmapEffect.ScopeType.Specific)
				{
					//
					System.Windows.Forms.ListView.SelectedListViewItemCollection items = scopeListView.SelectedItems;
					foreach (ListViewItem item in items)
					{
						BitmapEffect.ApplyToCharacters.Remove((ulong)item.Tag);
						scopeListView.Items.Remove(item);
					}
				}
			}
		}

		private void imageBoundaryRadioButton_CheckedChanged(object sender, EventArgs e)
		{
			RadioButton button = sender as RadioButton;
			if (button != null && button.Checked)
			{
				traceSettingsTableLayoutPanel.Enabled = false;
				BitmapEffect.Tracer = new BitmapBoundaryTracer();
			}
		}

		private void passThroughRadioButton_CheckedChanged(object sender, EventArgs e)
		{
			RadioButton button = sender as RadioButton;
			if (button != null && button.Checked)
			{
				traceSettingsTableLayoutPanel.Enabled = false;
				BitmapEffect.Tracer = null;
			}
		}

		private void imageRadioButton_CheckedChanged(object sender, EventArgs e)
		{
			RadioButton button = sender as RadioButton;
			if (button != null && button.Checked)
			{
				traceSettingsTableLayoutPanel.Enabled = true;
				AutoTrace.TraceConfiguration configuration = new AutoTrace.TraceConfiguration((int)dilationNumericUpDown.Value, smoothing: lineSmoothingCheckBox.Checked, pixelSmoothing: pixelSmoothingCheckBox.Checked, interpolateSharpCornersBy: (double)lineInterpolationNumericUpDown.Value, fillEdges: fillEdgesCheckBox.Checked);
				BitmapEffect.Tracer = new BitmapTracer(configuration);
			}
		}

		private void dilationNumericUpDown_ValueChanged(object sender, EventArgs e)
		{
			if (BitmapEffect.Tracer is BitmapTracer)
			{
				BitmapTracer tracer = BitmapEffect.Tracer as BitmapTracer;
				tracer.Configuration.Dilation = (int)dilationNumericUpDown.Value;
			}
		}

		private void lineInterpolationNumericUpDown_ValueChanged(object sender, EventArgs e)
		{
			if (BitmapEffect.Tracer is BitmapTracer)
			{
				BitmapTracer tracer = BitmapEffect.Tracer as BitmapTracer;
				tracer.Configuration.InterpolateSharpCornersBy = (double)lineInterpolationNumericUpDown.Value;
			}
		}

		private void pixelSmoothingCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			if (BitmapEffect.Tracer is BitmapTracer)
			{
				BitmapTracer tracer = BitmapEffect.Tracer as BitmapTracer;
				tracer.Configuration.PixelSmoothing = pixelSmoothingCheckBox.Checked;
			}
		}

		private void lineSmoothingCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			if (BitmapEffect.Tracer is BitmapTracer)
			{
				BitmapTracer tracer = BitmapEffect.Tracer as BitmapTracer;
				tracer.Configuration.LineSmoothing = lineSmoothingCheckBox.Checked;
			}
		}

		private void solidifyByNumericUpDown_ValueChanged(object sender, EventArgs e)
		{
			if (BitmapEffect.Tracer is BitmapTracer)
			{
				BitmapTracer tracer = BitmapEffect.Tracer as BitmapTracer;
				tracer.Configuration.Solidification = (double)solidifyByNumericUpDown.Value;
			}
		}

		private void invertedNormalsCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			if (BitmapEffect.Tracer is BitmapTracer)
			{
				BitmapTracer tracer = BitmapEffect.Tracer as BitmapTracer;
				tracer.Configuration.InvertedSolid = invertedNormalsCheckBox.Checked;
			}
		}

		private void xNumericUpDown_ValueChanged(object sender, EventArgs e)
		{
			if (BitmapEffect.Tracer is BitmapTracer)
			{
				BitmapTracer tracer = BitmapEffect.Tracer as BitmapTracer;
				tracer.Configuration.XAdjustment = (double)xAdjustmentNumericUpDown.Value;
			}
		}

		private void yNumericUpDown_ValueChanged(object sender, EventArgs e)
		{
			if (BitmapEffect.Tracer is BitmapTracer)
			{
				BitmapTracer tracer = BitmapEffect.Tracer as BitmapTracer;
				tracer.Configuration.YAdjustment = (double)yAdjustmentNumericUpDown.Value;
			}
		}

		private void zAdjustmentNumericUpDown_ValueChanged(object sender, EventArgs e)
		{
			if (BitmapEffect.Tracer is BitmapTracer)
			{
				BitmapTracer tracer = BitmapEffect.Tracer as BitmapTracer;
				tracer.Configuration.ZAdjustment = (double)zAdjustmentNumericUpDown.Value;
			}
		}

		private void fillEdgesCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			if (BitmapEffect.Tracer is BitmapTracer)
			{
				BitmapTracer tracer = BitmapEffect.Tracer as BitmapTracer;
				tracer.Configuration.FillEdges = fillEdgesCheckBox.Checked;
			}
		}
	}
}
