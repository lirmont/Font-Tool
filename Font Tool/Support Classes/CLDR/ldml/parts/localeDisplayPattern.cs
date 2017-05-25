
namespace CLDR.LocaleDataMarkupLanguage
{
	public partial class localeDisplayPattern
	{
		private object[] itemsField;
		private localeDisplayPatternDraft draftField;
		private bool draftFieldSpecified;
		private string referencesField;
		private string altField;

		[System.Xml.Serialization.XmlElementAttribute("alias", typeof(alias))]
		[System.Xml.Serialization.XmlElementAttribute("localeKeyTypePattern", typeof(localeKeyTypePattern))]
		[System.Xml.Serialization.XmlElementAttribute("localePattern", typeof(localePattern))]
		[System.Xml.Serialization.XmlElementAttribute("localeSeparator", typeof(localeSeparator))]
		[System.Xml.Serialization.XmlElementAttribute("special", typeof(special))]
		public object[] Items
		{
			get { return this.itemsField; }
			set { this.itemsField = value; }
		}
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public localeDisplayPatternDraft draft
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
	}
}