using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Db4oExplorer.Domain;
using NeoDatis.Odb;
using NeoDatis.Odb.Core;
using NeoDatis.Odb.Core.Layers.Layer2.Instance;
using NeoDatis.Odb.Core.Layers.Layer2.Meta;
using NeoDatis.Odb.Core.Layers.Layer3;
using NeoDatis.Odb.Core.Layers.Layer3.Engine;
using NeoDatis.Odb.Core.Query;
using NeoDatis.Odb.Core.Query.Execution;
using NeoDatis.Odb.Impl;
using NeoDatis.Odb.Impl.Core.Layers.Layer2.Instance;
using NeoDatis.Odb.Impl.Core.Layers.Layer3.Engine;
using NeoDatis.Odb.Impl.Core.Query.Criteria;
using NeoDatis.Tool.Wrappers.List;

namespace NeoDatisExplorer
{
	public class NeoDatisLocalConnection: LocalConnection
	{
		private ODB odb;
		private IStorageEngine storageEngine;

		public override IList<IStoredClass> Objects
		{
			get
			{
				IOdbList<ClassInfo> classInfos = storageEngine.GetSession(true).GetMetaModel().GetAllClasses();
				return classInfos.Select<ClassInfo,IStoredClass>(ci => new NeoDatisStoredClass(ci, this) { }).ToList();
			}
		}

		public override bool IsConnected
		{
			get { return storageEngine != null; }
		}

		public override IList<NameValue> Statistics
		{
			get { throw new NotImplementedException(); }
		}

		public override void Disconnect()
		{
			storageEngine.Close();
			storageEngine = null;
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

				var fileLockedException = e as ODBRuntimeException;
				if (fileLockedException != null && fileLockedException.Message.Contains("file is locked"))
					throw new FileLockedException(Path);

				throw;
			}
		}

		private void Connect()
		{
			//			odb = ODBFactory.Open(Profile.Path);
			OdbConfiguration.SetCheckModelCompatibility(false);
			OdbConfiguration.SetAutomaticallyIncreaseCacheSize(true);
			//			var fileParameter = new IOFileParameter(Profile.Path, false, "test", "test");
			var fileParameter = new IOFileParameter(Profile.Path, true, null, null);
			//			ICoreProvider coreProvider = OdbConfiguration.GetCoreProvider();
			var coreProvider = new ExtendedCoreProvider();

			storageEngine = coreProvider.GetClientStorageEngine(fileParameter);
		}

		public override void Defragment()
		{
			throw new NotImplementedException();
		}

		public override void CreateNewClass()
		{
			throw new NotImplementedException();
		}

		public override IList GetData(IStoredClass storedClass)
		{
			string className = storedClass.Name;
			return GetData(className);
		}

		public override IList GetData(string className)
		{
			var criteriaQuery = new CriteriaQuery(className);
			//			return storageEngine.GetObjectInfos<AbstractObjectInfo>(criteriaQuery, true, 0, 1, true).ToList();
			return ExecuteQuery(criteriaQuery);
			return null;
		}

		public void Save(IList<DbObject> dbObjects)
		{
			foreach (NeoDatisDbObject dbObject in dbObjects)
			{
				InternalSave(dbObject);
			}

			storageEngine.Commit();
		}

		public void Save(NeoDatisDbObject dbObject)
		{
			InternalSave(dbObject);
			storageEngine.Commit();
		}

		private void InternalSave(NeoDatisDbObject dbObject)
		{
			dbObject.UpdateGenericObject();

			storageEngine.UpdateObject(dbObject.GenericObject, false);
		}

		public IList ExecuteQuery(object query)
		{
			CriteriaQuery criteriaQuery = (CriteriaQuery) query;
			return storageEngine.GetObjectInfos<object>(criteriaQuery, true, -1, -1, true)
				.Select((oi, i) => new NeoDatisDbObject((NonNativeObjectInfo)oi))
				.ToList();
		}

		public object GetQuery(NeoDatisStoredClass neoDatisStoredClass)
		{
			return new CriteriaQuery(neoDatisStoredClass.Name);
		}

		public void Commit()
		{
			(storageEngine as AbstractStorageEngine).UpdateMetaModel();
			storageEngine.Commit();
		}
	}

	public class ExtendedLocalStorageEngine:LocalStorageEngine
	{
		public ExtendedLocalStorageEngine(IBaseIdentification parameters) : base(parameters)
		{
			provider = new ExtendedCoreProvider();
		}
	}

	public class ExtendedCoreProvider: DefaultCoreProvider
	{
		public override IInstanceBuilder GetLocalInstanceBuilder(IStorageEngine engine)
		{
			return new ExtendedLocalInstanceBuilder(engine);
		}

		public override IStorageEngine GetClientStorageEngine(IBaseIdentification baseIdentification)
		{
			return new ExtendedLocalStorageEngine(baseIdentification);
		}

		public override IObjectReader GetClientObjectReader(IStorageEngine engine)
		{
			return new ExtendedObjectReader(engine);
		}

		public override IMatchingObjectAction GetCollectionQueryResultAction(IStorageEngine engine, IQuery query, bool inMemory, bool returnObjects)
		{
					return new NeoDatis.Odb.Impl.Core.Query.Criteria.CollectionQueryResultAction<object>(query
			, inMemory, engine, returnObjects, new ExtendedLocalInstanceBuilder(engine)
			);
		}

//		public override IClassPool GetClassPool()
//		{
//			return new ExtendedClassPool();
//		}
	}

	public class ExtendedObjectReader : ObjectReader
	{
		public ExtendedObjectReader(IStorageEngine engine) : base(engine)
		{
		}

		protected override IInstanceBuilder BuildInstanceBuilder()
		{
			return new ExtendedLocalInstanceBuilder(base.storageEngine);
		}
	}

//
//	public class ExtendedClassPool : ODBClassPool
//	{
//		public override Type GetClass(string className)
//		{
//			return null;
//		}
//	}

	public class ExtendedLocalInstanceBuilder : LocalInstanceBuilder
	{
		public ExtendedLocalInstanceBuilder(IStorageEngine engine) : base(engine)
		{
		}

		public override object BuildOneInstance(NonNativeObjectInfo objectInfo)
		{
			return objectInfo;
		}
	}
}
