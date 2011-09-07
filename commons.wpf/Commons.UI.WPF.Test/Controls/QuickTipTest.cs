using System;
using Commons.UI.WPF.TestUtils;
using NUnit.Framework;

namespace Commons.UI.WPF.Test.Controls
{
	[TestFixture]
	public class QuickTipTest:ControlTest<QuickTip>
	{
		protected override QuickTip CreateControl()
		{
			return new QuickTip(){Title = "Title", Body= @"sadaffds
ssaffsfsa
dsfafdsafsdafassfddf dsfsdfsd fds fsd fds
fssfdfsd fds fsdfds"};
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
