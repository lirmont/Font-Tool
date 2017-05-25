using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace FontTool
{
	public class ParameterDescription
	{
		private string description = "";
		private Type type = null;
		private Control validatingControl = null;

		public string Description
		{
			get { return description; }
			set { description = value; }
		}

		public Type Type
		{
			get { return type; }
			set { type = value; }
		}

		public Control ValidatingControl
		{
			get { return validatingControl; }
			set { validatingControl = value; }
		}

		public ParameterDescription(string description, Type type, Control validatingControl)
		{
			this.description = description;
			this.type = type;
			this.validatingControl = validatingControl;
		}

		public Control GetNewValidatingControl(object value)
		{
			Type thisType = ValidatingControl.GetType();
			Control control = (Control)Activator.CreateInstance(thisType);
			if (control is MaskedTextBox)
			{
				MaskedTextBox thisControl = (MaskedTextBox)control;
				if (type != typeof(string))
				{
					thisControl.IsNumeric = true;
					if (type == typeof(uint)) {
						thisControl.Floor = 0;
						thisControl.Ceiling = uint.MaxValue;
					}
					else {
						thisControl.Floor = float.MinValue;
						thisControl.Ceiling = float.MaxValue;
					}
				}
				thisControl.EmptyText = description;
				thisControl.Anchor = AnchorStyles.Left | AnchorStyles.Right;
				if (value != null)
					thisControl.Text = string.Format("{0}", value);
			}
			else if (control is ComboBox) {
				if (type == typeof(ColorDirection))
				{
					ComboBox box = (ComboBox)control;
					box.DisplayMember = "Key";
					box.ValueMember = "Value";
					box.Items.Add(new KeyValuePair<string, ColorDirection>("East", ColorDirection.East));
					box.Items.Add(new KeyValuePair<string, ColorDirection>("West", ColorDirection.West));
					box.Items.Add(new KeyValuePair<string, ColorDirection>("North", ColorDirection.North));
					box.Items.Add(new KeyValuePair<string, ColorDirection>("South", ColorDirection.South));
					box.DropDownStyle = ComboBoxStyle.DropDownList;
					int thisValue = (int)value;
					if (thisValue > 0)
						box.SelectedIndex = thisValue - 1;
				}
			}
			else if (control is CheckBox) {
				CheckBox box = (CheckBox)control;
				box.Checked = (bool)value;
			}
			else if (control is Panel)
			{
				Panel thisControl = (Panel)control;
				thisControl.AutoSize = false;
				thisControl.Size = new Size(25, 25);
				thisControl.BorderStyle = BorderStyle.Fixed3D;
				if (value != null)
					if (value is XmlColor)
						thisControl.BackColor = (XmlColor)value;
					else if (value is Color)
						thisControl.BackColor = (Color)value;
			}
			else if (control is NumericUpDown)
			{
				NumericUpDown thisControl = (NumericUpDown)control;
				if (value is int)
					thisControl.Value = (int)value;
				else if (value is decimal)
					thisControl.Value = (decimal)value;
				else
					thisControl.Value = Convert.ToDecimal(value);
			}
			return control;
		}
	}
}
