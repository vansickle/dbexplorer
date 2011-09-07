using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Controls;
using Common.Logging;
using Db4oExplorer.Domain;
using Db4oExplorer.Explorer;
using Db4oExplorer.Explorer.Filters;
using Commons.UI.WPF;
using Commons.UI.WPF.Services;

namespace Db4oExplorer.Connections
{
	public class ConnectionPresenter : IConnectionPresenter
	{
		#region logging

		protected static readonly ILog LOG = LogManager.GetLogger<ConnectionPresenter>();

		#endregion

		public ConnectionPresenter(LocalConnectionProfileView localConnectionProfileView, IWindowManager windowManager, IMainWindow mainWindow, IConnectionProfileRepository repository, IConnectionFactory factory, RemoteConnectionProfileView remoteConnectionProfileView, IBrowseFileService browseFileService, IErrorHandler errorHandler)
		{
			this.windowManager = windowManager;
			this.remoteConnectionProfileView = remoteConnectionProfileView;
			this.browseFileService = browseFileService;
			this.errorHandler = errorHandler;
			this.localConnectionProfileView = localConnectionProfileView;
			localConnectionProfileView.BrowseFired += new Action(localConnectionProfileView_BrowseFired);

			this.mainWindow = mainWindow;
			this.repository = repository;
			this.factory = factory;
		}

		private IBrowseFileService browseFileService;
		private readonly IErrorHandler errorHandler;

		void localConnectionProfileView_BrowseFired()
		{
			BrowseResult result = browseFileService.Browse(null);

			if (result.Cancel) return;
			
			localConnectionProfileView.DataSource.Path = result.FilePath;
		}

		private IWindowManager windowManager;
		private readonly IMainWindow mainWindow;

		private LocalConnectionProfileView localConnectionProfileView;
		private RemoteConnectionProfileView remoteConnectionProfileView;

		private IConnectionProfileRepository repository;
		private IConnectionFactory factory;

		public void AddNew(string fileName)
		{
			IConnectionProfileView view = null;

			if(fileName==null)
			{
				var selectTypeComboBox = new ComboBox { Name = "selectTypeComboBox" };
				var localItem = new ComboBoxItem { Content = "New local connection" };
				selectTypeComboBox.Items.Add(localItem);
				selectTypeComboBox.Items.Add(new ComboBoxItem() {Content = "New remote connection"});
				selectTypeComboBox.SelectedItem = localItem;
				selectTypeComboBox.Height = 18;
				selectTypeComboBox.MinHeight = 18;
				selectTypeComboBox.Width = 300;
				selectTypeComboBox.MinWidth = 300;

				if (!windowManager.ShowDialog(selectTypeComboBox, "Select connection type"))
					return;

				if (selectTypeComboBox.SelectedItem == localItem)
				{
					view = localConnectionProfileView;
					view.DataSource = new LocalConnectionProfile();
				}
				else
				{
					view = remoteConnectionProfileView;
					view.DataSource = new RemoteConnectionProfile();
				}
			}
			else
			{
				view = localConnectionProfileView;
				view.DataSource = new LocalConnectionProfile()
				                  	{
				                  		Path = fileName
				                  	};

			}

			if (!windowManager.ShowDialog((Control) view, "Add new connection"))
				return;

			IConnectionProfile profile = view.DataSource;
			
			repository.CreateNew(profile);

			mainWindow.DataSource.Add(factory.Create(profile));
	
			LOG.Debug(m => m("Connection {0} created",localConnectionProfileView.DataSource));
		}

		public void Edit(IConnection conn)
		{
			var originalProfile = conn.Profile;
			var profile = originalProfile.Clone();
			localConnectionProfileView.DataSource = profile;

			if (!windowManager.ShowDialog(localConnectionProfileView, "Edit connection"))
				return;

			repository.Update(originalProfile,profile);

			conn.UpdateProfileBy(profile);
		}

		public void Delete(IConnection conn)
		{
			if (!windowManager.Ask(string.Format("Are you sure want to delete connection: {0} ?", conn), "Delete"))
				return;

			repository.Delete(conn.Profile);

			mainWindow.DataSource.Remove(conn);
		}

		public void LoadAll()
		{
			IList<IConnectionProfile> connectionProfiles = repository.GetAll();
			IList<IConnection> connections = factory.Create(connectionProfiles);

			var manager = new FilterManager();
			manager.Add(new Db4oNamespaceFilter(), new SystemNamespaceFilter(), new SortFilter());

			mainWindow.DataSource = new ExplorerViewModel(connections, manager, errorHandler);
		}
	}
}
