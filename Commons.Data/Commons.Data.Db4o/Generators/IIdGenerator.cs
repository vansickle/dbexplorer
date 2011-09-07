using System;
using Db4objects.Db4o;

namespace Commons.Data.Db4o.Generators
{
	public interface IIdGenerator
	{
		object Generate(IObjectContainer container);
	}

	public interface IIdGenerator<T>
	{
		T Generate(IObjectContainer container);
	}
}
