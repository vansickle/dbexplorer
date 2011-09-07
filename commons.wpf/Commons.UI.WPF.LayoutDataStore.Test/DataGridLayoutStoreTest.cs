using System;
using System.Windows;
using Commons.UI.WPF.TestUtils;
using Microsoft.Windows.Controls;
using NUnit.Framework;
using Commons.UI.LayoutDataStore;

namespace Commons.UI.WPF.LayoutDataStore.Test
{
    [TestFixture]
    public class DataGridLayoutStoreTest : ControlTest<Microsoft.Windows.Controls.DataGrid>
    {
        #region Setup/Teardown

        public override void SetUp()
        {
            base.SetUp();
            worker = new LayoutStoreWorker(defLayoutDataStorePathFactory, WindowName,
                new WindowLayoutStore(Window),
                new DataGridControlLayoutStore(Control, Window));
            worker.SetUI4LoadDefault();
            worker.SaveDefault();
            worker.Load();

            Window.Closed += Window_Closed;
        }

        #endregion

        protected ILayoutStoreWorker worker;

        private readonly ILayoutDataStorePathFactory defLayoutDataStorePathFactory
            = new LayoutDataStorePathFactory(LayoutDataTypePath.ExecutablePath);



        private void Window_Closed(object sender, EventArgs e)
        {
            if (worker != null) worker.Save();
        }

		protected override Microsoft.Windows.Controls.DataGrid CreateControl()
        {
			var grid = new Microsoft.Windows.Controls.DataGrid { Name = "testGrid" };
            grid.Columns.Add(new DataGridTextColumn {Header = "Head1"});
            grid.Columns.Add(new DataGridTextColumn {Header = "Head2"});
            grid.Columns.Add(new DataGridTextColumn {Header = "Head3"});
            grid.Columns.Add(new DataGridTextColumn {Header = "Head3a"});
            grid.Columns.Add(new DataGridTextColumn {Header = "Head4"});
            grid.HorizontalAlignment = HorizontalAlignment.Stretch;
            return grid;
        }


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
    }
}
