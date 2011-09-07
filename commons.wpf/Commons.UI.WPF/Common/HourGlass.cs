using System;
using System.Windows.Input;

namespace Commons.UI.WPF.Common
{
	public class HourGlass : IDisposable
	{
		public HourGlass()
		{
			Mouse.OverrideCursor = Cursors.Wait;
		}

		public void Dispose()
		{
			Mouse.OverrideCursor = null;
		}
	}
}
