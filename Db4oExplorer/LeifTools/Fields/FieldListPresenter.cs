using Db4oExplorer.Domain;
using Db4oExplorer.StoredClass;
using Commons.UI.WPF;

namespace Db4oExplorer.Fields
{
	public class FieldListPresenter : IFieldListPresenter
	{
		private readonly IWindowManager windowManager;
		private readonly FieldPresenter fieldPresenter;

		public FieldListPresenter(IWindowManager windowManager, FieldPresenter fieldPresenter)
		{
			this.windowManager = windowManager;
			this.fieldPresenter = fieldPresenter;
		}

		public void ShowFields(IStoredClass obj)
		{
			StoredClassFieldListView view = new StoredClassFieldListView();
			view.DataSource = obj.Fields;
			view.RenameFieldFired += fieldPresenter.RenameField;
			view.IndexFieldFired += fieldPresenter.IndexField;

			windowManager.AddMainPane(view, obj.Name, null);
		}
	}
}
