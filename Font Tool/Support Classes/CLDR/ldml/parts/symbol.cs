
namespace CLDR.LocaleDataMarkupLanguage
{
	public partial class symbol
	{
		private symbolDraft draftField;
		private bool draftFieldSpecified;
		private string referencesField;
		private string altField;
		private symbolChoice choiceField;
		private bool choiceFieldSpecified;
		private string valueField;

		[System.Xml.Serialization.XmlAttributeAttribute()]
		public symbolDraft draft
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
		public symbolChoice choice
		{
			get { return this.choiceField; }
			set { this.choiceField = value; }
		}
		[System.Xml.Serialization.XmlIgnoreAttribute()]
		public bool choiceSpecified
		{
			get { return this.choiceFieldSpecified; }
			set { this.choiceFieldSpecified = value; }
		}
		[System.Xml.Serialization.XmlTextAttribute()]
		public string Value
		{
			get { return this.valueField; }
			set { this.valueField = value; }
		}
	}
}