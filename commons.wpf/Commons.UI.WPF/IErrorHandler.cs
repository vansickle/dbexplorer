using System;

namespace Commons.UI.WPF
{
	public interface IErrorHandler
	{
		void Handle(Exception exception);
	}
}
