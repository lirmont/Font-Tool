using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FontTool
{
	[System.Reflection.ObfuscationAttribute(Feature = "renaming", ApplyToMembers = true)]
	public class CharacterCollection : KeyedCollection<ulong, Character>
	{
		new public Character this[int i]
		{
			get
			{
				return ((Collection<Character>)this)[i];
			}
		}

		public List<ulong> Keys
		{
			get
			{
				List<ulong> keys = new List<ulong>();
				if (Dictionary != null)
					foreach (ulong key in Dictionary.Keys)
						keys.Add(key);
				return keys;
			}
		}

		protected override ulong GetKeyForItem(Character item)
		{
			return item.Code;
		}

		protected override void InsertItem(int index, Character item)
		{
			if (base.Dictionary != null && base.Dictionary.ContainsKey(item.Code))
			{
				int thisIndex = 0;
				foreach (Character character in base.Items)
				{
					if (base[thisIndex].Code == item.Code)
					{
						SetItem(thisIndex, item);
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
