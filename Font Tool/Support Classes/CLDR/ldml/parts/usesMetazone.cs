
namespace CLDR.LocaleDataMarkupLanguage
{
	public partial class usesMetazone
	{
		private string mzoneField;
		private string fromField;
		private string toField;
		private usesMetazoneDraft draftField;
		private bool draftFieldSpecified;
		private string referencesField;
		private string altField;

		[System.Xml.Serialization.XmlAttributeAttribute(DataType = "NMTOKEN")]
		public string mzone
		{
			get { return this.mzoneField; }
			set { this.mzoneField = value; }
		}
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string from
		{
			get { return this.fromField; }
			set { this.fromField = value; }
		}
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string to
		{
			get { return this.toField; }
			set { this.toField = value; }
		}
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public usesMetazoneDraft draft
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