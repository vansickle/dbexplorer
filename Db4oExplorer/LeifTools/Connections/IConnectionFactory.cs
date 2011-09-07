using System;
using System.Collections.Generic;
using Db4oExplorer.Domain;

namespace Db4oExplorer.Connections
{
	public interface IConnectionFactory
	{
		IList<IConnection> Create(IList<IConnectionProfile> profiles);
		IConnection Create(IConnectionProfile profile);
	}
}
