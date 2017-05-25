using System;
using System.Security.Cryptography;
using System.Text;

namespace FontTool
{
	public partial class SupportFunctions
	{
		public static string GetProgramVersionString()
		{
			return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
		}
	}
}
