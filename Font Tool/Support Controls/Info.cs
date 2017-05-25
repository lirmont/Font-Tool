using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
//
using CLDR.SupplementalDataLocaleDataMarkupLanguage;

namespace FontTool
{
	[ToolboxItem(true)]
	[DefaultEvent("Click"), DefaultProperty("Text")]
	[System.Reflection.ObfuscationAttribute(Feature = "renaming")]
	public partial class Info : System.Windows.Forms.TableLayoutPanel
	{
		List<string> headers = new List<string> { "#", "Language", "Literacy %", "% by Population", "Status" };
		Label gdpValueLabel, populationValueLabel, literacyPercentValueLabel, literatePopulationValueLabel;

		private string type;
		private CollapsingGroupBox languageDataCollapsingGroupBox;

		public Info()
		{
			InitializeComponent();
			//
			initialize();
		}

		public Info(IContainer container)
		{
			container.Add(this);
			InitializeComponent();
			//
			initialize();
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string Type
		{
			get { return type; }
			set
			{
				type = value;
				//
				CLDR.LocaleDataMarkupLanguage.ldml en = SupportFunctions.GetLanguageScriptTerritory("en");
				supplementalData data = SupportFunctions.GetSupplementalData();
				territory thisTerritory = data.FindTerritoryInfo(type);
				//
				decimal gdp = decimal.Parse(thisTerritory.gdp);
				decimal population = decimal.Parse(thisTerritory.population);
				decimal literacyPercent = decimal.Parse(thisTerritory.literacyPercent);
				//
				gdpValueLabel.Text = string.Format("USD${0:#,0}", gdp);
				populationValueLabel.Text = string.Format("{0:#,0} people", population);
				literacyPercentValueLabel.Text = string.Format("{0}%", literacyPercent);
				literatePopulationValueLabel.Text = string.Format("{0:#,0}", (int)Math.Round(population * literacyPercent / 100.0m));
				//
				this.SuspendLayout();
				{
					// Clear out language data.
					int target = 4, current = RowCount;
					for (int i = current; i > target; i--)
					{
						int index = i - 1;
						for (int col = 0; col < 5; col++)
						{
							Control c = SupportFunctions.GetAnyControlAt(this, col, index);
							if (c != null)
							{
								this.Controls.Remove(c);
								c.Dispose();
							}
						}
						this.RowStyles.RemoveAt(index);
					}
					RowCount = target;
					// Create language data.
					int languageNumber = 1;
					foreach (languagePopulation languagePopulation in thisTerritory.languagePopulation)
					{
						// Add row.
						Label number = new Label();
						number.AutoSize = true;
						number.Text = string.Format("{0}.", languageNumber++);
						LinkLabel language = new LinkLabel();
						language.AutoSize = true;
						string thisType = languagePopulation.type;
						CLDR.LocaleDataMarkupLanguage.language languageData = en.LocaleDisplayNames.Languages.Find(item => item.type == thisType);
						// Fall back to a less-specific version of the name.
						if (languageData == null)
						{
							thisType = (languagePopulation.type.Contains("_")) ? languagePopulation.type.Substring(0, languagePopulation.type.IndexOf('_')).ToLower() : languagePopulation.type;
							languageData = en.LocaleDisplayNames.Languages.Find(item => item.type == thisType);
						}
						//
						int thisRow = this.RowStyles.Add(new RowStyle(SizeType.AutoSize));
						//
						language.Text = (languageData != null) ? languageData.Value : thisType;
						Label literacyPercentLabel = new Label();
						literacyPercentLabel.AutoSize = true;
						literacyPercentLabel.Text = string.Format("{0}%", languagePopulation.populationPercent);
						Label percentByPopulation = new Label();
						percentByPopulation.AutoSize = true;
						percentByPopulation.Text = string.Format("{0:#,0} people", (int)Math.Round(population * decimal.Parse(languagePopulation.populationPercent) / 100.0m));
						Label status = new Label();
						status.AutoSize = true;
						status.Text = string.Format("{0}", languagePopulation.OfficialStatus);
						//
						number.Visible = language.Visible = literacyPercentLabel.Visible = percentByPopulation.Visible = status.Visible = languageDataCollapsingGroupBox.Expanded;
						//
						this.Controls.Add(number, 0, thisRow);
						this.Controls.Add(language, 1, thisRow);
						this.Controls.Add(literacyPercentLabel, 2, thisRow);
						this.Controls.Add(percentByPopulation, 3, thisRow);
						this.Controls.Add(status, 4, thisRow);
						//
						RowCount = thisRow + 1;
					}
				}
				this.ResumeLayout(true);
			}
		}

		private void initialize()
		{
			int thisRow = 0;
			this.ColumnCount = headers.Count;
			{
				thisRow = this.RowStyles.Add(new RowStyle(SizeType.AutoSize));
				CollapsingGroupBox box = new CollapsingGroupBox();
				box.Expanded = true;
				box.Text = "Territory Information";
				box.AutoSize = true;
				box.Anchor = AnchorStyles.Left | AnchorStyles.Right;
				box.ContentExpansionChanged += new CollapsingGroupBox.ExpandedBoolean(territoryInfoBox_ContentExpansionChanged);
				this.Controls.Add(box, 0, thisRow);
				this.SetColumnSpan(box, headers.Count);
			}
			//
			thisRow = this.RowStyles.Add(new RowStyle(SizeType.AutoSize));
			TableLayoutPanel info = new TableLayoutPanel();
			info.AutoSize = true;
			info.Anchor = AnchorStyles.Left | AnchorStyles.Right;
			//
			Label gdp = new Label();
			gdp.Text = "Gross Domestic Product:";
			gdp.AutoSize = true;
			info.Controls.Add(gdp, 0, 0);
			gdpValueLabel = new Label();
			gdpValueLabel.Text = "Value";
			gdpValueLabel.AutoSize = true;
			info.Controls.Add(gdpValueLabel, 1, 0);
			//
			Label population = new Label();
			population.Text = "Population:";
			population.AutoSize = true;
			info.Controls.Add(population, 0, 1);
			populationValueLabel = new Label();
			populationValueLabel.Text = "Value";
			populationValueLabel.AutoSize = true;
			info.Controls.Add(populationValueLabel, 1, 1);
			//
			Label literacyPercent = new Label();
			literacyPercent.Text = "Literacy %:";
			literacyPercent.AutoSize = true;
			info.Controls.Add(literacyPercent, 0, 2);
			literacyPercentValueLabel = new Label();
			literacyPercentValueLabel.Text = "Value";
			literacyPercentValueLabel.AutoSize = true;
			info.Controls.Add(literacyPercentValueLabel, 1, 2);
			//
			Label literatePopulation = new Label();
			literatePopulation.Text = "Literate Population:";
			literatePopulation.AutoSize = true;
			info.Controls.Add(literatePopulation, 0, 3);
			literatePopulationValueLabel = new Label();
			literatePopulationValueLabel.Text = "Value";
			literatePopulationValueLabel.AutoSize = true;
			info.Controls.Add(literatePopulationValueLabel, 1, 3);
			//
			this.Controls.Add(info, 0, thisRow);
			this.SetColumnSpan(info, headers.Count);
			//
			{
				thisRow = this.RowStyles.Add(new RowStyle(SizeType.AutoSize));
				CollapsingGroupBox box = new CollapsingGroupBox();
				box.Expanded = true;
				box.Text = "Language Data";
				box.AutoSize = true;
				box.Anchor = AnchorStyles.Left | AnchorStyles.Right;
				box.ContentExpansionChanged += new CollapsingGroupBox.ExpandedBoolean(languagesBox_ContentExpansionChanged);
				languageDataCollapsingGroupBox = box;
				this.Controls.Add(box, 0, thisRow);
				this.SetColumnSpan(box, headers.Count);
			}
			// Add headers.
			if (headers != null)
			{
				thisRow = this.RowStyles.Add(new RowStyle(SizeType.AutoSize));
				this.RowCount = thisRow + 1;
				//
				int column = 0;
				foreach (string header in headers)
				{
					Label label = new Label();
					label.Text = header;
					label.AutoSize = true;
					Font f = new Font(label.Font, FontStyle.Bold);
					label.Font = f;
					label.Anchor = AnchorStyles.Left;
					this.Controls.Add(label, column, thisRow);
					//
					ColumnStyle style = new ColumnStyle();
					if (column == 4)
					{
						style.SizeType = SizeType.Percent;
						style.Width = 100;
					} else
						style.SizeType = SizeType.AutoSize;
					//
					this.ColumnStyles.Add(style);
					//
					column++;
				}
			}
		}

		void territoryInfoBox_ContentExpansionChanged(object sender, bool expanded)
		{
			Control c = SupportFunctions.GetAnyControlAt(this, 0, 1);
			c.Visible = expanded;
		}

		void languagesBox_ContentExpansionChanged(object sender, bool expanded)
		{
			SuspendLayout();
			{
				for (int row = 3; row < RowCount; row++)
				{
					for (int i = 0; i < 5; i++)
					{
						Control c = SupportFunctions.GetAnyControlAt(this, i, row);
						if (c != null)
							c.Visible = expanded;
					}
				}
			}
			ResumeLayout(true);
		}
	}
}
