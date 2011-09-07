using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using Db4oExplorer.Domain;
using Db4oExplorer.Explorer.Filters;
using Commons.UI.WPF;
using Commons.UI.WPF.EventWiring;

namespace Db4oExplorer.Explorer
{
	public class ConnectionViewModel : INotifyPropertyChanged
	{
		private readonly IConnection connection;
		private DisconnectCommand disconnectCommand;
		private ConnectCommand connectCommand;
		private IFilterManager filterManager;
		private readonly IErrorHandler errorHandler;
		private ReloadCommand reloadCommand;
		private DefragmentCommand defragmentCommand;

		public ConnectionViewModel(IConnection connection, IFilterManager filterManager, IErrorHandler errorHandler)
		{
			this.connection = connection;
			this.filterManager = filterManager;
			this.errorHandler = errorHandler;
			this.disconnectCommand = new DisconnectCommand(this);
			this.connectCommand = new ConnectCommand(this);
			this.reloadCommand = new ReloadCommand(this);
			this.defragmentCommand = new DefragmentCommand(this);
		}

		public IConnection Connection
		{
			get { return connection; }
		}

		public ReloadCommand ReloadCommand
		{
			get { return reloadCommand; }
		}

		public ConnectCommand ConnectCommand
		{
			get { return connectCommand; }
		}

		public DefragmentCommand DefragmentCommand
		{
			get { return defragmentCommand; }
		}

		public string Icon
		{
			get
			{
				if (connection is IRemoteConnection)
				{
					if (connection.Status == ConnectionStatus.CONNECTED)
						return "../Images/server_connect.png";
					if (connection.Status == ConnectionStatus.ERROR)
						return "../Images/server_error.png";
					return "../Images/server.png";
				}
				else
				{
					if (connection.Status == ConnectionStatus.CONNECTED)
						return "../Images/database_connect.png";
					if (connection.Status == ConnectionStatus.ERROR)
						return "../Images/database_error.png";
					return "../Images/database.png";
				}
			}
		}

		public string Name
		{
			get { return connection.Name; }
		}

		public string Path
		{
			get { return connection.Path; }
		}

		public IList<IStoredClass> Objects
		{
			get
			{
				IList<IStoredClass> classes = connection.Objects;

				return filterManager.Filter(classes);
			}
		}

		public ICommand DisconnectCommand
		{
			get { return disconnectCommand; }
		}

		public bool IsConnected
		{
			get { return connection.IsConnected; }
		}

		public void Disconnect()
		{
			connection.Disconnect();
			PropertyChanged.Fire(this, "Objects");
			PropertyChanged.Fire(this, "IsConnected");
			PropertyChanged.Fire(this, "Icon");
		}

		public void Connect()
		{
			try
			{
				connection.TryConnect();
			}
			catch (Exception e)
			{
				errorHandler.Handle(e);
			}
			finally
			{
				PropertyChanged.Fire(this, "Objects");
				PropertyChanged.Fire(this, "IsConnected");
				PropertyChanged.Fire(this, "Icon");
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		public void Switch()
		{
			if (IsConnected)
				Disconnect();
			else
				Connect();
		}

		public void RaiseReload()
		{
			PropertyChanged.Fire(this, "Objects");
		}

		public bool IsSatisfy(string filterText)
		{
			bool satisfy = Name.Contains(filterText);

			if(satisfy)
				return true;

			//check children
//			IList<IStoredClass> classes = Objects;
//			foreach (var storedClass in classes)
//			{
//				
//			}

			return false;
		}
	}
}
