using System;
using System.Reflection;
using Common.Logging;
using Db4objects.Db4o;

namespace Commons.Data.Db4o
{
	/// <summary>
	/// singleton for objectcontainer
	/// </summary>
	public class Db4oDAOBase
	{
		#region logging

		protected static readonly ILog LOG =
			LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

		#endregion

		private IContainerFactory containerFactory;

		public Db4oDAOBase(IContainerFactory containerFactory)
		{
			this.containerFactory = containerFactory;
		}

		protected IObjectContainer GetDb()
		{
			return containerFactory.GetContainer();
		}

		protected void WithCommit(Action<IObjectContainer> action)
		{
			IObjectContainer db = GetDb();
			try
			{
				action.Invoke(db);
				db.Commit();
			}
			catch (Exception ex)
			{
				db.Rollback();
				if (LOG.IsDebugEnabled)
					LOG.Debug(ex);
				throw ex;
			}
		}
	}
}
