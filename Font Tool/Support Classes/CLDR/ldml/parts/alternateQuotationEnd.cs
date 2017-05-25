
namespace CLDR.LocaleDataMarkupLanguage
{
	public partial class alternateQuotationEnd
	{
		private cp[] itemsField;
		private string[] textField;
		private alternateQuotationEndDraft draftField;
		private bool draftFieldSpecified;
		private string referencesField;
		private string altField;

		[System.Xml.Serialization.XmlElementAttribute("cp")]
		public cp[] Items
		{
			get { return this.itemsField; }
			set { this.itemsField = value; }
		}
		[System.Xml.Serialization.XmlTextAttribute()]
		public string[] Text
		{
			get { return this.textField; }
			set { this.textField = value; }
		}
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public alternateQuotationEndDraft draft
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