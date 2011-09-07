using System;
using Commons.UI.WPF.TestUtils;
using Db4oExplorer.Connections;
using Db4oExplorer.Domain;
using NUnit.Framework;
using Commons.TestUtils;

namespace Db4oExplorer.Test.Connection
{
	[TestFixture]
	public class LocalConnectionProfileViewTest:ControlTest<LocalConnectionProfileView>
	{
		protected override LocalConnectionProfileView CreateControl()
		{
			Logging.Setup();
			return new LocalConnectionProfileView(){DataSource = new LocalConnectionProfile()};
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
