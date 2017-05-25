using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.IO;

namespace FontTool
{
	class UnicodePrivateFontCollection
	{
		[DllImport("gdi32.dll")]
		private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont, IntPtr pdv, [In] ref uint pcFonts);

		private static FontFamily defaultFamily = null;
		private static FontFamily code2001Family = null;
		private static FontFamily andagiiFamily = null;
		private static FontFamily aegyptusFamily = null;
		private static FontFamily akkadianFamily = null;
		private static FontFamily analectaFamily = null;
		private static FontFamily avestamanusFamily;
		private static FontFamily symbolaFamily = null;
		private static FontFamily eeyekFamily = null;
		private static FontFamily aegeanFamily = null;
		private static FontFamily lastResortFamily = null;

		public static FontFamily DefaultFamily
		{
			get { 
				if (defaultFamily == null)
					defaultFamily = CreateFontFromResource("unifont-5-1");
				return UnicodePrivateFontCollection.defaultFamily;
			}
			set { UnicodePrivateFontCollection.defaultFamily = value; }
		}

		public static FontFamily Code2001Family
		{
			get {
				if (code2001Family == null)
					code2001Family = CreateFontFromResource("CODE2001");
				return UnicodePrivateFontCollection.code2001Family; }
			set { UnicodePrivateFontCollection.code2001Family = value; }
		}

		public static FontFamily AndagiiFamily
		{
			get {
				if (andagiiFamily == null)
					andagiiFamily = CreateFontFromResource("ANDAGII");
				return UnicodePrivateFontCollection.andagiiFamily; }
			set { UnicodePrivateFontCollection.andagiiFamily = value; }
		}

		public static FontFamily AegyptusFamily
		{
			get {
				if (aegyptusFamily == null)
					aegyptusFamily = CreateFontFromResource("Aegyptus");
				return UnicodePrivateFontCollection.aegyptusFamily; }
			set { UnicodePrivateFontCollection.aegyptusFamily = value; }
		}

		public static FontFamily AkkadianFamily
		{
			get {
				if (akkadianFamily == null)
					akkadianFamily = CreateFontFromResource("Akkadian");
				return UnicodePrivateFontCollection.akkadianFamily; }
			set { UnicodePrivateFontCollection.akkadianFamily = value; }
		}

		public static FontFamily AnalectaFamily
		{
			get {
				if (analectaFamily == null)
					analectaFamily = CreateFontFromResource("Analecta");
				return UnicodePrivateFontCollection.analectaFamily; }
			set { UnicodePrivateFontCollection.analectaFamily = value; }
		}

		public static FontFamily AvestamanusFamily
		{
			get
			{
				if (avestamanusFamily == null)
					avestamanusFamily = CreateFontFromResource("Avestamanus");
				return UnicodePrivateFontCollection.avestamanusFamily;
			}
			set { UnicodePrivateFontCollection.avestamanusFamily = value; }
		}

		public static FontFamily SymbolaFamily
		{
			get {
				if (symbolaFamily == null)
					symbolaFamily = CreateFontFromResource("Symbola");
				return UnicodePrivateFontCollection.symbolaFamily; }
			set { UnicodePrivateFontCollection.symbolaFamily = value; }
		}

		public static FontFamily EeyekFamily
		{
			get {
				if (eeyekFamily == null)
					eeyekFamily = CreateFontFromResource("Eeyek");
				return UnicodePrivateFontCollection.eeyekFamily; }
			set { UnicodePrivateFontCollection.eeyekFamily = value; }
		}

		public static FontFamily AegeanFamily
		{
			get {
				if (aegeanFamily == null)
					aegeanFamily = CreateFontFromResource("Aegean");
				return UnicodePrivateFontCollection.aegeanFamily; }
			set { UnicodePrivateFontCollection.aegeanFamily = value; }
		}

		public static FontFamily LastResortFamily
		{
			get {
				if (lastResortFamily == null)
					lastResortFamily = CreateFontFromResource("LastResort");
				return UnicodePrivateFontCollection.lastResortFamily; }
			set { UnicodePrivateFontCollection.lastResortFamily = value; }
		}
		
