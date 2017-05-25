using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FontTool
{
	[System.Reflection.ObfuscationAttribute(Feature = "renaming", ApplyToMembers = true)]
	public class SizeCollection : KeyedCollection<float?, FontInSize>
	{
		new public FontInSize this[int i]
		{
			get
			{
				return ((Collection<FontInSize>)this)[i];
			}
		}

		public List<float?> Keys {
			get {
				List<float?> keys = new List<float?>();
				if (base.Dictionary != null)
					foreach (float? key in base.Dictionary.Keys)
						keys.Add(key);
				return keys;
			}
		}

		protected override float? GetKeyForItem(FontInSize item)
		{
			return item.Size;
		}

		protected override void InsertItem(int index, FontInSize item)
		{
			if (base.Dictionary != null && base.Dictionary.ContainsKey(item.Size))
			{
				int thisIndex = 0;
				foreach (FontInSize fontInSize in base.Items)
				{
					if (base[thisIndex].Size == item.Size)
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
