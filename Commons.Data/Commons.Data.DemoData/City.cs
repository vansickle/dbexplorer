using System;

namespace Commons.Data.DemoData
{
	[Serializable]
	public class City
	{
		public string Name { get; set; }

		public Country Country { get; set; }
	}

}
