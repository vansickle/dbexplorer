using Db4oExplorer.Domain;

namespace Db4oExplorer.StoredClass
{
	public interface IDbObjectFactory
	{
		DbObject Create(IStoredClass storedClass);
	}
}
