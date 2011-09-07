using System;
using System.Collections;
using System.IO;
using System.Linq;
using Db4oExplorer.Domain;
using NUnit.Framework;
using Commons.Utils;

namespace NeoDatisExplorer.Test
{
	[TestFixture]
	public class NeoDatisChangeDataTest
	{
		private NeoDatisLocalConnection connection;

		#region Setup/Teardown
		[SetUp]
		public void SetUp()
		{
			string changedNdat = "test-changed.ndat";
			FileUtils.DeleteIfExists(changedNdat);
			File.Copy("test.ndat",changedNdat);

			connection = new NeoDatisLocalConnection() { Profile = new LocalConnectionProfile() { Path = "test-changed.ndat" } };
			connection.TryConnect();
		}

		#endregion

		[Test]
		public void Can_change_data()
		{
			IList list = connection.GetData(new NeoDatisStoredClass()
			                                	{Name = "Commons.Data.DemoData.Sports.Sport,Commons.Data.DemoData"});

			NeoDatisDbObject dbObject = list.OfType<NeoDatisDbObject>().First();
			
			Assert.That(dbObject[0],Is.EqualTo("volley-ball"));

			dbObject[0] = "football";

			connection.Save(dbObject);

			connection.Disconnect();

			connection.TryConnect();

			list = connection.GetData(new NeoDatisStoredClass() { Name = "Commons.Data.DemoData.Sports.Sport,Commons.Data.DemoData" });

			dbObject = list.OfType<NeoDatisDbObject>().First();

			Assert.That(dbObject[0], Is.EqualTo("football"));
		}
		
		[Test]
		public void Can_change_dateTime_data()
		{
			IList list = connection.GetData(new NeoDatisStoredClass()
			                                	{Name = "Commons.Data.DemoData.Sports.Sport,Commons.Data.DemoData"});

			NeoDatisDbObject dbObject = list.OfType<NeoDatisDbObject>().First();

			Assert.That(dbObject[1], Is.EqualTo(new DateTime(1972, 01, 01, 00, 00, 00)));

			dbObject[1] = new DateTime(1982,01,01,10,30,00);

			connection.Save(dbObject);

			connection.Disconnect();

			connection.TryConnect();

			list = connection.GetData(new NeoDatisStoredClass() { Name = "Commons.Data.DemoData.Sports.Sport,Commons.Data.DemoData" });

			dbObject = list.OfType<NeoDatisDbObject>().First();

			Assert.That(dbObject[1], Is.EqualTo(new DateTime(1982, 01, 01, 10, 30, 00)));
		}

		[TearDown]
		public void TearDown()
		{
			connection.Disconnect();
		}
	}
}
