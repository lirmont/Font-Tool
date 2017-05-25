
namespace CLDR.LocaleDataMarkupLanguage
{
	public partial class indexLabels
	{
		private indexSeparator[] indexSeparatorField;
		private compressedIndexSeparator[] compressedIndexSeparatorField;
		private indexRangePattern[] indexRangePatternField;
		private indexLabelBefore[] indexLabelBeforeField;
		private indexLabelAfter[] indexLabelAfterField;
		private indexLabel[] indexLabelField;

		[System.Xml.Serialization.XmlElementAttribute("indexSeparator")]
		public indexSeparator[] indexSeparator
		{
			get { return this.indexSeparatorField; }
			set { this.indexSeparatorField = value; }
		}
		[System.Xml.Serialization.XmlElementAttribute("compressedIndexSeparator")]
		public compressedIndexSeparator[] compressedIndexSeparator
		{
			get { return this.compressedIndexSeparatorField; }
			set { this.compressedIndexSeparatorField = value; }
		}
		[System.Xml.Serialization.XmlElementAttribute("indexRangePattern")]
		public indexRangePattern[] indexRangePattern
		{
			get { return this.indexRangePatternField; }
			set { this.indexRangePatternField = value; }
		}
		[System.Xml.Serialization.XmlElementAttribute("indexLabelBefore")]
		public indexLabelBefore[] indexLabelBefore
		{
			get { return this.indexLabelBeforeField; }
			set { this.indexLabelBeforeField = value; }
		}
		[System.Xml.Serialization.XmlElementAttribute("indexLabelAfter")]
		public indexLabelAfter[] indexLabelAfter
		{
			get { return this.indexLabelAfterField; }
			set { this.indexLabelAfterField = value; }
		}
		[System.Xml.Serialization.XmlElementAttribute("indexLabel")]
		public indexLabel[] indexLabel
		{
			get { return this.indexLabelField; }
			set { this.indexLabelField = value; }
		}
	}
}