using Db4objects.Db4o;

namespace Commons.Data.Db4o.Generators
{
	public abstract class AbstractIdGenerator<T>:IIdGenerator,IIdGenerator<T>
	{
		object IIdGenerator.Generate(IObjectContainer container)
		{
			return Generate(container);
		}

		public abstract T Generate(IObjectContainer container);
	}
}
