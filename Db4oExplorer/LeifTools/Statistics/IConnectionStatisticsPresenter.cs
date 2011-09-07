using Db4oExplorer.Domain;

namespace Db4oExplorer.Statistics
{
	public interface IConnectionStatisticsPresenter
	{
		void Show(IConnection obj);
	}
}
