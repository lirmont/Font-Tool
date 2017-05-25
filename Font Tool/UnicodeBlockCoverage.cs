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
	public partial class UnicodeBlockCoverage : Form
	{
		private List<ulong> characters = new List<ulong>();
		private List<UnicodeBlock> unicodeBlocks = new List<UnicodeBlock>();
		private List<KeyValuePair<string, double>> coveragePairs = null;

		public UnicodeBlockCoverage(List<ulong> characters)
		{
			this.characters = characters;
			//
			InitializeComponent();
		}

		private void SetStatus(string s, UnicodeBlockCoverage instance)
		{
			if (instance.IsHandleCreated)
			{
				instance.Invoke((MethodInvoker)delegate
				{
					instance.toolStripStatusLabel.Text = s;
				});
			}
		}

		private void SetEnabledness(Control c, bool enabled, UnicodeBlockCoverage instance)
		{
			if (instance.IsHandleCreated)
			{
				instance.Invoke((MethodInvoker)delegate
				{
					c.Enabled = enabled;
				});
			}
		}

		private void SetText(Control c, string s, UnicodeBlockCoverage instance)
		{
			if (instance.IsHandleCreated)
			{
				instance.Invoke((MethodInvoker)delegate
				{
					c.Text = s;
				});
			}
		}

		private void UnicodeBlockCoverage_Shown(object sender, EventArgs e)
		{
			doInitialLoad();
		}

		private void doInitialLoad()
		{
			//
			Thread doBlockCoverage = new Thread(new ThreadStart(delegate()
			{
				SetStatus("Calculating blocks covered by this font.", this);
				//
				List<ulong> characterWorkspace = new List<ulong>();
				characterWorkspace.AddRange(characters);
				foreach (UnicodeBlock block in UnicodeBlocks.Blocks)
				{
					foreach (UnicodeCharacter character in block.Characters)
					{
						bool characterSupported = characterWorkspace.Contains(character.Id);
						if (characterSupported)
						{
							unicodeBlocks.Add(block);
							// Remove everything below.
							ulong lastCharacter = block.Characters[block.Characters.Count - 1].Id;
							int lastIndex = characterWorkspace.FindLastIndex(item => item <= lastCharacter);
							characterWorkspace.RemoveRange(0, lastIndex + 1);
							// Break back to parent loop.
							break;
						}
					}
				}
				recalculateText();
				//
				SetStatus("Ready.", this);
			}));
			doBlockCoverage.Name = "Do Unicode Coverage";
			doBlockCoverage.Start();
		}

		private void recalculateText()
		{
			//
			Thread doBlockCoverage = new Thread(new ThreadStart(delegate()
			{
				SetStatus("Recalculating text.", this);
				//
				SetEnabledness(surroundWithQuotesCheckBox, false, this);
				SetEnabledness(includeNewLinesCheckBox, false, this);
				SetEnabledness(includePercentCoverageCheckBox, false, this);
				SetEnabledness(outputTextBox, false, this);
				//
				if (coveragePairs == null)
				{
					coveragePairs = new List<KeyValuePair<string, double>>();
					//
					foreach (UnicodeBlock block in unicodeBlocks)
					{
						int count = 0;
						foreach (UnicodeCharacter character in block.Characters)
						{
							bool characterSupported = characters.Contains(character.Id);
							if (characterSupported)
								count++;
						}
						//
						double coverageValue = 0;
						List<UnicodeCharacter> printableCharacters = block.PrintableCharacters;
						if (printableCharacters != null && printableCharacters.Count > 0)
						{
							coverageValue = System.Math.Min(100, (100 * count / (double)printableCharacters.Count));
							coveragePairs.Add(new KeyValuePair<string, double>(block.BlockName, coverageValue));
						}
					}
				}
				//
				List<string> parts = new List<string>();
				foreach (KeyValuePair<string, double> pair in coveragePairs)
				{
					string prefix = (pair.Value < 1) ? "~" : "";
					string secondHalf = (includePercentCoverageCheckBox.Checked) ? string.Format(" ({0}{1:0.##}%)", new object[] { prefix, pair.Value }) : "";
					if (surroundWithQuotesCheckBox.Checked)
						parts.Add(
							string.Format("\"{0}{1}\"", new object[] { pair.Key, secondHalf })
						);
					else
						parts.Add(
							string.Format("{0}{1}", new object[] { pair.Key, secondHalf })
						);
				}
				//
				string separator = (includeNewLinesCheckBox.Checked) ? System.Environment.NewLine : " ";
				string coverage = string.Join("," + separator, parts.ToArray());
				coverage = (coverage == "") ? "(Empty)" : coverage;
				//
				SetText(outputTextBox, coverage, this);
				//
				SetEnabledness(surroundWithQuotesCheckBox, true, this);
				SetEnabledness(includeNewLinesCheckBox, true, this);
				SetEnabledness(includePercentCoverageCheckBox, true, this);
				SetEnabledness(outputTextBox, true, this);
				//
				SetStatus("Ready.", this);
			}));
			doBlockCoverage.Name = "Do Unicode Coverage";
			doBlockCoverage.Start();
		}

		private void surroundWithQuotesCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			recalculateText();
		}

		private void includePercentCoverageCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			recalculateText();
		}

		private void includeNewLinesCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			recalculateText();
		}
	}
}
