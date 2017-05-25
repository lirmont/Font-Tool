using System;
using System.Collections.Generic;
using System.Text;
using LumenWorks.Framework.IO.Csv;
using System.IO;
using System.Globalization;

namespace FontTool
{
	public static class UnicodeScripts
	{
		private static UnicodeScriptCollection scripts = new UnicodeScriptCollection();

		public static UnicodeScriptCollection Scripts
		{
			get { return UnicodeScripts.scripts; }
			set { UnicodeScripts.scripts = value; }
		}

		public static UnicodeScript GetScriptForLanguage(string language)
		{
			if (scripts.Contains(language))
				return scripts[language];
			return null;
		}

		static UnicodeScripts() {
			// Open the file "Scripts.txt" which is a CSV file (no headers).
			using (CsvReader csv = new CsvReader(new StreamReader(SupportFunctions.Combine("Unicode Specification", "Scripts.txt")), false, ';', '"', '\\', '#', ValueTrimmingOptions.UnquotedOnly))
			{
				csv.SkipEmptyLines = true;
				ulong? start = null;
				ulong? end = null;
				string scriptName = null;
				string controlCode = null;
				//
				while (csv.ReadNextRecord())
				{
					bool success = false;
					try
					{
						parse(csv, ref start, ref end, ref scriptName, ref controlCode);
						success = true;
					}
					catch { }
					if (success)
					{
						UnicodeScript script = null;
						if (!scripts.Contains(scriptName))
							scripts.Add(new UnicodeScript(scriptName, characters: new Dictionary<string, List<ulong>>()));
						script = scripts[scriptName];
						//
						if (script != null)
						{
							if (start != null && end != null)
								script.AddCharactersByRange(start.Value, end.Value, controlCode: controlCode);
							else if (start != null)
								script.AddCharacterById(start.Value, controlCode: controlCode);
						}
					}
					//
					start = end = null;
					scriptName = null;
					controlCode = null;
				}
			}
		}


		private static void parse(CsvReader csv, ref ulong? start, ref ulong? end, ref string language, ref string controlCode)
		{
			for (int i = 1; i >= 0; i--)
			{
				switch (i)
				{
					case 0:
						string s = csv[0];
						bool isRange = s.Contains("..");
						if (isRange)
						{
							string[] split = s.Split(new string[] { ".." }, StringSplitOptions.RemoveEmptyEntries);
							ulong thisStart = 0, thisEnd = 0;
							bool startSuccess = ulong.TryParse(split[0], NumberStyles.HexNumber, null, out thisStart);
							bool endSuccess = ulong.TryParse(split[1], NumberStyles.HexNumber, null, out thisEnd);
							if (startSuccess)
								start = thisStart;
							if (endSuccess)
								end = thisEnd;
						}
						else
						{
							ulong thisStart = 0;
							bool startSuccess = ulong.TryParse(s, NumberStyles.HexNumber, null, out thisStart);
							if (startSuccess)
								start = thisStart;
						}
						break;
					case 1:
						language = csv[1];
						bool hasComment = language.Contains("#");
						if (hasComment)
						{
							int index = language.IndexOf("#");
							controlCode = language.Substring(index + 2, 2);
							language = language.Substring(0, index);
							language = language.Trim();
						}
						break;
				}
			}
		}
	}
}
