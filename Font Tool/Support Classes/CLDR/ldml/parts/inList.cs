
namespace CLDR.LocaleDataMarkupLanguage
{
	public partial class inList
	{
		private inListCasing casingField;
		private bool casingFieldSpecified;
		private inListDraft draftField;
		private bool draftFieldSpecified;
		private string referencesField;
		private string altField;
		private string valueField;

		[System.Xml.Serialization.XmlAttributeAttribute()]
		public inListCasing casing
		{
			get { return this.casingField; }
			set { this.casingField = value; }
		}
		[System.Xml.Serialization.XmlIgnoreAttribute()]
		public bool casingSpecified
		{
			get { return this.casingFieldSpecified; }
			set { this.casingFieldSpecified = value; }
		}
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public inListDraft draft
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
		[System.Xml.Serialization.XmlTextAttribute()]
		public string Value
		{
			get { return this.valueField; }
			set { this.valueField = value; }
		}
	}
}