using System;
using Commons.UI.WPF.TestUtils;
using NUnit.Framework;
using Commons.UI.WPF.Controls;

namespace Commons.UI.WPF.Test.Controls
{
	[TestFixture]
	public class ImageButtonTest:ControlTest<ImageButton>
	{
		protected override ImageButton CreateControl()
		{
			return new ImageButton();
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
