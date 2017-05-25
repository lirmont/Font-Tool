
namespace CLDR.LocaleDataMarkupLanguage
{
	public partial class pattern
	{
		private string typeField;
		private patternDraft draftField;
		private bool draftFieldSpecified;
		private string numbersField;
		private patternCount countField;
		private bool countFieldSpecified;
		private string referencesField;
		private string altField;
		private string valueField;
		public pattern()
		{
			this.typeField = "standard";
		}
		[System.Xml.Serialization.XmlAttributeAttribute(DataType = "NMTOKEN")]
		[System.ComponentModel.DefaultValueAttribute("standard")]
		public string type
		{
			get { return this.typeField; }
			set { this.typeField = value; }
		}
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public patternDraft draft
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
		public string numbers
		{
			get { return this.numbersField; }
			set { this.numbersField = value; }
		}
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public patternCount count
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