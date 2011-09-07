using System;
using System.Collections;
using System.Collections.Generic;
using Commons.Data.DemoData;

namespace Db4oExplorer.Demo
{
	public class Db4oDemoBuilder
	{
		private CountryBuilder countryBuilder = new CountryBuilder();
		private CityBuilder cityBuilder = new CityBuilder();

		public IList CreateAll()
		{
			var list = new ArrayList();

			list.AddRange(countryBuilder.BuildAll());
			list.AddRange(cityBuilder.BuildAll());

			return list;
		}
	}
}
