using System.IO;
using System.Collections.Generic;
using System;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;

namespace FontTool.Plugins
{
	public class EmbeddedTextureBAMFont : IPlugin
	{
		string bamFile = null;
		BitmapFont fontToBAM = null;

		public EmbeddedTextureBAMFont(string eggFile)
		{
			if (File.Exists(eggFile))
			{
				this.bamFile = eggFile;
			}
		}

		public EmbeddedTextureBAMFont(BitmapFont font)
		{
			this.fontToBAM = font;
		}

		BitmapFont IPlugin.Acquire()
		{
			if (File.Exists(bamFile))
			{
				// Attempt to parse it.
			}
			return null;
		}

		string IPlugin.Save(Configuration configuration, SupportFunctions.ProgressUpdater updater) {
			configuration = configuration ?? Configuration.Empty;
			configuration.ForceAbsolutePaths = true;
			//
			bool hasUpdater = (updater != null) ? true : false;
			if (!hasUpdater)
				updater = delegate(string s) { };
			string directoryPath = SupportFunctions.GetTemporaryDirectory();
			//
			TextureBasedEGGFont textureBasedEGGFontExporter = new TextureBasedEGGFont(this.fontToBAM);
			IPlugin plugin = (IPlugin)textureBasedEGGFontExporter;
			string temporaryPathFromExporter = plugin.Save(configuration, updater);
			if (Directory.Exists(temporaryPathFromExporter)) {
				string[] fileList = Directory.GetFiles(temporaryPathFromExporter, "*.egg");
				string executable = "egg2bam";
				foreach (string file in fileList) {
					string bamStub = Path.GetFileNameWithoutExtension(file);
					string bamOut = Path.Combine(directoryPath, string.Format("{0}.bam", bamStub));
					updater(string.Format("Converting {0}.egg to BAM.", bamStub));
					try
					{
						ProcessStartInfo startInfo = new ProcessStartInfo();
						startInfo.UseShellExecute = false;
						startInfo.FileName = executable;
						startInfo.CreateNoWindow = true;
						startInfo.WindowStyle = ProcessWindowStyle.Hidden;
						//if (configuration.WantMIPMapToSmallerFonts)
						//	startInfo.Arguments = string.Format("-NC -rawtex -mipmap -o \"{0}\" \"{1}\"", new object[] { bamOut, file });
						//else
						startInfo.Arguments = string.Format("-flatten 0 -NC -rawtex -o \"{0}\" \"{1}\"", new object[] { bamOut, file });
						Process p = Process.Start(startInfo);
						p.WaitForExit();
					}
					catch {
						updater(string.Format("Failed to converting {0}.egg to BAM.", bamStub));
					}
				}
			}
			// Copy index files.
			updater("Copying index files into the workspace folder...");
			SupportFunctions.CopyDirectory(temporaryPathFromExporter, directoryPath, filter: "*.json");
			// Delete.
			updater("Deleting inherited temporary workspace folder...");
			SupportFunctions.TryDelete(temporaryPathFromExporter);
			//
			return directoryPath;
		}
	}
}
