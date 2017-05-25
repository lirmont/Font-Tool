using System.Collections.Generic;
using System.Xml.Serialization;

namespace CLDR.LocaleDataMarkupLanguage
{
	public partial class characters
	{
		private object[] itemsField;
		private charactersDraft draftField;
		private bool draftFieldSpecified;

		[XmlIgnore]
		public List<exemplarCharacters> ExemplarCharacters
		{
			get
			{
				List<exemplarCharacters> list = new List<exemplarCharacters>();
				foreach (object item in itemsField)
				{
					if (item is exemplarCharacters)
						list.Add((exemplarCharacters)item);
				}
				return list;
			}
		}

		[XmlIgnore]
		public exemplarCharacters StandardCharacters
		{
			get
			{
				List<exemplarCharacters> list = ExemplarCharacters;
				if (list.Exists(itemsField => itemsField.type == exemplarCharactersType.standard))
					return list.FindLast(itemsField => itemsField.type == exemplarCharactersType.standard);
				return null;
			}
		}

		[XmlIgnore]
		public exemplarCharacters IndexCharacters
		{
			get
			{
				List<exemplarCharacters> list = ExemplarCharacters;
				if (list.Exists(itemsField => itemsField.type == exemplarCharactersType.index))
					return list.FindLast(itemsField => itemsField.type == exemplarCharactersType.index);
				return null;
			}
		}

		[XmlIgnore]
		public exemplarCharacters AuxiliaryCharacters
		{
			get
			{
				List<exemplarCharacters> list = ExemplarCharacters;
				if (list.Exists(itemsField => itemsField.type == exemplarCharactersType.auxiliary))
					return list.FindLast(itemsField => itemsField.type == exemplarCharactersType.auxiliary);
				return null;
			}
		}

		[XmlIgnore]
		public exemplarCharacters PunctuationCharacters
		{
			get
			{
				List<exemplarCharacters> list = ExemplarCharacters;
				if (list.Exists(itemsField => itemsField.type == exemplarCharactersType.punctuation))
					return list.FindLast(itemsField => itemsField.type == exemplarCharactersType.punctuation);
				return null;
			}
		}

		[XmlIgnore]
		public exemplarCharacters CurrencySymbolCharacters
		{
			get
			{
				List<exemplarCharacters> list = ExemplarCharacters;
				if (list.Exists(itemsField => itemsField.type == exemplarCharactersType.currencySymbol))
					return list.FindLast(itemsField => itemsField.type == exemplarCharactersType.currencySymbol);
				return null;
			}
		}

		[System.Xml.Serialization.XmlElementAttribute("alias", typeof(alias))]
		[System.Xml.Serialization.XmlElementAttribute("exemplarCharacters", typeof(exemplarCharacters))]
		[System.Xml.Serialization.XmlElementAttribute("ellipsis", typeof(ellipsis))]
		[System.Xml.Serialization.XmlElementAttribute("indexLabels", typeof(indexLabels))]
		[System.Xml.Serialization.XmlElementAttribute("mapping", typeof(mapping))]
		[System.Xml.Serialization.XmlElementAttribute("moreInformation", typeof(moreInformation))]
		[System.Xml.Serialization.XmlElementAttribute("special", typeof(special))]
		[System.Xml.Serialization.XmlElementAttribute("stopwords", typeof(stopwords))]
		public object[] Items
		{
			get { return this.itemsField; }
			set { this.itemsField = value; }
		}

		[System.Xml.Serialization.XmlAttributeAttribute()]
		public charactersDraft draft
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
	}
}