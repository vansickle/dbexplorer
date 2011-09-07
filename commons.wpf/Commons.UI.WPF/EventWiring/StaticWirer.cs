using System;

namespace Commons.UI.WPF.EventWiring
{
	/// <summary>
	/// <see cref="Wirer"/>
	/// </summary>
	public class StaticWirer
	{
		private readonly Action action;

		public StaticWirer(Action action)
		{
			this.action = action;
		}

		public void On()
		{
			if (action != null)
				action.Invoke();
		}
	}
}
