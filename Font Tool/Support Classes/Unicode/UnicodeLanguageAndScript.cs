using System;
using System.Collections.Generic;
using System.Text;

namespace FontTool
{
	public class UnicodeLanguageAndScript
	{
		private string languageType;
		private string scriptType;
		private string status;
		private string references;
		private bool? hasLDML = null;
		private CLDR.LocaleDataMarkupLanguage.ldml ldml = null;

		public string LanguageType
		{
			get { return languageType; }
			set { languageType = value; }
		}

		public string ScriptType
		{
			get { return scriptType; }
			set { scriptType = value; }
		}

		public string Status
		{
			get { return status; }
			set { status = value; }
		}

		public string References
		{
			get { return references; }
			set { references = value; }
		}

		public bool? HasLDML
		{
			get { return hasLDML; }
			set { hasLDML = value; }
		}

		public string LanguageAndScriptCode
		{
			get {
				return string.Format("{0}_{1}", new object[] { LanguageType, ScriptType });
			}
		}

		public CLDR.LocaleDataMarkupLanguage.ldml LocaleDataMarkupLanguage
		{
			get {
				if (ldml == null && HasLDML != false) {
					// Get language with explicit script.
					ldml = SupportFunctions.GetLanguageScriptTerritory(LanguageAndScriptCode);
					// If that doesn't exist, fall back to just the language type.
					if (ldml == null)
						ldml = SupportFunctions.GetLanguageScriptTerritory(LanguageType);
					// If neither of those exist, it doesn't exist.
					if (ldml == null)
						HasLDML = false;
				}
				return ldml;
			}
			set { ldml = value; }
		}

		public CLDR.LocaleDataMarkupLanguage.ldml this[string code]{
			get {
				// Try to get {0}_{1}_{2}, language-type, script-type, territory-type.
				CLDR.LocaleDataMarkupLanguage.ldml ldml = SupportFunctions.GetLanguageScriptTerritory(string.Format("{0}_{1}", new object[] { LanguageAndScriptCode, code }));
				// If that doesn't exist, just try to get {0}_{1}, language-type, territory-type.
				if (ldml == null)
					ldml = SupportFunctions.GetLanguageScriptTerritory(string.Format("{0}_{1}", new object[] { LanguageType, code }));
				// Send it back.
				return ldml;
			}
		}

		public UnicodeLanguageAndScript(string languageType, string scriptType, string status = "primary", string references = "")
		{
			// Store language type.
			this.languageType = languageType;
			// Store script type.
			this.scriptType = scriptType;
			// Store status (enum?): primary or secondary.
			this.status = SupportFunctions.TitleCaseString(status);
			// Store references text.
			this.references = references;
		}
	}
}
