using System.Collections.Specialized;
using System.Collections.Generic;
using System;

namespace FontTool
{
	public class CharactersByBlock : OrderedDictionary
	{
		public List<KeyValuePair<UnicodeBlock, CharacterCollection>> Entries
		{
			get {
				List<KeyValuePair<UnicodeBlock, CharacterCollection>> entries = new List<KeyValuePair<UnicodeBlock, CharacterCollection>>();
				var enumerator = GetEnumerator();
				while(enumerator.MoveNext()) {
					entries.Add(
						new KeyValuePair<UnicodeBlock, CharacterCollection> (
							(UnicodeBlock)enumerator.Key,
							(CharacterCollection)enumerator.Value
						)
					);
				}
				return entries;
			}
		}

		// Convenience method to treat all characters as a continuous collection for the purpose of splitting them into a number of arbitrarily sized groups (because Panda3d cannot load lage model files on Windows platforms).
		public CharactersByEGGFile getEggFiles(int characterCutOff = 550)
		{
			return SliceCharactersIntoGroups(this, characterCutOff: characterCutOff);
		}

		// Yield a list of keys ("", ".1", ".2", ".3", etc.) for a list of character collections by unicode block.
		private static CharactersByEGGFile SliceCharactersIntoGroups(CharactersByBlock allCharacters, int characterCutOff = 550)
		{
			//
			CharactersByEGGFile eggFiles = new CharactersByEGGFile();
			int currentCharacters = 0;
			//
			int valueIndex = 0;
			string fileExtensionAsCurrentIndex = "";
			// Add the base font (i.e. the font that will not have a number attached to it).
			eggFiles.Add(fileExtensionAsCurrentIndex, new CharactersByBlock());
			// Slice the major list into minor lists, cut by the amount of charactes to export per file.
			foreach (KeyValuePair<UnicodeBlock, CharacterCollection> block in allCharacters.Entries)
			{
				CharacterCollection charactersInThisBlock = block.Value;
				while (charactersInThisBlock.Count > 0)
				{
					CharactersByBlock currentBlock = (CharactersByBlock)eggFiles[fileExtensionAsCurrentIndex];
					//
					int sizeAfterAdd = currentCharacters + charactersInThisBlock.Count;
					if (sizeAfterAdd > characterCutOff)
					{
						// Get size.
						int spaceAvailable = characterCutOff - currentCharacters;
						CharacterCollection leftList = SliceOffsetableUnicodeCharacterList(charactersInThisBlock, spaceAvailable);
						// Add to current list.
						currentBlock.Add(
							block.Key,
							leftList
						);
						// Move to next list.
						valueIndex++;
						// Yield: .1, .2, .3, .4, .5, etc.
						fileExtensionAsCurrentIndex = string.Format(".{0}", valueIndex);
						// Create next list.
						eggFiles.Add(fileExtensionAsCurrentIndex, new CharactersByBlock());
						currentCharacters = 0;
						CharacterCollection rightList = SliceOffsetableUnicodeCharacterList(charactersInThisBlock, charactersInThisBlock.Count - spaceAvailable, startIndex: spaceAvailable);
						charactersInThisBlock = rightList;
					}
					else
					{
						// Copy block.
						currentBlock.Add(
							block.Key,
							charactersInThisBlock
						);
						currentCharacters += charactersInThisBlock.Count;
						// Advance the loop into the next state.
						if (sizeAfterAdd == characterCutOff)
						{
							valueIndex++;
							// Yield: .1, .2, .3, .4, .5, etc.
							fileExtensionAsCurrentIndex = string.Format(".{0}", valueIndex);
							eggFiles.Add(fileExtensionAsCurrentIndex, new CharactersByBlock());
							currentCharacters = 0;
						}
						break;
					}
				}
			}
			return eggFiles;
		}

		private static CharacterCollection SliceOffsetableUnicodeCharacterList(CharacterCollection characters, int amountToSlice, int startIndex = 0)
		{
			int thisAmountToSlice = Math.Min(characters.Count - startIndex, amountToSlice);
			CharacterCollection collection = new CharacterCollection();
			// Effectively: characters.CopyTo(startIndex, array, 0, thisAmountToSlice);
			for (int index = startIndex; index < Math.Min(thisAmountToSlice, characters.Count); index++)
				collection.Add(characters[index]);
			return collection;
		}
	}
}
