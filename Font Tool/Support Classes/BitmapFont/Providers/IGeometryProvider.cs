using System.Collections.Generic;
using System.Drawing;

namespace FontTool
{
	[System.Reflection.ObfuscationAttribute(Feature = "renaming", ApplyToMembers = true)]
	public interface IGeometryProvider
	{
		AutoTrace.Tracing GetGeometry(Bitmap image);
	}
}
