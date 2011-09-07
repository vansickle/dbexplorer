using System;
using System.Collections.Generic;
using System.Linq;
using Db4oExplorer.Domain;

namespace Db4oExplorer.Explorer.Filters
{
	public class SystemNamespaceFilter : AbstractStoredClassFilter
	{
		public SystemNamespaceFilter()
		{
			IsEnabled = true;
		}

		public override IList<IStoredClass> Apply(IList<IStoredClass> classes)
		{
			return classes.Where(clazz => !clazz.Name.StartsWith("System.")).ToList();
		}

		public override string ActionName
		{
			get { return "Hide System.* classes"; }
		}
	}
}
