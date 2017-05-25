using System;
using System.Collections.Generic;
using System.Text;
using LumenWorks.Framework.IO.Csv;
using System.IO;

namespace FontTool
{
	public static class UnicodeLanguageAndScripts
	{
		private static UnicodeLanguageAndScriptCollection languageAndScripts = new UnicodeLanguageAndScriptCollection();

		public static UnicodeLanguageAndScriptCollection LanguageAndScripts
		{
			get { return UnicodeLanguageAndScripts.languageAndScripts; }
			set { UnicodeLanguageAndScripts.languageAndScripts = value; }
		}

		static UnicodeLanguageAndScripts()
		{
			// Open the file "language_script_raw.txt" which is a CSV file (no headers, comment for first line).
			using (CsvReader csv = new CsvReader(new StreamReader(SupportFunctions.Combine("Unicode Specification", "language_script_raw.txt")), false, '\t'))
			{
				csv.SkipEmptyLines = true;
				// #Lcode	LanguageName	Status	Scode	ScriptName	References
				string languageCode, languageName, status, scriptCode, scriptName, references;
				//
				while (csv.ReadNextRecord())
				{
					try
					{
						languageCode = csv[0];
						languageName = csv[1];
						status = csv[2];
						scriptCode = csv[3];
						scriptName = csv[4];
						references = csv[5];
						//
						string key = string.Format("{0}_{1}", new object[] { languageCode, scriptCode });
						if (!languageAndScripts.Contains(key))
							languageAndScripts.Add(new UnicodeLanguageAndScript(languageCode, scriptCode, status: status, references: references));
					}
					catch { }
					//
					languageCode = null;
					languageName = null;
					status = null;
					scriptCode = null;
					scriptName = null;
					references = null;
				}
			}
		}
	}
}
