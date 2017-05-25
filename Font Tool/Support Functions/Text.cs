using System.Collections.Generic;

namespace FontTool
{
	public partial class SupportFunctions
	{
		public delegate void ProgressUpdater(string text);

		public static string GetUPlusString(UnicodeCharacter character)
		{
			return string.Format("U+{0:X} ({0})", character.Id);
		}

		// Given base name, get: BaseName, BaseName1, BaseName2, ..., BaseNameN.
		public static List<string> GetRangeOfNames(string baseName, int number)
		{
			List<string> list = new List<string>();
			for (int i = 0; i < number; i++)
				list.Add((i != 0) ? string.Format("{0}{1}", new object[] { baseName, i }) : baseName);
			return list;
		}
	}
}
