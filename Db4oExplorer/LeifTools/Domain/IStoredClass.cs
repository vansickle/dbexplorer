using System.Collections;
using System.Collections.Generic;

namespace Db4oExplorer.Domain
{
	public interface IStoredClass
	{
		string Name { get; }
		IList<Field> Fields { get; set; }
		IConnection Connection { get; }
		string PureName { get; }
		IEnumerable<string> FieldNames { get; }
		void Rename(string text);
		void Save(IList<DbObject> dbObjects);
		void Delete(IList objects);
		IList GetData();
		object GetQuery();
		IList GetData(object query);
		int GetFieldIndex(string fieldName);
		bool HasField(string name);
	}
}
