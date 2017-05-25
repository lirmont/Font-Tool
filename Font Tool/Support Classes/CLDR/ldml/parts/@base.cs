
namespace CLDR.LocaleDataMarkupLanguage
{
	public partial class @base
	{
		private object itemField;

		[System.Xml.Serialization.XmlElementAttribute("alias", typeof(alias))]
		[System.Xml.Serialization.XmlElementAttribute("special", typeof(special))]
		public object Item
		{
			get { return this.itemField; }
			set { this.itemField = value; }
		}
	}
}