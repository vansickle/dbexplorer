using Db4objects.Db4o;
using Db4objects.Db4o.Config;
using Db4objects.Db4o.Ext;

namespace Commons.Data.Db4o
{
	public class ContainerFactory : IContainerFactory
	{
		private static IObjectContainer file;

		private readonly string filePath;

		public ContainerFactory(string filePath)
		{
			this.filePath = filePath;
		}

		public string FilePath
		{
			get { return filePath; }
		}

		public IObjectContainer GetContainer()
		{
			return GetContainer(filePath);
		}
		
		public IObjectContainer GetContainer(string path)
		{
			IConfiguration configure = Db4oFactory.Configure();
			//TODO configure container
			if (file == null)
				file = Db4oFactory.OpenFile(path);
			return file;
		}
	}
}
