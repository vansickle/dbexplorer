using Db4oExplorer;
using Commons.UI.WPF;

namespace LeifTools.Export
{
	public class SqlExportPresenter:AbstractTextExportPresenter
	{
		public SqlExportPresenter(MainWindow mainWindow, IWindowManager windowManager, IFileManager fileManager) : base(mainWindow, windowManager, fileManager, new SqlExporter(), "sql")
		{
		}
	}
}
