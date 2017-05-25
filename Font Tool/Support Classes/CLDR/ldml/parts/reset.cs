
namespace CLDR.LocaleDataMarkupLanguage
{
	public partial class reset
	{
		private object[] itemsField;
		private string[] textField;
		private string beforeField;

		[System.Xml.Serialization.XmlElementAttribute("cp", typeof(cp))]
		[System.Xml.Serialization.XmlElementAttribute("first_non_ignorable", typeof(first_non_ignorable))]
		[System.Xml.Serialization.XmlElementAttribute("first_primary_ignorable", typeof(first_primary_ignorable))]
		[System.Xml.Serialization.XmlElementAttribute("first_secondary_ignorable", typeof(first_secondary_ignorable))]
		[System.Xml.Serialization.XmlElementAttribute("first_tertiary_ignorable", typeof(first_tertiary_ignorable))]
		[System.Xml.Serialization.XmlElementAttribute("first_trailing", typeof(first_trailing))]
		[System.Xml.Serialization.XmlElementAttribute("first_variable", typeof(first_variable))]
		[System.Xml.Serialization.XmlElementAttribute("last_non_ignorable", typeof(last_non_ignorable))]
		[System.Xml.Serialization.XmlElementAttribute("last_primary_ignorable", typeof(last_primary_ignorable))]
		[System.Xml.Serialization.XmlElementAttribute("last_secondary_ignorable", typeof(last_secondary_ignorable))]
		[System.Xml.Serialization.XmlElementAttribute("last_tertiary_ignorable", typeof(last_tertiary_ignorable))]
		[System.Xml.Serialization.XmlElementAttribute("last_trailing", typeof(last_trailing))]
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
		[System.Xml.Serialization.XmlAttributeAttribute(DataType = "NMTOKEN")]
		public string before
		{
			get { return this.beforeField; }
			set { this.beforeField = value; }
		}
	}

}