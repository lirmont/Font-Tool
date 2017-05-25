
namespace CLDR.LocaleDataMarkupLanguage
{
	public partial class settings
	{
		private special[] specialField;
		private settingsStrength strengthField;
		private bool strengthFieldSpecified;
		private settingsAlternate alternateField;
		private bool alternateFieldSpecified;
		private settingsBackwards backwardsField;
		private bool backwardsFieldSpecified;
		private settingsNormalization normalizationField;
		private bool normalizationFieldSpecified;
		private settingsCaseLevel caseLevelField;
		private bool caseLevelFieldSpecified;
		private settingsCaseFirst caseFirstField;
		private bool caseFirstFieldSpecified;
		private settingsHiraganaQuarternary hiraganaQuarternaryField;
		private bool hiraganaQuarternaryFieldSpecified;
		private settingsHiraganaQuaternary hiraganaQuaternaryField;
		private bool hiraganaQuaternaryFieldSpecified;
		private settingsNumeric numericField;
		private bool numericFieldSpecified;
		private settingsPrivate privateField;
		private bool privateFieldSpecified;
		private string variableTopField;
		private string reorderField;

		[System.Xml.Serialization.XmlElementAttribute("special")]
		public special[] special
		{
			get { return this.specialField; }
			set { this.specialField = value; }
		}
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public settingsStrength strength
		{
			get { return this.strengthField; }
			set { this.strengthField = value; }
		}
		[System.Xml.Serialization.XmlIgnoreAttribute()]
		public bool strengthSpecified
		{
			get { return this.strengthFieldSpecified; }
			set { this.strengthFieldSpecified = value; }
		}
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public settingsAlternate alternate
		{
			get { return this.alternateField; }
			set { this.alternateField = value; }
		}
		[System.Xml.Serialization.XmlIgnoreAttribute()]
		public bool alternateSpecified
		{
			get { return this.alternateFieldSpecified; }
			set { this.alternateFieldSpecified = value; }
		}
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public settingsBackwards backwards
		{
			get { return this.backwardsField; }
			set { this.backwardsField = value; }
		}
		[System.Xml.Serialization.XmlIgnoreAttribute()]
		public bool backwardsSpecified
		{
			get { return this.backwardsFieldSpecified; }
			set { this.backwardsFieldSpecified = value; }
		}
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public settingsNormalization normalization
		{
			get { return this.normalizationField; }
			set { this.normalizationField = value; }
		}
		[System.Xml.Serialization.XmlIgnoreAttribute()]
		public bool normalizationSpecified
		{
			get { return this.normalizationFieldSpecified; }
			set { this.normalizationFieldSpecified = value; }
		}
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public settingsCaseLevel caseLevel
		{
			get { return this.caseLevelField; }
			set { this.caseLevelField = value; }
		}
		[System.Xml.Serialization.XmlIgnoreAttribute()]
		public bool caseLevelSpecified
		{
			get { return this.caseLevelFieldSpecified; }
			set { this.caseLevelFieldSpecified = value; }
		}
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public settingsCaseFirst caseFirst
		{
			get { return this.caseFirstField; }
			set { this.caseFirstField = value; }
		}
		[System.Xml.Serialization.XmlIgnoreAttribute()]
		public bool caseFirstSpecified
		{
			get { return this.caseFirstFieldSpecified; }
			set { this.caseFirstFieldSpecified = value; }
		}
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public settingsHiraganaQuarternary hiraganaQuarternary
		{
			get { return this.hiraganaQuarternaryField; }
			set { this.hiraganaQuarternaryField = value; }
		}
		[System.Xml.Serialization.XmlIgnoreAttribute()]
		public bool hiraganaQuarternarySpecified
		{
			get { return this.hiraganaQuarternaryFieldSpecified; }
			set { this.hiraganaQuarternaryFieldSpecified = value; }
		}
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public settingsHiraganaQuaternary hiraganaQuaternary
		{
			get { return this.hiraganaQuaternaryField; }
			set { this.hiraganaQuaternaryField = value; }
		}
		[System.Xml.Serialization.XmlIgnoreAttribute()]
		public bool hiraganaQuaternarySpecified
		{
			get { return this.hiraganaQuaternaryFieldSpecified; }
			set { this.hiraganaQuaternaryFieldSpecified = value; }
		}
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public settingsNumeric numeric
		{
			get { return this.numericField; }
			set { this.numericField = value; }
		}
		[System.Xml.Serialization.XmlIgnoreAttribute()]
		public bool numericSpecified
		{
			get { return this.numericFieldSpecified; }
			set { this.numericFieldSpecified = value; }
		}
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public settingsPrivate @private
		{
			get { return this.privateField; }
			set { this.privateField = value; }
		}
		[System.Xml.Serialization.XmlIgnoreAttribute()]
		public bool privateSpecified
		{
			get { return this.privateFieldSpecified; }
			set { this.privateFieldSpecified = value; }
		}
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string variableTop
		{
			get { return this.variableTopField; }
			set { this.variableTopField = value; }
		}
		[System.Xml.Serialization.XmlAttributeAttribute(DataType = "NMTOKENS")]
		public string reorder
		{
			get { return this.reorderField; }
			set { this.reorderField = value; }
		}
	}
}