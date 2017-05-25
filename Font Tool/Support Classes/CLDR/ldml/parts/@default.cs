
namespace CLDR.LocaleDataMarkupLanguage
{
	public partial class @default
	{
		private special[] specialField;
		private string typeField;
		private string choiceField;
		private defaultDraft draftField;
		private bool draftFieldSpecified;
		private string referencesField;
		private string altField;

		[System.Xml.Serialization.XmlElementAttribute("special")]
		public special[] special
		{
			get { return this.specialField; }
			set { this.specialField = value; }
		}
		[System.Xml.Serialization.XmlAttributeAttribute(DataType = "NMTOKEN")]
		public string type
		{
			get { return this.typeField; }
			set { this.typeField = value; }
		}
		[System.Xml.Serialization.XmlAttributeAttribute(DataType = "NMTOKEN")]
		public string choice
		{
			get { return this.choiceField; }
			set { this.choiceField = value; }
		}
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public defaultDraft draft
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