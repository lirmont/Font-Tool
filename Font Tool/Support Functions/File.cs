using System.IO;
using System.Windows.Forms;

namespace FontTool
{
	public static partial class SupportFunctions
	{
		public static string GetBaseLocation()
		{
			return Path.GetDirectoryName(Application.ExecutablePath);
		}

		public static string Combine(params string[] paths)
		{
			if (paths == null)
				return null;
			string currentPath = GetBaseLocation();
			for (int i = 0; i < paths.Length; i++)
				currentPath = Path.Combine(currentPath, paths[i]);
			return currentPath;
		}

		public static void TryDelete(string directory) {
			try
			{
				Directory.Delete(directory, true);
			}
			catch { }
		}

		public static void CopyDirectory(string source, string target, string filter = "*")
		{
			if (!Directory.Exists(target)) Directory.CreateDirectory(target);
			string[] sysEntries = Directory.GetFileSystemEntries(source, filter);
			foreach (string sysEntry in sysEntries)
			{
				string fileName = Path.GetFileName(sysEntry);
				string targetPath = Path.Combine(target, fileName);
				if (Directory.Exists(sysEntry))
				{
					CopyDirectory(sysEntry, targetPath, filter: filter);
				}
				else
					File.Copy(sysEntry, targetPath, true);
			}
		}

		public static string GetTemporaryDirectory()
		{
			string tempDirectory = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
			Directory.CreateDirectory(tempDirectory);
			return tempDirectory;
		}
	}
}
