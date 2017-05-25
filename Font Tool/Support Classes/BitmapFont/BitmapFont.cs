using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace FontTool
{
	[System.Reflection.ObfuscationAttribute(Feature = "renaming", ApplyToMembers = true)]
	[XmlRoot(ElementName = "font", Namespace = "http://darkabstraction.com/schemas/font.xsd")]
	public class BitmapFont
	{
		private string name = null;
		private SizeCollection sizes = null;

		[XmlElement(ElementName = "name")]
		public string Name
		{
			get { return name; }
			set { name = value; }
		}

		[XmlArray(ElementName = "colors")]
		[XmlArrayItem(ElementName = "color")]
		public BitmapColorCollection Colors { get; set; }

		[XmlArray(ElementName = "sizes")]
		[XmlArrayItem(ElementName = "size")]
		public SizeCollection Sizes
		{
			get {
				if (sizes != null && sizes.Count > 0)
				{
					for (int i = 0; i < sizes.Count; i++)
					{
						if (sizes[i].Font != this)
							sizes[i].Font = this;
					}
				}
				return sizes;
			}
			set {
				sizes = value;
			}
		}

		public uint Variations
		{
			get {
				int colors = Math.Max(1, Colors.Count);
				int totalVariations = 0;
				foreach (FontInSize size in Sizes) {
					int thisVariations = (size.Variants != null) ? size.Variants.Count * colors : 0;
					totalVariations += thisVariations;
				}
				return (uint)totalVariations;
			}
		}

		public string Path
		{
			get
			{
				return SupportFunctions.Combine("fonts", Name);
			}
		}

		private string XmlFilePath
		{
			get {
				return SupportFunctions.Combine(Path, "font.xml");
			}
		}

		public BitmapFont()
		{
		}

		private SizeCollection GetSizesFromDirectoryNames()
		{
			SizeCollection collection = new SizeCollection();
			string[] paths = Directory.GetDirectories(Path);
			foreach (string path in paths) {
				float size = 0f;
				string lastDirectory = System.IO.Path.GetFileName(path);
				bool parsed = float.TryParse(lastDirectory, out size);
				if (parsed)
					collection.Add(new FontInSize(size, this));
			}
			return collection;
		}

		public FontInSize AddSize(float size)
		{
			string path = System.IO.Path.Combine(Path, string.Format("{0}", size));
			if (!Directory.Exists(path))
				Directory.CreateDirectory(path);
			if (Sizes == null)
				Sizes = new SizeCollection();
			FontInSize fontInSize = new FontInSize(size, this);
			Sizes.Add(fontInSize);
			return fontInSize;
		}

		public BitmapColor AddColor(string name)
		{
			if (Colors == null)
				Colors = new BitmapColorCollection();
			if (!Colors.Contains(name))
			{
				BitmapColor color = new BitmapColor(name);
				Colors.Add(color);
				return color;
			}
			else
				return Colors[name];
		}

		private volatile MemoryStream sharedSerializationMemoryStream = new MemoryStream();

		public bool Save()
		{
			bool succeeded = true;
			try
			{
				// Lock MemoryStream object.
				lock (sharedSerializationMemoryStream)
				{
					if (!Directory.Exists(Path))
						Directory.CreateDirectory(Path);
					// Serialize to memory stream.
					SupportFunctions.BitmapFontSerializer.Serialize(sharedSerializationMemoryStream, this);
					// Reset memory stream position to start of file.
					sharedSerializationMemoryStream.Position = 0;
					// Create XML reader.
					using (XmlReader reader = XmlReader.Create(sharedSerializationMemoryStream))
					{
						SupportFunctions.TransformXML(reader, XmlFilePath);
					}
					// Erase everything, but do not recreate it.
					sharedSerializationMemoryStream.Position = 0;
					sharedSerializationMemoryStream.SetLength(0);
				}
			}
			catch {
				succeeded = false;
			}
			return succeeded;
		}

		public static BitmapFont Construct(string name) {
			BitmapFont thisFont = null;
			try
			{
				using (StreamReader reader = new StreamReader(SupportFunctions.Combine("fonts", name, "font.xml")))
				{
					thisFont = (BitmapFont)SupportFunctions.BitmapFontSerializer.Deserialize(reader);
					// Derive all the variants again (not stored in XML).
					foreach (FontInSize size in thisFont.Sizes)
						foreach (BitmapVariant variant in size.Variants)
							variant.SynchronizeCharactersToParent();
				}
			}
			catch { }
			//
			return thisFont;
		}
	}
}
