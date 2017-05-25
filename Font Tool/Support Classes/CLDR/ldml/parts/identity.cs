using System.Xml.Serialization;

namespace CLDR.LocaleDataMarkupLanguage
{
	public partial class identity
	{
		private version versionField;
		private generation generationField;
		private language languageField;
		private territory territoryField;
		private variant variantField;
		private script scriptField;
		private alias aliasField;
		private special specialField;

		[XmlElement(ElementName = "version")]
		public version Version
		{
			get { return versionField; }
			set { versionField = value; }
		}

		[XmlElement(ElementName = "generation")]
		public generation Generation
		{
			get { return generationField; }
			set { generationField = value; }
		}

		[XmlElement(ElementName = "language")]
		public language Language
		{
			get { return languageField; }
			set { languageField = value; }
		}

		[XmlElement(ElementName = "territory")]
		public territory Territory
		{
			get { return territoryField; }
			set { territoryField = value; }
		}

		[XmlElement(ElementName = "variant")]
		public variant Variant
		{
			get { return variantField; }
			set { variantField = value; }
		}

		[XmlElement(ElementName = "script")]
		public script Script
		{
			get { return scriptField; }
			set { scriptField = value; }
		}

		[XmlElement(ElementName = "alias")]
		public alias Alias
		{
			get { return aliasField; }
			set { aliasField = value; }
		}

		[XmlElement(ElementName = "special")]
		public special Special
		{
			get { return specialField; }
			set { specialField = value; }
		}
	}
}