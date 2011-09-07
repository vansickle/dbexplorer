using System;

namespace Db4oExplorer.Domain
{
	[Serializable]
	public class RemoteConnectionProfile:IConnectionProfile
	{
		public string Name
		{
			get; set;
		}


		//TODO remove Path
		public string Path
		{
			get; set;
		}

		public string Hostname { get; set; }

		public int Port { get; set; }

		public string Login { get; set; }

		public string Password { get; set; }

		public IConnectionProfile Clone()
		{
			throw new NotImplementedException();
		}
	}
}
