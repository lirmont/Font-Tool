using System;
using System.Collections.Generic;
using System.Text;

namespace FontTool
{
	public class UnicodeBlock
	{
		ulong? start = null, end = null;
		string language = null;
		List<UnicodeCharacter> characters = new List<UnicodeCharacter>();

		public ulong? Start
		{
			get { return start; }
			set { start = value; }
		}

		public ulong? End
		{
			get { return end; }
			set { end = value; }
		}

		public string BlockName
		{
			get { return language; }
			set { language = value; }
		}

		internal List<UnicodeCharacter> Characters
		{
			get { return characters; }
			set { characters = value; }
		}

		internal List<UnicodeCharacter> PrintableCharacters
		{
			get {
				List<UnicodeCharacter> printableCharacters = new List<UnicodeCharacter>(Characters);
				printableCharacters.RemoveAll(item => (item.IsControlCharacter || item.IsSurrogate) && item.Id != 0x000009);
				return printableCharacters;
			}
		}

		public UnicodeBlock() { }

		public UnicodeBlock(string language, ulong start, ulong end)
		{
			this.start = start;
			this.end = end;
			this.language = language;
		}
	}
}
