
namespace CLDR.LocaleDataMarkupLanguage
{
	public partial class otherNumberingSystems
	{
		private object[] itemsField;
		private otherNumberingSystemsDraft draftField;
		private bool draftFieldSpecified;
		private string altField;

		[System.Xml.Serialization.XmlElementAttribute("alias", typeof(alias))]
		[System.Xml.Serialization.XmlElementAttribute("finance", typeof(finance))]
		[System.Xml.Serialization.XmlElementAttribute("native", typeof(native))]
		[System.Xml.Serialization.XmlElementAttribute("traditional", typeof(traditional))]
		public object[] Items
		{
			get { return this.itemsField; }
			set { this.itemsField = value; }
		}
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public otherNumberingSystemsDraft draft
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
		[System.Xml.Serialization.XmlAttributeAttribute(DataType = "NMTOKENS")]
		public string alt
		{
			get { return this.altField; }
			set { this.altField = value; }
		}
	}
}