using System.IO;
using System.Collections.Generic;
using System;
using System.Windows.Forms;
using System.Drawing;

namespace FontTool.Plugins
{
	public class TextureBasedEGGFont : IPlugin
	{
		string eggFile = null;
		BitmapFont fontToEGG = null;

		public TextureBasedEGGFont(string eggFile)
		{
			if (File.Exists(eggFile))
			{
				this.eggFile = eggFile;
			}
		}

		public TextureBasedEGGFont(BitmapFont font)
		{
			this.fontToEGG = font;
		}

		BitmapFont IPlugin.Acquire()
		{
			if (File.Exists(eggFile))
			{
				// Attempt to parse it.
			}
			return null;
		}

		string IPlugin.Save(Configuration configuration, SupportFunctions.ProgressUpdater updater) {
			configuration = configuration ?? Configuration.Empty;
			//
			bool hasUpdater = (updater != null) ? true : false;
			if (!hasUpdater)
				updater = delegate(string s) { };
			string directoryPath = SupportFunctions.GetTemporaryDirectory();
			if (fontToEGG != null)
			{
				// Actually export the stuff out.
				uint iteration = 0;
				uint iterations = fontToEGG.Variations;
				foreach (FontInSize size in fontToEGG.Sizes)
				{
					string sizeSpecificIndexFile = null;
					if (configuration.HasVariantWithSize(size))
					{
						// Create the size-specific index file (to be referenced as a tag in all parts).
						updater(string.Format("Generating an index of files and characters for {0} {1}px...", new object[] { fontToEGG.Name, size.Size }));
						sizeSpecificIndexFile = makeIndexFile(directoryPath, size.Variants[0], configuration);
					}
					//
					foreach (BitmapVariant variant in size.Variants)
					{
						if (configuration.HasExplicitCombinations && !configuration.TargetVariants.Contains(variant))
							continue;
						//
						List<BitmapColor> list = new List<BitmapColor>();
						if (fontToEGG.Colors.Count > 0)
							foreach (BitmapColor color in fontToEGG.Colors)
								list.Add(color);
						else
							list.Add(null);
						//
						foreach (BitmapColor color in list)
						{
							if (fontToEGG.Colors.Count > 0 && configuration.HasExplicitCombinations && !configuration.TargetColors.Contains(color))
								continue;
							//
							iteration++;
							string update;
							if (color != null)
								update = string.Format("Exporting {0} ({4}/{5}): {2} {3} {1}px ", new object[] { fontToEGG.Name, size.Size, color.Name, variant.Name, iteration, iterations });
							else
								update = string.Format("Exporting {0} ({3}/{4}): {2} {1}px ", new object[] { fontToEGG.Name, size.Size, variant.Name, iteration, iterations });
							// Update the status.
							updater(update);
							//
							makeEGGFile(directoryPath, size, variant, color, sizeSpecificIndexFile, configuration);
						}
					}
				}
			}
			return directoryPath;
		}

		private string makeIndexFile(string directoryPath, BitmapVariant representativeVariant, Configuration configuration) {
			// Get all the characters to export.
			CharactersByEGGFile eggFiles = representativeVariant.CharactersByBlock.getEggFiles(characterCutOff: configuration.CharactersPerFile);
			// Store the entries.
			List<string> indexEntries = new List<string>();
			List<string> indexCharacterEntries = new List<string>();
			// Form the index.
			int index = 0;
			foreach (KeyValuePair<string, CharactersByBlock> unicodeCharactersByBlockByEGG in eggFiles.Entries)
			{
				string fileExtension = unicodeCharactersByBlockByEGG.Key;
				CharactersByBlock charactersByBlock = unicodeCharactersByBlockByEGG.Value;
				// Add character entries to character entries index.
				var list = charactersByBlock.Entries;
				foreach (KeyValuePair<UnicodeBlock, CharacterCollection> pair in list)
				{
					// Ignore unicode block; just focus on getting the fact that the id can be resolved in this specific file.
					foreach (Character thisCharacter in pair.Value)
					{
						string thisEntry = string.Format("\"{0}\":{1}", new object[] { thisCharacter.Id, index });
						indexCharacterEntries.Add(thisEntry);
					}
				}
				//
				KeyValuePair<UnicodeBlock, CharacterCollection> firstPair = list[0];
				KeyValuePair<UnicodeBlock, CharacterCollection> lastPair = list[list.Count - 1];
				ulong firstCharacter = firstPair.Value[0].Id;
				ulong lastCharacter = lastPair.Value[lastPair.Value.Count - 1].Id;
				// Create the part range portion of the index tag.
				string entry = TextureBasedEGGFont.indexRangeEntry;
				entry = entry.Replace("{Font Part}", string.Format("{0}", index));
				entry = entry.Replace("{Start Character Number}", string.Format("{0}", firstCharacter));
				entry = entry.Replace("{End Character Number}", string.Format("{0}", lastCharacter));
				//
				indexEntries.Add(entry);
				//
				index++;
			}
			// Create the index file (JSON) that is to be referenced in the tag.
			string jsonContents = TextureBasedEGGFont.indexContents;
			jsonContents = jsonContents.Replace("{Index Parts Range}", string.Join(",", indexEntries.ToArray()));
			jsonContents = jsonContents.Replace("{Index Characters to Parts}", string.Join(",", indexCharacterEntries.ToArray()));
			// Write contents out to file.
			string fileName = string.Format("Index - {0} {1}px.json", new object[] { representativeVariant.Dependency.Font.Name, representativeVariant.Dependency.Size });
			using (StreamWriter writer = File.CreateText(Path.Combine(directoryPath, fileName))) {
				writer.WriteLine(jsonContents);
			}
			// Send back the name to reference.
			return fileName;
		}

