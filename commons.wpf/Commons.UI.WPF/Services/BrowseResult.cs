using System.IO;

namespace Commons.UI.WPF.Services
{
	public class BrowseResult
	{
		private readonly bool ok;
		private readonly string filePath;

		public BrowseResult(bool ok, string filePath)
		{
			this.ok = ok;
			this.filePath = filePath;
		}

		public string FileName
		{
			get { return Path.GetFileName(filePath); }
		}

		public bool Cancel
		{
			get { return !ok; }
		}

		public string FilePath
		{
			get {
				return filePath;
			}
		}
	}
}
