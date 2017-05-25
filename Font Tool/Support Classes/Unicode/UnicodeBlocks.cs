using System;
using System.IO;
using LumenWorks.Framework.IO.Csv;
using System.Collections.Generic;
using System.Globalization;

namespace FontTool
{
	public static class UnicodeBlocks
	{
		public static List<UnicodeBlock> Blocks
		{
			get { return blocks; }
		}

		private static List<UnicodeBlock> blocks = new List<UnicodeBlock>();

		public static UnicodeBlock GetBlockForCharacter(char c) {
			return GetBlockForCharacter((ulong)c);
		}

		public static UnicodeBlock GetBlockForCharacter(ulong value)
		{
			return blocks.FindLast(item => item.Start <= value && item.End >= value);
		}

		static UnicodeBlocks() {
			// Open the file "data.csv" which is a CSV file (no headers).
			using (CsvReader csv = new CsvReader(new StreamReader(SupportFunctions.Combine("Unicode Specification", "blocks.txt")), false, ';'))
			{
				int fieldCount = csv.FieldCount;
				ulong? start = null;
				ulong? end = null;
				string language = null;
				//
				while (csv.ReadNextRecord())
				{
					for (int i = 0; i < fieldCount; i++)
					{
						switch (i) {
							case 0:
								string s = csv[0];
								string[] split = s.Split(new string[] {".."}, StringSplitOptions.RemoveEmptyEntries);
								ulong thisStart = 0, thisEnd = 0;
								bool startSuccess = ulong.TryParse(split[0], NumberStyles.HexNumber, null, out thisStart);
								bool endSuccess = ulong.TryParse(split[1], NumberStyles.HexNumber, null, out thisEnd);
								if (startSuccess)
									start = thisStart;
								if (endSuccess)
									end = thisEnd;
								break;
							case 1:
								language = csv[1];
								break;
						}
					}
					if (start != null && end != null)
						blocks.Add(new UnicodeBlock(language, start.Value, end.Value));
				}
			}
			// Open the file "data.csv" which is a CSV file (no headers).
			using (CsvReader csv = new CsvReader(new StreamReader(SupportFunctions.Combine("Unicode Specification", "data.txt")), false, ';'))
			{
				int fieldCount = csv.FieldCount;
				ulong? id = null;
				string primaryName = null;
				string secondaryName = null;
				//
				UnicodeBlock blockForImplicitRange = null;
				ulong? startRange = null;
				ulong? endRange = null;
				//
				while (csv.ReadNextRecord())
				{
					for (int i = 0; i < fieldCount; i++)
					{
						switch (i)
						{
							case 0:
								string s = csv[0];
								ulong thisId = 0;
								bool idSuccess = ulong.TryParse(csv[0], NumberStyles.HexNumber, null, out thisId);
								if (idSuccess)
									id = thisId;
								break;
							case 1:
								primaryName = csv[1];
								if (primaryName.Contains(", First>"))
								{
									startRange = id;
									blockForImplicitRange = GetBlockForCharacter(id.Value);
								}
								else if (primaryName.Contains(", Last>"))
									endRange = id;
								break;
							case 10:
								secondaryName = csv[10];
								break;
						}
					}
					if (blockForImplicitRange == null && id != null) {
						UnicodeBlock block = GetBlockForCharacter(id.Value);
						if (block != null)
							block.Characters.Add(new UnicodeCharacter(id.Value, primaryName, secondaryName, block));
					}
					else if (blockForImplicitRange != null && startRange != null && endRange != null) {
						for (ulong current = startRange.Value; current < endRange.Value; current++) {
							blockForImplicitRange.Characters.Add(new UnicodeCharacter(current, string.Format("{0} #{1}", new object[] { blockForImplicitRange.BlockName, current }), null, blockForImplicitRange));
						}
						blockForImplicitRange = null;
						startRange = null;
						endRange = null;
					}
				}
			}
		}

		public static void List(List<string> names = null, bool listCharacters = false, bool suppressRangeData = true) {
			List<UnicodeBlock> foundBlocks = GetBlocksByName(names);
			foreach (UnicodeBlock block in foundBlocks) {
				if (suppressRangeData)
					Console.WriteLine("{0}", block.Start, block.End, block.BlockName);
				else
					Console.WriteLine("{0} {1} {2}", block.Start, block.End, block.BlockName);
				if (listCharacters)
					foreach (UnicodeCharacter character in block.Characters) {
						Console.WriteLine("\t{0}: {1}", character.Id, character.PrimaryName);
					}
			}
		}

		public static List<UnicodeBlock> GetBlocksByName(List<string> names = null)
		{
			List<UnicodeBlock> foundBlocks = new List<UnicodeBlock>();
			if (names != null)
				foundBlocks = blocks.FindAll(item => names.Contains(item.BlockName));
			else
				foundBlocks = blocks;
			//
			return foundBlocks;
		}

		public static UnicodeBlock GetBlockByName(string name)
		{
			List<UnicodeBlock> blocks = UnicodeBlocks.GetBlocksByName(name);
			return (blocks.Count > 0) ? blocks[0] : null;
		}

		public static List<UnicodeBlock> GetBlocksByName(string name)
		{
			return GetBlocksByName(new List<string> { name });
		}

		public static UnicodeCharacter GetCharacterById(ulong id)
		{
			UnicodeCharacter character = null;
			UnicodeBlock foundBlock = blocks.FindLast(item => item.Start <= id && item.End >= id);
			if (foundBlock != null)
				character = foundBlock.Characters.FindLast(item => item.Id == id);
			return character;
		}
	}
}
