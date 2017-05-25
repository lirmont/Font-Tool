
namespace CLDR.LocaleDataMarkupLanguage
{
	public partial class variables
	{
		private object[] itemsField;

		[System.Xml.Serialization.XmlElementAttribute("alias", typeof(alias))]
		[System.Xml.Serialization.XmlElementAttribute("variable", typeof(variable))]
		public object[] Items
		{
			get { return this.itemsField; }
			set { this.itemsField = value; }
		}
	}
}