		private void makeEGGFile(string directoryPath, FontInSize size, BitmapVariant variant, BitmapColor color, string indexFilename, Configuration configuration)
		{
			// Get all the characters to export.
			CharactersByEGGFile eggFiles = variant.CharactersByBlock.getEggFiles(characterCutOff: configuration.CharactersPerFile);
			int thisFilePart = 0;
			foreach (KeyValuePair<string, CharactersByBlock> charactersByUnicodeBlock in eggFiles.Entries)
			{
				// Get the key as the file's dual extension (like, "", ".1", ".2", ".3", etc).
				string dualExtension = charactersByUnicodeBlock.Key;
				CharactersByBlock unicodeCharacters = charactersByUnicodeBlock.Value;
				//
				string thisFileStub = getEGGFilenameStub(size, variant, color, dualFileExtension: dualExtension);
				string thisEGGFile = string.Format("{0}.egg", thisFileStub);
				string thisTextureFolder = Path.Combine(directoryPath, thisFileStub);
				string path = Path.Combine(directoryPath, thisEGGFile);
				//
				if (!Directory.Exists(thisTextureFolder))
					Directory.CreateDirectory(thisTextureFolder);
				//
				string thisIndexFilename = (thisFilePart == 0) ? indexFilename : null;
				string template = getEGGHeader(fontToEGG.Name, size, thisIndexFilename, configuration);
				//
				makeEGG(unicodeCharacters, variant, color, thisFileStub, thisTextureFolder, path, template, configuration);
				//
				thisFilePart++;
			}
		}

		private string getEGGFilenameStub(FontInSize size, BitmapVariant variant, BitmapColor color, string dualFileExtension = "")
		{
			string thisFileStub = string.Format("{0}{1}{2}-{3}px{4}", new object[]{
				fontToEGG.Name,
				(color == null) ? "" : string.Format("-{0}", color.Name),
				(variant.Name.ToLower() == "normal" || variant.Name.ToLower() == "regular") ? "" : string.Format("-{0}", variant.Name),
				string.Format("{0}", size.Size).Replace('.', '-'),
				dualFileExtension
			});
			return thisFileStub;
		}

