using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FontTool
{
	[System.Reflection.ObfuscationAttribute(Feature = "renaming", ApplyToMembers = true)]
	public class TracedCharacterCollection : KeyedCollection<ulong, TracedCharacter>
	{
		new public TracedCharacter this[int i]
		{
			get
			{
				return ((Collection<TracedCharacter>)this)[i];
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

		protected override ulong GetKeyForItem(TracedCharacter item)
		{
			return item.Id;
		}

		protected override void InsertItem(int index, TracedCharacter item)
		{
			if (item != null)
			{
				if (base.Dictionary != null && base.Dictionary.ContainsKey(item.Id))
				{
					int thisIndex = 0;
					foreach (TracedCharacter tracedCharacter in base.Items)
					{
						if (base[thisIndex].Id == item.Id)
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
}
