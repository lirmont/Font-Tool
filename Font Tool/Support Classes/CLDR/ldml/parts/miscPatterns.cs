
namespace CLDR.LocaleDataMarkupLanguage
{
	public partial class miscPatterns
	{
		private object[] itemsField;
		private miscPatternsDraft draftField;
		private bool draftFieldSpecified;
		private string numberSystemField;

		[System.Xml.Serialization.XmlElementAttribute("alias", typeof(alias))]
		[System.Xml.Serialization.XmlElementAttribute("default", typeof(@default))]
		[System.Xml.Serialization.XmlElementAttribute("pattern", typeof(pattern))]
		[System.Xml.Serialization.XmlElementAttribute("special", typeof(special))]
		public object[] Items
		{
			get { return this.itemsField; }
			set { this.itemsField = value; }
		}
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public miscPatternsDraft draft
		{
			get { return this.draftField; }
			set { this.draftField = value; }
		}
		[System.Xml.Serialization.XmlIgnoreAttribute()]
		public bool draftSpecified
		{
			get { return this.draftFieldSpecified; }
			set { this.draftFieldSpecified = value; }
		}
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string numberSystem
		{
			get { return this.numberSystemField; }
			set { this.numberSystemField = value; }
		}
	}
}