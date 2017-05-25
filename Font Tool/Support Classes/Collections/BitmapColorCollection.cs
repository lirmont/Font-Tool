using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace FontTool
{
	[System.Reflection.ObfuscationAttribute(Feature = "renaming", ApplyToMembers = true)]
	public class BitmapColorCollection : KeyedCollection<string, BitmapColor>
	{
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

		protected override string GetKeyForItem(BitmapColor item)
		{
			return item.Name;
		}

		protected override void InsertItem(int index, BitmapColor item)
		{
			if (base.Dictionary != null && item.Name != null && base.Dictionary.ContainsKey(item.Name))
			{
				int thisIndex = 0;
				foreach (BitmapColor variant in base.Items)
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