		private static FontFamily CreateFontFromResource(string resourceKey) {
			// Create the byte array and get its length.
			string path = SupportFunctions.Combine("Unicode Specification", string.Format("{0}.ttf", resourceKey));
			if (File.Exists(path))
			{
				byte[] fontArray = File.ReadAllBytes(path);
				int dataLength = fontArray.Length;
				// Assign memory.
				IntPtr ptrData = Marshal.AllocCoTaskMem(dataLength);
				// Copy byte array to that location.
				Marshal.Copy(fontArray, 0, ptrData, dataLength);
				uint cFonts = 0;
				AddFontMemResourceEx(ptrData, (uint)fontArray.Length, IntPtr.Zero, ref cFonts);
				PrivateFontCollection pfc = new PrivateFontCollection();
				// Pass the font to the PrivateFontCollection object.
				pfc.AddMemoryFont(ptrData, dataLength);
				// Free the unmanaged memory.
				Marshal.FreeCoTaskMem(ptrData);
				// Send the font family back.
				return pfc.Families[0];
			}
			return FontFamily.GenericSansSerif;
		}

		private static List<string> code2001Blocks = new List<string> { "Aegean Numbers", "Cypriot Syllabary", "Deseret", "Gothic", "Linear B Ideograms", "Linear B Syllabary", "Mathematical Alphanumeric Symbols", "Old Italic", "Old Persian", "Shavian", "Tai Xuan Jing Symbols", "Ugaritic" };
		private static List<string> andagiiBlocks = new List<string> { "Osmanya" };
		private static List<string> aegyptusBlocks = new List<string> { "Egyptian Hieroglyphs" };
		private static List<string> akkadianBlocks = new List<string> { "Cuneiform", "Cuneiform Numbers and Punctuation" };
		private static List<string> analectaBlocks = new List<string> { "Ancient Greek Numbers", "Ancient Symbols" };
		private static List<string> avestamanusBlocks = new List<string> { "Avestan" };
		private static List<string> symbolaBlocks = new List<string> { "Alchemical Symbols", "Ancient Greek Musical Notation", "Byzantine Musical Symbols", "Counting Rod Numerals", "Domino Tiles", "Emoticons", "Mahjong Tiles", "Miscellaneous Symbols And Pictographs", "Musical Symbols", "Playing Cards", "Transport And Map Symbols" };
		private static List<string> eeyekBlocks = new List<string> { "Meetei Mayek" };
		private static List<string> aegeanBlocks = new List<string> { "Carian", "Lycian", "Lydian", "Phaistos Disc", "Phoenician", };
		private static List<string> lastResortBlocks = new List<string> { "Arabic Mathematical Alphabetic Symbols", "Avestan", "Bamum Supplement", "Bamum", "Batak", "Brahmi", "CJK Compatibility Ideographs Supplement", "CJK Unified Ideographs Extension B", "CJK Unified Ideographs Extension C", "CJK Unified Ideographs Extension D", "Chakma", "Common Indic Number Forms", "Devanagari Extended", "Enclosed Alphanumeric Supplement", "Enclosed Ideographic Supplement", "Ethiopic Extended-A", "Hangul Jamo Extended-B", "Imperial Aramaic", "Inscriptional Pahlavi", "Inscriptional Parthian", "Javanese", "Kaithi", "Kana Supplement", "Kharoshthi", "Lisu", "Meetei Mayek Extensions", "Meroitic Cursive", "Meroitic Hieroglyphs", "Miao", "Myanmar Extended-A", "Old South Arabian", "Old Turkic", "Rumi Numeral Symbols", "Sharada", "Sora Sompeng", "Sundanese Supplement", "Supplementary Private Use Area-A", "Supplementary Private Use Area-B", "Tags", "Tai Tham", "Tai Viet", "Takri", "Variation Selectors Supplement", "Vedic Extensions" };

