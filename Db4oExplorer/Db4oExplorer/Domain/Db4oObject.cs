using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using Db4objects.Db4o.Reflect;
using Db4objects.Db4o.Reflect.Generic;
using Db4oExplorer.Common;

namespace Db4oExplorer.Domain
{
	public class Db4oObject:DbObject, IComparable
	{
		private readonly GenericObject genericObject;

		
		public GenericObject GenericObject
		{
			get { return genericObject; }
		}

		public Db4oObject(Db4oStoredClass storedClass):this(new GenericObject(storedClass.GenericClass))
		{
		}
		
		public Db4oObject(GenericObject genericObject)
		{
			this.genericObject = genericObject;
			GenericClass genericClass = genericObject.GetGenericClass();
			clazz = new Db4oStoredClass(genericClass);

			IReflectField[] fields = genericClass.GetDeclaredFields();

			this.fields = new object[fields.Length];

			int index = 0;
			while (index < fields.Length)
			{
				GenericField field = (GenericField) fields[index];
				object o = field.Get(genericObject);
//				object o = genericObject.Get(index);
//				if (o == null)
//				{
//					GenericClass fieldType = (GenericClass) field.GetFieldType();
//					o = fieldType.GetName();
//				}
//				else 
				if (o is GenericObject)
					o = new Db4oObject((GenericObject)o);
				this.fields[index] = o;
				index++;
			}
		}

		public Db4oObject()
		{
		}

		public object this[int i]
		{
			get
			{
				return fields[i];
			}
			set
			{
				fields[i] = value;
				RaisePropertyChanged("[" + i + "]");
			}
		}
	
		public object this[string fieldName]
		{
			get
			{
				int index = clazz.GetFieldIndex(fieldName);
				return this[index];
			}
			set
			{
				int index = clazz.GetFieldIndex(fieldName);
				this[index] = value;
			}
		}

		public override string ToString()
		{
			var builder = new StringBuilder();
			builder.AppendFormat("[{0}] ", clazz);
			foreach (object property in fields)
				builder.Append(property + " ");
			return builder.ToString();
		}

		public int CompareTo(object obj)
		{
			if (obj == null) return 1;

			Db4oObject o = obj as Db4oObject;

			if (o == null) return -1;


			if (fields.Length == 0)
				return String.Compare(this.Clazz.Name, o.Clazz.Name);

			int result = 0;

			int i = 0;
			while(result==0 && i<fields.Length)
			{
				object field = fields[i];
				object otherField = o.fields[i];

				if (otherField == null && field == null)
					break;

				if (otherField == null)
				{
					result = 1;
					break;
				}

				if(field == null)
				{
					result = -1;
				}

				var comparableField = field as IComparable;

				if(comparableField!=null)
				{
					result = comparableField.CompareTo(otherField);
					break;
				}

				
				string fieldString = field.ToString();
				string otherfieldString = otherField.ToString();

				result = String.Compare(fieldString, otherfieldString);

				i++;
			}

			return result;
		}

		public void UpdateGenericObject()
		{
			fields.ForEach((i,o)=>
			               	{
			               		Db4oObject dbObject = o as Db4oObject;

			               		if (dbObject != null)
			               			o = dbObject.GenericObject;

			               		genericObject.Set(i, o);
			               	});
		}

		public override bool TryGetMember(GetMemberBinder binder, out object result)
		{
			string name = binder.Name;
			if (clazz.HasField(name))
			{
				result = this[name];
				return true;
			}

			return base.TryGetMember(binder, out result); 
		}

		public override bool TrySetMember(SetMemberBinder binder, object value)
		{
			string name = binder.Name;
			if (clazz.HasField(name))
			{
				this[name] = value;
				return true;
			}

			return base.TrySetMember(binder, value);
		}

		public override IEnumerable<string> GetDynamicMemberNames()
		{
			return clazz.FieldNames;
		}
	}
}
