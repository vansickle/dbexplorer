using System.Collections.Generic;
using Db4oExplorer.Domain;

namespace Db4oExplorer.Explorer.Filters
{
	public interface IFilterManager
	{
		IList<IStoredClass> Filter(IList<IStoredClass> classes);
		List<IStoredClassFilter> Filters { get; }
	}
}
