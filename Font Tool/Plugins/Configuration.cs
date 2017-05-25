using System.Collections.Generic;
using System;
using OpenTK;

namespace FontTool.Plugins
{
	public class Configuration
	{
		public static Configuration Empty = new Configuration();
		//
		private string path = null;
		private decimal horizontalScale = 1m;
		private decimal verticalScale = 1m;
		private string minificationFilter = "NEAREST";
		private string magnificationFilter = "NEAREST";
		private int anistropicDegree = 0;
		private List<BitmapVariant> targetVariants = new List<BitmapVariant>();
		private List<BitmapColor> targetColors = new List<BitmapColor>();
		private bool wantMIPMapToSmallerFonts = true;
		private bool forceAbsolutePaths;
		private int charactersPerFile = int.MaxValue;

		public int CharactersPerFile
		{
			get { return charactersPerFile; }
			set { charactersPerFile = value; }
		}

		public List<BitmapVariant> TargetVariants
		{
			get { return targetVariants; }
			set { targetVariants = value; }
		}

		public List<BitmapColor> TargetColors
		{
			get { return targetColors; }
			set { targetColors = value; }
		}

		public string Path
		{
			get { return path; }
			set { path = value; }
		}

		public decimal HorizontalScale
		{
			get { return horizontalScale; }
			set { horizontalScale = value; }
		}

		public decimal VerticalScale
		{
			get { return verticalScale; }
			set { verticalScale = value; }
		}

		public Vector3d ScreenScale
		{
			get
			{
				return new Vector3d((double)HorizontalScale, (double)VerticalScale, (double)Math.Max(HorizontalScale, VerticalScale));
			}
		}

		public string MinificationFilter
		{
			get { return minificationFilter; }
			set { minificationFilter = value; }
		}

		public string MagnificationFilter
		{
			get { return magnificationFilter; }
			set { magnificationFilter = value; }
		}

		public string MIPMapMinificationFilter
		{
			get
			{
				if (WantMIPMapToSmallerFonts)
					return string.Format("{0}_MIPMAP_{0}", minificationFilter);
				else
					return minificationFilter;
			}
		}

		public string MIPMapMagnificationFilter
		{
			get
			{
				if (WantMIPMapToSmallerFonts)
					return string.Format("{0}_MIPMAP_{0}", magnificationFilter);
				else
					return magnificationFilter;
			}
		}

		public int AnistropicDegree
		{
			get { return anistropicDegree; }
			set { anistropicDegree = value; }
		}

		public bool WantMIPMapToSmallerFonts
		{
			get { return wantMIPMapToSmallerFonts; }
			set { wantMIPMapToSmallerFonts = value; }
		}

		// For some reason, egg2bam fails to find textures with -rawtex -mipmap if absolute Panda3d paths are not used in the EGG file. Set internally by the EmbeddedTextureBAMFont plugin.
		public bool ForceAbsolutePaths
		{
			get { return forceAbsolutePaths; }
			set { forceAbsolutePaths = value; }
		}

		public Configuration()
		{
		}

		public Configuration(string path, decimal horizontalScale, decimal verticalScale, string minificationFilter, string magnificationFilter, int anistropicDegree, List<BitmapVariant> targetVariants, List<BitmapColor> targetColors, bool wantMIPMapToSmallerFonts, bool forceAbsolutePaths = false, int? charactersPerFile = null)
		{
			Path = path;
			HorizontalScale = horizontalScale;
			VerticalScale = verticalScale;
			MinificationFilter = minificationFilter;
			MagnificationFilter = magnificationFilter;
			AnistropicDegree = anistropicDegree;
			TargetVariants = targetVariants;
			TargetColors = targetColors;
			WantMIPMapToSmallerFonts = wantMIPMapToSmallerFonts;
			ForceAbsolutePaths = forceAbsolutePaths;
			if (charactersPerFile != null)
				this.charactersPerFile = charactersPerFile.Value;
		}

		public bool HasExplicitCombinations
		{
			get {
				return (targetVariants.Count == 0 && targetColors.Count == 0) ? false : true;
			}
		}

		public bool HasVariant(BitmapVariant variant)
		{
			return TargetVariants.Exists(item => item == variant);
		}

		public bool HasVariantWithSize(FontInSize size)
		{
			return TargetVariants.Exists(item => item.Dependency == size);
		}

		// ARCHIVED CODE: Changes to process to include 3D complicates traditional mip-map method. Likely impossible to guarantee good result outside of 1:1 rectangular boundary traces. Lack metrics to find that information out.
		/*
		// Check against MIP Map options.
		bool textureHasMIPMaps = false;
		List<BitmapVariant> mipMaps = new List<BitmapVariant>();
		if (configuration.WantMIPMapToSmallerFonts)
		{
			// Figure out if this character is included.
			if (MIPMapHalfSizes.Count > 0)
			{
				// Add uninterrupted variants that contain the character.
				for (int mipMapIndex = 1; mipMapIndex < MIPMapHalfSizes.Count + 1; mipMapIndex++)
				{
					BitmapVariant thisVariant = MIPMapHalfSizes[mipMapIndex - 1];
					if (thisVariant.Characters.Contains(character.Id))
						mipMaps.Add(thisVariant);
					else
						break;
				}
				// If the list survived the filter, there are MIP maps, so add the level 0 MIP map.
				if (mipMaps.Count > 0)
				{
					mipMaps.Insert(0, variant);
					textureHasMIPMaps = true;
				}
			}
		}
		// If there are MIP Maps, iterate through all the provided variants, making character images.
		List<string> filenames = new List<string>();
		Rectangle thisRectangle = new Rectangle(0, 0, (variant.Dependency != null) ? (int)variant.Dependency.DefaultWidth : 0, 0);
		for (int r = 0; r < tracedCharacter.Traces.Count; r++)
		{
			AutoTrace.Tracing thisTrace = tracedCharacter.Traces[r];
			// If there are no MIP Maps, don't include a pound sign in the filename.
			string thisImageFilename = string.Format("{0}/{1}.png", new object[] { thisFileStub, character.Id });
			//
			string thisImageSaveLocation = Path.Combine(thisTextureFolder, string.Format("{0}.png", character.Id));
			// If the description exists, save it out and make a rectangle that represents it.
			if (thisTrace.Image != null)
			{
				Bitmap thisBitmap = thisTrace.Image;
				thisBitmap.Save(thisImageSaveLocation);
				thisRectangle = new Rectangle(0, 0, thisBitmap.Width, thisBitmap.Height);
				thisBitmap.Dispose();
			}
			else
			{
				Bitmap thisBitmap = new Bitmap(1, 1);
				thisBitmap.Save(thisImageSaveLocation);
				thisBitmap.Dispose();
			}
		}
		*/
	}
}
