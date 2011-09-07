using Db4oExplorer.Domain;

namespace Db4oExplorer.Connections
{
	public interface IConnectionPresenter
	{
		void AddNew(string fileName);
		void Edit(IConnection conn);
		void Delete(IConnection conn);
	}
}
