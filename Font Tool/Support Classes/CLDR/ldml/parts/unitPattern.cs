﻿
namespace CLDR.LocaleDataMarkupLanguage
{
	public partial class unitPattern
	{
		private unitPatternCount countField;
		private unitPatternDraft draftField;
		private bool draftFieldSpecified;
		private string referencesField;
		private string altField;
		private string validSubLocalesField;
		private string valueField;

		[System.Xml.Serialization.XmlAttributeAttribute()]
		public unitPatternCount count
		{
			get { return this.countField; }
			set { this.countField = value; }
		}
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public unitPatternDraft draft
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
		[System.Xml.Serialization.XmlTextAttribute()]
		public string Value
		{
			get { return this.valueField; }
			set { this.valueField = value; }
		}
	}
}