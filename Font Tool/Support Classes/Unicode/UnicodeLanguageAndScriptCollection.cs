using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FontTool
{
	[System.Reflection.ObfuscationAttribute(Feature = "renaming", ApplyToMembers = true)]
	public class UnicodeLanguageAndScriptCollection : KeyedCollection<string, UnicodeLanguageAndScript>
	{
		new public UnicodeLanguageAndScript this[string s]
		{
			get
			{
				return GetUnicodeLanguageAndScript(s);
			}
		}

		private UnicodeLanguageAndScript GetUnicodeLanguageAndScript(string key)
		{
			string thisKey = key; //.Replace("_", " ");
			foreach (UnicodeLanguageAndScript script in (Collection<UnicodeLanguageAndScript>)this)
			{
				if (script.LanguageAndScriptCode == thisKey)
					return script;
			}
			return null;
		}

		public List<string> Keys
		{
			get
			{
				List<string> keys = new List<string>();
				if (Dictionary != null)
					foreach (string key in Dictionary.Keys)
						keys.Add(key);
				return keys;
			}
		}

		protected override string GetKeyForItem(UnicodeLanguageAndScript item)
		{
			return item.LanguageAndScriptCode;
		}

		protected override void InsertItem(int index, UnicodeLanguageAndScript item)
		{
			if (base.Dictionary != null && base.Dictionary.ContainsKey(item.LanguageAndScriptCode))
			{
				int thisIndex = 0;
				foreach (UnicodeLanguageAndScript script in base.Items)
				{
					if (base[thisIndex].LanguageAndScriptCode == item.LanguageAndScriptCode)
					{
						base[thisIndex] = item;
						return;
					}
					thisIndex++;
				}
			}
			else
				base.InsertItem(index, item);
		}
	}
}
