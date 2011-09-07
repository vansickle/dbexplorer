using System.Collections.Generic;
using Db4oExplorer.Connections;
using Db4oExplorer.Domain;

namespace NeoDatisExplorer
{
	public class NeoDatisConnectionFactory : IConnectionFactory
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
			NeoDatisLocalConnection connection = null;
			if (profile is RemoteConnectionProfile)
				connection = new NeoDatisRemoteConnection();
			else
			{
				connection = new NeoDatisLocalConnection();
			}
			connection.Profile = profile;
			return connection;
		}
	}
}
