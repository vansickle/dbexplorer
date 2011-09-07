using System;
using Commons.UI.WPF.TestUtils;
using Db4oExplorer.Connections;
using Db4oExplorer.Domain;
using NUnit.Framework;

namespace Db4oExplorer.Test.Connection
{
	[TestFixture]
	public class RemoteConnectionProfileViewTest:ControlTest<RemoteConnectionProfileView>
	{
		protected override RemoteConnectionProfileView CreateControl()
		{
			return new RemoteConnectionProfileView()
			       	{
			       		DataSource = new RemoteConnectionProfile()
			       		             	{
			       		             		Hostname = "127.0.0.1",
											Port = 53501,
											Login = "default",
											Password = "default"
			       		             	}
			       	};
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
