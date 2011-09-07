using System;
using System.IO;
using Commons.UI.WPF.TestUtils;
using NUnit.Framework;
using Commons.UI.WPF.DataGrid;

namespace Commons.UI.WPF.Test.DataGrid
{
	[TestFixture]
	public class BinaryViewerTest:ControlTest<BinaryViewer>
	{
		protected override BinaryViewer CreateControl()
		{
			byte[] bytes = File.ReadAllBytes("testimage.jpg");
			return new BinaryViewer(){DataSource = bytes};
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
