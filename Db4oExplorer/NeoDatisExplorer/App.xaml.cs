using System;
using System.Collections.Specialized;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Common.Logging;
using Db4oExplorer;
using Db4oExplorer.Connections;
using Db4oExplorer.Fields;
using Db4oExplorer.Statistics;
using Db4oExplorer.StoredClass;
using LeifTools.Export;
using LeifTools.QueryTool;
using Commons.Logging;
using Commons.UI.LayoutDataStore;
using Commons.UI.WPF;
using Commons.UI.WPF.Services;
using ImageSourceConverter = Commons.UI.WPF.Converters.ImageSourceConverter;

namespace NeoDatisExplorer
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : ExtendedApplication
	{
		public App()
		{
			NameValueCollection collection = new NameValueCollection();
			collection.Add("configType", "FILE");
			collection.Add("configFile", "~/log.config");
			collection.Add("optionalConfigFile", "~/log.config.local");
			ExtendedLog4NetLoggerFactoryAdapter adapter = new ExtendedLog4NetLoggerFactoryAdapter(collection);
			LogManager.Adapter = adapter;


			//							<arg key="configType" value="FILE" />
			//				<arg key="configFile" value="~/log.config" />
			//				<arg key="optionalConfigFile" value="~/log.config.local" />

			ConnectionProfileRepository repository = new ConnectionProfileRepository();

			MainWindow mainWindow = new MainWindow();
			var appName = "NeoDatisExplorer";
			mainWindow.Title = appName;

			mainWindow.Icon = BitmapFrame.Create(new Uri("pack://application:,,,/NeoDatisExplorer;component/Images/logo.png"));

			var layoutDataStorePathFactory = new LayoutDataStorePathFactory(LayoutDataTypePath.ApplicationData);

			WindowManager windowManager = new WindowManager(mainWindow, layoutDataStorePathFactory);

			var browseFileService = new BrowseFileService();

			StoredClassDataPresenter storedClassDataPresenter = new StoredClassDataPresenter(windowManager, browseFileService, new NeoDatisObjectFactory());

			var errorHandler = new ErrorHandler(windowManager);

			ConnectionPresenter connectionPresenter = new ConnectionPresenter(new LocalConnectionProfileView(), windowManager, mainWindow, repository, new NeoDatisConnectionFactory(), new RemoteConnectionProfileView(), browseFileService, errorHandler);
			connectionPresenter.LoadAll();

			StoredClassPresenter storedClassPresenter = new StoredClassPresenter(windowManager);

			var fieldPresenter = new FieldPresenter(windowManager);

			var fieldListPresenter = new FieldListPresenter(windowManager, fieldPresenter);

			var connectionStatisticsPresenter = new ConnectionStatisticsPresenter(windowManager);

			var queryToolPresenterFactory = new QueryToolPresenterFactory(mainWindow, windowManager, browseFileService, new NeoDatisSampleQueryGenerator());

			var fileManager = new FileManager(browseFileService);

			mainWindow.ExportDataToSqlFired += new SqlExportPresenter(mainWindow, windowManager, fileManager).Export;

			mainWindow.ExportDataToCsvFired += new CsvExportPresenter(mainWindow, windowManager, fileManager).Export;

			MainController mainController = new MainController(mainWindow, windowManager, storedClassDataPresenter, connectionPresenter, storedClassPresenter, fieldPresenter, fieldListPresenter, connectionStatisticsPresenter);

			mainWindow.explorer.ShowQueryToolFired += queryToolPresenterFactory.Show;

			var aboutForm = new AboutView();
			aboutForm.AppName = appName;

			var aboutPresenter = new AboutPresenter(mainWindow, aboutForm, windowManager);

			this.ErrorHandler = errorHandler;

			windowManager.ShowWindow(mainWindow);
		}
	}
}


