using System;

namespace Db4oExplorer.Domain
{
	public class FileLockedException : Exception
	{
		public FileLockedException(string path):base(String.Format(
@"File {0} is locked. Probably it was used by your application.
You must first stop your application and only then open file here.
Otherwise you can use client-server(remote) connection here and 
in your application.", path
			))
		{
		}
	}
}
