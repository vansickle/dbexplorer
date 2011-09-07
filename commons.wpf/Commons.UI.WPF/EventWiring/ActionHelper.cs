using System;

namespace Commons.UI.WPF.EventWiring
{
	/// <summary>
	/// <see cref="Wirer"/>
	/// </summary>
	public static class ActionHelper
	{
		public static void Wire(this Action action)
		{
			if (action != null)
				action.Invoke();
		}
	}
}
