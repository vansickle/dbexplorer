using System.Collections.Generic;
using Db4oExplorer.Domain;

namespace Db4oExplorer.Explorer
{
	public class StoredClassViewModel
	{
		private readonly IStoredClass clazz;

		public StoredClassViewModel(IStoredClass clazz)
		{
			this.clazz = clazz;
		}

		public IList<Field> Fields
		{
			get { return clazz.Fields; }
		}

		public string Name
		{
			get { return clazz.Name; }
		}
	}
}
