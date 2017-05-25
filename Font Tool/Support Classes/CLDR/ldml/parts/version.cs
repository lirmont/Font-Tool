using System.Xml.Serialization;

namespace CLDR.LocaleDataMarkupLanguage
{
	public partial class version
	{
		private string numberField;
		private string cldrVersionField;

		public version()
		{
			this.cldrVersionField = "24";
		}

		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string number
		{
			get { return this.numberField; }
			set { this.numberField = value; }
		}

		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string cldrVersion
		{
			get { return this.cldrVersionField; }
			set { this.cldrVersionField = value; }
		}
	}
}