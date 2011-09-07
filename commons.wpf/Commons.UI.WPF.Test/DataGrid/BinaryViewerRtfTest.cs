using System;
using System.IO;
using Commons.UI.WPF.TestUtils;
using NUnit.Framework;
using Commons.UI.WPF.DataGrid;

namespace Commons.UI.WPF.Test.DataGrid
{
	[TestFixture]
	public class BinaryViewerRtfTest:ControlTest<BinaryViewer>
	{
		protected override BinaryViewer CreateControl()
		{
			return new BinaryViewer() { DataSource = File.ReadAllBytes("testrtfdocument.rtf") };
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
