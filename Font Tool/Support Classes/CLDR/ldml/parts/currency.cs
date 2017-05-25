
namespace CLDR.LocaleDataMarkupLanguage
{
	public partial class currency
	{
		private object[] itemsField;
		private string typeField;
		private currencyDraft draftField;
		private bool draftFieldSpecified;
		private string referencesField;
		private string altField;
		private string validSubLocalesField;
		public currency()
		{
			this.typeField = "standard";
		}
		[System.Xml.Serialization.XmlElementAttribute("alias", typeof(alias))]
		[System.Xml.Serialization.XmlElementAttribute("decimal", typeof(@decimal))]
		[System.Xml.Serialization.XmlElementAttribute("displayName", typeof(displayName))]
		[System.Xml.Serialization.XmlElementAttribute("group", typeof(group))]
		[System.Xml.Serialization.XmlElementAttribute("pattern", typeof(pattern))]
		[System.Xml.Serialization.XmlElementAttribute("special", typeof(special))]
		[System.Xml.Serialization.XmlElementAttribute("symbol", typeof(symbol))]
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
		public currencyDraft draft
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
	}
}