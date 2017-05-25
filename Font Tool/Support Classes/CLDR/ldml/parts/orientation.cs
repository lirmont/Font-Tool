
namespace CLDR.LocaleDataMarkupLanguage
{
	public partial class orientation
	{
		private string[] characterOrderField;
		private string[] lineOrderField;
		private special[] specialField;
		private orientationCharacters charactersField;
		private bool charactersFieldSpecified;
		private orientationLines linesField;
		private bool linesFieldSpecified;
		private orientationDraft draftField;
		private bool draftFieldSpecified;
		private string standardField;
		private string referencesField;
		private string altField;

		[System.Xml.Serialization.XmlElementAttribute("characterOrder")]
		public string[] characterOrder
		{
			get { return this.characterOrderField; }
			set { this.characterOrderField = value; }
		}
		[System.Xml.Serialization.XmlElementAttribute("lineOrder")]
		public string[] lineOrder
		{
			get { return this.lineOrderField; }
			set { this.lineOrderField = value; }
		}
		[System.Xml.Serialization.XmlElementAttribute("special")]
		public special[] special
		{
			get { return this.specialField; }
			set { this.specialField = value; }
		}
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public orientationCharacters characters
		{
			get { return this.charactersField; }
			set { this.charactersField = value; }
		}
		[System.Xml.Serialization.XmlIgnoreAttribute()]
		public bool charactersSpecified
		{
			get { return this.charactersFieldSpecified; }
			set { this.charactersFieldSpecified = value; }
		}
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public orientationLines lines
		{
			get { return this.linesField; }
			set { this.linesField = value; }
		}
		[System.Xml.Serialization.XmlIgnoreAttribute()]
		public bool linesSpecified
		{
			get { return this.linesFieldSpecified; }
			set { this.linesFieldSpecified = value; }
		}
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public orientationDraft draft
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
		public string standard
		{
			get { return this.standardField; }
			set { this.standardField = value; }
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
	}
}