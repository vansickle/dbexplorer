using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Db4oExplorer.Domain;
using Db4oExplorer.Explorer.Filters;
using Commons.UI.WPF;
using Commons.UI.WPF.EventWiring;

namespace Db4oExplorer.Explorer
{
	public class ExplorerViewModel:INotifyPropertyChanged
	{
		private readonly IFilterManager filterManager;
		private readonly IErrorHandler errorHandler;
		private string filterText;
		public string FilterText
		{
			get { return filterText; }
			set
			{
				filterText = value;
				PropertyChanged.Fire(this,"Connections");
			}
		}

		public List<IStoredClassFilter> Filters
		{
			get
			{
				if(filterManager==null)
					return new List<IStoredClassFilter>();
				return filterManager.Filters;
			}
		}

		private ObservableCollection<ConnectionViewModel> connections;

		public ExplorerViewModel(IList<IConnection> connections, 
			IFilterManager filterManager, 
			IErrorHandler errorHandler)
		{
			this.filterManager = filterManager;
			this.errorHandler = errorHandler;
			IEnumerable<ConnectionViewModel> enumerable = from connection in connections
			                                              select new ConnectionViewModel(connection,filterManager,errorHandler);
			this.connections = new ObservableCollection<ConnectionViewModel>(enumerable);
		}

		public ExplorerViewModel()
		{
		}


		public ObservableCollection<ConnectionViewModel> Connections
		{
			get
			{
				//TODO check for WatermarkText is bad, find another way
				if (!String.IsNullOrEmpty(filterText)&&(filterText!="Filter"))
					return new ObservableCollection<ConnectionViewModel>(from connection in connections
					                                                     where connection.Name.Contains(filterText)
					                                                     select connection);

				return connections;
			}
		}

		public bool IsEmpty
		{
			get
			{
				if (connections == null)
					return true;                    
				return Connections.Count == 0;
			}
		}

		public void Add(IConnection connection)
		{
			connections.Add(new ConnectionViewModel(connection,filterManager,errorHandler));
		}

		public void Remove(IConnection connection)
		{
			connections.Remove(new ConnectionViewModel(connection,filterManager,errorHandler));
		}

		public event PropertyChangedEventHandler PropertyChanged;
	}
}
