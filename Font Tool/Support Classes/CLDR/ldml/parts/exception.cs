
namespace CLDR.LocaleDataMarkupLanguage
{
	public partial class exception
	{
		private exceptionDraft draftField;
		private bool draftFieldSpecified;
		private string valueField;

		[System.Xml.Serialization.XmlAttributeAttribute()]
		public exceptionDraft draft
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
		[System.Xml.Serialization.XmlTextAttribute()]
		public string Value
		{
			get { return this.valueField; }
			set { this.valueField = value; }
		}
	}
}