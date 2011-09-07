using System;
using System.Collections.Generic;
using Commons.UI.WPF.TestUtils;
using Db4oExplorer.Domain;
using Db4oExplorer.Explorer;
using NUnit.Framework;

namespace Db4oExplorer.Test.UI
{
	[TestFixture]
	public class MainWindowTest:WindowTest<MainWindow>
	{
		public override MainWindow CreateWindow()
		{
			MainWindow mainWindow = new MainWindow();
			mainWindow.DataSource = new ExplorerViewModel(
				new List<IConnection>{new Db4oLocalConnectionStub(){Profile = new LocalConnectionProfile(){Name="test"}}},null,null);
			return mainWindow;
		}

		[Test]
		[Explicit]
		public void ExplicitShow()
		{
			Show();
			RunApp();
		}

		[Test]
		public void Show()
		{
			ShowWin();
		}
	}
}
