using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using Db4objects.Db4o;
using Db4objects.Db4o.Config;
using Db4objects.Db4o.Defragment;
using Db4objects.Db4o.Ext;
using Db4objects.Db4o.Internal;
using Db4objects.Db4o.Query;
using Db4objects.Db4o.Reflect;
using Db4objects.Db4o.Reflect.Generic;
using Db4objects.Db4o.Reflect.Net;
using Sharpen.Lang;

namespace Db4oExplorer.Domain
{
	public class Db4oLocalConnection:LocalConnection
	{
		protected IObjectContainer container;

		public IObjectContainer Container
		{
			get { return container; }
		}

		public override IList<IStoredClass> Objects
		{
			get
			{
				if(!IsConnected)
					return new List<IStoredClass>();
	
				//TryConnect();
				
				Db4objects.Db4o.Ext.IStoredClass[] classes = container.Ext().StoredClasses();
				IReflectClass[] reflectClasses = container.Ext().KnownClasses();
				
				List<IStoredClass> storedClasses = new List<IStoredClass>();
//				foreach (IReflectClass reflectClass in reflectClasses)
//				{
//					storedClasses.Add(new Db4oStoredClass((GenericClass) reflectClass));
//				}

				foreach (Db4objects.Db4o.Ext.IStoredClass clazz in classes)
				{
//					var reflector = new GenericReflector(null, container.Ext().Reflector());
//					container.Ext().K
//					reflector.ForClass()
					storedClasses.Add(new Db4oStoredClass(clazz,this));
				}

				return storedClasses;
			}
		}

		public override bool IsConnected
		{
			get { return Status == ConnectionStatus.CONNECTED; }
		}

		public override IList<NameValue> Statistics 
		{ 
			get
			{
				var nameValues = new NameValueList();
				IExtObjectContainer ext = container.Ext();
				ISystemInfo systemInfo = ext.SystemInfo();
				nameValues.Add(new NameValue(){Name = "Total size", Value = systemInfo.TotalSize().ToString()});
				nameValues.Add(new NameValue(){Name = "Free space size", Value = systemInfo.FreespaceSize().ToString()});
				nameValues.Add(new NameValue(){Name = "Number of known classes", Value = ext.KnownClasses().Length.ToString()});
				
				return nameValues;
			}
		}

		public override void Disconnect()
		{
			container.Close();
			container = null;
			Status = ConnectionStatus.DISCONNECTED;
		}

		public override void TryConnect()
		{
			try
			{
				Connect();
			}
			catch (Exception e)
			{
				Status = ConnectionStatus.ERROR;

				var fileLockedException = e as DatabaseFileLockedException;
				if (fileLockedException != null)
					throw new FileLockedException(Path);
				
				throw;
			}
		}

		public override void Defragment()
		{
			container.Ext().Backup(Path + ".backup");
//			Db4objects.Db4o.Defragment.Defragment.Defrag(Path,Path+".backup");
			
		}

		public override void CreateNewClass()
		{
			IReflectClass[] classes1 = Container.Ext().KnownClasses();

			Db4objects.Db4o.Ext.IStoredClass[] storedClasses1 = Container.Ext().StoredClasses();
			IObjectContainer container = Container;
			//			var reflector = new GenericReflector(null, container.Ext().Reflector());
			var reflector = container.Ext().Reflector();

			IReflectClass reflectClass = container.Ext().Reflector().ForName("Test.NewClass, Test");

			var genericClass = new GenericClass(reflector, null, "Test.NewClass, Test", null);

			reflector.Register(genericClass);

			//			reflector.


			//			new GenericClass();

			IReflectClass[] classes = Container.Ext().KnownClasses();
			Db4objects.Db4o.Ext.IStoredClass[] storedClasses = container.Ext().StoredClasses();
		}

		private void Connect()
		{
			IConfiguration configuration = Db4oFactory.NewConfiguration();
			IObjectClass objectClass = configuration.ObjectClass(typeof(Type));
			objectClass.Translate(new HackedTType());
			configuration.ObjectClass(typeof(Type).GetType()).Translate(new HackedTType());

			if(container==null)
				container = Db4oFactory.OpenFile(configuration,Path);

			Status = ConnectionStatus.CONNECTED;
		}

		public override IList GetData(IStoredClass storedClass)
		{
			string className = storedClass.Name;

			return GetData(className);
		}

		public override IList GetData(string className)
		{
			IQuery query = GetQuery(className);

			return ExecuteQuery(query);
		}

		private IList ExecuteQuery(IQuery query)
		{
			IObjectSet objectSet = query.Execute();

			//TODO make wrapper for list
			//TODO make wrapper for object (instead of array we need to use some sort of proxy,
			//or domain object with TypeDescriptionProvider (look at SDO properties impl.) 
			
			ArrayList list = new ArrayList();

			foreach (GenericObject genericObject in objectSet)
			{
				Db4oObject dbObject = new Db4oObject(genericObject);
				
				list.Add(dbObject);
			}

			return list;
		}

		private IQuery GetQuery(string className)
		{
			IReflectClass reflectClass = container.Ext().Reflector().ForName(className);
			
			IQuery query = container.Query();

			query.Constrain(reflectClass);
			return query;
		}

		public void Delete(IList objects)
		{
			foreach (Db4oObject dbObject in objects)
			{
				container.Delete(dbObject.GenericObject);
			}
			
			container.Commit();
		}

		public void Save(IList<DbObject> objects)
		{
			foreach (Db4oObject dbObject in objects)
			{
				dbObject.UpdateGenericObject();
				container.Store(dbObject.GenericObject);
			}

			container.Commit();
		}

		public GenericClass GetGenericClass(string name)
		{
			return (GenericClass) container.Ext().Reflector().ForName(name);
		}


		public object GetQuery(Db4oStoredClass storedClass)
		{
			return GetQuery(storedClass.Name);
		}

		public IList ExecuteQuery(object query)
		{
			return ExecuteQuery(query as IQuery);
		}
	}

	/// <summary>
	/// version of TType that return typename, instead of null - otherwise we can't show saved fields of type System.Type
	/// </summary>
	public class HackedTType : IObjectConstructor
	{
		public void OnActivate(IObjectContainer objectContainer, object obj, object members)
		{
		}

		public Object OnInstantiate(IObjectContainer objectContainer, object obj)
		{
			if (obj != null)
			{
				try
				{
					return TypeReference.FromString((string)obj).Resolve();
				}
				catch
				{
					return obj as string;
				}
			}
			return null;
		}

		public Object OnStore(IObjectContainer objectContainer, object obj)
		{
			if (obj == null) return null;
			return TypeReference.FromType((Type)obj).GetUnversionedName();
		}

		public Type StoredClass()
		{
			return typeof(string);
		}
	}
}
