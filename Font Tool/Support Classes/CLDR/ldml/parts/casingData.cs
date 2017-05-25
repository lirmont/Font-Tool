
namespace CLDR.LocaleDataMarkupLanguage
{
	public partial class casingData
	{
		private casingItem[] casingItemField;

		[System.Xml.Serialization.XmlElementAttribute("casingItem")]
		public casingItem[] casingItem
		{
			get { return this.casingItemField; }
			set { this.casingItemField = value; }
		}
	}
}