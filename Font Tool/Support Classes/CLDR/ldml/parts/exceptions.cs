
namespace CLDR.LocaleDataMarkupLanguage
{
	public partial class exceptions
	{
		private exception[] exceptionField;

		[System.Xml.Serialization.XmlElementAttribute("exception")]
		public exception[] exception
		{
			get { return this.exceptionField; }
			set { this.exceptionField = value; }
		}
	}
}