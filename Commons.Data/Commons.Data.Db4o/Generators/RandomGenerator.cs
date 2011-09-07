using System;
using Db4objects.Db4o;

namespace Commons.Data.Db4o.Generators
{
	public class RandomGenerator:AbstractIdGenerator<int>
	{
		private Random random;

		public RandomGenerator()
		{
			random = new Random();
		}

		public override int Generate(IObjectContainer container)
		{
			return random.Next();
		}
	}
}
