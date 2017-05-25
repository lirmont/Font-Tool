
namespace CLDR.LocaleDataMarkupLanguage
{
	public partial class commonlyUsed
	{
		private commonlyUsedUsed usedField;
		private bool usedFieldSpecified;
		private commonlyUsedDraft draftField;
		private bool draftFieldSpecified;
		private string referencesField;
		private string altField;
		private string valueField;

		[System.Xml.Serialization.XmlAttributeAttribute()]
		public commonlyUsedUsed used
		{
			get { return this.usedField; }
			set { this.usedField = value; }
		}
		[System.Xml.Serialization.XmlIgnoreAttribute()]
		public bool usedSpecified
		{
			get { return this.usedFieldSpecified; }
			set { this.usedFieldSpecified = value; }
		}
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public commonlyUsedDraft draft
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