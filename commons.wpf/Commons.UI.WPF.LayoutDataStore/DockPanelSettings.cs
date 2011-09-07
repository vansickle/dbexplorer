using System;
using System.Collections.Generic;

namespace Commons.UI.WPF.LayoutDataStore
{
	[Serializable]
	public class DockPanelSettings
	{
		private List<DockPanelChildSettings> children = new List<DockPanelChildSettings>();

		public List<DockPanelChildSettings> Children
		{
			get { return children; }
			set { children = value; }
		}

		public void AddChild(DockPanelChildSettings settings)
		{
			children.Add(settings);
		}
	}
}
