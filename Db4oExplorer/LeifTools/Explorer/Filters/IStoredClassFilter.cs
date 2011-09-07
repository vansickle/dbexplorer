using System.Collections.Generic;
using Db4oExplorer.Domain;

namespace Db4oExplorer.Explorer.Filters
{
	public interface IStoredClassFilter
	{
		IList<IStoredClass> Apply(IList<IStoredClass> classes);

		string ActionName { get; }
		bool IsEnabled { get; set; }
	}
}
