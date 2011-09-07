using System.Collections.Generic;
using Db4oExplorer.Domain;

namespace Db4oExplorer.Connections
{
	public class Db4oConnectionFactory : IConnectionFactory
	{
		public IList<IConnection> Create(IList<IConnectionProfile> profiles)
		{
			List<IConnection> result = new List<IConnection>();

			foreach (var profile in profiles)
			{
				result.Add(Create(profile));
			}

			return result;
		}

		public IConnection Create(IConnectionProfile profile)
		{
			Db4oLocalConnection connection = null;
			if (profile is RemoteConnectionProfile)
				connection = new Db4oRemoteConnection();
			else
			{
				connection = new Db4oLocalConnection();
			}
			connection.Profile = profile;
			return connection;
		}
	}
}