		public static Font GetFontForUnicodeBlock(string name = "Basic Latin")
		{
			FontFamily family = GetFontFamilyForUnicodeBlock(name);
			float fontSize = GetFontSizeForUnicodeBlock(name);
			return new Font(family, fontSize, FontStyle.Regular);
		}

		public static FontFamily GetFontFamilyForUnicodeBlock(string name = "Basic Latin")
		{
			if (code2001Blocks.Contains(name))
				return Code2001Family;
			else if (andagiiBlocks.Contains(name))
				return AndagiiFamily;
			else if (aegyptusBlocks.Contains(name))
				return AegyptusFamily;
			else if (akkadianBlocks.Contains(name))
				return AkkadianFamily;
			else if (analectaBlocks.Contains(name))
				return AnalectaFamily;
			else if (avestamanusBlocks.Contains(name))
				return AvestamanusFamily;
			else if (symbolaBlocks.Contains(name))
				return SymbolaFamily;
			else if (eeyekBlocks.Contains(name))
				return EeyekFamily;
			else if (aegeanBlocks.Contains(name))
				return AegeanFamily;
			else if (lastResortBlocks.Contains(name))
				return LastResortFamily;
			return DefaultFamily;
		}

		public static float GetFontSizeForUnicodeBlock(string name = "Basic Latin")
		{
			float defaultSize = 12f;
			if (code2001Blocks.Contains(name))
				return defaultSize;
			else if (andagiiBlocks.Contains(name))
				return defaultSize;
			else if (aegyptusBlocks.Contains(name))
				return 60f;
			else if (akkadianBlocks.Contains(name))
				return defaultSize;
			else if (analectaBlocks.Contains(name))
				return defaultSize;
			else if (avestamanusBlocks.Contains(name))
				return defaultSize;
			else if (symbolaBlocks.Contains(name))
				return 34f;
			else if (eeyekBlocks.Contains(name))
				return defaultSize;
			else if (aegeanBlocks.Contains(name))
				return defaultSize;
			else if (lastResortBlocks.Contains(name))
				return defaultSize;
			return defaultSize;
		}

		public static bool GetAliasedDefaultFontNatureForUnicodeBlock(string name = "Basic Latin")
		{
			bool defaultValue = true;
			if (code2001Blocks.Contains(name))
				return defaultValue;
			else if (andagiiBlocks.Contains(name))
				return defaultValue;
			else if (aegyptusBlocks.Contains(name))
				return defaultValue;
			else if (akkadianBlocks.Contains(name))
				return defaultValue;
			else if (analectaBlocks.Contains(name))
				return defaultValue;
			else if (avestamanusBlocks.Contains(name))
				return defaultValue;
			else if (symbolaBlocks.Contains(name))
				return defaultValue;
			else if (eeyekBlocks.Contains(name))
				return defaultValue;
			else if (aegeanBlocks.Contains(name))
				return defaultValue;
			else if (lastResortBlocks.Contains(name))
				return defaultValue;
			return defaultValue;
		}

		//
		public static bool GetAliasedDefaultFontNatureForUnicodeCharacter(UnicodeCharacter character)
		{
			string language = (character != null) ? UnicodeBlocks.GetBlockForCharacter(character.Id).BlockName : null;
			return UnicodePrivateFontCollection.GetAliasedDefaultFontNatureForUnicodeBlock(language);
		}

		public static Font GetFontForUnicodeCharacter(UnicodeCharacter character) {
			string language = (character != null) ? UnicodeBlocks.GetBlockForCharacter(character.Id).BlockName : null;
			return UnicodePrivateFontCollection.GetFontForUnicodeBlock(language);
		}

		public static FontFamily GetFontForUnicodeCharacter(ulong id) {
			UnicodeBlock block = UnicodeBlocks.GetBlockForCharacter(id);
			if (block != null)
				return UnicodePrivateFontCollection.GetFontFamilyForUnicodeBlock(block.BlockName);
			return DefaultFamily;
		}
	}
}
