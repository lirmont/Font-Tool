using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace FontTool
{
	public partial class UnicodeCoverageReportViewer : Form
	{
		private List<ulong> characters = new List<ulong>();

		public UnicodeCoverageReportViewer(List<ulong> characters = null)
		{
			InitializeComponent();
			this.characters = characters;
		}

		private void UnicodeCoverageReportViewer_Shown(object sender, EventArgs e)
		{
			Application.DoEvents();
			//
			Thread doTerritories = new Thread(new ThreadStart(delegate()
			{
				SetStatus("Retrieving world territories.", this);
				//
				//
				CLDR.LocaleDataMarkupLanguage.ldml en = SupportFunctions.GetLanguageScriptTerritory("en");
				Dictionary<string, CLDR.LocaleDataMarkupLanguage.territory> territoriesInEnglish = en.LocaleDisplayNames.Territories;
				CLDR.SupplementalDataLocaleDataMarkupLanguage.supplementalData data = SupportFunctions.GetSupplementalData();
				if (data != null)
				{
					TableLayoutPanel table = GetTableLayoutPanel();
					//
					Dictionary<string, CLDR.SupplementalDataLocaleDataMarkupLanguage.group> dictionary = new Dictionary<string, CLDR.SupplementalDataLocaleDataMarkupLanguage.group>();
					dictionary.Add("001", data.FindTerritoryContainment("001"));
					int row = 0;
					foreach (string key in dictionary.Keys)
					{
						ComboBox activeComboBox = null;
						SupportFunctions.RecursiveTerritoryContainment(territoriesInEnglish, data, key, handler: (Action<object[]>)delegate(object[] objects) {
							int depth = (int)objects[0];
							// Ignore printing "World".
							if (depth > 0)
							{
								string type = objects[1] as string;
								CLDR.SupplementalDataLocaleDataMarkupLanguage.group group = (CLDR.SupplementalDataLocaleDataMarkupLanguage.group)objects[2];
								string name = territoriesInEnglish[type].Value;
								//
								if (group != null)
								{
									// Add section.
									CollapsingGroupBox box = new CollapsingGroupBox();
									box.Text = name;
									box.Margin = new Padding(12 * depth, 3, 10, 3);
									box.Anchor = AnchorStyles.Left | AnchorStyles.Right;
									box.AutoSize = true;
									box.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
									box.Tag = depth;
									box.Expanded = false;
									if (depth > 1)
										box.Hide();
									box.ContentExpansionChanged += new CollapsingGroupBox.ExpandedBoolean(box_ContentExpansionChanged);
									table.Controls.Add(box, 0, row++);
									//
									table.RowStyles.Add(new RowStyle(SizeType.AutoSize));
									//Console.WriteLine(new string('\t', depth) + "" + name);
									activeComboBox = null;
								}
								else
								{
									//Console.WriteLine(new string('\t', depth) + "" + name + ": " + string.Format("{0:#,0}", int.Parse(data.FindTerritoryInfo(type).population)));
									if (activeComboBox == null)
									{
										activeComboBox = new ComboBox();
										activeComboBox.DisplayMember = "Key";
										activeComboBox.ValueMember = "Value";
										activeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
										activeComboBox.Margin = new Padding(12 * depth, 3, 10, 3);
										activeComboBox.SelectedIndexChanged += new EventHandler(activeComboBox_SelectedIndexChanged);
										table.Controls.Add(activeComboBox, 0, row++);
										activeComboBox.Hide();
										//
										table.RowStyles.Add(new RowStyle(SizeType.AutoSize));
										//
										Info info = new Info();
										info.Margin = new Padding(12 * depth, 3, 20, 3);
										info.Anchor = AnchorStyles.Left | AnchorStyles.Right;
										info.AutoSize = true;
										info.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
										info.Tag = activeComboBox;
										table.Controls.Add(info, 0, row++);
										table.RowStyles.Add(new RowStyle(SizeType.AutoSize));
										info.Hide();
										//
										activeComboBox.Tag = info;
									}
									activeComboBox.Items.Add(new KeyValuePair<string, string>(name, type));
									Label testLabel = new Label();
									testLabel.AutoSize = true;
									testLabel.Text = name;
									int testWidth = testLabel.PreferredWidth + 30;
									if (testWidth > activeComboBox.PreferredSize.Width)
										activeComboBox.Size = new System.Drawing.Size(testWidth, activeComboBox.PreferredHeight);
									testLabel.Dispose();
								}
							}
						});
					}
					// Add one more row to act as a buffer.
					table.Controls.Add(new Label(), 0, row++);
					table.RowCount = row;
					ReplaceControl(table, languagesTableLayoutPanel, this);
				}
				//
				SetStatus("Ready.", this);
			}));
			doTerritories.Name = "Do Territories";
			//
			Thread doScriptCoverage = new Thread(new ThreadStart(delegate()
			{
				SetStatus("Calculating scripts covered by this font.", this);
				SupportFunctions.ProgressUpdater updater = delegate(string s)
				{
					SetStatus(s, this);
				};
				TableLayoutPanel panel = DoScriptCoverage(characters, handler: updater);
				ReplaceControl(panel, scriptsTableLayoutPanel, this);
				//
				doTerritories.Start();
			}));
			doScriptCoverage.Name = "Do Unicode Coverage";
			//
			Thread doBlockCoverage = new Thread(new ThreadStart(delegate()
			{
				SetStatus("Calculating blocks covered by this font.", this);
				//
				List<string> blockNames = new List<string>();
				List<ulong> characterWorkspace = new List<ulong>();
				characterWorkspace.AddRange(characters);
				foreach (UnicodeBlock block in UnicodeBlocks.Blocks) {
					foreach (UnicodeCharacter character in block.Characters) {
						bool characterSupported = characterWorkspace.Contains(character.Id);
						if (characterSupported)
						{
							blockNames.Add(block.BlockName);
							// Set it.
							SetUnicodeBlocksCount(string.Format("{0}", blockNames.Count), this);
							// Remove everything below.
							ulong lastCharacter = block.Characters[block.Characters.Count - 1].Id;
							int lastIndex = characterWorkspace.FindLastIndex(item => item <= lastCharacter);
							characterWorkspace.RemoveRange(0, lastIndex + 1);
							// Break back to parent loop.
							break;
						}
					}
				}
				//
				SetEnabledness(sectionCountValueLabel, true, this);
				//
				doScriptCoverage.Start();
			}));
			doBlockCoverage.Name = "Do Unicode Coverage";
			doBlockCoverage.Start();
		}

		void activeComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			ComboBox box = (ComboBox)sender;
			Info display = (Info)box.Tag;
			var item = (KeyValuePair<string, string>)box.SelectedItem;
			display.Type = item.Value;
		}

		void box_ContentExpansionChanged(object sender, bool expanded)
		{
			CollapsingGroupBox box = (CollapsingGroupBox)sender;
			int depth = (int)box.Tag;
			int deltaDepth = 0;
			int row = 0;
			TableLayoutPanel panel = null;
			List<Type> ignore = new List<Type> { typeof(ComboBox) };
			Stack<bool> expansion = new Stack<bool>();
			foreach (Control control in box.Parent.Controls)
			{
				if (control == box)
				{
					panel = (TableLayoutPanel)box.Parent;
					row = panel.GetRow(control);
					expansion.Push(expanded);
					//
					for (int i = row + 1; i < panel.RowCount; i++)
					{
						Control thisControl = SupportFunctions.GetAnyControlAt(panel, 0, i);
						// If this control is a CollapsingGroupBox, only continue if it's depth is greater.
						if (thisControl is CollapsingGroupBox)
						{
							int thisDepth = (int)thisControl.Tag;
							if (thisDepth <= depth)
								return;
							CollapsingGroupBox otherBox = (CollapsingGroupBox)thisControl;
							int oldDelta = deltaDepth;
							deltaDepth = thisDepth - depth;
							if (oldDelta >= deltaDepth)
								expansion.Pop();
						}
						// Hide/show the control using current top of the stack's display value.
						ToggleRowVisibility(i, visible: expansion.Peek(), panel: panel, mode: SizeType.AutoSize);
						// Auto-select first item if it's a the display.
						if (expansion.Peek() && thisControl is Info)
						{
							Info thisDisplay = (Info)thisControl;
							ComboBox thisComboBox = (ComboBox)thisDisplay.Tag;
							if (thisComboBox.SelectedIndex == -1)
								thisComboBox.SelectedIndex = 0;
						}
						// If this control is a CollapsingGroupBox, push its expansion.
						if (thisControl is CollapsingGroupBox)
						{
							CollapsingGroupBox otherBox = (CollapsingGroupBox)thisControl;
							expansion.Push(otherBox.Expanded);
						}
					}
				}
			}
		}

		private void ReplaceControl(Control c, Control old, UnicodeCoverageReportViewer instance)
		{
			if (instance.IsHandleCreated)
			{
				instance.Invoke((MethodInvoker)delegate
				{
					Control parent = old.Parent;
					if (parent is TableLayoutPanel)
					{
						TableLayoutPanel thisLayout = parent as TableLayoutPanel;
						int row = thisLayout.GetRow(old);
						int col = thisLayout.GetColumn(old);
						int rowSpan = thisLayout.GetRowSpan(old);
						int columnSpan = thisLayout.GetColumnSpan(old);
						bool visible = old.Visible;
						old.Dispose();
						c.Visible = visible;
						thisLayout.Controls.Add(c, col, row);
						thisLayout.SetRowSpan(c, rowSpan);
						thisLayout.SetColumnSpan(c, columnSpan);
					}
					else
					{
						bool visible = old.Visible;
						int x = old.Location.X;
						int y = old.Location.Y;
						Size size = old.Size;
						old.Dispose();
						parent.Controls.Add(c);
						c.Location.Offset(x, y);
						c.Size = size;
						c.Visible = visible;
					}
					old = c;
				});
			}
		}

		private void SetEnabledness(Control c, bool enabled, UnicodeCoverageReportViewer instance)
		{
			if (instance.IsHandleCreated)
			{
				instance.Invoke((MethodInvoker)delegate
				{
					c.Enabled = enabled;
				});
			}
		}

		private void SetStatus(string s, UnicodeCoverageReportViewer instance)
		{
			if (instance.IsHandleCreated) {
				instance.Invoke((MethodInvoker)delegate {
					instance.toolStripStatusLabel.Text = s;
				});
			}
		}

		private void SetUnicodeBlocksCount(string s, UnicodeCoverageReportViewer instance)
		{
			if (instance.IsHandleCreated)
			{
				instance.Invoke((MethodInvoker)delegate
				{
					instance.sectionCountValueLabel.Text = s;
				});
			}
		}

		private void sectionCountValueLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			UnicodeBlockCoverage dlg = new UnicodeBlockCoverage(characters);
			dlg.Show();
		}

		class CoverageRecord {
			private string name;

			private int totalCount;
			private int missingCount;
			private int haveCount;
			private double coverage;
			private List<ulong> missingList;

			public string Name
			{
				get { return name; }
				set { name = value; }
			}

			public int TotalCount
			{
				get { return totalCount;}
				set { totalCount = value;}
			}

			public int MissingCount
			{
				get { return missingCount;}
				set { missingCount = value;}
			}

			public int HaveCount
			{
				get { return haveCount;}
				set { haveCount = value;}
			}

			public double Coverage
			{
				get { return coverage;}
				set { coverage = value;}
			}

			public List<ulong> MissingList
			{
				get { return missingList;}
				set { missingList = value;}
			}

			public CoverageRecord(string name, int haveCount, int missingCount, int totalCount, double coverage, List<ulong> missingList)
			{
				this.name = name;
				this.haveCount = haveCount;
				this.missingCount = missingCount;
				this.totalCount = totalCount;
				this.coverage = coverage;
				this.missingList = missingList;
			}
		}

		private static TableLayoutPanel DoScriptCoverage(List<ulong> characters, SupportFunctions.ProgressUpdater handler = null)
		{
			//
			List<KeyValuePair<UnicodeScript, CoverageRecord>> coverageList = new List<KeyValuePair<UnicodeScript, CoverageRecord>>();
			int scriptIndex = 0;
			int scriptCount = UnicodeScripts.Scripts.Count;
			foreach (UnicodeScript script in UnicodeScripts.Scripts)
			{
				scriptIndex++;
				if (handler != null)
					handler(string.Format("Calculating scripts covered by this font: {0} ({1}/{2}).", new object[] { script.Name, scriptIndex, scriptCount }));
				//
				List<ulong> missingList = script.GetMissingCharacters(characters);
				string formattedName = (script.Name.Contains("_")) ? script.Name.Replace("_", " ") : script.Name;
				int missingCount = missingList.Count;
				int totalCount = script.PrintableCount;
				int haveCount = totalCount - missingCount;
				double coverage = (totalCount > 0) ? 100.0 * Math.Min(1, (totalCount - missingCount) / (double)totalCount) : 0;
				CoverageRecord record = new CoverageRecord(formattedName, haveCount, missingCount, totalCount, coverage, missingList);
				coverageList.Add(new KeyValuePair<UnicodeScript, CoverageRecord>(script, record));
			}
			//
			coverageList.Sort((itemA, itemB) => itemB.Value.Coverage.CompareTo(itemA.Value.Coverage));
			//
			TableLayoutPanel panel = GetTableLayoutPanel(headers: new List<string> { "", "Name", "Have", "Missing", "Total", "% Covered" });
			//
			if (handler != null)
				handler("Organizing scripts into a chart.");
			int row = 1;
			foreach (KeyValuePair<UnicodeScript, CoverageRecord> pair in coverageList)
			{
				UnicodeScript script = pair.Key;
				CoverageRecord record = pair.Value;
				//
				if (handler != null)
					handler(string.Format("Organizing scripts into a chart: {0} ({1}/{2})", new object[] { record.Name, row, scriptCount }));
				//
				Label number = new Label();
				number.AutoSize = true;
				number.Text = string.Format("{0}.", row);
				number.Anchor = AnchorStyles.Right;
				number.TextAlign = ContentAlignment.MiddleRight;
				//
				Label scriptName = new Label();
				scriptName.AutoSize = true;
				scriptName.Text = record.Name;
				scriptName.Anchor = AnchorStyles.Left;
				//
				LinkLabel have = new LinkLabel();
				have.Text = record.HaveCount.ToString();
				have.Anchor = AnchorStyles.Left;
				have.AutoSize = true;
				//
				LinkLabel missing = new LinkLabel();
				missing.Text = record.MissingCount.ToString();
				missing.Anchor = AnchorStyles.Left;
				missing.AutoSize = true;
				//
				LinkLabel total = new LinkLabel();
				total.Text = record.TotalCount.ToString();
				total.Anchor = AnchorStyles.Left;
				total.AutoSize = true;
				//
				LinkLabel percentComplete = new LinkLabel();
				percentComplete.Text = string.Format("{0:0.##}%", record.Coverage);
				percentComplete.Anchor = AnchorStyles.Left;
				percentComplete.AutoSize = true;
				//
				panel.Controls.Add(number, 0, row);
				panel.Controls.Add(scriptName, 1, row);
				panel.Controls.Add(have, 2, row);
				panel.Controls.Add(missing, 3, row);
				panel.Controls.Add(total, 4, row);
				panel.Controls.Add(percentComplete, 5, row);
				//
				row++;
			}
			return panel;
		}

		private static TableLayoutPanel GetTableLayoutPanel(List<string> headers = null)
		{
			TableLayoutPanel panel = new TableLayoutPanel();
			panel.AutoSize = false;
			panel.AutoScroll = true;
			panel.BackColor = SystemColors.Control;
			panel.Margin = new Padding(20, 0, 0, 0);
			panel.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Top;
			if (headers != null) {
				int column = 0;
				foreach (string header in headers) {
					Label label = new Label();
					label.Text = header;
					label.AutoSize = true;
					Font f = new Font(label.Font, FontStyle.Bold);
					label.Font = f;
					label.Anchor = AnchorStyles.Left;
					panel.Controls.Add(label, column, 0);
					//
					ColumnStyle style = new ColumnStyle();
					style.SizeType = (column > 0) ? SizeType.AutoSize : SizeType.Percent;
					panel.ColumnStyles.Add(style);
					//
					column++;
				}
			}
			return panel;
		}

		private void scriptsCollapsingGroupBox_ContentExpansionChanged(object sender, bool expanded)
		{
			ToggleRowVisibility(2, visible: expanded);
		}

		private void languagesCollapsingGroupBox_ContentExpansionChanged(object sender, bool expanded)
		{
			ToggleRowVisibility(4, visible: expanded);
		}

		private void ToggleRowVisibility(int row, bool visible = true, TableLayoutPanel panel = null, SizeType mode = SizeType.Percent, List<Type> ignoreList = null)
		{
			panel = (panel != null) ? panel : tableLayoutPanel;
			if (ignoreList == null)
			{
				foreach (Control ctl in panel.Controls)
					if (panel.GetRow(ctl) == row) ctl.Visible = visible;
			}
			else
			{
				foreach (Control ctl in panel.Controls)
					if (panel.GetRow(ctl) == row && !ignoreList.Contains(ctl.GetType())) ctl.Visible = visible;
			}
			//
			if (panel.RowStyles.Count > row)
				panel.RowStyles[row].SizeType = mode;
			if (mode == SizeType.Percent)
				panel.RowStyles[row].Height = (visible) ? 50.0f : 0.0f;
		}
	}
}