		private string getEGGHeader(string name, FontInSize size, string indexFilename, Configuration configuration)
		{
			string mainTemplateBlock = mainTemplate;
			float boundingBox = size.TargetAscent + size.TargetDescent;
			// Clear out the index tag if this isn't the first file in a parted file (tag is used to store index JSON filename).
			string propertiesEntry;
			if (indexFilename != null)
			{
				propertiesEntry = TextureBasedEGGFont.PropertiesEntryWithIndex;
				propertiesEntry = propertiesEntry.Replace("{Index Filename}", indexFilename);
				propertiesEntry = propertiesEntry.Replace("{(Ascent + Descent (in pixels)) * Screen Scale}", string.Format("{0}", (decimal)boundingBox * configuration.VerticalScale));
			}
			else
			{
				propertiesEntry = EGGFont.PropertiesEntry;
				propertiesEntry = propertiesEntry.Replace("{(Ascent + Descent (in pixels)) * Screen Scale}", string.Format("{0}", (decimal)boundingBox * configuration.VerticalScale));
			}
			//
			mainTemplateBlock = mainTemplateBlock.Replace("{Properties Entry for Line Height}", propertiesEntry);
			mainTemplateBlock = mainTemplateBlock.Replace("{Font Name}", string.Format("{0}", name));
			mainTemplateBlock = mainTemplateBlock.Replace("{Ascent (in pixels)}", string.Format("{0}", size.TargetAscent));
			mainTemplateBlock = mainTemplateBlock.Replace("{Descent (in pixels)}", string.Format("{0}", size.TargetDescent));
			mainTemplateBlock = mainTemplateBlock.Replace("{Ascent + Descent (in pixels)}", string.Format("{0}", boundingBox));
			mainTemplateBlock = mainTemplateBlock.Replace("{Screen Scale (string)}", string.Format("({0}, {1})", new object[] { configuration.HorizontalScale, configuration.VerticalScale }));
			mainTemplateBlock = mainTemplateBlock.Replace("{Ascent (in pixels) * Screen Scale}", string.Format("{0}", (decimal)size.TargetAscent * configuration.VerticalScale));
			mainTemplateBlock = mainTemplateBlock.Replace("{Descent (in pixels) * Screen Scale}", string.Format("{0}", (decimal)size.TargetDescent * configuration.VerticalScale));
			mainTemplateBlock = mainTemplateBlock.Replace("{(Ascent + Descent (in pixels)) * Screen Scale}", string.Format("{0}", (decimal)boundingBox * configuration.VerticalScale));
			return mainTemplateBlock;
		}

