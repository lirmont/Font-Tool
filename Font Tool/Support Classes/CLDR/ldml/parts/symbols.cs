
namespace CLDR.LocaleDataMarkupLanguage
{
	public partial class symbols
	{
		private object[] itemsField;
		private symbolsDraft draftField;
		private bool draftFieldSpecified;
		private string standardField;
		private string referencesField;
		private string altField;
		private string validSubLocalesField;
		private string numberSystemField;

		[System.Xml.Serialization.XmlElementAttribute("alias", typeof(alias))]
		[System.Xml.Serialization.XmlElementAttribute("currencyDecimal", typeof(currencyDecimal))]
		[System.Xml.Serialization.XmlElementAttribute("currencyGroup", typeof(currencyGroup))]
		[System.Xml.Serialization.XmlElementAttribute("decimal", typeof(@decimal))]
		[System.Xml.Serialization.XmlElementAttribute("exponential", typeof(exponential))]
		[System.Xml.Serialization.XmlElementAttribute("group", typeof(group))]
		[System.Xml.Serialization.XmlElementAttribute("infinity", typeof(infinity))]
		[System.Xml.Serialization.XmlElementAttribute("list", typeof(list))]
		[System.Xml.Serialization.XmlElementAttribute("minusSign", typeof(minusSign))]
		[System.Xml.Serialization.XmlElementAttribute("nan", typeof(nan))]
		[System.Xml.Serialization.XmlElementAttribute("nativeZeroDigit", typeof(nativeZeroDigit))]
		[System.Xml.Serialization.XmlElementAttribute("patternDigit", typeof(patternDigit))]
		[System.Xml.Serialization.XmlElementAttribute("perMille", typeof(perMille))]
		[System.Xml.Serialization.XmlElementAttribute("percentSign", typeof(percentSign))]
		[System.Xml.Serialization.XmlElementAttribute("plusSign", typeof(plusSign))]
		[System.Xml.Serialization.XmlElementAttribute("special", typeof(special))]
		[System.Xml.Serialization.XmlElementAttribute("superscriptingExponent", typeof(superscriptingExponent))]
		public object[] Items
		{
			get { return this.itemsField; }
			set { this.itemsField = value; }
		}
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public symbolsDraft draft
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
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string numberSystem
		{
			get { return this.numberSystemField; }
			set { this.numberSystemField = value; }
		}
	}
}