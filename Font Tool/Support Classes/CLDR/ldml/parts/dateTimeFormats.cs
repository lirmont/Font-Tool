﻿
namespace CLDR.LocaleDataMarkupLanguage
{
	public partial class dateTimeFormats
	{
		private object[] itemsField;
		private dateTimeFormatsDraft draftField;
		private bool draftFieldSpecified;
		private string validSubLocalesField;

		[System.Xml.Serialization.XmlElementAttribute("alias", typeof(alias))]
		[System.Xml.Serialization.XmlElementAttribute("appendItems", typeof(appendItems))]
		[System.Xml.Serialization.XmlElementAttribute("availableFormats", typeof(availableFormats))]
		[System.Xml.Serialization.XmlElementAttribute("dateTimeFormatLength", typeof(dateTimeFormatLength))]
		[System.Xml.Serialization.XmlElementAttribute("default", typeof(@default))]
		[System.Xml.Serialization.XmlElementAttribute("intervalFormats", typeof(intervalFormats))]
		[System.Xml.Serialization.XmlElementAttribute("special", typeof(special))]
		public object[] Items
		{
			get { return this.itemsField; }
			set { this.itemsField = value; }
		}
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public dateTimeFormatsDraft draft
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
		public string validSubLocales
		{
			get { return this.validSubLocalesField; }
			set { this.validSubLocalesField = value; }
		}
	}
}