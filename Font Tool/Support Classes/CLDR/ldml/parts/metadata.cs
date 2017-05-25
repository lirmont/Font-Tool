
namespace CLDR.LocaleDataMarkupLanguage
{
	public partial class metadata
	{
		private casingItem[] casingDataField;

		[System.Xml.Serialization.XmlArrayItemAttribute("casingItem", IsNullable = false)]
		public casingItem[] casingData
		{
			get { return this.casingDataField; }
			set { this.casingDataField = value; }
		}
	}
}