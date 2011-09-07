using System;
using Db4oExplorer.Connections;
using Db4oExplorer.Fields;
using Db4oExplorer.Statistics;
using Db4oExplorer.StoredClass;
using LeifTools.QueryTool;
using Commons.UI.WPF;

namespace Db4oExplorer
{
	public class MainController
	{
		private readonly MainWindow window;
		private IWindowManager windowManager;
		private IStoredClassDataPresenter storedClassDataPresenter;
		private readonly IConnectionPresenter connectionPresenter;
		private readonly IStoredClassPresenter storedClassPresenter;

		public MainController(MainWindow window, 
			IWindowManager windowManager, 
			IStoredClassDataPresenter storedClassDataPresenter, 
			IConnectionPresenter connectionPresenter, 
			IStoredClassPresenter storedClassPresenter,
			IFieldPresenter fieldPresenter,
			IFieldListPresenter fieldListPresenter,
			IConnectionStatisticsPresenter connectionStatisticsPresenter
			)
		{
			this.window = window;
			this.windowManager = windowManager;
			this.storedClassDataPresenter = storedClassDataPresenter;
			this.connectionPresenter = connectionPresenter;
			this.storedClassPresenter = storedClassPresenter;

			this.window.explorer.ShowDataFired += storedClassDataPresenter.ShowData;
			this.window.explorer.ShowFieldsFired += fieldListPresenter.ShowFields;
			this.window.AddNewConnectionFired += this.connectionPresenter.AddNew;
			
			this.window.explorer.EditConnectionFired += this.connectionPresenter.Edit;
			this.window.explorer.DeleteConnectionFired += this.connectionPresenter.Delete;
			this.window.explorer.RenameClassFired += storedClassPresenter.RenameClass;
			this.window.explorer.RenameFieldFired += fieldPresenter.RenameField;
			this.window.explorer.ShowStatisticsFired += connectionStatisticsPresenter.Show;
			this.window.explorer.CreateNewStoredClassFired += storedClassPresenter.CreateNew;
		}
	}
}
