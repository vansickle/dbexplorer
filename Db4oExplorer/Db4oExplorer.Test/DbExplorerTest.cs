using System;
using Commons.UI.WPF.TestUtils;
using Db4oExplorer.Explorer;
using NUnit.Framework;

namespace Db4oExplorer.Test
{
	[TestFixture]
	public class DbExplorerTest:ControlTest<DbExplorer>
	{
		protected override DbExplorer CreateControl()
		{
			return new DbExplorer(){DataSource = new ExplorerViewModel()};
		}

		[Test]
		public void Show()
		{
			ShowWin();
		}

		[Test]
		[Explicit]
		public void ExplicitShow()
		{
			Show();
			RunApp();
		}
	}
}
