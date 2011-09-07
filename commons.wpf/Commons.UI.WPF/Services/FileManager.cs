using System.IO;
using System.Text;
using Commons.UI.WPF.Services;

namespace LeifTools.Export
{
	public class FileManager : IFileManager
	{
		private readonly IBrowseFileService browseFileService;

		public FileManager(IBrowseFileService browseFileService)
		{
			this.browseFileService = browseFileService;
		}

		public void Save(string text, BrowseParams browseParams)
		{
			var browseResult = browseFileService.Save(browseParams);

			if(browseResult.Cancel)
				return;
			
			File.WriteAllText(browseResult.FilePath, text, Encoding.UTF8);
		}
	}
}
