
namespace CLDR.LocaleDataMarkupLanguage
{
	public partial class casingItem
	{
		private string typeField;
		private casingItemOverride overrideField;
		private bool overrideFieldSpecified;
		private string valueField;

		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string type
		{
			get { return this.typeField; }
			set { this.typeField = value; }
		}
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public casingItemOverride @override
		{
			get { return this.overrideField; }
			set { this.overrideField = value; }
		}
		[System.Xml.Serialization.XmlIgnoreAttribute()]
		public bool overrideSpecified
		{
			get { return this.overrideFieldSpecified; }
			set { this.overrideFieldSpecified = value; }
		}
		[System.Xml.Serialization.XmlTextAttribute()]
		public string Value
		{
			get { return this.valueField; }
			set { this.valueField = value; }
		}
	}
}