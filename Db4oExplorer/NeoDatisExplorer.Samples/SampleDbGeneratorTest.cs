using System;
using NeoDatis.Odb;
using NUnit.Framework;
using Commons.Utils;
using Commons.Data.DemoData;
using Commons.Data.DemoData.Sports;

namespace NeoDatisExplorer.Test
{
	[TestFixture]
	public class SampleDbGeneratorTest
	{
		[Test]
		public void Create()
		{
			FileUtils.DeleteIfExists("sample.odb");

			// Create instance
			Sport sport1 = new Sport() { Name = "volley-ball", Olympic = new DateTime(1972, 01, 01, 00, 00, 00) };
			Sport sport2 = new Sport() { Name = "basket-ball" };
			Sport sport3 = new Sport() { Name = "hockey" };

			var cityBuilder = new CityBuilder();

			ODB odb = null;

			try
			{
				// Open the database
				odb = ODBFactory.Open("sample.odb");
//				odb = ODBFactory.Open("test.neod","test","test");

				// Store the object
				odb.Store(sport1);
				odb.Store(sport2);
				odb.Store(sport3);

				var convertableList = cityBuilder.BuildAll();

				foreach (var item in convertableList)
				{
					odb.Store(item);
				}
			}
			finally
			{
				if (odb != null)
				{
					// Close the database
					odb.Close();
				}
			}
		}
	}
}
