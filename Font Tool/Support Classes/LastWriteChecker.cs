using System;
using System.IO;

namespace FontTool
{
	class LastWriteChecker
	{
		private DateTime lastWrite;
		private string filename;
		private ulong id;

		public ulong Id
		{
			get { return id; }
			set { id = value; }
		}

		public DateTime CurrentLastWrite
		{
			get
			{
				return File.GetLastWriteTime(this.filename);
			}
		}

		public LastWriteChecker(ulong id, string filename)
		{
			this.id = id;
			this.filename = filename;
			lastWrite = CurrentLastWrite;
		}

		// This method is called by the timer delegate. 
		public void CheckStatus(Object stateInfo)
		{
			DateTime thisWrite = CurrentLastWrite;
			if (DateTime.Compare(thisWrite, lastWrite) > 0)
			{
				// Update last write this object observed.
				lastWrite = thisWrite;
				// Signal reload.
				OnFileChanged();
			}
		}

		public delegate void FileChangedHandler(object sender);
		public event FileChangedHandler FileChanged;

		protected void OnFileChanged(object sender = null)
		{
			sender = sender ?? this;
			// Make sure there is a listener.
			if (FileChanged == null) return;
			FileChanged(sender);
		}
	}
}
