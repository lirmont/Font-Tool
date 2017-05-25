
namespace CLDR.LocaleDataMarkupLanguage
{
	public partial class scientificFormatLength
	{
		private object[] itemsField;
		private scientificFormatLengthType typeField;
		private bool typeFieldSpecified;
		private scientificFormatLengthDraft draftField;
		private bool draftFieldSpecified;
		private string standardField;
		private string referencesField;
		private string altField;
		private string validSubLocalesField;

		[System.Xml.Serialization.XmlElementAttribute("alias", typeof(alias))]
		[System.Xml.Serialization.XmlElementAttribute("default", typeof(@default))]
		[System.Xml.Serialization.XmlElementAttribute("scientificFormat", typeof(scientificFormat))]
		[System.Xml.Serialization.XmlElementAttribute("special", typeof(special))]
		public object[] Items
		{
			get { return this.itemsField; }
			set { this.itemsField = value; }
		}
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public scientificFormatLengthType type
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
		public scientificFormatLengthDraft draft
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
		public string standard
		{
			get { return this.standardField; }
			set { this.standardField = value; }
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