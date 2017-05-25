using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FontTool
{
	[System.Reflection.ObfuscationAttribute(Feature = "renaming", ApplyToMembers = true)]
	public class UnicodeScriptCollection : KeyedCollection<string, UnicodeScript>
	{
		new public UnicodeScript this[string s]
		{
			get
			{
				return GetUnicodeScript(s);
			}
		}

		private UnicodeScript GetUnicodeScript(string key)
		{
			string thisKey = key; //.Replace("_", " ");
			foreach (UnicodeScript script in (Collection<UnicodeScript>)this) {
				if (script.Name == thisKey)
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

		protected override string GetKeyForItem(UnicodeScript item)
		{
			return item.Name;
		}

		protected override void InsertItem(int index, UnicodeScript item)
		{
			if (base.Dictionary != null && base.Dictionary.ContainsKey(item.Name))
			{
				int thisIndex = 0;
				foreach (UnicodeScript script in base.Items)
				{
					if (base[thisIndex].Name == item.Name)
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
