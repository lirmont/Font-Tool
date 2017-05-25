using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FontTool
{
	[System.Reflection.ObfuscationAttribute(Feature = "renaming", ApplyToMembers = true)]
	public class UlongCollection : KeyedCollection<ulong, ulong>
	{
		new public ulong this[int i]
		{
			get
			{
				return ((Collection<ulong>)this)[i];
			}
		}

		public List<ulong> Keys
		{
			get
			{
				List<ulong> keys = new List<ulong>();
				if (base.Dictionary != null)
					foreach (uint key in base.Dictionary.Keys)
						keys.Add(key);
				return keys;
			}
		}

		protected override ulong GetKeyForItem(ulong item)
		{
			return item;
		}

		protected override void InsertItem(int index, ulong item)
		{
			if (base.Dictionary != null && base.Dictionary.ContainsKey(item))
			{
				int thisIndex = 0;
				foreach (ulong thisItem in base.Items)
				{
					if (base[thisIndex] == thisItem)
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
