using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using Db4objects.Db4o;
using Db4objects.Db4o.Ext;
using Db4objects.Db4o.Reflect;
using Db4objects.Db4o.Reflect.Generic;
using LeifTools.Domain;
using Commons.UI.WPF.EventWiring;

namespace Db4oExplorer.Domain
{
	public class Db4oStoredClass : IStoredClass,INotifyPropertyChanged
	{
		private readonly GenericClass genericClass;

		public GenericClass GenericClass
		{
			get { return genericClass; }
		}

		private readonly Db4oLocalConnection connection;

		public IConnection Connection
		{
			get { return connection; }
		}

		private string name;
		public string Name
		{
			get { return name; }
			set
			{
				name = value;
				PropertyChanged.Fire(this,"Name");
			}
		}

		public IEnumerable<string> FieldNames
		{
			get
			{
				return Fields.Select(f=>f.Name);
			}
		}

		public void Rename(string newName)
		{
			Name = newName;
			Db4oLocalConnection conn = (Db4oLocalConnection) Connection;
			IObjectContainer container = conn.Container;
			Db4objects.Db4o.Ext.IStoredClass storedClass = container.Ext().StoredClass(genericClass);
			storedClass.Rename(newName);
		}

		private IList<Field> fields;

		public Db4oStoredClass(GenericClass clazz)
		{
			this.genericClass = clazz;
			Name = clazz.GetName();
			var list = new List<Field>();

			IReflectField[] declaredFields = clazz.GetDeclaredFields();

			foreach (IReflectField field in declaredFields)
			{
				list.Add(new Db4oField(field));
			}

			Fields = list;
		}

		private DotNetPureNameParser dotNetPureNameParser = new DotNetPureNameParser();

		/// <summary>
		/// Name without namespace, packages n etc., just name
		/// </summary>
		public string PureName
		{
			get
			{
				return dotNetPureNameParser.Parse(Name);
			}
		}

		public Db4oStoredClass(Db4objects.Db4o.Ext.IStoredClass clazz, Db4oLocalConnection connection)
		{
			this.connection = connection;
			Name = clazz.GetName();
			genericClass = connection.GetGenericClass(clazz.GetName());
			List<Field> list = new List<Field>();
			
			IStoredField[] storedFields = clazz.GetStoredFields();
			foreach (IStoredField field in storedFields)
			{
				list.Add(new Db4oField(field));
			}
			Fields = list;
		}

		public Db4oStoredClass()
		{
		}

		public IList<Field> Fields
		{
			get { return fields; }
			set { fields = value; }
		}

		public override string ToString()
		{
			return string.Format("{0}", Name);
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
			for (int i = 0; i < Fields.Count; i++)
			{
				if(Fields[i].Name == fieldName)
					return i;
			}
			throw new FieldNotFoundException();
		}

		public bool HasField(string name)
		{
			foreach (var fieldName in FieldNames)
			{
				if (name == fieldName)
					return true;
			}
			return false;
		}

		public void Delete(IList objects)
		{
			connection.Delete(objects);
		}

		public void Save(IList<DbObject> objects)
		{
			connection.Save(objects);
		}

		public event PropertyChangedEventHandler PropertyChanged;
	}

	public class FieldNotFoundException : ApplicationException
	{
		public FieldNotFoundException()
		{
		}

		public FieldNotFoundException(string message) : base(message)
		{
		}

		public FieldNotFoundException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected FieldNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
