using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Db4oExplorer.Domain;

namespace LeifTools.QueryTool
{
	/// <summary>
	/// wrapper around connection, which user can access in QueryTool
	/// </summary>
	public class QueryObject
	{
		private readonly IConnection connection;
		private IStoredClass storedClass;

		public IStoredClass StoredClass
		{
			get { return storedClass; }
		}

		public QueryObject(IConnection connection)
		{
			this.connection = connection;
		}

		public object GetQuery(string typeName)
		{
			storedClass = connection.Objects.First(sc => sc.Name.Equals(typeName));

			return storedClass.GetQuery();

//			return storedClass;

//			return new Tuple<IStoredClass, IList>(storedClass, connection.GetQuery(typeName));
		}

		public void Save(IList<DbObject> dbObjects)
		{
			storedClass.Save(dbObjects);
		}
		
		public void Save(DbObject dbObject)
		{
			var dbObjects = new List<DbObject>(){dbObject};
			storedClass.Save(dbObjects);
		}

		public IList GetData(object query)
		{
			return StoredClass.GetData(query);
		}
	}
}
