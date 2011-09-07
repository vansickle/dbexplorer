using System.Collections.Generic;
using Db4oExplorer.Domain;
using Db4oExplorer.Explorer;

namespace Db4oExplorer
{
	public interface IMainWindow
	{
		ExplorerViewModel DataSource { get; set; }
	}
}
