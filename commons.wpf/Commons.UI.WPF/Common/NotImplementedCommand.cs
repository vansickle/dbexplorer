using System;
using System.Windows.Input;

namespace Commons.UI.WPF.Common
{
	/// <summary>
	/// commands that throws not implemented exception for show user of functionality 
	/// that just a stub for current moment
	/// </summary>
	public class NotImplementedCommand:ICommand
	{
		public void Execute(object parameter)
		{
			throw new NotImplementedException();
		}

		public bool CanExecute(object parameter)
		{
			return true;
		}

		public event EventHandler CanExecuteChanged;
	}
}
