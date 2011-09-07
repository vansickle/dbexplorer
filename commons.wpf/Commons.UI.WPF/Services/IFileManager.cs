using Commons.UI.WPF.Services;

namespace LeifTools.Export
{
	public interface IFileManager
	{
		void Save(string text, BrowseParams browseParams);
	}
}
