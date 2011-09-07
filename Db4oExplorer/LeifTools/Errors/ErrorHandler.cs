using System;
using Db4oExplorer.Domain;
using Commons.UI.WPF;

namespace Db4oExplorer
{
	public class ErrorHandler : BaseErrorHandler
	{
		public ErrorHandler(IWindowManager windowManager) : base(windowManager)
		{
			
		}

		public override void Handle(Exception exception)
		{
			var fileLockedException = exception as FileLockedException;

			if(fileLockedException!=null)
			{
				windowManager.Error(exception.Message);
				return;
			}

			base.Handle(exception);
		}
	}
}
