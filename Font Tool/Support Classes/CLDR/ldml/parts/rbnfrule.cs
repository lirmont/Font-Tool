
namespace CLDR.LocaleDataMarkupLanguage
{
	public partial class rbnfrule
	{
		private string valueField;
		private string radixField;
		private string decexpField;
		private rbnfruleDraft draftField;
		private bool draftFieldSpecified;
		private string valueField1;

		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string value
		{
			get { return this.valueField; }
			set { this.valueField = value; }
		}
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string radix
		{
			get { return this.radixField; }
			set { this.radixField = value; }
		}
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string decexp
		{
			get { return this.decexpField; }
			set { this.decexpField = value; }
		}
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public rbnfruleDraft draft
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
			get { return this.valueField1; }
			set { this.valueField1 = value; }
		}
	}
}