using Db4objects.Db4o;

namespace Db4oExplorer.Domain
{
	public class Db4oRemoteConnection:Db4oLocalConnection,IRemoteConnection
	{
		public override void TryConnect()
		{
			RemoteConnectionProfile profile = (RemoteConnectionProfile) Profile;

			if (container == null)
				container = Db4oFactory.OpenClient(profile.Hostname, profile.Port, profile.Login, profile.Password);
		}
	}
}
