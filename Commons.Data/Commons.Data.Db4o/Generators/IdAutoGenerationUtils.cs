using System;
using System.Collections;
using System.Collections.Generic;
using Db4objects.Db4o;
using Db4objects.Db4o.Events;
using Commons.Utils.Delegates;

namespace Commons.Data.Db4o.Generators
{
	//NOTE it's all is prototype, not tested
	public class AutoIdGenerator
	{
		private readonly IObjectContainer container;
		private readonly GeneratorMap generatorMap = new GeneratorMap();

		public AutoIdGenerator(IObjectContainer container)
		{
			this.container = container;
			IEventRegistry registry = EventRegistryFactory.ForObjectContainer(container);
			registry.Creating += new CancellableObjectEventHandler(registry_Creating);
		}

		void registry_Creating(object sender, CancellableObjectEventArgs args)
		{
			object entity = args.Object;

			TypeGeneratorPair pair = generatorMap.Get(entity.GetType());
			if (pair != null)
				pair.SetId(entity,container);
		}
	}

	public class GeneratorMap
	{
		private readonly List<TypeGeneratorPair> list = new List<TypeGeneratorPair>();

		public void Add(TypeGeneratorPair pair)
		{
			list.Add(pair);
		}

		public TypeGeneratorPair Get(Type type)
		{
			foreach (var pair in list)
			{
				if (pair.IsApplicable(type))
					return pair;
			}
			return null;
		}
	}

	public class TypeGeneratorPair
	{
		private readonly Type type;
		private readonly VoidDelegate<object, object> setter;
		private readonly IIdGenerator generator;

		public TypeGeneratorPair(Type type, VoidDelegate<object,object> setter, IIdGenerator generator)
		{
			this.type = type;
			this.setter = setter;
			this.generator = generator;
		}

		public bool IsApplicable(Type type)
		{
			return type.IsAssignableFrom(type);
		}

		public void SetId(object entity, IObjectContainer container)
		{
			setter.Invoke(entity,generator.Generate(container));
		}
	}
}
