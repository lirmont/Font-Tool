
namespace CLDR.LocaleDataMarkupLanguage
{
	public partial class calendar
	{
		private object[] itemsField;
		private string typeField;
		private calendarDraft draftField;
		private bool draftFieldSpecified;
		private string standardField;
		private string referencesField;
		private string altField;
		private string validSubLocalesField;

		[System.Xml.Serialization.XmlElementAttribute("alias", typeof(alias))]
		[System.Xml.Serialization.XmlElementAttribute("am", typeof(am))]
		[System.Xml.Serialization.XmlElementAttribute("cyclicNameSets", typeof(cyclicNameSets))]
		[System.Xml.Serialization.XmlElementAttribute("dateFormats", typeof(dateFormats))]
		[System.Xml.Serialization.XmlElementAttribute("dateTimeFormats", typeof(dateTimeFormats))]
		[System.Xml.Serialization.XmlElementAttribute("dayAbbr", typeof(dayAbbr))]
		[System.Xml.Serialization.XmlElementAttribute("dayNames", typeof(dayNames))]
		[System.Xml.Serialization.XmlElementAttribute("dayPeriods", typeof(dayPeriods))]
		[System.Xml.Serialization.XmlElementAttribute("days", typeof(days))]
		[System.Xml.Serialization.XmlElementAttribute("eras", typeof(eras))]
		[System.Xml.Serialization.XmlElementAttribute("fields", typeof(fields))]
		[System.Xml.Serialization.XmlElementAttribute("monthAbbr", typeof(monthAbbr))]
		[System.Xml.Serialization.XmlElementAttribute("monthNames", typeof(monthNames))]
		[System.Xml.Serialization.XmlElementAttribute("monthPatterns", typeof(monthPatterns))]
		[System.Xml.Serialization.XmlElementAttribute("months", typeof(months))]
		[System.Xml.Serialization.XmlElementAttribute("pm", typeof(pm))]
		[System.Xml.Serialization.XmlElementAttribute("quarters", typeof(quarters))]
		[System.Xml.Serialization.XmlElementAttribute("special", typeof(special))]
		[System.Xml.Serialization.XmlElementAttribute("timeFormats", typeof(timeFormats))]
		[System.Xml.Serialization.XmlElementAttribute("week", typeof(week))]
		public object[] Items
		{
			get { return this.itemsField; }
			set { this.itemsField = value; }
		}
		[System.Xml.Serialization.XmlAttributeAttribute(DataType = "NMTOKEN")]
		public string type
		{
			get { return this.typeField; }
			set { this.typeField = value; }
		}
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public calendarDraft draft
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