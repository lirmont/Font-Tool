using System;
using System.Collections.Generic;
using System.Text;

namespace FontTool
{
	public static partial class SupportFunctions
	{
		public static DerivedCharacterCollection GenerateDerivedCharacterCollection(CharacterCollection baseCollection, BitmapVariant variant)
		{
			DerivedCharacterCollection collection = new DerivedCharacterCollection();
			if (baseCollection != null)
				foreach (ulong id in baseCollection.Keys)
					SynchronizeCharacterBetweenCollections(id, baseCollection, variant, ref collection, forceAdd: true);
			return collection;
		}

		public static void SynchronizeCharacterBetweenCollections(ulong id, CharacterCollection baseCollection, BitmapVariant variant, ref DerivedCharacterCollection collection, bool forceAdd = false)
		{
			Character character = baseCollection[id];
			if ((forceAdd && !collection.Contains(id)) || !collection.Contains(id))
				collection.Add(new DerivedCharacter(character));
			else {
				collection[id] = new DerivedCharacter(character);
			}
		}
	}
}
