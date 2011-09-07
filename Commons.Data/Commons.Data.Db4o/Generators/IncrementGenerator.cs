using System;
using Db4objects.Db4o;

namespace Commons.Data.Db4o.Generators
{
	public class IncrementGenerator:AbstractIdGenerator<int>
	{
		private readonly Type type;
		private string semaphoreName;

		public IncrementGenerator(Type type)
		{
			if (type == null) throw new ArgumentNullException("type");
			
			this.type = type;
			semaphoreName = string.Format("id_gen_{0}", type.Name);
		}

		public override int Generate(IObjectContainer container)
		{
			//why this? not lock?
			while (!container.Ext().SetSemaphore(semaphoreName, 1000)) ;
			IObjectSet set = container.QueryByExample(new IdGeneratorData {Type = type});

			IdGeneratorData generatorData;
			if (set.Count == 0)
				generatorData = new IdGeneratorData { Type = type };
			else
				generatorData = (IdGeneratorData) set[0];

			var result = ++generatorData.Value;
			
			container.Store(generatorData);
			container.Ext().ReleaseSemaphore(semaphoreName);
			
			return result;
		}
	}

	public class IdGeneratorData
	{
		public Type Type { get; set; }
		public int Value { get; set; }
	}
}
