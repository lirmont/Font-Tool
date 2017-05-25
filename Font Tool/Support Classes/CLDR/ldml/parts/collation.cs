
namespace CLDR.LocaleDataMarkupLanguage
{
	public partial class collation
	{
		private object[] itemsField;
		private string typeField;
		private collationDraft draftField;
		private bool draftFieldSpecified;
		private string standardField;
		private string referencesField;
		private string altField;
		private string validSubLocalesField;
		private collationVisibility visibilityField;
		public collation()
		{
			this.typeField = "standard";
			this.visibilityField = collationVisibility.external;
		}
		[System.Xml.Serialization.XmlElementAttribute("alias", typeof(alias))]
		[System.Xml.Serialization.XmlElementAttribute("base", typeof(@base))]
		[System.Xml.Serialization.XmlElementAttribute("cr", typeof(string))]
		[System.Xml.Serialization.XmlElementAttribute("import", typeof(import))]
		[System.Xml.Serialization.XmlElementAttribute("optimize", typeof(optimize))]
		[System.Xml.Serialization.XmlElementAttribute("rules", typeof(rules))]
		[System.Xml.Serialization.XmlElementAttribute("settings", typeof(settings))]
		[System.Xml.Serialization.XmlElementAttribute("special", typeof(special))]
		[System.Xml.Serialization.XmlElementAttribute("suppress_contractions", typeof(suppress_contractions))]
		public object[] Items
		{
			get { return this.itemsField; }
			set { this.itemsField = value; }
		}
		[System.Xml.Serialization.XmlAttributeAttribute(DataType = "NMTOKEN")]
		[System.ComponentModel.DefaultValueAttribute("standard")]
		public string type
		{
			get { return this.typeField; }
			set { this.typeField = value; }
		}
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public collationDraft draft
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
		[System.Xml.Serialization.XmlAttributeAttribute(DataType = "NMTOKENS")]
		public string alt
		{
			get { return this.altField; }
			set { this.altField = value; }
		}
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string validSubLocales
		{
			get { return this.validSubLocalesField; }
			set { this.validSubLocalesField = value; }
		}
		[System.Xml.Serialization.XmlAttributeAttribute()]
		[System.ComponentModel.DefaultValueAttribute(collationVisibility.external)]
		public collationVisibility visibility
		{
			get { return this.visibilityField; }
			set { this.visibilityField = value; }
		}
	}
}