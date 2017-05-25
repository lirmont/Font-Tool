
namespace CLDR.LocaleDataMarkupLanguage
{
	public partial class references
	{
		private reference[] referenceField;

		[System.Xml.Serialization.XmlElementAttribute("reference")]
		public reference[] reference
		{
			get { return this.referenceField; }
			set { this.referenceField = value; }
		}
	}
}