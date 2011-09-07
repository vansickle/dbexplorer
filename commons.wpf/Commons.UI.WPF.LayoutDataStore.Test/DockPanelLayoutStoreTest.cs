using System;
using Commons.UI.WPF.TestUtils;
using NUnit.Framework;
using Commons.UI.LayoutDataStore;

namespace Commons.UI.WPF.LayoutDataStore.Test
{
	[TestFixture]
	public class DockPanelLayoutStoreTest:ControlTest<DockPanelLayoutStoreTestControl>
	{
		private LayoutStoreWorker worker;

		protected override DockPanelLayoutStoreTestControl CreateControl()
		{
			return new DockPanelLayoutStoreTestControl();
		}

		public override void SetUp()
		{
			base.SetUp();
			var stores = Control.GetLayoutStores();
			worker = new LayoutStoreWorker(new LayoutDataStorePathFactory(LayoutDataTypePath.ExecutablePath), "DockPanelLayoutStoreTestControl",stores);
			worker.Init();
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

		public override void TearDown()
		{
			base.TearDown();
			worker.Save();
		}
	}
}
