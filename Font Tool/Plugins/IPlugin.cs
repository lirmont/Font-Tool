namespace FontTool.Plugins
{
	public interface IPlugin
	{
		BitmapFont Acquire();
		string Save(Configuration configuration = null, FontTool.SupportFunctions.ProgressUpdater updater = null);
	}
}
