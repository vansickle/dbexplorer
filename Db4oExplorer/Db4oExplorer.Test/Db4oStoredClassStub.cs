using System;
using System.Collections;
using System.Collections.Generic;
using Db4oExplorer.Domain;

namespace Db4oExplorer.Test
{
	public class Db4oStoredClassStub : IStoredClass
	{
		public string Name { get; set; }
		public IConnection Connection
		{
			get { throw new NotImplementedException(); }
		}

		public string PureName
		{
			get { throw new NotImplementedException(); }
		}

		public IEnumerable<string> FieldNames
		{
			get { throw new NotImplementedException(); }
		}

		public void Rename(string text)
		{
			throw new NotImplementedException();
		}

		public void Save(IList<DbObject> dbObjects)
		{
			throw new NotImplementedException();
		}

		public void Delete(IList objects)
		{
			throw new NotImplementedException();
		}

		public IList GetData()
		{
			throw new NotImplementedException();
		}

		public object GetQuery()
		{
			throw new NotImplementedException();
		}

		public IList GetData(object query)
		{
			throw new NotImplementedException();
		}

		public int GetFieldIndex(string fieldName)
		{
			throw new NotImplementedException();
		}

		public bool HasField(string name)
		{
			throw new NotImplementedException();
		}

		private IList<Field> fields;

		public IList<Field> Fields
		{
			get { return new List<Field>
			             	{
			             		new Db4oField{Name = "Number"},
			             		new Db4oField{Name = "Name"},
			             		new Db4oField{Name = "Year"},
			             		new Db4oField{Name = "Owner"},
			             	}; }
			set { fields = value; }
		}

		public override string ToString()
		{
			return string.Format("{0}", Name);
		}
	}
}
