using System.Collections.Generic;

namespace FontTool
{
	public static partial class SupportFunctions
	{
		public static List<TracedCharacter> GetTracedCharactersForVariantAndColor(BitmapVariant variant, BitmapColor color)
		{
			List<TracedCharacter> tracedCharacters = new List<TracedCharacter>();
			foreach (Character character in variant.Characters)
				tracedCharacters.Add(
					TracedCharacter.Factory(character, variant, color, useEffects: true)
				);
			//
			return tracedCharacters;
		}
	}
}
