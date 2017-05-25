
namespace CLDR.LocaleDataMarkupLanguage
{
	public partial class ic
	{
		private object[] itemsField;
		private string[] textField;

		[System.Xml.Serialization.XmlElementAttribute("cp", typeof(cp))]
		[System.Xml.Serialization.XmlElementAttribute("last_variable", typeof(last_variable))]
		public object[] Items
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