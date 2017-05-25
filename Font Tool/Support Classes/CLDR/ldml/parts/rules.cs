
namespace CLDR.LocaleDataMarkupLanguage
{
	public partial class rules
	{
		private object[] itemsField;

		[System.Xml.Serialization.XmlElementAttribute("alias", typeof(alias))]
		[System.Xml.Serialization.XmlElementAttribute("i", typeof(i))]
		[System.Xml.Serialization.XmlElementAttribute("ic", typeof(ic))]
		[System.Xml.Serialization.XmlElementAttribute("import", typeof(import))]
		[System.Xml.Serialization.XmlElementAttribute("p", typeof(p))]
		[System.Xml.Serialization.XmlElementAttribute("pc", typeof(pc))]
		[System.Xml.Serialization.XmlElementAttribute("q", typeof(q))]
		[System.Xml.Serialization.XmlElementAttribute("qc", typeof(qc))]
		[System.Xml.Serialization.XmlElementAttribute("reset", typeof(reset))]
		[System.Xml.Serialization.XmlElementAttribute("s", typeof(s))]
		[System.Xml.Serialization.XmlElementAttribute("sc", typeof(sc))]
		[System.Xml.Serialization.XmlElementAttribute("t", typeof(t))]
		[System.Xml.Serialization.XmlElementAttribute("tc", typeof(tc))]
		[System.Xml.Serialization.XmlElementAttribute("x", typeof(x))]
		public object[] Items
		{
			get { return this.itemsField; }
			set { this.itemsField = value; }
		}
	}

}