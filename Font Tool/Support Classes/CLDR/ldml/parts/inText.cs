
namespace CLDR.LocaleDataMarkupLanguage
{
	public partial class inText
	{
		private inTextType typeField;
		private bool typeFieldSpecified;
		private inTextDraft draftField;
		private bool draftFieldSpecified;
		private string referencesField;
		private string altField;
		private string valueField;

		[System.Xml.Serialization.XmlAttributeAttribute()]
		public inTextType type
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
		public inTextDraft draft
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