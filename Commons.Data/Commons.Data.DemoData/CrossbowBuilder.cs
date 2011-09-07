using System;
using System.Collections;
using Commons.Utils.Delegates;

namespace Commons.Data.DemoData
{
	public class CrossbowBuilder
	{
		public IList BuildAll(FuncDelegate<object,Crossbow> convert)
		{
			return BuildAll().Convert(convert);
		}

		public ConvertableList<Crossbow> BuildAll()
		{
			var list = new ConvertableList<Crossbow>();

			list.Add(new Crossbow {Length = 1, Type = "Pull lever"});
			list.Add(new Crossbow {Length = 1.25, Type = "Pull lever"});
			list.Add(new Crossbow {Length = 1.25, Type = "Push lever"});
			list.Add(new Crossbow {Length = 1.25, Type = "Cranequin (Rack & Pinion)"});
			list.Add(new Crossbow {Length = 1.25, Type = "Windlass"});

			return list;
		}
	}
}
