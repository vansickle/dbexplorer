using System;
using System.Collections.Generic;
using System.Linq;
using Db4oExplorer.Domain;

namespace Db4oExplorer.Explorer.Filters
{
	public class SortFilter : AbstractStoredClassFilter
	{
		public SortFilter()
		{
			IsEnabled = true;
		}

		public override IList<IStoredClass> Apply(IList<IStoredClass> classes)
		{
			return classes.OrderBy(x => x.Name).ToList();
		}

		public override string ActionName
		{
			get { return "Sort by Name"; }
		}
	}
}
