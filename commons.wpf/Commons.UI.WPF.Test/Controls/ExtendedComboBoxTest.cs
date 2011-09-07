using System;
using System.Collections;
using Commons.UI.WPF.TestUtils;
using NUnit.Framework;
using Commons.UI.WPF.Controls;

namespace Commons.UI.WPF.Test.Controls
{
	[TestFixture]
	public class ExtendedComboBoxTest : ControlTest<ExtendedComboBox>
	{
		protected override ExtendedComboBox CreateControl()
		{
			return new ExtendedComboBox
			       	{ItemsSource = new ArrayList
			       	               	{
			       	               		new ComboDataStub("test1"),
			       	               		new ComboDataStub("test2"),
			       	               		new ComboDataStub("test3")
			       	               	}};
		}

		[Test]
		[Explicit]
		public void ExplicitShow()
		{
			ShowWin();
			RunApp();
		}
	}

	public class ComboDataStub
	{
		private readonly string data;

		public ComboDataStub(string data)
		{
			this.data = data;
		}

		public override string ToString()
		{
			return string.Format("Data: {0}", data);
		}
	}
}
