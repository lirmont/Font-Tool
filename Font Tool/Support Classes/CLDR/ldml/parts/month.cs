
namespace CLDR.LocaleDataMarkupLanguage
{
	public partial class month
	{
		private cp[] itemsField;
		private string[] textField;
		private monthType typeField;
		private monthYeartype yeartypeField;
		private bool yeartypeFieldSpecified;
		private string referencesField;
		private string altField;
		private monthDraft draftField;
		private bool draftFieldSpecified;

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
		public monthType type
		{
			get { return this.typeField; }
			set { this.typeField = value; }
		}
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public monthYeartype yeartype
		{
			get { return this.yeartypeField; }
			set { this.yeartypeField = value; }
		}
		[System.Xml.Serialization.XmlIgnoreAttribute()]
		public bool yeartypeSpecified
		{
			get { return this.yeartypeFieldSpecified; }
			set { this.yeartypeFieldSpecified = value; }
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
		public monthDraft draft
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