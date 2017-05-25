using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Xml.Serialization;

namespace FontTool
{
	[System.Reflection.ObfuscationAttribute(Feature = "renaming", ApplyToMembers = true)]
	public class BitmapVariant : IImageProvider, IEffectImageProvider
	{
		private FontInSize dependency = null;
		private string name = null;
		private List<BitmapEffect> effects = null;

		[XmlAttribute(AttributeName = "name")]
		public string Name
		{
			get { return name; }
			set { name = value; }
		}

		[XmlIgnore]
		public FontInSize Dependency
		{
			get { return dependency; }
			set { dependency = value; }
		}

		[XmlIgnore]
		public DerivedCharacterCollection Characters = null; // Public before it becomes Characters.

		[XmlArray(ElementName = "effects")]
		[XmlArrayItem(ElementName = "effect", Type = typeof(BitmapEffect))]
		public List<BitmapEffect> Effects
		{
			get { return effects; }
			set { effects = value; }
		}

		[XmlIgnore]
		public CharactersByBlock CharactersByBlock
		{
			get
			{
				CharactersByBlock dictionary = new CharactersByBlock();
				foreach (ulong key in Characters.Keys)
				{
					Character thisCharacter = Characters[key];
					// Get block.
					UnicodeBlock block = UnicodeBlocks.GetBlockForCharacter(key);
					// Make character collection if not already made.
					if (!dictionary.Contains(block))
						dictionary[block] = new CharacterCollection();
					// Add record.
					CharacterCollection collection = (CharacterCollection)dictionary[block];
					if (collection != null)
						collection.Add(thisCharacter);
				}
				return dictionary;
			}
		}

		[XmlIgnore]
		public IImageProvider BaseImageProvider
		{
			get
			{
				return this as IImageProvider;
			}
		}

		[XmlIgnore]
		public IEffectImageProvider VariantImageProvider
		{
			get
			{
				return this as IEffectImageProvider;
			}
		}

		public BitmapVariant()
		{
			this.effects = new List<BitmapEffect>();
		}

		public BitmapVariant(BitmapVariant variant)
		{
			this.name = variant.name;
			this.dependency = variant.dependency;
			this.effects = variant.effects;
			//
			SynchronizeCharactersToParent();
		}

		public BitmapVariant(string name, FontInSize dependency)
		{
			this.name = name;
			this.dependency = dependency;
			this.effects = new List<BitmapEffect>();
			//
			SynchronizeCharactersToParent();
		}

		public void SynchronizeCharactersToParent()
		{
			this.Characters = SupportFunctions.GenerateDerivedCharacterCollection(Dependency.Characters, this);
		}

		private string CharacterPath(Character character)
		{
			return SupportFunctions.Combine(Dependency.Font.Name, string.Format("{0}", Dependency.Size), Name, string.Format("{0}.png", character.Code));
		}

		public override bool Equals(object obj)
		{
			if (obj is BitmapVariant)
			{
				BitmapVariant comparingObject = (BitmapVariant)obj;
				if (this.Name != comparingObject.Name)
					return false;
				else
					return SupportFunctions.Equivalent(this.Effects, comparingObject.Effects);
			}
			return base.Equals(obj);
		}

		// Use dependency as image provider/storer.
		ImageDescription IImageProvider.GetImage(Character character, bool pushToContext)
		{
			if (Dependency != null)
			{
				IImageProvider provider = (Dependency as IImageProvider);
				return provider.GetImage(character, pushToContext);
			}
			return null;
		}

		bool IImageProvider.SetImage(Bitmap image, Character character)
		{
			if (Dependency != null)
			{
				IImageProvider provider = (Dependency as IImageProvider);
				return provider.SetImage(image, character);
			}
			return false;
		}

		//
		ImageDescription IEffectImageProvider.GetImage(Character character, bool pushToContext)
		{
			// Get the base image (as created by the user).
			ImageDescription baseImageDescription = BaseImageProvider.GetImage(character, pushToContext);
			// If there is a base image, build up from it.
			if (baseImageDescription != null)
			{
				Bitmap baseBitmap = baseImageDescription.SignificantBitmap;
				Bitmap currentImage = baseBitmap;
				foreach (BitmapEffect effect in effects)
				{
					if (effect.shouldApplyEffectTo(character))
					{
						// Apply the effect to the top of the stack.
						currentImage = effect.Apply(currentImage, character: character, size: dependency);
						// If an effect sent nothing back, assume that's a signal to use the base image.
						if (currentImage == null)
							currentImage = baseImageDescription.SignificantBitmap;
					}
				}
				// Free the base image if it can be freed, replacing it with the final image.
				if (baseBitmap != currentImage)
				{
					baseBitmap.Dispose();
					baseImageDescription.replaceWithBitmap(currentImage);
				}
				return baseImageDescription;
			}
			return null;
		}
	}
}
