namespace FontTool
{
	[System.Reflection.ObfuscationAttribute(Feature = "renaming", ApplyToMembers = true)]
	public interface IColoredEffectImageProvider
	{
		ImageDescription GetImage(Character character, BitmapColor color, bool pushToContext);
	}
}
