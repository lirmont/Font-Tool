
namespace CLDR.LocaleDataMarkupLanguage
{
	public partial class singleCountries
	{
		private string listField;
		private string altField;
		private singleCountriesDraft draftField;
		private bool draftFieldSpecified;
		private string referencesField;

		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string list
		{
			get { return this.listField; }
			set { this.listField = value; }
		}
		[System.Xml.Serialization.XmlAttributeAttribute(DataType = "NMTOKENS")]
		public string alt
		{
			get { return this.altField; }
			set { this.altField = value; }
		}
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public singleCountriesDraft draft
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