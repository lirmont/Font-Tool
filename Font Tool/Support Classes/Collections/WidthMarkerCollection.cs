using System;
using System.Collections.Generic;
using System.Text;

namespace FontTool.Plugins
{
	public class WidthMarkerCollection : List<double>
	{
		// Template strings.
		static string CharacterWidthAsXPool =
@"<VertexPool> characterWidthAsXPool {
{Unicode Character Widths as Vertices}
}";
		static string UnicodeCharacterWidthAsVertex =
@"	<Vertex> {Width Marker (int)} { {Character Width * Screen Scale} 0 0 }";

		public int getIndexOfWidth(double width)
		{
			// If it doesn't exist in the collection, add it.
			if (!Contains(width))
				Add(width);
			// Send the index back.
			return IndexOf(width) + 1;
		}

		public int getIndexOfWidth(TracedCharacter tracedCharacter)
		{
			return getIndexOfWidth(tracedCharacter.DerivedUnicodeCharacter.FinalOffset.OffsetWidth);
		}

		public string getCollectionAsVertexPool(Configuration configuration)
		{
			// Prepare a place to store individual entries.
			List<string> entries = new List<string>();
			for (int i = 0; i < Count; i++)
			{
				double thisWidth = this[i] * (double)configuration.HorizontalScale;
				// Conversions to strings.
				string indexAsStringStartingFromOne = string.Format("{0}", i + 1);
				string widthAsString = string.Format("{0}", thisWidth);
				// Creation of the entries.
				string entryTemplate = WidthMarkerCollection.UnicodeCharacterWidthAsVertex;
				entryTemplate = entryTemplate.Replace("{Width Marker (int)}", indexAsStringStartingFromOne);
				entryTemplate = entryTemplate.Replace("{Character Width * Screen Scale}", widthAsString);
				// Add the entry.
				entries.Add(entryTemplate);
			}
			// Create the body for the main template.
			string entriesAsString = string.Join(Environment.NewLine, entries.ToArray());
			// Create the filled in template.
			string template = WidthMarkerCollection.CharacterWidthAsXPool;
			template = template.Replace("{Unicode Character Widths as Vertices}", entriesAsString);
			// Send it back.
			return template;
		}
	}
}
