namespace FontTool
{
	[System.Reflection.ObfuscationAttribute(Feature = "renaming", ApplyToMembers = true)]
	public class Character : OffsetableUnicodeCharacter
	{
		public Character()
		{
		}

		public Character(UnicodeCharacter character, float width, float advances, float ascends, float scaleX, float scaleY)
			: base(character, width, advances, ascends, scaleX, scaleY)
		{
		}
	}
}
