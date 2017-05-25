
namespace CLDR.LocaleDataMarkupLanguage
{
	public partial class currencySpacing
	{
		private object[] itemsField;

		[System.Xml.Serialization.XmlElementAttribute("afterCurrency", typeof(afterCurrency))]
		[System.Xml.Serialization.XmlElementAttribute("alias", typeof(alias))]
		[System.Xml.Serialization.XmlElementAttribute("beforeCurrency", typeof(beforeCurrency))]
		[System.Xml.Serialization.XmlElementAttribute("special", typeof(special))]
		public object[] Items
		{
			get { return this.itemsField; }
			set { this.itemsField = value; }
		}
	}
}