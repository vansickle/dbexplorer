using System;
using System.Collections.ObjectModel;
using Db4oExplorer;
using Db4oExplorer.Domain;
using Db4oExplorer.Explorer;
using Commons.UI.WPF;
using Commons.UI.WPF.Services;

namespace LeifTools.QueryTool
{
	public class QueryToolPresenterFactory
	{
		private readonly IWindowManager windowManager;
		private readonly IBrowseFileService browseFileService;
		private ObservableCollection<ConnectionViewModel> connectionViewModels;
		private ISampleQueryGenerator sampleQueryGenerator;

		public QueryToolPresenterFactory(MainWindow mainWindow, IWindowManager windowManager, IBrowseFileService browseFileService, ISampleQueryGenerator sampleQueryGenerator)
		{
			this.windowManager = windowManager;
			this.sampleQueryGenerator = sampleQueryGenerator;
			this.browseFileService = browseFileService;
			connectionViewModels = mainWindow.DataSource.Connections;
			mainWindow.QueryToolFired += new Action(mainWindow_QueryToolFired);
		}

		void mainWindow_QueryToolFired()
		{
			var queryToolView = new QueryToolView();
			new QueryToolPresenter(queryToolView, connectionViewModels, browseFileService);
			queryToolView.QueryText = sampleQueryGenerator.Generate();
			windowManager.AddMainPane(queryToolView, "Query Tool", null);
		}

		public void Show(IStoredClass storedClass)
		{
			var queryToolView = new QueryToolView();
			var queryToolPresenter = new QueryToolPresenter(queryToolView, connectionViewModels, browseFileService);

			queryToolView.QueryText = sampleQueryGenerator.Generate(storedClass);
			windowManager.AddMainPane(queryToolView, "Query Tool", null);
		}
	}

	public interface ISampleQueryGenerator
	{
		string Generate(IStoredClass storedClass);
		string Generate();
	}
}
