﻿
namespace CLDR.LocaleDataMarkupLanguage
{
	public partial class cyclicNameSets
	{
		private object[] itemsField;
		private cyclicNameSetsDraft draftField;
		private bool draftFieldSpecified;
		private string referencesField;
		private string altField;
		private string validSubLocalesField;

		[System.Xml.Serialization.XmlElementAttribute("alias", typeof(alias))]
		[System.Xml.Serialization.XmlElementAttribute("cyclicNameSet", typeof(cyclicNameSet))]
		[System.Xml.Serialization.XmlElementAttribute("special", typeof(special))]
		public object[] Items
		{
			get { return this.itemsField; }
			set { this.itemsField = value; }
		}
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public cyclicNameSetsDraft draft
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
		[System.Xml.Serialization.XmlAttributeAttribute(DataType = "NMTOKENS")]
		public string alt
		{
			get { return this.altField; }
			set { this.altField = value; }
		}
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string validSubLocales
		{
			get { return this.validSubLocalesField; }
			set { this.validSubLocalesField = value; }
		}
	}
}