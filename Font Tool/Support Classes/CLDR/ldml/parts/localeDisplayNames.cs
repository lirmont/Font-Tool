using System.Collections.Generic;

namespace CLDR.LocaleDataMarkupLanguage
{
	public partial class localeDisplayNames
	{
		private object[] itemsField;
		private localeDisplayNamesDraft draftField;
		private bool draftFieldSpecified;

		public List<language> Languages
		{
			get
			{
				List<language> list = new List<language>();
				if (Items != null)
				{
					foreach (object obj in Items)
					{
						if (obj is languages)
						{
							languages languages = obj as languages;
							if (languages != null)
							{
								foreach (object child in languages.Items)
								{
									if (child is language)
										list.Add((language)child);
								}
							}
						}
					}
				}
				return list;
			}
		}

		public List<script> Scripts
		{
			get
			{
				List<script> list = new List<script>();
				if (Items != null)
				{
					foreach (object obj in Items)
					{
						if (obj is scripts)
						{
							scripts scripts = obj as scripts;
							if (scripts != null)
							{
								foreach (object child in scripts.Items)
								{
									if (child is script)
										list.Add((script)child);
								}
							}
						}
					}
				}
				return list;
			}
		}

		[System.Xml.Serialization.XmlIgnore()]
		public Dictionary<string, territory> Territories
		{
			get
			{
				Dictionary<string, territory> list = new Dictionary<string, territory>();
				if (Items != null)
				{
					foreach (object obj in Items)
					{
						if (obj is territories)
						{
							territories territories = obj as territories;
							if (territories != null)
							{
								foreach (object child in territories.Items)
								{
									if (child is territory) {
										territory thisChild = (territory)child;
										string key = (thisChild.alt == null) ? thisChild.type : string.Format("{0}-{1}", new object[] { thisChild.type, thisChild.alt });
										if (list.ContainsKey(thisChild.type))
											list[thisChild.type] = thisChild;
										else
											list.Add(thisChild.type, thisChild);
									}
								}
							}
						}
					}
				}
				return list;
			}
		}

		public List<variant> Variants
		{
			get
			{
				List<variant> list = new List<variant>();
				if (Items != null)
				{
					foreach (object obj in Items)
					{
						if (obj is variants)
						{
							variants variants = obj as variants;
							if (variants != null)
							{
								foreach (object child in variants.Items)
								{
									if (child is variant)
										list.Add((variant)child);
								}
							}
						}
					}
				}
				return list;
			}
		}

		public List<codePattern> CodePatterns
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

		public List<type> Types
		{
			get
			{
				List<type> list = new List<type>();
				if (Items != null)
				{
					foreach (object obj in Items)
					{
						if (obj is types)
						{
							types theseTypes = obj as types;
							if (theseTypes != null)
							{
								foreach (object child in theseTypes.Items)
								{
									if (child is type)
										list.Add((type)child);
								}
							}
						}
					}
				}
				return list;
			}
		}

		[System.Xml.Serialization.XmlElementAttribute("alias", typeof(alias))]
		[System.Xml.Serialization.XmlElementAttribute("keys", typeof(keys))]
		[System.Xml.Serialization.XmlElementAttribute("localeDisplayPattern", typeof(localeDisplayPattern))]
		[System.Xml.Serialization.XmlElementAttribute("measurementSystemNames", typeof(measurementSystemNames))]
		[System.Xml.Serialization.XmlElementAttribute("special", typeof(special))]
		[System.Xml.Serialization.XmlElementAttribute("types", typeof(types))]
		[System.Xml.Serialization.XmlElementAttribute("codePatterns", typeof(codePatterns))]
		[System.Xml.Serialization.XmlElementAttribute("variants", typeof(variants))]
		[System.Xml.Serialization.XmlElementAttribute("territories", typeof(territories))]
		[System.Xml.Serialization.XmlElementAttribute("scripts", typeof(scripts))]
		[System.Xml.Serialization.XmlElementAttribute("languages", typeof(languages))]
		[System.Xml.Serialization.XmlElementAttribute("transformNames", typeof(transformNames))]
		public object[] Items
		{
			get { return this.itemsField; }
			set { this.itemsField = value; }
		}
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public localeDisplayNamesDraft draft
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