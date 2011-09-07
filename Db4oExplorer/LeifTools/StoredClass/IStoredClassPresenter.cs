using Db4oExplorer.Domain;

namespace Db4oExplorer.StoredClass
{
	public interface IStoredClassPresenter
	{
		void RenameClass(IStoredClass obj);
		void CreateNew(IConnection obj);
	}
}
