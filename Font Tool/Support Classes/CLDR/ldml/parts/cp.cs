namespace CLDR.LocaleDataMarkupLanguage
{
	public partial class cp
	{
		private special[] specialField;
		private string hexField;

		[System.Xml.Serialization.XmlElementAttribute("special")]
		public special[] special
		{
			get { return this.specialField; }
			set { this.specialField = value; }
		}

		[System.Xml.Serialization.XmlAttributeAttribute(DataType = "NMTOKEN")]
		public string hex
		{
			get { return this.hexField; }
			set { this.hexField = value; }
		}
	}
}