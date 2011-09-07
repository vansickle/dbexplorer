using Commons.UI.WPF;

namespace Db4oExplorer
{
	public class AboutPresenter
	{
		private readonly MainWindow mainWindow;
		private readonly AboutView aboutView;
		private readonly IWindowManager windowManager;

		public AboutPresenter(MainWindow mainWindow, AboutView aboutView, IWindowManager windowManager)
		{
			this.mainWindow = mainWindow;
			this.aboutView = aboutView;
			this.windowManager = windowManager;

			this.mainWindow.ShowAboutFired += window_ShowAboutFired;
		}


		void window_ShowAboutFired()
		{
			//TODO SHOW_DIALOG set not resizable
			//TODO SHOW_DIALOG set only ok container
			windowManager.ShowDialog(aboutView, "About");
		}
	}
}
