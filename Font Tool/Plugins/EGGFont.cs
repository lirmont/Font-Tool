using System.IO;
using System.Collections.Generic;
using System;
using System.Windows.Forms;
using System.Drawing;
using OpenTK;

namespace FontTool.Plugins
{
	public class EGGFont : IPlugin
	{
		string eggFile = null;
		BitmapFont fontToEGG = null;

		public EGGFont(string eggFile)
		{
			if (File.Exists(eggFile))
			{
				this.eggFile = eggFile;
			}
		}

		public EGGFont(BitmapFont font)
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

		string IPlugin.Save(Configuration configuration, SupportFunctions.ProgressUpdater updater)
		{
			configuration = configuration ?? Configuration.Empty;
			//
			bool hasUpdater = (updater != null) ? true : false;
			if (!hasUpdater)
				updater = delegate(string s) { };
			string directoryPath = SupportFunctions.GetTemporaryDirectory();
			if (fontToEGG != null)
			{
				uint iteration = 0;
				uint iterations = fontToEGG.Variations;
				foreach (FontInSize size in fontToEGG.Sizes)
				{
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
							// Make the file.
							makeTextureAndEGG(directoryPath, size, variant, color, configuration);
						}
					}
				}
			}
			return directoryPath;
		}

		private void makeTextureAndEGG(string directoryPath, FontInSize size, BitmapVariant variant, BitmapColor color, Configuration configuration)
		{
			string thisFileStub = getFilenameStub(size, variant, color);
			string thisEGGFile = string.Format("{0}.egg", thisFileStub);
			string thisTextureFile = string.Format("{0}.png", thisFileStub);
			string path = Path.Combine(directoryPath, thisEGGFile);
			string textureFile = Path.Combine(directoryPath, thisTextureFile);
			List<TracedCharacter> tracedCharacters = SupportFunctions.GetTracedCharactersForVariantAndColor(variant, color);
			// Create large texture.
			Size textureSize = makeTextureImage(textureFile, tracedCharacters, edgePadding: 0);
			// Create egg.
			makeEGG(size, variant, color, thisFileStub, thisTextureFile, path, textureSize, tracedCharacters, configuration);
		}

		private string getFilenameStub(FontInSize size, BitmapVariant variant, BitmapColor color)
		{
			// Name color variant-sizepx
			string thisFileStub = string.Format(
				"{0}{1}{2}-{3}px",
				new object[] {
					fontToEGG.Name,
					(color == null) ? "" : string.Format("-{0}", color.Name),
					(variant.Name.ToLower() == "normal" || variant.Name.ToLower() == "regular") ? "" : string.Format("-{0}", variant.Name),
					string.Format("{0}", size.Size).Replace('.', '-')
				}
			);
			return thisFileStub;
		}

		private static Size makeTextureImage(string textureFile, List<TracedCharacter> tracedCharacters, int horizontalSpacing = 2, int verticalSpacing = 2, int edgePadding = 2)
		{
			// Get an image size that can hold at least the amount of pixels required by the images that will be drawn into it.
			int square = SupportFunctions.GetAcceptableSquaredImageSize(tracedCharacters, padding: 10);
			// Make an image to encompass all the other images.
			Bitmap thisBitmap = new Bitmap(square, square);
			int thisMaxRowHeight = 0, runningWidth = 0, runningHeight = 0;
			using (Graphics g = Graphics.FromImage(thisBitmap))
			{
				foreach (TracedCharacter character in tracedCharacters)
				{
					//
					foreach (AutoTrace.Tracing trace in character.Traces) {
						Bitmap description = trace.Image;
						if (description != null)
						{
							// Hit an edge.
							if (runningWidth + horizontalSpacing + description.Width > square)
							{
								runningWidth = 0;
								runningHeight += thisMaxRowHeight + verticalSpacing;
								thisMaxRowHeight = 0;
							}
							// Still running from left to right.
							if (runningWidth + description.Width < square)
							{
								Point imageLocation = new Point(runningWidth, runningHeight);
								Size imageSize = new Size(description.Width, description.Height);
								// Draw image for trace into the larger image (unpadded).
								g.DrawImage(description, imageLocation);
								// Add in the image padding (done later).
								imageLocation.Offset(edgePadding, edgePadding);
								// Point to the part of the combined texture that the sub-image exists at.
								trace.CachedGlobalTextureOffset = new Vector3d(imageLocation.X, imageLocation.Y, 0);
								//
								thisMaxRowHeight = (description.Height > thisMaxRowHeight) ? description.Height : thisMaxRowHeight;
								runningWidth += description.Width + horizontalSpacing;
							}
						}
					}
				}
			}
			// Write out the actual texture as an image.
			using (Bitmap temporaryBitmap = SupportFunctions.TrimBitmap(thisBitmap, preserveLeftSide: true))
			{
				int padding = 2 * edgePadding;
				//
				uint newWidth = SupportFunctions.NextPowerOfTwo((uint)(temporaryBitmap.Width + padding)), newHeight = SupportFunctions.NextPowerOfTwo((uint)(temporaryBitmap.Height + padding));
				thisBitmap = new Bitmap((int)newWidth, (int)newHeight);
				using (Graphics g = Graphics.FromImage(thisBitmap))
				{
					g.DrawImage(temporaryBitmap, new Point(edgePadding, edgePadding));
				}
			}
			// Save the image.
			thisBitmap.Save(textureFile);
			// Get the texture size (for documentation usage; it is not used for UV coordinates as those are stored in pixel offsets rather than percentages of dimensions).
			Size textureSize = thisBitmap.Size;
			thisBitmap.Dispose();
			return textureSize;
		}


		private void makeEGG(FontInSize size, BitmapVariant variant, BitmapColor color, string thisFileStub, string thisTextureFile, string path, Size textureSize, List<TracedCharacter> tracedCharacters, Configuration configuration)
		{
			//
			int textureWidth = textureSize.Width, textureHeight = textureSize.Height;
			// Write contents to temp file.
			using (FileStream stream = File.Create(path))
			{
				try
				{
					//
					float boundingBox = size.TargetAscent + size.TargetDescent;
					// Calculate and store common values required by the EGG file.
					string textureName = thisFileStub.Replace("#", "").Replace("-", "").Replace(" ", "");
					string properties = EGGFont.GetLineHeightPropertiesEntry(size, configuration, boundingBox);
					//
					string mainTemplateBlock = GenericEGGFileTemplate;
					mainTemplateBlock = mainTemplateBlock.Replace("{Font Name}", string.Format("{0}", fontToEGG.Name));
					mainTemplateBlock = mainTemplateBlock.Replace("{Ascent (in pixels)}", string.Format("{0}", size.TargetAscent));
					mainTemplateBlock = mainTemplateBlock.Replace("{Descent (in pixels)}", string.Format("{0}", size.TargetDescent));
					mainTemplateBlock = mainTemplateBlock.Replace("{Ascent + Descent (in pixels)}", string.Format("{0}", boundingBox));
					mainTemplateBlock = mainTemplateBlock.Replace("{Screen Scale (string)}", string.Format("({0}, {1})", new object[] { configuration.HorizontalScale, configuration.VerticalScale }));
					mainTemplateBlock = mainTemplateBlock.Replace("{Ascent (in pixels) * Screen Scale}", string.Format("{0}", (decimal)size.TargetAscent * configuration.VerticalScale));
					mainTemplateBlock = mainTemplateBlock.Replace("{Descent (in pixels) * Screen Scale}", string.Format("{0}", (decimal)size.TargetDescent * configuration.VerticalScale));
					mainTemplateBlock = mainTemplateBlock.Replace("{(Ascent + Descent (in pixels)) * Screen Scale}", string.Format("{0}", (decimal)boundingBox * configuration.VerticalScale));
					mainTemplateBlock = mainTemplateBlock.Replace("{Texture Width}", string.Format("{0}", textureWidth));
					mainTemplateBlock = mainTemplateBlock.Replace("{Texture Height}", string.Format("{0}", textureHeight));
					mainTemplateBlock = mainTemplateBlock.Replace("{Inverse of Texture Width (decimal)}", string.Format("{0}", 1 / (decimal)textureWidth));
					mainTemplateBlock = mainTemplateBlock.Replace("{Negative Inverse of Texture Height}", string.Format("{0}", -1 / (decimal)textureHeight));
					mainTemplateBlock = mainTemplateBlock.Replace("{Properties Entry for Line Height}", properties);
					mainTemplateBlock = mainTemplateBlock.Replace("{Texture Entries}", ImageDescription.GetImageDescriptionAsTextureEntry(textureWidth, textureHeight, thisFileStub, configuration, flipped: true));
					//
					WidthMarkerCollection widthMarkers = new WidthMarkerCollection();
					List<string> unicodeBlockGroups = new List<string>();
					// For each character in the block, export it as a numbered group (Unicode ID), including vertices, faces, and advance. NOTE: Cannot store individual character groups in logical for Unicode block group; Panda3d's PNMText will complain.
					foreach (KeyValuePair<UnicodeBlock, CharacterCollection> block in variant.CharactersByBlock.Entries)
					{
						// Get characters.
						CharacterCollection characters = block.Value;
						// Track character meshes (groups).
						List<string> polygonGroups = new List<string>();
						// Make each character into a width and polygon.
						foreach (Character variantCharacter in characters)
						{
							TracedCharacter tracedCharacter = tracedCharacters.Find(item => item.DerivedUnicodeCharacter.Id == variantCharacter.Id);
							// Get the index to an appropriate width scalar.
							int thisWidthMarker = widthMarkers.getIndexOfWidth(tracedCharacter.DerivedUnicodeCharacter.FinalOffset.OffsetWidth);
							// Local to Unicode Block's Group.
							addCharacterAsVertexPoolAndPolygonGroup(polygonGroups, tracedCharacter, thisWidthMarker, configuration.ScreenScale, textureName);
						}
						// Add the entry.
						string thisUnicodeBlock = string.Join(Environment.NewLine, polygonGroups.ToArray());
						unicodeBlockGroups.Add(thisUnicodeBlock);
					}
					//
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

		private static void addCharacterAsVertexPoolAndPolygonGroup(List<string> polygonGroups, TracedCharacter tracedCharacter, int thisWidthMarker, Vector3d screenScale, string textureName = null)
		{
			// Create a centered display list (contains all necessary offsets).
			DisplayList displayList = new DisplayList(tracedCharacter, pushToGraphicsContext: false, calculateSimplifications: true);
			// Create vertex pool entries.
			string vertexPoolBaseName = string.Format("{0}Pool", tracedCharacter.SafeName);
			string vertexPoolEntries = displayList.getEGGVertexPools(vertexPoolBaseName, screenScale: screenScale, indentation: 1);
			List<string> vertexPoolNames = SupportFunctions.GetRangeOfNames(vertexPoolBaseName, tracedCharacter.Traces.Count);
			// Create polygon entries (a <Group>).
			List<string> textureNames = new List<string> { textureName };
			string polygonEntries = displayList.getPolygonEntries(vertexPoolNames, textureNames, screenScale, indentation: 1);
			// Add polygon group.
			string subBlock = (polygonEntries != "") ? EGGFont.UnicodeCharacterAsGroup : EGGFont.UnicodeCharacterAsWidth;
			subBlock = subBlock.Replace("{Character Code (int)}", string.Format("{0}", tracedCharacter.Id));
			subBlock = subBlock.Replace("{Width Marker (int)}", string.Format("{0}", thisWidthMarker));
			subBlock = subBlock.Replace("{Polygon Entries}", polygonEntries);
			subBlock = subBlock.Replace("{Unicode Character as Vertex Pools}", vertexPoolEntries);
			// Adds the polygon group representing the character to the list of other characters.
			polygonGroups.Add(subBlock);
		}

		public static string GetLineHeightPropertiesEntry(FontInSize size, Configuration configuration, float boundingBox)
		{
			// Calculate line height.
			return EGGFont.PropertiesEntry.Replace("{(Ascent + Descent (in pixels)) * Screen Scale}", string.Format("{0}", (decimal)boundingBox * configuration.VerticalScale));
		}

		// A generic egg file, meant to contain Unicode characters in numbered groups where the number corresponds to the Unicode number of the character as the name of a "<Group>" element.
		public static string GenericEGGFileTemplate =
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
		Image Dimensions: {Texture Width}, {Texture Height}
		Texture Coordinates Scale: (1/{Texture Width}, -1/{Texture Height}) -> ({Inverse of Texture Width (decimal)}, {Negative Inverse of Texture Height})
		Descent Range: -{Descent (in pixels) * Screen Scale} - 0
		Ascent Range: 0 - {Ascent (in pixels) * Screen Scale}
}
{Properties Entry for Line Height}
{Texture Entries}
{Width Marker Vertex Pool}
{Unicode Characters as Individual Groups}";
		public static string PropertiesEntry =
@"<Group> properties {
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
		public static string UnicodeCharacterAsGroup =
@"<Group> {Character Code (int)} {
{Unicode Character as Vertex Pools}
{Polygon Entries}
	<PointLight> {
		<VertexRef> { {Width Marker (int)} <Ref> { characterWidthAsXPool } }
	}
}";
		public static string UnicodeCharacterAsWidth =
@"<Group> {Character Code (int)} {
	<PointLight> {
		<VertexRef> { {Width Marker (int)} <Ref> { characterWidthAsXPool } }
	}
}";
	}
}
