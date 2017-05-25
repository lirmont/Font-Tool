using System.Drawing;

namespace FontTool
{
	public static partial class SupportFunctions
	{
		public static FontStyle GetFirstAvailableFontStyle(FontFamily family)
		{
			FontStyle style = family.IsStyleAvailable(FontStyle.Regular) ? FontStyle.Regular : family.IsStyleAvailable(FontStyle.Bold) ? FontStyle.Bold : family.IsStyleAvailable(FontStyle.Italic) ? FontStyle.Italic : family.IsStyleAvailable(FontStyle.Underline) ? FontStyle.Underline : FontStyle.Strikeout;
			return style;
		}
	}
}
