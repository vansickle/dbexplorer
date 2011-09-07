using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Commons.UI.LayoutDataStore;
using Commons.UI.WPF.LayoutDataStore;

namespace Commons.UI.WPF.Controls
{
    public class DialogLayoutStoreWorkerContainer : AbstractLayoutStoreWorkerContainer, IDisposable
    {
        public DialogLayoutStoreWorkerContainer(Control window, IList<ILayoutDataStore> stores, ILayoutDataStorePathFactory layoutDataStorePathFactory) : base(window, stores, layoutDataStorePathFactory)
        {
        }

        #region IDisposable Members

        public void Dispose()
        {
            Close();
        }

        #endregion
    }

    public class AbstractLayoutStoreWorkerContainer
    {
        private readonly ILayoutStoreWorker worker;

        protected AbstractLayoutStoreWorkerContainer(Control window, IList<ILayoutDataStore> stores, ILayoutDataStorePathFactory layoutDataStorePathFactory)
        {
        	stores = stores ?? new List<ILayoutDataStore>();

            stores.Add(new WindowLayoutStore(window));
            worker = new LayoutStoreWorker(layoutDataStorePathFactory, window.Name, stores);
            LayoutStoreSupportUtils.Load(worker);
        }

        protected internal void Close()
        {
            LayoutStoreSupportUtils.Close(worker);
        }
    }

	public class LayoutStoreWorkerContainer
	{
		private ILayoutStoreWorker worker;

		public LayoutStoreWorkerContainer(string name, IList<ILayoutDataStore> stores, ILayoutDataStorePathFactory layoutDataStorePathFactory)
		{
			worker = new LayoutStoreWorker(layoutDataStorePathFactory, name, stores);
			LayoutStoreSupportUtils.Load(worker);
		}

		protected internal void Close()
		{
			LayoutStoreSupportUtils.Close(worker);
		}
	}

    /// <summary>
    /// works using Form.Closed event, instead of dispose, needed when we use Form.Show() (non-blocked) instead of Form.ShowDialog()
    /// </summary>
    public class NonblockingDialogLayoutStoreWorkerContainer : AbstractLayoutStoreWorkerContainer
    {
        public NonblockingDialogLayoutStoreWorkerContainer(Window window, IList<ILayoutDataStore> stores, ILayoutDataStorePathFactory layoutDataStorePathFactory)
            : base(window, stores, layoutDataStorePathFactory)
        {
            window.Closed += window_Closed;
        }

        private void window_Closed(object sender, EventArgs e)
        {
            Close();
        }
    }
}
