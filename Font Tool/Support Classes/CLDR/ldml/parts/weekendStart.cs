
namespace CLDR.LocaleDataMarkupLanguage
{
	public partial class weekendStart
	{
		private special[] specialField;
		private weekendStartDay dayField;
		private string timeField;
		private weekendStartDraft draftField;
		private bool draftFieldSpecified;
		private string referencesField;
		private string altField;
		public weekendStart()
		{
			this.timeField = "00:00";
		}
		[System.Xml.Serialization.XmlElementAttribute("special")]
		public special[] special
		{
			get { return this.specialField; }
			set { this.specialField = value; }
		}
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public weekendStartDay day
		{
			get { return this.dayField; }
			set { this.dayField = value; }
		}
		[System.Xml.Serialization.XmlAttributeAttribute()]
		[System.ComponentModel.DefaultValueAttribute("00:00")]
		public string time
		{
			get { return this.timeField; }
			set { this.timeField = value; }
		}
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public weekendStartDraft draft
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