using Commons.UI.WPF.TestUtils;
using NUnit.Framework;
using Commons.UI.WPF.Controls;

namespace Commons.UI.WPF.Test.Controls
{
	[TestFixture]
	public class WatermarkTextBoxTest:ControlTest<WatermarkTextBox>
	{
		protected override WatermarkTextBox CreateControl()
		{
			return new WatermarkTextBox();
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
			ShowWin();
			RunApp();
		}
	}
}
