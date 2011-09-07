using System;
using Commons.UI.WPF.TestUtils;
using LeifTools.QueryTool;
using NUnit.Framework;

namespace Db4oExplorer.Test.QueryTool
{
	[TestFixture]
	public class QueryToolViewTest:ControlTest<QueryToolView>
	{
		protected override QueryToolView CreateControl()
		{
			return new QueryToolView();
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
