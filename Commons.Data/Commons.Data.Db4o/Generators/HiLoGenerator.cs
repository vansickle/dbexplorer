using System;
using System.Runtime.CompilerServices;
using Db4objects.Db4o;

namespace Commons.Data.Db4o.Generators
{
	public class HiLoGenerator:IncrementGenerator
	{
		private readonly int capacity;
		private int currentLo;
		private int currentHi;

		public HiLoGenerator(int capacity, Type type) : base(type)
		{
			this.capacity = capacity;
			currentLo = capacity + 1;
		}

		[MethodImpl(MethodImplOptions.Synchronized)]
		public override int Generate(IObjectContainer container)
		{
			if(currentLo>=capacity)
			{
				currentHi = base.Generate(container);
				currentLo = 0;
			}

			return (currentHi - 1)*capacity + (++currentLo);
		}
	}
}
