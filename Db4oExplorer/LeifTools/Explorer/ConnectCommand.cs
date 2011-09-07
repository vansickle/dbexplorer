using System;
using System.ComponentModel;
using System.Windows.Input;
using EventFiringHelper=Commons.UI.WPF.EventWiring.EventFiringHelper;

namespace Db4oExplorer.Explorer
{
	public class ConnectCommand : ICommand
	{
		private readonly ConnectionViewModel model;

		public ConnectCommand(ConnectionViewModel model)
		{
			this.model = model;
			model.PropertyChanged += new PropertyChangedEventHandler(model_PropertyChanged);
		}

		void model_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if(e.PropertyName=="IsConnected")
				EventFiringHelper.Fire(CanExecuteChanged, this);
		}

		public void Execute(object parameter)
		{
			model.Connect();
		}

		public bool CanExecute(object parameter)
		{
			return !model.IsConnected;
		}

		public event EventHandler CanExecuteChanged;
	}
}
