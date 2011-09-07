using System;
using System.IO;
using System.Text;
using Commons.UI.WPF.TestUtils;
using NUnit.Framework;
using Commons.UI.WPF.DataGrid;

namespace Commons.UI.WPF.Test.DataGrid
{
	[TestFixture]
	public class BinaryViewerTextTest:ControlTest<BinaryViewer>
	{
		protected override BinaryViewer CreateControl()
		{
			return new BinaryViewer() { DataSource = UTF8Encoding.UTF8.GetBytes("Just a string\nand new line") };
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
