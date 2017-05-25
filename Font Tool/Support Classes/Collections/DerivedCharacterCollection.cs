using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FontTool
{
	[System.Reflection.ObfuscationAttribute(Feature = "renaming", ApplyToMembers = true)]
	public class DerivedCharacterCollection : KeyedCollection<ulong, DerivedCharacter>
	{
		public new DerivedCharacter this[ulong index] {
			get {
				return this.Dictionary[index];
			}
			set {
				Dictionary[index] = value;
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

		protected override ulong GetKeyForItem(DerivedCharacter item)
		{
			return item.Id;
		}
	}
}