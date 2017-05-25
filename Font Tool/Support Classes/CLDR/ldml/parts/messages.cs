
namespace CLDR.LocaleDataMarkupLanguage
{
	public partial class messages
	{
		private object[] itemsField;
		private messagesDraft draftField;
		private bool draftFieldSpecified;
		private string referencesField;
		private string altField;
		private string validSubLocalesField;

		[System.Xml.Serialization.XmlElementAttribute("alias", typeof(alias))]
		[System.Xml.Serialization.XmlElementAttribute("noexpr", typeof(noexpr))]
		[System.Xml.Serialization.XmlElementAttribute("nostr", typeof(nostr))]
		[System.Xml.Serialization.XmlElementAttribute("yesexpr", typeof(yesexpr))]
		[System.Xml.Serialization.XmlElementAttribute("yesstr", typeof(yesstr))]
		public object[] Items
		{
			get { return this.itemsField; }
			set { this.itemsField = value; }
		}
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public messagesDraft draft
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