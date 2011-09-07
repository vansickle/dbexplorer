using System.Windows.Controls;
using Db4oExplorer.Domain;
using Commons.UI.WPF;

namespace Db4oExplorer.Statistics
{
	public class ConnectionStatisticsPresenter : IConnectionStatisticsPresenter
	{
		private readonly IWindowManager windowManager;

		public ConnectionStatisticsPresenter(IWindowManager windowManager)
		{
			this.windowManager = windowManager;
		}

		public void Show(IConnection obj)
		{
			windowManager.AddMainPane(new Label{Content = new TextBlock(){Text = obj.Statistics.ToString()}},obj.Name + " statistics",null);
		}
	}
}
