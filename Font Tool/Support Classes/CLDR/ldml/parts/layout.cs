
namespace CLDR.LocaleDataMarkupLanguage
{
	public partial class layout
	{
		private object[] itemsField;
		private layoutDraft draftField;
		private bool draftFieldSpecified;
		private string referencesField;

		[System.Xml.Serialization.XmlElementAttribute("alias", typeof(alias))]
		[System.Xml.Serialization.XmlElementAttribute("inList", typeof(inList))]
		[System.Xml.Serialization.XmlElementAttribute("inText", typeof(inText))]
		[System.Xml.Serialization.XmlElementAttribute("orientation", typeof(orientation))]
		[System.Xml.Serialization.XmlElementAttribute("special", typeof(special))]
		public object[] Items
		{
			get { return this.itemsField; }
			set { this.itemsField = value; }
		}
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public layoutDraft draft
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
		public string references
		{
			get { return this.referencesField; }
			set { this.referencesField = value; }
		}
	}
}