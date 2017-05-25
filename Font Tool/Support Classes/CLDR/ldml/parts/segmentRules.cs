
namespace CLDR.LocaleDataMarkupLanguage
{
	public partial class segmentRules
	{
		private object[] itemsField;

		[System.Xml.Serialization.XmlElementAttribute("alias", typeof(alias))]
		[System.Xml.Serialization.XmlElementAttribute("rule", typeof(rule))]
		public object[] Items
		{
			get { return this.itemsField; }
			set { this.itemsField = value; }
		}
	}
}