
namespace CLDR.LocaleDataMarkupLanguage
{
	public partial class alias
	{
		private special[] specialField;
		private string sourceField;
		private string pathField;
		private aliasDraft draftField;
		private bool draftFieldSpecified;
		private string altField;

		[System.Xml.Serialization.XmlElementAttribute("special")]
		public special[] special
		{
			get { return this.specialField; }
			set { this.specialField = value; }
		}

		[System.Xml.Serialization.XmlAttributeAttribute(DataType = "NMTOKEN")]
		public string source
		{
			get { return this.sourceField; }
			set { this.sourceField = value; }
		}

		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string path
		{
			get { return this.pathField; }
			set { this.pathField = value; }
		}

		[System.Xml.Serialization.XmlAttributeAttribute()]
		public aliasDraft draft
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

		[System.Xml.Serialization.XmlAttributeAttribute(DataType = "NMTOKENS")]
		public string alt
		{
			get { return this.altField; }
			set { this.altField = value; }
		}
	}
}