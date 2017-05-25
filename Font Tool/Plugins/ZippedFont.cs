using System.IO;
using System.IO.Compression;
using ICSharpCode.SharpZipLib.Zip;

namespace FontTool.Plugins
{
	public class ZippedFont : IPlugin
	{
		string zipFileName = null;
		BitmapFont fontToZip = null;

		// Test archive then unzip.
		public ZippedFont(string file)
		{
			if (File.Exists(file))
			{
				this.zipFileName = file;
			}
		}

		// Zip font, then save.
		public ZippedFont(BitmapFont font)
		{
			this.fontToZip = font;
		}

		BitmapFont IPlugin.Acquire()
		{
			try
			{
				if (File.Exists(zipFileName))
				{
					string shortName = Path.GetFileNameWithoutExtension(zipFileName);
					FastZip zipFile = new FastZip();
					zipFile.ExtractZip(zipFileName, SupportFunctions.Combine("fonts", shortName), null);
					return BitmapFont.Construct(shortName);
				}
			}
			catch { }
			return null;
		}

		string IPlugin.Save(Configuration configuration, SupportFunctions.ProgressUpdater updater)
		{
			configuration = configuration ?? Configuration.Empty;
			//
			string path = Path.GetTempFileName();
			FastZip zipFile = new FastZip();
			zipFile.CreateZip(path, fontToZip.Path, true, ".*\\.(xml|png)$");
			return path;
		}
	}
}
