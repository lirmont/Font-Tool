using System.Collections.Specialized;
using System.Collections.Generic;

namespace FontTool
{
	public class CharactersByEGGFile : OrderedDictionary
	{
		public List<KeyValuePair<string, CharactersByBlock>> Entries
		{
			get
			{
				List<KeyValuePair<string, CharactersByBlock>> entries = new List<KeyValuePair<string, CharactersByBlock>>();
				var enumerator = GetEnumerator();
				while (enumerator.MoveNext())
				{
					entries.Add(
						new KeyValuePair<string, CharactersByBlock>(
							(string)enumerator.Key,
							(CharactersByBlock)enumerator.Value
						)
					);
				}
				return entries;
			}
		}
	}
}
