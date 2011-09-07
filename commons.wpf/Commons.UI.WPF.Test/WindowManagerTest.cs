using System;
using System.Windows.Controls;
using Commons.UI.WPF.TestUtils;
using NUnit.Framework;
using Commons.UI.LayoutDataStore;

namespace Commons.UI.WPF.LayoutDataStore.Test
{
	[TestFixture]
	public class WindowManagerTest : UITest
	{
		#region Setup/Teardown

		[SetUp]
		public void Setup()
		{
			windowManager = new WindowManager(null, new LayoutDataStorePathFactory(LayoutDataTypePath.ExecutablePath));
		}

		#endregion

		private IWindowManager windowManager;

		private static Label GetLabelControl()
		{
			return new Label
			       	{
			       		Width = 800,
			       		Height = 50,
			       		Name = "testLabel",
			       		Content = "my test label"
			       	};
		}

		[Test, ExpectedException(typeof (ArgumentException))]
		public void control_name_must_be_fillng()
		{
			Label control = new Label
			                	{
			                		Content = "my test label"
			                	};
			windowManager.ShowDialog(control, "Test");
		}

		[Test]
		public void ShowControl()
		{
			windowManager.Show(GetLabelControl(), "Test");
		}

		[Test, Explicit]
		public void ShowControlExplicit()
		{
			ShowControl();
			RunApp();
		}

		[Test, Explicit]
		public void ShowDialog()
		{
			Label control = GetLabelControl();
			windowManager.ShowDialog(control, "Test");
		}

		[Test, Explicit]
		public void ShowDialogNoOkCancel()
		{
			Control control = new Control {Name = "testControl"};
			windowManager.ShowDialog(control, "Test", false);
		}

		[Test]
		public void ComboBox()
		{
			var selectTypeComboBox = new ComboBox { Name = "selectTypeComboBox" };
			var localItem = new ComboBoxItem { Content = "New local connection" };
			selectTypeComboBox.Items.Add(localItem);
			selectTypeComboBox.Items.Add(new ComboBoxItem() {Content = "New remote connection"});
			selectTypeComboBox.SelectedItem = localItem;
			selectTypeComboBox.Height = 18;
			selectTypeComboBox.MinHeight = 18;
//			selectTypeComboBox.MaxHeight = 18;
			selectTypeComboBox.Width = 300;
			selectTypeComboBox.MinWidth = 300;

			windowManager.ShowDialog(selectTypeComboBox, "Select connection type");
		}
	}
}
