using System;
using System.IO;
using Microsoft.Win32;

namespace Commons.UI.WPF.Services
{
	public class BrowseFileService : IBrowseFileService
	{
		public BrowseResult Browse(BrowseParams browseParams)
		{
			if(browseParams==null) browseParams = new BrowseParams();
			
			FileDialog dialog = new OpenFileDialog();
			return OpenDialog(dialog, browseParams);
		}

		public BrowseResult Save(BrowseParams browseParams)
		{
			var saveFileDialog = new SaveFileDialog();
			return OpenDialog(saveFileDialog, browseParams);
		}

		private BrowseResult OpenDialog(FileDialog dialog, BrowseParams browseParams)
		{
			using (new PreserveWorkingDirectory())
			{
				browseParams.Apply(dialog);
				if (dialog.ShowDialog().GetValueOrDefault())
				{
					return new BrowseResult(true, dialog.FileName);
				}
				return new BrowseResult(false, null);
			}
		}
	}
}
