
namespace CLDR.LocaleDataMarkupLanguage
{
	public partial class rbnf
	{
		private object[] itemsField;

		[System.Xml.Serialization.XmlElementAttribute("alias", typeof(alias))]
		[System.Xml.Serialization.XmlElementAttribute("rulesetGrouping", typeof(rulesetGrouping))]
		public object[] Items
		{
			get { return this.itemsField; }
			set { this.itemsField = value; }
		}
	}
}