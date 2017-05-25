
namespace CLDR.LocaleDataMarkupLanguage
{
	public partial class indexLabel
	{
		private string indexSourceField;
		private indexLabelPriority priorityField;
		private bool priorityFieldSpecified;
		private indexLabelDraft draftField;
		private bool draftFieldSpecified;
		private string referencesField;
		private string altField;
		private string valueField;

		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string indexSource
		{
			get { return this.indexSourceField; }
			set { this.indexSourceField = value; }
		}
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public indexLabelPriority priority
		{
			get { return this.priorityField; }
			set { this.priorityField = value; }
		}
		[System.Xml.Serialization.XmlIgnoreAttribute()]
		public bool prioritySpecified
		{
			get { return this.priorityFieldSpecified; }
			set { this.priorityFieldSpecified = value; }
		}
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public indexLabelDraft draft
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