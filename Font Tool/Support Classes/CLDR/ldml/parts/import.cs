
namespace CLDR.LocaleDataMarkupLanguage
{
	public partial class import
	{
		private string sourceField;
		private string typeField;

		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string source
		{
			get { return this.sourceField; }
			set { this.sourceField = value; }
		}
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string type
		{
			get { return this.typeField; }
			set { this.typeField = value; }
		}
	}

}