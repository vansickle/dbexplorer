using System;
using System.Collections;

namespace Commons.Data.DemoData
{
	public class CountryBuilder
	{
		public ICollection BuildAll()
		{
			return InternalBuildAll();
		}

		private ArrayList InternalBuildAll()
		{
			var list = new ArrayList();

			list.Add(new Country {Name = "Russia", Area = 17075400, InternetTLD = ".ru" });
			list.Add(new Country { Name = "United States of America", Area = 9826630, InternetTLD  = ".us"});
			list.Add(new Country { Name = "Canada", Area = 9984670, InternetTLD = ".ca"});
			list.Add(new Country { Name = "Mexico", Area = 1972550, InternetTLD = ".mx" });
			list.Add(new Country { Name = "Colombia", Area = 1141748, InternetTLD = ".co"});

			return list;
		}

		public object Build(int i)
		{
			return InternalBuildAll()[i];
		}
	}
}
