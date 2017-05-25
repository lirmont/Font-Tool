
namespace CLDR.LocaleDataMarkupLanguage
{
	public partial class abbreviationFallback
	{
		private abbreviationFallbackType typeField;
		private bool typeFieldSpecified;
		private abbreviationFallbackChoice choiceField;
		private bool choiceFieldSpecified;
		private string altField;
		private abbreviationFallbackDraft draftField;
		private bool draftFieldSpecified;
		private string referencesField;

		[System.Xml.Serialization.XmlAttributeAttribute()]
		public abbreviationFallbackType type
		{
			get { return this.typeField; }
			set { this.typeField = value; }
		}
		[System.Xml.Serialization.XmlIgnoreAttribute()]
		public bool typeSpecified
		{
			get { return this.typeFieldSpecified; }
			set { this.typeFieldSpecified = value; }
		}
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public abbreviationFallbackChoice choice
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
		[System.Xml.Serialization.XmlAttributeAttribute(DataType = "NMTOKENS")]
		public string alt
		{
			get { return this.altField; }
			set { this.altField = value; }
		}
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public abbreviationFallbackDraft draft
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
	}
}