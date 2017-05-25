
namespace CLDR.LocaleDataMarkupLanguage
{
	public partial class extend
	{
		private cp[] itemsField;
		private string[] textField;

		[System.Xml.Serialization.XmlElementAttribute("cp")]
		public cp[] Items
		{
			get { return this.itemsField; }
			set { this.itemsField = value; }
		}
		[System.Xml.Serialization.XmlTextAttribute()]
		public string[] Text
		{
			get { return this.textField; }
			set { this.textField = value; }
		}
	}
}