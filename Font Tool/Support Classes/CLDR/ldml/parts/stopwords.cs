
namespace CLDR.LocaleDataMarkupLanguage
{
	public partial class stopwords
	{
		private stopwordList[] stopwordListField;

		[System.Xml.Serialization.XmlElementAttribute("stopwordList")]
		public stopwordList[] stopwordList
		{
			get { return this.stopwordListField; }
			set { this.stopwordListField = value; }
		}
	}
}