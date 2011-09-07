using System;
using System.Windows.Controls;
using Commons.UI.WPF.TestUtils;
using NUnit.Framework;
using Commons.UI.WPF.Controls;

namespace Commons.UI.WPF.Test
{
	[TestFixture]
	public class OKCancelControlContainerTest:WindowTest<OKCancelControlContainer>
	{
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

		public override OKCancelControlContainer CreateWindow()
		{
			OKCancelControlContainer container = new OKCancelControlContainer();
			container.Control = new Button();
			return container;
		}
	}
}
