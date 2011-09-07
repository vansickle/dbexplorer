namespace Commons.UI.LayoutDataStore
{
    public static class LayoutStoreSupportUtils
    {
        public static void Load(ILayoutStoreWorker worker)
        {
            if (worker == null) return;
        	worker.Init();
        }

        public static void Close(ILayoutStoreWorker worker)
        {
            if (worker != null) worker.Save();
        }
    }
}
