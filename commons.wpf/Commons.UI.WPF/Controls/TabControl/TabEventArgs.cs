using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace Commons.UI.WPF.Controls.TabControl
{
	public class TabItemEventArgs : EventArgs
	{
		public ExtendedTabItem TabItem { get; private set; }

		public TabItemEventArgs(ExtendedTabItem item)
		{
			TabItem = item;
		}
	}

	public class NewTabItemEventArgs : EventArgs
	{
		/// <summary>
		///     The object to be used as the Content for the new TabItem
		/// </summary>
		public object Content { get; set; }
	}

	public class TabItemCancelEventArgs : CancelEventArgs
	{
		public ExtendedTabItem TabItem { get; private set; }

		public TabItemCancelEventArgs(ExtendedTabItem item)
		{
			TabItem = item;
		}
	}
}
