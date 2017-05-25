using System.Xml.Serialization;

namespace CLDR.LocaleDataMarkupLanguage
{
	public partial class generation
	{
		private string dateField;

		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string date
		{
			get { return this.dateField; }
			set { this.dateField = value; }
		}
	}
}