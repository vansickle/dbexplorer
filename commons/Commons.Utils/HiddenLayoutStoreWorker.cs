using System.Collections.Generic;

namespace SP.Controls.LayoutDataStore
{
	public class HiddenLayoutStoreWorker:LayoutStoreWorker
	{
		public HiddenLayoutStoreWorker():base()
		{
			CreateHiddenLayoutDataTypePath();
		}

		private void CreateHiddenLayoutDataTypePath()
		{
			this.LayoutDataStorePathFactory= new LayoutDataStorePathFactory();
			LayoutDataStorePathFactory.TypePath = LayoutDataTypePath.ExecutablePath;
		}

		public HiddenLayoutStoreWorker(IForm4SaveLayout form, IList<ILayoutDataStore> stores) : base(form, stores)
		{
			CreateHiddenLayoutDataTypePath();

		}

		public HiddenLayoutStoreWorker(IForm4SaveLayout form, params ILayoutDataStore[] stores) : base(form, stores)
		{
			CreateHiddenLayoutDataTypePath();
		}

		public HiddenLayoutStoreWorker(string onlyFileName, IList<ILayoutDataStore> stores) : base(onlyFileName, stores)
		{
			CreateHiddenLayoutDataTypePath();
		}

		public HiddenLayoutStoreWorker(string onlyFileName, params ILayoutDataStore[] stores) : base(onlyFileName, stores)
		{
			CreateHiddenLayoutDataTypePath();
		}
	}
}
