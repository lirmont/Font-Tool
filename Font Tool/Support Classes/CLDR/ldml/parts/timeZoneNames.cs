
namespace CLDR.LocaleDataMarkupLanguage
{
	public partial class timeZoneNames
	{
		private object[] itemsField;
		private timeZoneNamesDraft draftField;
		private bool draftFieldSpecified;
		private string validSubLocalesField;

		[System.Xml.Serialization.XmlElementAttribute("abbreviationFallback", typeof(abbreviationFallback))]
		[System.Xml.Serialization.XmlElementAttribute("alias", typeof(alias))]
		[System.Xml.Serialization.XmlElementAttribute("default", typeof(@default))]
		[System.Xml.Serialization.XmlElementAttribute("fallbackFormat", typeof(fallbackFormat))]
		[System.Xml.Serialization.XmlElementAttribute("fallbackRegionFormat", typeof(fallbackRegionFormat))]
		[System.Xml.Serialization.XmlElementAttribute("gmtFormat", typeof(gmtFormat))]
		[System.Xml.Serialization.XmlElementAttribute("gmtZeroFormat", typeof(gmtZeroFormat))]
		[System.Xml.Serialization.XmlElementAttribute("hourFormat", typeof(hourFormat))]
		[System.Xml.Serialization.XmlElementAttribute("hoursFormat", typeof(hoursFormat))]
		[System.Xml.Serialization.XmlElementAttribute("metazone", typeof(metazone))]
		[System.Xml.Serialization.XmlElementAttribute("preferenceOrdering", typeof(preferenceOrdering))]
		[System.Xml.Serialization.XmlElementAttribute("regionFormat", typeof(regionFormat))]
		[System.Xml.Serialization.XmlElementAttribute("singleCountries", typeof(singleCountries))]
		[System.Xml.Serialization.XmlElementAttribute("special", typeof(special))]
		[System.Xml.Serialization.XmlElementAttribute("zone", typeof(zone))]
		public object[] Items
		{
			get { return this.itemsField; }
			set { this.itemsField = value; }
		}
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public timeZoneNamesDraft draft
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
		public string validSubLocales
		{
			get { return this.validSubLocalesField; }
			set { this.validSubLocalesField = value; }
		}
	}
}