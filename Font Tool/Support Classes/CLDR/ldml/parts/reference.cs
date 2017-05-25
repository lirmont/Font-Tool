
namespace CLDR.LocaleDataMarkupLanguage
{
	public partial class reference
	{
		private string typeField;
		private referenceStandard standardField;
		private bool standardFieldSpecified;
		private string uriField;
		private referenceDraft draftField;
		private bool draftFieldSpecified;
		private string altField;
		private string valueField;

		[System.Xml.Serialization.XmlAttributeAttribute(DataType = "NMTOKEN")]
		public string type
		{
			get { return this.typeField; }
			set { this.typeField = value; }
		}
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public referenceStandard standard
		{
			get { return this.standardField; }
			set { this.standardField = value; }
		}
		[System.Xml.Serialization.XmlIgnoreAttribute()]
		public bool standardSpecified
		{
			get { return this.standardFieldSpecified; }
			set { this.standardFieldSpecified = value; }
		}
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string uri
		{
			get { return this.uriField; }
			set { this.uriField = value; }
		}
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public referenceDraft draft
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
		[System.Xml.Serialization.XmlTextAttribute()]
		public string Value
		{
			get { return this.valueField; }
			set { this.valueField = value; }
		}
	}
}