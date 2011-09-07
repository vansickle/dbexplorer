using System.Collections;
using Db4oExplorer.Domain;
using NUnit.Framework;
using System.Linq;

namespace NeoDatisExplorer.Test
{
	[TestFixture]
	public class NeoDatisLocalConnectionTest
	{
		private NeoDatisLocalConnection connection;

		#region Setup/Teardown
		[SetUp]
		public void SetUp()
		{
			connection = new NeoDatisLocalConnection(){Profile = new LocalConnectionProfile(){Path="test.ndat"}};
			connection.TryConnect();
		}

		#endregion

		[Test]
		public void Can_get_data()
		{
			//ATTENTION you need create "test.ndat" first via SampleDbGenerator and put it to bin

			IEnumerable enumerable = connection.GetData(new NeoDatisStoredClass() { Name = "Commons.Data.DemoData.Sports.Sport,Commons.Data.DemoData" });
			Assert.That(enumerable,Has.Count.GreaterThan(0));
		}

		[Test]
		public void Class_fields_has_data_types()
		{
			IStoredClass storedClass = connection.Objects.First();
			Field field1 = storedClass.Fields[0];
			Assert.That(field1.DataType, Text.Contains("System.String"));

			Field field2 = storedClass.Fields[1];
			Assert.That(field2.DataType, Text.Contains("System.DateTime"));
		}

		[TearDown]
		public void TearDown()
		{
			connection.Disconnect();
		}
	}
}
