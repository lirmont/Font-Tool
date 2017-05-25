
namespace CLDR.LocaleDataMarkupLanguage
{
	public partial class currencyFormats
	{
		private object[] itemsField;
		private currencyFormatsDraft draftField;
		private bool draftFieldSpecified;
		private string validSubLocalesField;
		private string numberSystemField;

		[System.Xml.Serialization.XmlElementAttribute("alias", typeof(alias))]
		[System.Xml.Serialization.XmlElementAttribute("currencyFormatLength", typeof(currencyFormatLength))]
		[System.Xml.Serialization.XmlElementAttribute("currencySpacing", typeof(currencySpacing))]
		[System.Xml.Serialization.XmlElementAttribute("default", typeof(@default))]
		[System.Xml.Serialization.XmlElementAttribute("special", typeof(special))]
		[System.Xml.Serialization.XmlElementAttribute("unitPattern", typeof(unitPattern))]
		public object[] Items
		{
			get { return this.itemsField; }
			set { this.itemsField = value; }
		}
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public currencyFormatsDraft draft
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
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string numberSystem
		{
			get { return this.numberSystemField; }
			set { this.numberSystemField = value; }
		}
	}
}