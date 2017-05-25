
namespace CLDR.LocaleDataMarkupLanguage
{
	public partial class afterCurrency
	{
		private object[] itemsField;

		[System.Xml.Serialization.XmlElementAttribute("alias", typeof(alias))]
		[System.Xml.Serialization.XmlElementAttribute("currencyMatch", typeof(currencyMatch))]
		[System.Xml.Serialization.XmlElementAttribute("insertBetween", typeof(insertBetween))]
		[System.Xml.Serialization.XmlElementAttribute("surroundingMatch", typeof(surroundingMatch))]
		public object[] Items
		{
			get { return this.itemsField; }
			set { this.itemsField = value; }
		}
	}
}