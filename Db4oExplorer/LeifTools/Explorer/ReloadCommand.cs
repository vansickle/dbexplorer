using System;
using System.Windows.Input;

namespace Db4oExplorer.Explorer
{
	public class ReloadCommand:ICommand
	{
		private readonly ConnectionViewModel model;

		public ReloadCommand(ConnectionViewModel model)
		{
			this.model = model;
		}

		public void Execute(object parameter)
		{
			model.RaiseReload();
		}

		public bool CanExecute(object parameter)
		{
			return model.IsConnected;
		}

		public event EventHandler CanExecuteChanged;
	}
}
