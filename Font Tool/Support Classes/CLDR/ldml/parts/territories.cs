using System.Collections.Generic;

namespace CLDR.LocaleDataMarkupLanguage
{
	public partial class territories
	{
		private object[] itemsField;
		private territoriesDraft draftField;
		private bool draftFieldSpecified;
		private string standardField;
		private string referencesField;
		private string validSubLocalesField;

		public List<territory> Territories
		{
			get
			{
				List<territory> list = new List<territory>();
				if (Items != null)
				{
					foreach (object obj in Items)
					{
						if (obj is territories)
						{
							territories territories = obj as territories;
							if (territories != null)
							{
								foreach (object child in territories.Items)
								{
									if (child is territory)
										list.Add((territory)child);
								}
							}
						}
					}
				}
				return list;
			}
		}

		[System.Xml.Serialization.XmlElementAttribute("alias", typeof(alias))]
		[System.Xml.Serialization.XmlElementAttribute("special", typeof(special))]
		[System.Xml.Serialization.XmlElementAttribute("territory", typeof(territory))]
		public object[] Items
		{
			get { return this.itemsField; }
			set { this.itemsField = value; }
		}
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public territoriesDraft draft
		{
			get { return this.draftField; }
			set { this.draftField = value; }
		}
		[System.Xml.Serialization.XmlIgnoreAttribute()]
		public bool draftSpecified
		{
			get { return this.draftFieldSpecified; }
			set { this.draftFieldSpecified = value; }
		}
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string standard
		{
			get { return this.standardField; }
			set { this.standardField = value; }
		}
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string references
		{
			get { return this.referencesField; }
			set { this.referencesField = value; }
		}
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string validSubLocales
		{
			get { return this.validSubLocalesField; }
			set { this.validSubLocalesField = value; }
		}
	}
}