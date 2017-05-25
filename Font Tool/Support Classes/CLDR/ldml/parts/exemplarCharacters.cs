using System.Collections.Generic;
using System.Xml.Serialization;

namespace CLDR.LocaleDataMarkupLanguage
{
	public partial class exemplarCharacters
	{
		private cp[] itemsField;
		private string[] textField;
		private exemplarCharactersType typeField;
		private bool typeFieldSpecified;
		private exemplarCharactersDraft draftField;
		private bool draftFieldSpecified;
		private string standardField;
		private string referencesField;
		private string altField;
		private string validSubLocalesField;

		[XmlIgnore]
		public List<char> Characters
		{
			get
			{
				List<char> list = new List<char>();
				foreach (string text in textField)
				{
					if (text.Length >= 2)
					{
						string subString = text.Substring(1, text.Length - 2);
						subString = subString.Replace(" ", "");
						foreach (char c in subString)
							list.Add(c);
					}
				}
				return list;
			}
		}

		[System.Xml.Serialization.XmlElementAttribute("cp")]
		public cp[] Items
		{
			get { return this.itemsField; }
			set { this.itemsField = value; }
		}

		[System.Xml.Serialization.XmlTextAttribute()]
		public string[] Text
		{
			get { return this.textField; }
			set { this.textField = value; }
		}

		[System.Xml.Serialization.XmlAttributeAttribute()]
		public exemplarCharactersType type
		{
			get { return this.typeField; }
			set { this.typeField = value; }
		}

		[System.Xml.Serialization.XmlIgnoreAttribute()]
		public bool typeSpecified
		{
			get { return this.typeFieldSpecified; }
			set { this.typeFieldSpecified = value; }
		}

		[System.Xml.Serialization.XmlAttributeAttribute()]
		public exemplarCharactersDraft draft
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

		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string validSubLocales
		{
			get { return this.validSubLocalesField; }
			set { this.validSubLocalesField = value; }
		}
	}
}