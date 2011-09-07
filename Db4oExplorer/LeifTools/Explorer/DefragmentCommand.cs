using System;
using System.Windows.Input;

namespace Db4oExplorer.Explorer
{
	public class DefragmentCommand:ICommand
	{
		private readonly ConnectionViewModel connectionViewModel;

		public DefragmentCommand(ConnectionViewModel connectionViewModel)
		{
			this.connectionViewModel = connectionViewModel;
		}

		public void Execute(object parameter)
		{
			connectionViewModel.Connection.Defragment();
		}

		public bool CanExecute(object parameter)
		{
			return true;
		}

		public event EventHandler CanExecuteChanged;
	}
}
