using System.Drawing;

namespace FontTool
{
	[System.Reflection.ObfuscationAttribute(Feature = "renaming", ApplyToMembers = true)]
	public interface IImageProvider
	{
		ImageDescription GetImage(Character character, bool pushToContext);
		bool SetImage(Bitmap image, Character character);
	}
}
