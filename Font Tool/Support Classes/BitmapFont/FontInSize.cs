using System.Drawing;
using System.IO;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace FontTool
{
	[System.Reflection.ObfuscationAttribute(Feature = "renaming", ApplyToMembers = true)]
	public class FontInSize : IImageProvider
	{
		private BitmapFont font = null;
		private float size = 0f;
		private float defaultWidth = 0f;
		private CharacterCollection characters = null;
		private BitmapVariantCollection variants = null;
		private List<BitmapEffect> effects = null;

		[XmlAttribute(AttributeName = "default-width")]
		public float DefaultWidth {
			get { return (defaultWidth > 0f) ? defaultWidth : size / 2.0f; }
			set { defaultWidth = value; }
		}

		[XmlAttribute(AttributeName = "value")]
		public float Size
		{
			get { return size; }
			set { size = value; }
		}

		[XmlAttribute(AttributeName = "target-ascent")]
		public float TargetAscent { get; set; }

		[XmlAttribute(AttributeName = "target-descent")]
		public float TargetDescent { get; set; }

		[XmlIgnore]
		public BitmapFont Font
		{
			get { return font; }
			set { font = value; }
		}

		[XmlArray(ElementName = "characters", Order = 0)]
		[XmlArrayItem(ElementName = "character")]
		public CharacterCollection Characters
		{
			get { return characters; }
			set { characters = value; }
		}

		[XmlArray(ElementName = "variants", Order = 1)]
		[XmlArrayItem(ElementName = "variant")]
		public BitmapVariantCollection Variants
		{
			get {
				if (variants != null && variants.Count > 0) {
					for (int i = 0; i < variants.Count; i++)
						variants[i].Dependency = this;
				}
				return variants;
			}
			set { variants = value; }
		}

		public string Path
		{
			get
			{
				return SupportFunctions.Combine("fonts", Font.Name, string.Format("{0}", Size));
			}
		}

		public string UnicodeCoverage
		{
			get
			{
				string coverage = "";
				List<KeyValuePair<string, double>> list = GetUnicodeCoverageList();
				//
				List<string> parts = new List<string>();
				foreach (KeyValuePair<string, double> entry in list)
				{
					string unicodeBlock = entry.Key;
					double coverageValue = entry.Value;
					string prefix = (coverageValue < 1) ? "~" : "";
					parts.Add(
						string.Format("{0} ({1}{2:0.##}%)", new object[] { unicodeBlock, prefix, coverageValue })
					);
				}
				coverage = string.Join("," + System.Environment.NewLine, parts.ToArray());
				//
				return (coverage == "") ? "(Empty)" : coverage;
			}
		}

		public List<KeyValuePair<string, double>> GetUnicodeCoverageList()
		{
			// Compose the coverage list.
			List<KeyValuePair<UnicodeBlock, List<ulong>>> coverageList = new List<KeyValuePair<UnicodeBlock, List<ulong>>>();
			if (Characters != null && Characters.Count > 0)
			{
				foreach (ulong characterCode in Characters.Keys)
				{
					UnicodeCharacter character = UnicodeBlocks.GetCharacterById(characterCode);
					if (character != null)
					{
						if (!coverageList.Exists(item => item.Key == character.Block))
							coverageList.Add(new KeyValuePair<UnicodeBlock, List<ulong>>(character.Block, new List<ulong>()));
						KeyValuePair<UnicodeBlock, List<ulong>> entry = coverageList.FindLast(item => item.Key == character.Block);
						entry.Value.Add(characterCode);
					}
				}
			}
			// Sort the list so that its display is uniformed.
			coverageList.Sort(SupportFunctions.CompareKeyValuePairByBlockCode);
			// Compose the coverage string.
			if (coverageList.Count > 0)
			{
				List<KeyValuePair<string, double>> list = new List<KeyValuePair<string, double>>();
				foreach (KeyValuePair<UnicodeBlock, List<ulong>> entry in coverageList)
				{
					List<UnicodeCharacter> printableCharacters = entry.Key.PrintableCharacters;
					if (printableCharacters != null && printableCharacters.Count > 0)
					{
						double value = System.Math.Min(100, (100 * entry.Value.Count / printableCharacters.Count));
						list.Add(new KeyValuePair<string, double>(entry.Key.BlockName, value));
					}
				}
				return list;
			}
			return null;
		}

		public FontInSize()
		{
		}

		public FontInSize(FontInSize fontInSize)
		{
			//
			this.size = fontInSize.Size;
			this.characters = fontInSize.Characters;
			this.defaultWidth = fontInSize.defaultWidth;
			this.effects = fontInSize.effects;
			this.Font = fontInSize.Font;
			this.variants = fontInSize.variants;
			this.TargetAscent = fontInSize.TargetAscent;
			this.TargetDescent = fontInSize.TargetDescent;
		}

		public FontInSize(float size, BitmapFont font)
		{
			//
			this.size = size;
			this.defaultWidth = size / 2.0f;
			this.font = font;
			this.characters = GetCharactersFromImageNames();
			this.variants = GetVariantsFromDirectoryNames();
			this.effects = new List<BitmapEffect>();
		}

		public CharacterCollection GetCharactersFromImageNames()
		{
			CharacterCollection collection = new CharacterCollection();
			if (Directory.Exists(Path))
			{
				string[] characterGlyphs = Directory.GetFiles(Path, "*.png", SearchOption.TopDirectoryOnly);
				foreach (string glyph in characterGlyphs)
				{
					string idString = System.IO.Path.GetFileNameWithoutExtension(glyph);
					ulong id = 0U;
					bool parsed = ulong.TryParse(idString, out id);
					if (parsed)
					{
						UnicodeCharacter thisCharacter = UnicodeBlocks.GetCharacterById(id);
						if (thisCharacter != null)
							collection.Add(new Character(thisCharacter, defaultWidth, 0f, 0f, 1f, 1f));
					}
				}
			}
			return collection;
		}

		private BitmapVariantCollection GetVariantsFromDirectoryNames()
		{
			BitmapVariantCollection collection = new BitmapVariantCollection();
			string[] paths = Directory.GetDirectories(Path);
			foreach (string path in paths)
			{
				string variant = System.IO.Path.GetFileName(path);
				collection.Add(new BitmapVariant(variant, this));
			}
			return collection;
		}

		public BitmapVariant AddVariant(string name)
		{
			BitmapVariant variant = new BitmapVariant(name, this);
			Variants.Add(variant);
			return variant;
		}

		private string CharacterPath(Character character)
		{
			return (character != null) ? System.IO.Path.Combine(Path, string.Format("{0}.png", character.Code)) : null;
		}

		public bool IsCharacterRealized(ulong key)
		{
			string path = CharacterPath((Characters != null && Characters.Contains(key)) ? Characters[key] : null);
			return (File.Exists(path)) ? true : false;
		}

		public bool IsCharacterRealized(Character character)
		{
			return (character != null) ? IsCharacterRealized(character.Id) : false;
		}

		public void SynchronizeCharacter(ulong key, OffsetableUnicodeCharacter character)
		{
			// Add character to font or update its values.
			if (!Characters.Contains(key))
			{
				Characters.Add(new Character(character, character.OffsetWidth, character.OffsetX, character.OffsetY, character.ScaleX, character.ScaleY));
				// Add across all variants.
				foreach (BitmapVariant variant in Variants)
					SupportFunctions.SynchronizeCharacterBetweenCollections(key, Characters, variant, ref variant.Characters);
			} else {
				// Update values in font.
				Characters[key].OffsetWidth = character.OffsetWidth;
				Characters[key].OffsetX = character.OffsetX;
				Characters[key].OffsetY = character.OffsetY;
				Characters[key].ScaleX = character.ScaleX;
				Characters[key].ScaleY = character.ScaleY;
				// Synch across all variants.
				foreach (BitmapVariant variant in Variants)
					SupportFunctions.SynchronizeCharacterBetweenCollections(key, Characters, variant, ref variant.Characters);
			}
		}

		public void SynchronizeCharacter(Character character)
		{
			if (character != null)
				SynchronizeCharacter(character.Id, character);
		}

		public List<Color> GetUsedColors()
		{
			List<Color> list = new List<Color>();
			IImageProvider provider = (IImageProvider)this;
			// Add colors in images.
			if (Characters != null)
			{
				foreach (Character character in Characters)
				{
					ImageDescription description = provider.GetImage(character, pushToContext: false);
					if (description != null)
					{
						Bitmap bitmap = description.SignificantBitmap;
						BitmapProcessing.FastBitmap processor = new BitmapProcessing.FastBitmap(bitmap);
						processor.LockImage();
						{
							for (int column = 0; column < bitmap.Width; column++)
								for (int row = 0; row < bitmap.Height; row++)
								{
									Color pixel = processor.GetPixel(column, row);
									if (pixel.A > 0 && !list.Contains(pixel))
										list.Add(pixel);
								}
						}
						processor.UnlockImage();
						bitmap.Dispose();
					}
				}
			}
			// Add colors in effects.
			if (effects != null) {
				foreach (BitmapEffect effect in effects) {
					foreach (EffectParameter parameter in effect.Parameters) {
						if (parameter.Value != null && parameter.Value is XmlColor) {
							Color color = (System.Drawing.Color)((XmlColor)parameter.Value);
							if (!list.Contains(color))
								list.Add(color);
						}
					}
				}
			}
			return list;
		}

		// Interfaces
		ImageDescription IImageProvider.GetImage(Character character, bool pushToContext)
		{
			string path = CharacterPath(character);
			return (File.Exists(path)) ? new ImageDescription(character.Id, filename: path, pushToContext: pushToContext) : null;
		}

		bool IImageProvider.SetImage(Bitmap image, Character character)
		{
			string filename = CharacterPath(character);
			string path = System.IO.Path.GetDirectoryName(filename);
			//
			if (File.Exists(path))
			{
				image.Save(filename);
				if (File.Exists(filename))
					return true;
			}
			return false;
		}
	}
}
