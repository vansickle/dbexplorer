using Db4objects.Db4o;

namespace Commons.Data.Db4o
{
	public interface IContainerFactory
	{
		IObjectContainer GetContainer();
	    string FilePath { get; }
	}
}
