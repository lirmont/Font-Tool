using System.Collections.Generic;

namespace CLDR.LocaleDataMarkupLanguage
{
	public partial class codePatterns
	{
		private object[] itemsField;

		public List<codePattern> Patterns
		{
			get
			{
				List<codePattern> list = new List<codePattern>();
				if (Items != null)
				{
					foreach (object obj in Items)
					{
						if (obj is codePatterns)
						{
							codePatterns patterns = obj as codePatterns;
							if (patterns != null)
							{
								foreach (object child in patterns.Items)
								{
									if (child is codePattern)
										list.Add((codePattern)child);
								}
							}
						}
					}
				}
				return list;
			}
		}

		[System.Xml.Serialization.XmlElementAttribute("alias", typeof(alias))]
		[System.Xml.Serialization.XmlElementAttribute("special", typeof(special))]
		[System.Xml.Serialization.XmlElementAttribute("codePattern", typeof(codePattern))]
		public object[] Items
		{
			get { return this.itemsField; }
			set { this.itemsField = value; }
		}
	}
}