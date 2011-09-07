using System.Collections.Generic;
using Db4oExplorer.Domain;

namespace Db4oExplorer.Explorer.Filters
{
	public abstract class AbstractStoredClassFilter : IStoredClassFilter
	{
		public abstract IList<IStoredClass> Apply(IList<IStoredClass> classes);
		public abstract string ActionName { get; }
		public bool IsEnabled { get; set; }
	}
}
