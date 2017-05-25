
namespace CLDR.LocaleDataMarkupLanguage
{
	public enum ellipsisType
	{
		initial, medial, final,
		[System.Xml.Serialization.XmlEnumAttribute("word-initial")]
		wordinitial,
		[System.Xml.Serialization.XmlEnumAttribute("word-medial")]
		wordmedial,
		[System.Xml.Serialization.XmlEnumAttribute("word-final")]
		wordfinal,
	}
}