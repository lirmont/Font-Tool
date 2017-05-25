
namespace CLDR.LocaleDataMarkupLanguage
{
	public partial class firstDay
	{
		private special[] specialField;
		private string dayField;
		private firstDayDraft draftField;
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
		public string day
		{
			get { return this.dayField; }
			set { this.dayField = value; }
		}
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public firstDayDraft draft
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