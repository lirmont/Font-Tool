
namespace CLDR.LocaleDataMarkupLanguage
{
	public partial class ruleset
	{
		private object[] itemsField;
		private string typeField;
		private rulesetAccess accessField;
		private bool accessFieldSpecified;
		private rulesetAllowsParsing allowsParsingField;
		private bool allowsParsingFieldSpecified;
		private rulesetDraft draftField;
		private bool draftFieldSpecified;

		[System.Xml.Serialization.XmlElementAttribute("alias", typeof(alias))]
		[System.Xml.Serialization.XmlElementAttribute("rbnfrule", typeof(rbnfrule))]
		public object[] Items
		{
			get { return this.itemsField; }
			set { this.itemsField = value; }
		}
		[System.Xml.Serialization.XmlAttributeAttribute(DataType = "NMTOKEN")]
		public string type
		{
			get { return this.typeField; }
			set { this.typeField = value; }
		}
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public rulesetAccess access
		{
			get { return this.accessField; }
			set { this.accessField = value; }
		}
		[System.Xml.Serialization.XmlIgnoreAttribute()]
		public bool accessSpecified
		{
			get { return this.accessFieldSpecified; }
			set { this.accessFieldSpecified = value; }
		}
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public rulesetAllowsParsing allowsParsing
		{
			get { return this.allowsParsingField; }
			set { this.allowsParsingField = value; }
		}
		[System.Xml.Serialization.XmlIgnoreAttribute()]
		public bool allowsParsingSpecified
		{
			get { return this.allowsParsingFieldSpecified; }
			set { this.allowsParsingFieldSpecified = value; }
		}
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public rulesetDraft draft
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
	}
}