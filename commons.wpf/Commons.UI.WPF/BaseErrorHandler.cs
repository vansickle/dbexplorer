using System;

namespace Commons.UI.WPF
{
	public class BaseErrorHandler : IErrorHandler
	{
		protected readonly IWindowManager windowManager;

		public BaseErrorHandler(IWindowManager windowManager)
		{
			this.windowManager = windowManager;
		}

		public virtual void Handle(Exception exception)
		{
			string errorMessage = exception.ToString();

			if (exception is NotImplementedException)
				errorMessage = "This functionality not implemented yet - ask developer for new version";

			windowManager.Error(errorMessage);
		}
	}
}
