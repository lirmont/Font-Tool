namespace FontTool
{
	[System.Reflection.ObfuscationAttribute(Feature = "renaming", ApplyToMembers = true)]
	public interface IEffectImageProvider
	{
		ImageDescription GetImage(Character character, bool pushToContext);
	}
}
