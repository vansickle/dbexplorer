using System;
using System.Collections.Generic;
using System.Linq;
using Db4oExplorer.Domain;

namespace Db4oExplorer.Explorer.Filters
{
	public class Db4oNamespaceFilter : AbstractStoredClassFilter
	{
		public Db4oNamespaceFilter()
		{
			IsEnabled = true;
		}

		public override IList<IStoredClass> Apply(IList<IStoredClass> classes)
		{
			return classes.Where(clazz => !clazz.Name.StartsWith("Db4objects.Db4o.")).ToList();
		}

		public override string ActionName
		{
			get { return "Hide Db4objects.Db4o.* classes"; }
		}
	}
}
