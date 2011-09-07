using System;
using Db4objects.Db4o;

namespace Commons.Data.Db4o.Generators
{
	public class GuidGenerator:AbstractIdGenerator<Guid>
	{
		public override Guid Generate(IObjectContainer container)
		{
			return Guid.NewGuid();
		}
	}
}
