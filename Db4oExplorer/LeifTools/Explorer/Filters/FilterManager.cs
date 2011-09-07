using System;
using System.Collections.Generic;
using Db4oExplorer.Domain;

namespace Db4oExplorer.Explorer.Filters
{
	public class FilterManager : IFilterManager
	{
		private List<IStoredClassFilter> filters = new List<IStoredClassFilter>();

		public IList<IStoredClass> Filter(IList<IStoredClass> classes)
		{
			foreach (var filter in filters)
			{
				if(filter.IsEnabled)
					classes = filter.Apply(classes);
			}
			return classes;
		}

		public void Add(params IStoredClassFilter[] newFilters)
		{
			filters.AddRange(newFilters);
		}


		public List<IStoredClassFilter> Filters
		{
			get { return filters; }
		}
	}
}
