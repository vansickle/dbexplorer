using System;
using System.Windows;
using System.Windows.Controls;
using Commons.UI.WPF.TestUtils;
using NUnit.Framework;
using Commons.UI.WPF.DataGrid;

namespace Commons.UI.WPF.Test.DataGrid
{
	[TestFixture]
	public class DataGridBinaryColumnTest:ControlTest<FrameworkElement>
	{
		protected override FrameworkElement CreateControl()
		{
			var column = new DataGridBinaryColumn();
			return column.BuildElement();
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
