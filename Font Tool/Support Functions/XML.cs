using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Xsl;

namespace FontTool
{
	public partial class SupportFunctions
	{
		public static void TransformXML(XmlReader xml, string outputPath)
		{
			using (Stream stream = new MemoryStream(Properties.Resources.SortedFont))
			{
				using (XmlReader reader = XmlReader.Create(stream))
				{
					// Create XSLT transformer.
					XslCompiledTransform transformer = new XslCompiledTransform();
					transformer.Load(reader);
					// Do transform, writing file to file system.
					using (XmlWriter writer = XmlWriter.Create(outputPath, transformer.OutputSettings))
					{
						transformer.Transform(xml, writer);
					}
				}
			}
		}

		public static CLDR.SupplementalDataLocaleDataMarkupLanguage.supplementalData GetSupplementalData()
		{
			string supplementalDataXML = "supplementalData.xml";
			string currentDirectory = Environment.CurrentDirectory;
			string basePath = SupportFunctions.Combine("CLDR", "common", "supplemental");
			string xmlPath = SupportFunctions.Combine("CLDR", "common", "supplemental", supplementalDataXML);
			//string dtd = SupportFunctions.Combine("CLDR", "common", "dtd", "ldml.dtd");
			CLDR.SupplementalDataLocaleDataMarkupLanguage.supplementalData supplementalData = null;
			if (File.Exists(xmlPath))
			{
				Environment.CurrentDirectory = basePath;
				// Using a FileStream, create an XmlTextReader.
				using (Stream fs = new FileStream(xmlPath, FileMode.Open))
				{
					XmlReaderSettings settings = new XmlReaderSettings();
					XmlResolver resolver = new XmlUrlResolver();
					settings.XmlResolver = resolver;
					settings.ProhibitDtd = false;
					settings.ValidationType = ValidationType.None;
					XmlReader reader = XmlReader.Create(fs, settings); // new XmlTextReader(fs);
					if (SupportFunctions.SupplementalDataSerializer.CanDeserialize(reader))
					{
						supplementalData = (CLDR.SupplementalDataLocaleDataMarkupLanguage.supplementalData)SupportFunctions.SupplementalDataSerializer.Deserialize(reader);
					}
					reader.Close();
					fs.Close();
				}
				Environment.CurrentDirectory = currentDirectory;
			}
			return supplementalData;
		}

		public static void RecursiveTerritoryContainment(Dictionary<string, CLDR.LocaleDataMarkupLanguage.territory> territoriesInEnglish, CLDR.SupplementalDataLocaleDataMarkupLanguage.supplementalData data, string type, int depth = 0, string previousName = null, Action<object[]> handler = null)
		{
			CLDR.SupplementalDataLocaleDataMarkupLanguage.group otherGroup = data.FindTerritoryContainment(type);
			if (otherGroup != null)
			{
				string name = territoriesInEnglish[type].Value;
				int nextDepth = depth;
				if (name != previousName)
				{
					if (handler != null)
					{
						handler(new object[] { nextDepth, type, otherGroup });
					}
					nextDepth = nextDepth + 1;
				}
				
				foreach (string subType in otherGroup.TerritoryTypes)
				{
					string childName = territoriesInEnglish[subType].Value;
					CLDR.SupplementalDataLocaleDataMarkupLanguage.group childGroup = data.FindTerritoryContainment(subType);
					if (handler != null)
					{
						handler(new object[] { nextDepth, subType, childGroup });
					}
					RecursiveTerritoryContainment(territoriesInEnglish, data, subType, depth: nextDepth + 1, previousName: childName, handler: handler);
				}
			}
		}

		public static List<string> GetLocalesFromCommonLocaleDataRepository() {
			List<string> files = new List<string>();
			string basePath = SupportFunctions.Combine("CLDR", "common", "main");
			if (Directory.Exists(basePath)) {
				string[] fileList = Directory.GetFiles(basePath, "*.xml");
				foreach (string file in fileList)
					files.Add(Path.GetFileNameWithoutExtension(file));
			}
			return files;
		}

		public static CLDR.LocaleDataMarkupLanguage.ldml GetLanguageScriptTerritory(string lang_script_territory)
		{
			string languageScriptTerritoryXML = string.Format("{0}.xml", lang_script_territory);
			string currentDirectory = Environment.CurrentDirectory;
			string basePath = SupportFunctions.Combine("CLDR", "common", "main");
			string xmlPath = SupportFunctions.Combine("CLDR", "common", "main", languageScriptTerritoryXML);
			//string dtd = SupportFunctions.Combine("CLDR", "common", "dtd", "ldml.dtd");
			CLDR.LocaleDataMarkupLanguage.ldml languageScriptTerritory = null;
			if (File.Exists(xmlPath))
			{
				Environment.CurrentDirectory = basePath;
				// Using a FileStream, create an XmlTextReader.
				using (Stream fs = new FileStream(xmlPath, FileMode.Open))
				{
					XmlReaderSettings settings = new XmlReaderSettings();
					XmlResolver resolver = new XmlUrlResolver();
					settings.XmlResolver = resolver;
					settings.ProhibitDtd = false;
					settings.ValidationType = ValidationType.None;
					XmlReader reader = XmlReader.Create(fs, settings); // new XmlTextReader(fs);
					if (SupportFunctions.LDMLSerializer.CanDeserialize(reader))
					{
						languageScriptTerritory = (CLDR.LocaleDataMarkupLanguage.ldml)SupportFunctions.LDMLSerializer.Deserialize(reader);
					}
					reader.Close();
					fs.Close();
				}
				Environment.CurrentDirectory = currentDirectory;
			}
			return languageScriptTerritory;
		}

		public static void ListAllLocales()
		{
			Console.WriteLine("{0,15}: {1,-25}{2,-25}{3,-25}", new object[] { "locale", "Language", "Script", "Territory" });
			List<string> files = SupportFunctions.GetLocalesFromCommonLocaleDataRepository();
			foreach (string f in files)
			{
				CLDR.LocaleDataMarkupLanguage.ldml locale = SupportFunctions.GetLanguageScriptTerritory(f);
				string languageType = (locale.Identity.Language != null) ? locale.Identity.Language.type : null;
				string scriptType = (locale.Identity.Script != null) ? locale.Identity.Script.type : null;
				string territoryType = (locale.Identity.Territory != null) ? locale.Identity.Territory.type : null;
				Console.WriteLine("{0,15}: {1,-25}{2,-25}{3,-25}", new object[] { f, languageType, scriptType, territoryType });
			}
		}
	}
}
