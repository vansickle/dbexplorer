using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Threading;
using Common.Logging;
using Db4oExplorer.Connections;
using Db4oExplorer.Domain;
using Db4oExplorer.Fields;
using Db4oExplorer.Statistics;
using Db4oExplorer.StoredClass;
using LeifTools.Export;
using LeifTools.QueryTool;
using log4net.Config;
using Commons.Logging;
using Commons.UI.LayoutDataStore;
using Commons.UI.WPF;
using Commons.UI.WPF.Services;

namespace Db4oExplorer
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

			var layoutDataStorePathFactory = new LayoutDataStorePathFactory(LayoutDataTypePath.ApplicationData);
			
			WindowManager windowManager = new WindowManager(mainWindow, layoutDataStorePathFactory);

			var browseFileService = new BrowseFileService();

			StoredClassDataPresenter storedClassDataPresenter = new StoredClassDataPresenter(windowManager,browseFileService, new Db4oObjectFactory());

			Db4oConnectionFactory factory = new Db4oConnectionFactory();

			var errorHandler = new ErrorHandler(windowManager);

			ConnectionPresenter connectionPresenter = new ConnectionPresenter(new LocalConnectionProfileView(), windowManager, mainWindow, repository, factory, new RemoteConnectionProfileView(),browseFileService,errorHandler);
			connectionPresenter.LoadAll();

			var queryToolPresenterFactory = new QueryToolPresenterFactory(mainWindow, windowManager, browseFileService, new Db4oSampleQueryGenerator());

			var fileManager = new FileManager(browseFileService);

			mainWindow.ExportDataToSqlFired += new SqlExportPresenter(mainWindow, windowManager, fileManager).Export;

			mainWindow.ExportDataToCsvFired += new CsvExportPresenter(mainWindow, windowManager, fileManager).Export;

			StoredClassPresenter storedClassPresenter = new StoredClassPresenter(windowManager);

			var fieldPresenter = new FieldPresenter(windowManager);

			var fieldListPresenter = new FieldListPresenter(windowManager,fieldPresenter);

			var connectionStatisticsPresenter = new ConnectionStatisticsPresenter(windowManager);

			MainController mainController = new MainController(mainWindow, windowManager, storedClassDataPresenter, connectionPresenter, storedClassPresenter, fieldPresenter, fieldListPresenter,connectionStatisticsPresenter);

			mainWindow.explorer.ShowQueryToolFired += queryToolPresenterFactory.Show;

			var aboutForm = new AboutView();
			var aboutPresenter = new AboutPresenter(mainWindow, aboutForm, windowManager);

			this.ErrorHandler = errorHandler;

			windowManager.ShowWindow(mainWindow);			
		}
	}
}
