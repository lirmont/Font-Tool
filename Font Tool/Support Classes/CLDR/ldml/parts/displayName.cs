
namespace CLDR.LocaleDataMarkupLanguage
{
	public partial class displayName
	{
		private displayNameCount countField;
		private bool countFieldSpecified;
		private displayNameDraft draftField;
		private bool draftFieldSpecified;
		private string referencesField;
		private string altField;
		private string valueField;

		[System.Xml.Serialization.XmlAttributeAttribute()]
		public displayNameCount count
		{
			get { return this.countField; }
			set { this.countField = value; }
		}
		[System.Xml.Serialization.XmlIgnoreAttribute()]
		public bool countSpecified
		{
			get { return this.countFieldSpecified; }
			set { this.countFieldSpecified = value; }
		}
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public displayNameDraft draft
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