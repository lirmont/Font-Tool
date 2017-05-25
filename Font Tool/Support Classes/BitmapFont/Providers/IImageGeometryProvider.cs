using System;
using System.Collections.Generic;
using System.Text;

namespace FontTool
{
	[System.Reflection.ObfuscationAttribute(Feature = "renaming", ApplyToMembers = true)]
	public interface IImageGeometryProvider
	{
		AutoTrace.Tracing GetGeometry(Character character);
	}
}
