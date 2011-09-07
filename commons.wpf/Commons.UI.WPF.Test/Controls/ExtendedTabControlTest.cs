using System;
using Commons.UI.WPF.TestUtils;
using NUnit.Framework;
using Commons.UI.WPF.Controls.TabControl;

namespace Commons.UI.WPF.Test.Controls
{
	[TestFixture]
	public class ExtendedTabControlTest:ControlTest<ExtendedTabControl>
	{
		protected override ExtendedTabControl CreateControl()
		{
			return new ExtendedTabControl();
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
