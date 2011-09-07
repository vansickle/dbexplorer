using System;
using System.Collections;

namespace Commons.Data.DemoData
{
	public class CityBuilder
	{
		public ConvertableList<City> BuildAll()
		{
			var list = new ConvertableList<City>();

			list.Add(new City {Name = "Moscow", Country = new Country(){Name = "Russia"}});
			list.Add(new City {Name = "Washington, D.C.", Country = new Country(){Name = "USA"}});
			list.Add(new City {Name = "Ottawa", Country = new Country(){Name = "Ottawa"}});
			list.Add(new City {Name = "Mexico City", Country = new Country(){Name = "Mexico"}});
			list.Add(new City {Name = "Bogota", Country = new Country(){Name = "Columbia"}});

			return list;
		}
	}
}
