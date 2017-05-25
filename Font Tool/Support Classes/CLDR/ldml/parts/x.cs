
namespace CLDR.LocaleDataMarkupLanguage
{
	public partial class x
	{
		private context contextField;
		private object[] itemsField;
		private extend extendField;

		public context context
		{
			get { return this.contextField; }
			set { this.contextField = value; }
		}
		[System.Xml.Serialization.XmlElementAttribute("i", typeof(i))]
		[System.Xml.Serialization.XmlElementAttribute("ic", typeof(ic))]
		[System.Xml.Serialization.XmlElementAttribute("p", typeof(p))]
		[System.Xml.Serialization.XmlElementAttribute("pc", typeof(pc))]
		[System.Xml.Serialization.XmlElementAttribute("q", typeof(q))]
		[System.Xml.Serialization.XmlElementAttribute("qc", typeof(qc))]
		[System.Xml.Serialization.XmlElementAttribute("s", typeof(s))]
		[System.Xml.Serialization.XmlElementAttribute("sc", typeof(sc))]
		[System.Xml.Serialization.XmlElementAttribute("t", typeof(t))]
		[System.Xml.Serialization.XmlElementAttribute("tc", typeof(tc))]
		public object[] Items
		{
			get { return this.itemsField; }
			set { this.itemsField = value; }
		}
		public extend extend
		{
			get { return this.extendField; }
			set { this.extendField = value; }
		}
	}
}