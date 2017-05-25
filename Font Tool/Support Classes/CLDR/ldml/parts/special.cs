using System.Xml.Serialization;

namespace CLDR.LocaleDataMarkupLanguage
{
	public partial class special
	{
		private System.Xml.XmlNode[] anyField;

		[System.Xml.Serialization.XmlTextAttribute()]
		[System.Xml.Serialization.XmlAnyElementAttribute()]
		public System.Xml.XmlNode[] Any
		{
			get { return this.anyField; }
			set { this.anyField = value; }
		}
	}
}