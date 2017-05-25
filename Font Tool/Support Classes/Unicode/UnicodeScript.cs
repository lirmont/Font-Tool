using System;
using System.Collections.Generic;
using System.Text;

namespace FontTool
{
	public class UnicodeScript
	{
		private string name;
		private Dictionary<string, List<ulong>> unicodeCharacters;

		public string Name
		{
			get { return name; }
			set { this.name = value; }
		}

		public int GetCharacterCount(List<string> ignored = null)
		{
			ignored = ignored ?? new List<string>();
			int count = 0;
			foreach (string key in unicodeCharacters.Keys)
				if (!ignored.Contains(key))
					count += unicodeCharacters[key].Count;
			return count;
		}

		public Dictionary<string, List<ulong>> UnicodeCharacters
		{
			get { return unicodeCharacters; }
			set { unicodeCharacters = value; }
		}

		public UnicodeScript(string language, Dictionary<string, List<ulong>> characters = null)
		{
			this.name = language; //.Replace("_", " ");
			this.unicodeCharacters = characters;
		}

		public void AddCharacterById(ulong id, string controlCode = "L&")
		{
			if (!this.unicodeCharacters.ContainsKey(controlCode))
				this.unicodeCharacters.Add(controlCode, new List<ulong>());
			this.unicodeCharacters[controlCode].Add(id);
		}

		public void AddCharactersByRange(ulong start, ulong end, string controlCode = "L&")
		{
			for (ulong current = start; current <= end; current++)
				AddCharacterById(current, controlCode: controlCode);
		}

		public static List<string> TwoLetterControlCodes = new List<string> { "Cc", "Cf" };
		public List<ulong> GetMissingCharacters(List<ulong> existingCharacters, bool ignoreControlCharacters = true) {
			List<ulong> list = new List<ulong>();
			foreach (string code in this.unicodeCharacters.Keys)
			{
				if (!ignoreControlCharacters || (ignoreControlCharacters && !TwoLetterControlCodes.Contains(code)))
				{
					foreach (ulong id in this.unicodeCharacters[code])
					{
						if (!existingCharacters.Contains(id))
							list.Add(id);
					}
				}
			}
			return list;
		}

		public int GetMissingCharactersCount(List<ulong> existingCharacters, bool ignoreControlCharacters = true)
		{
			int count = 0;
			foreach (string code in this.unicodeCharacters.Keys)
			{
				if (!ignoreControlCharacters || (ignoreControlCharacters && !TwoLetterControlCodes.Contains(code)))
				{
					foreach (ulong id in this.unicodeCharacters[code])
					{
						count++;
					}
				}
			}
			return count;
		}

		public List<UnicodeCharacter> GetMissingUnicodeCharacters(List<ulong> existingCharacters, bool ignoreControlCharacters = false)
		{
			List<UnicodeCharacter> list = new List<UnicodeCharacter>();
			foreach (string code in this.unicodeCharacters.Keys)
			{
				if (!ignoreControlCharacters || (ignoreControlCharacters && !TwoLetterControlCodes.Contains(code)))
				{
					foreach (ulong id in this.unicodeCharacters[code])
					{
						UnicodeCharacter character = UnicodeBlocks.GetCharacterById(id);
						if (character != null)
							list.Add(character);
					}
				}
			}
			return list;
		}

		public int Count {
			get { return GetCharacterCount(); }
		}

		public int PrintableCount {
			get { return GetCharacterCount(TwoLetterControlCodes); }
		}

		public double GetCoveragePercent(List<ulong> existingCharacters, bool ignoreControlCharacters = true) {
			List<ulong> missingList = GetMissingCharacters(existingCharacters);
			List<string> ignoreList = (ignoreControlCharacters) ? TwoLetterControlCodes : null;
			int count = GetCharacterCount(ignored: ignoreList);
			if (count > 0)
				return Math.Min(1, (count - missingList.Count) / (double)count);
			return 0;
		}
	}
}
