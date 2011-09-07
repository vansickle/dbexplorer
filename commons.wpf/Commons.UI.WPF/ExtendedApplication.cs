using System;
using System.Windows;
using System.Windows.Threading;

namespace Commons.UI.WPF
{
	public class ExtendedApplication:Application
	{
		public IErrorHandler ErrorHandler{ get; set; }

		public ExtendedApplication()
		{
			DispatcherUnhandledException += App_DispatcherUnhandledException;
		}

		private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
		{
            HandleException(e.Exception);
		    e.Handled = true;
		}

	    protected void HandleException(Exception exception)
	    {
			if(ErrorHandler!=null)
			{
				ErrorHandler.Handle(exception);
				return;
			}

			string messageBoxText = exception.ToString();

			if (exception is NotImplementedException)
				messageBoxText = "This functionality not implemented yet - ask developer for new version";

			MessageBox.Show(messageBoxText);

	    }
	}
}
