using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FontTool
{
	[System.Reflection.ObfuscationAttribute(Feature = "renaming", ApplyToMembers = true)]
	public class ImageDescriptionCollection : KeyedCollection<ulong, ImageDescription>
	{
		new public ImageDescription this[int i]
		{
			get
			{
				return ((Collection<ImageDescription>)this)[i];
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

		protected override ulong GetKeyForItem(ImageDescription item)
		{
			return item.Id;
		}

		protected override void InsertItem(int index, ImageDescription item)
		{
			if (item != null)
			{
				if (base.Dictionary != null && base.Dictionary.ContainsKey(item.Id))
				{
					int thisIndex = 0;
					foreach (ImageDescription description in base.Items)
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
