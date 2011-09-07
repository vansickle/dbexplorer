using Db4oExplorer.Domain;

namespace Db4oExplorer.Connections
{
	public interface IConnectionProfileView
	{
		IConnectionProfile DataSource { get; set; }
	}
}
