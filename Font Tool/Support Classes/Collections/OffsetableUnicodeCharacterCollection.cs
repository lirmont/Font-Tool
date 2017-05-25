using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FontTool
{
	[System.Reflection.ObfuscationAttribute(Feature = "renaming", ApplyToMembers = true)]
	public class OffsetableUnicodeCharacterCollection : KeyedCollection<ulong, OffsetableUnicodeCharacter>
	{
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

		protected override ulong GetKeyForItem(OffsetableUnicodeCharacter item)
		{
			return item.Id;
		}
	}
}