using Db4oExplorer;
using Commons.UI.WPF;

namespace LeifTools.Export
{
	public class CsvExportPresenter:AbstractTextExportPresenter
	{
		public CsvExportPresenter(MainWindow mainWindow, IWindowManager windowManager, IFileManager fileManager) : base(mainWindow, windowManager, fileManager, new CsvExporter(), "csv")
		{
		}
	}
}
