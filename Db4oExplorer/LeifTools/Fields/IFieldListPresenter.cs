using Db4oExplorer.Domain;

namespace Db4oExplorer.Fields
{
	public interface IFieldListPresenter
	{
		void ShowFields(IStoredClass obj);
	}
}
