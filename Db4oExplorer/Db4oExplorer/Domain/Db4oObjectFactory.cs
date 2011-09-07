using System;
using Db4oExplorer.StoredClass;

namespace Db4oExplorer.Domain
{
	public class Db4oObjectFactory:IDbObjectFactory
	{
		public DbObject Create(IStoredClass storedClass)
		{
			return new Db4oObject((Db4oStoredClass)storedClass);
		}
	}
}