		private void makeEGG(CharactersByBlock charactersToExport, BitmapVariant variant, BitmapColor color, string thisFileStub, string thisTextureFolder, string path, string mainTemplateBlock, Configuration configuration)
		{
			string directoryPrefix = "";
			if (configuration.ForceAbsolutePaths)
			{
				string pandaPath = Path.GetDirectoryName(path).Replace(@"\", @"/").Replace(":", "");
				string pandaDriveName = pandaPath.Substring(0, 1).ToLower();
				pandaPath = "/" + pandaDriveName + pandaPath.Substring(1, pandaPath.Length - 1) + "/";
				directoryPrefix = pandaPath;
			}
			//
			using (FileStream stream = File.Create(path))
			{
				// Write contents to temp file.
				try
				{
					//
					WidthMarkerCollection widthMarkers = new WidthMarkerCollection();
					List<string> perCharacterTextures = new List<string>();
					List<string> unicodeBlockGroups = new List<string>();
					OpenTK.Vector3d screenScale = configuration.ScreenScale;
					foreach (KeyValuePair<UnicodeBlock, CharacterCollection> block in charactersToExport.Entries)
					{
						//
						UnicodeBlock thisUnicodeBlock = block.Key;
						CharacterCollection characters = block.Value;
						//
						List<string> polygonGroups = new List<string>();
						//
						foreach (Character variantBaseCharacter in characters)
						{
							// Get a traced character out of the font.
							TracedCharacter tracedCharacter = TracedCharacter.Factory(variantBaseCharacter, variant, color);
							// Create a centered display list (contains all necessary offsets).
							DisplayList displayList = new DisplayList(tracedCharacter, pushToGraphicsContext: false, calculateSimplifications: true);
							// Overall width.
							int thisWidthMarker = widthMarkers.getIndexOfWidth(tracedCharacter);
							// Add texture entry.
							List<OpenTK.Vector3d> textureOffsets = new List<OpenTK.Vector3d>();
							ImageDescription description = displayList.exportTexturesAsSingleTexture(variantBaseCharacter.Name, textureOffsets, relativeBasePath: thisTextureFolder);
							if (description != null)
							{
								perCharacterTextures.Add(
									description.getTextureEntry(configuration, relativeBasePath: thisFileStub, flipped: true)
								);
							}
							// Add the entries.
							addTracedCharacterAsPolygonEntry(displayList, tracedCharacter, thisWidthMarker, description, textureOffsets, polygonGroups, configuration, screenScale);
						}
						unicodeBlockGroups.Add(string.Join(Environment.NewLine, polygonGroups.ToArray()));
					}
					// Push values.
					mainTemplateBlock = mainTemplateBlock.Replace("{Texture Entries}", string.Join(Environment.NewLine, perCharacterTextures.ToArray()));
					mainTemplateBlock = mainTemplateBlock.Replace("{Width Marker Vertex Pool}", widthMarkers.getCollectionAsVertexPool(configuration));
					mainTemplateBlock = mainTemplateBlock.Replace("{Unicode Characters as Individual Groups}", string.Join(Environment.NewLine, unicodeBlockGroups.ToArray()));
					//
					using (StreamWriter writer = new StreamWriter(stream))
					{
						writer.Write(mainTemplateBlock);
					}
					//
					mainTemplateBlock = "";
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
				}
			}
		}

		private static void addTracedCharacterAsPolygonEntry(DisplayList displayList, TracedCharacter tracedCharacter, int thisWidthMarker, ImageDescription texture, List<OpenTK.Vector3d> textureOffsets, List<string> polygonGroups, Configuration configuration, OpenTK.Vector3d screenScale)
		{
			// Create vertex pool entries.
			string vertexPoolBaseName = string.Format("{0}Pool", tracedCharacter.SafeName);
			List<string> vertexPoolNames = new List<string> { vertexPoolBaseName };
			List<int> vertexOffsets = new List<int>();
			List<string> textureNames = (texture != null) ? new List<string> { tracedCharacter.SafeName } : null;
			// Get vertex pool entry.
			string vertexPoolEntry = displayList.getSingleEGGVertexPool(vertexPoolBaseName, vertexOffsets, textureOffsets: textureOffsets, screenScale: screenScale, indentation: 1);
			// Create polygon entries (a <Group>).
			string polygonEntries = displayList.getPolygonEntries(vertexPoolNames, textureNames, screenScale, indentation: 1, treatAsSingleVertexPool: true, vertexOffsets: vertexOffsets);
			// Add polygon group.
			string subBlock = (polygonEntries != "") ? EGGFont.UnicodeCharacterAsGroup : EGGFont.UnicodeCharacterAsWidth;
			subBlock = subBlock.Replace("{Character Code (int)}", string.Format("{0}", tracedCharacter.Id));
			subBlock = subBlock.Replace("{Width Marker (int)}", string.Format("{0}", thisWidthMarker));
			subBlock = subBlock.Replace("{Polygon Entries}", polygonEntries);
			subBlock = subBlock.Replace("{Unicode Character as Vertex Pools}", vertexPoolEntry);
			// Adds the polygon group representing the character to the list of other characters.
			polygonGroups.Add(subBlock);
		}

		//
		static string mainTemplate =
@"<Comment> { 
	1. Font

		Font Name: {Font Name}
		Ascent: {Ascent (in pixels)}
		Descent: {Descent (in pixels)}
		Bounding Box Height: {Ascent (in pixels)} + {Descent (in pixels)} = {Ascent + Descent (in pixels)}

	2. Egg

		One Pixel is Considered to be: {Screen Scale (string)} of a unit
		Ascent: {Ascent (in pixels) * Screen Scale}
		Descent: {Descent (in pixels) * Screen Scale}
		Bounding Box Height: {Ascent (in pixels) * Screen Scale} + {Descent (in pixels) * Screen Scale} = {(Ascent + Descent (in pixels)) * Screen Scale}
		Descent Range: -{Descent (in pixels) * Screen Scale} - 0
		Ascent Range: 0 - {Ascent (in pixels) * Screen Scale}
}
{Properties Entry for Line Height}
{Texture Entries}
{Width Marker Vertex Pool}
{Unicode Characters as Individual Groups}";
		static string PropertiesEntryWithIndex =
@"<Group> properties {
	<Tag> index { ""{Index Filename}"" }
	<VertexPool> boundingBoxHeightPool {
		<Vertex> 0 {
			0 {(Ascent + Descent (in pixels)) * Screen Scale} 0
		}
	}
	<Group> ds {
		<PointLight> {
			<VertexRef> { 0 <Ref> { boundingBoxHeightPool } }
		}
	}
}";
		static string indexContents = @"{""parts"":{{Index Parts Range}},""characters"":{{Index Characters to Parts}}}";
		static string indexRangeEntry = @"""{Font Part}"":[{Start Character Number},{End Character Number}]";
	}
}
