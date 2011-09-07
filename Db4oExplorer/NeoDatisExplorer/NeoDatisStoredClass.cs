using System;
using System.Collections;
using System.Collections.Generic;
using Db4oExplorer.Domain;
using LeifTools.Domain;
using NeoDatis.Odb.Core.Layers.Layer2.Meta;
using System.Linq;

namespace NeoDatisExplorer
{
	public class NeoDatisStoredClass:IStoredClass
	{
		private readonly ClassInfo ci;
		private readonly NeoDatisLocalConnection connection;
		private IList<Field> fields;

		public NeoDatisStoredClass(ClassInfo ci, NeoDatisLocalConnection connection)
		{
			this.ci = ci;
			this.connection = connection;
			Name = ci.GetFullClassName();
			var classAttributeInfos = ci.GetAttributes();
			fields = classAttributeInfos.Select(cai => (Field) new NeoDatisField() {Name = cai.GetName(), DataType = cai.GetAttributeType().GetName()}).ToList();
		}

		public NeoDatisStoredClass()
		{
		}

		public string Name
		{
			get; set;
		}

		public IList<Field> Fields
		{
			get { return fields; }
			set { fields = value; }
		}

		public IConnection Connection
		{
			get { throw new NotImplementedException(); }
		}

		private DotNetPureNameParser pureNameParser = new DotNetPureNameParser();

		public string PureName
		{
			get { return pureNameParser.Parse(Name); }
		}

		public IEnumerable<string> FieldNames
		{
			get { throw new NotImplementedException(); }
		}

		public void Rename(string text)
		{
			ci.SetFullClassName(text);
			Name = text;
			connection.Commit();
		}

		public void Save(IList<DbObject> dbObjects)
		{
			connection.Save(dbObjects);
		}

		public void Delete(IList objects)
		{
			throw new NotImplementedException();
		}

		public IList GetData()
		{
			return connection.GetData(this);
		}

		public object GetQuery()
		{
			return connection.GetQuery(this);
		}

		public IList GetData(object query)
		{
			return connection.ExecuteQuery(query);
		}

		public int GetFieldIndex(string fieldName)
		{
			throw new NotImplementedException();
		}

		public bool HasField(string name)
		{
			throw new NotImplementedException();
		}
	}
}
