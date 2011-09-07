using Microsoft.Win32;

namespace Commons.UI.WPF.Services
{
	public class BrowseParams
	{
		public void Apply(FileDialog dialog)
		{
			dialog.Filter = Filter;
			dialog.FileName = FileNameWithoutExt;
			dialog.DefaultExt = DefaultExt;
			if (!string.IsNullOrEmpty(DefaultExt))
				dialog.AddExtension = true;
			dialog.Filter = Filter;
		}

		public string Filter { get; set; }

		public string DefaultExt { get; set; }

		public string FileNameWithoutExt { get; set; }
	}
}
