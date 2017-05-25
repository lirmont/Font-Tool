
namespace CLDR.LocaleDataMarkupLanguage
{
	public partial class dayPeriodWidth
	{
		private object[] itemsField;
		private dayPeriodWidthType typeField;
		private dayPeriodWidthDraft draftField;
		private bool draftFieldSpecified;
		private string referencesField;

		[System.Xml.Serialization.XmlElementAttribute("alias", typeof(alias))]
		[System.Xml.Serialization.XmlElementAttribute("dayPeriod", typeof(dayPeriod))]
		public object[] Items
		{
			get { return this.itemsField; }
			set { this.itemsField = value; }
		}
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public dayPeriodWidthType type
		{
			get { return this.typeField; }
			set { this.typeField = value; }
		}
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public dayPeriodWidthDraft draft
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