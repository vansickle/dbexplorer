using System;
using System.Collections;
using System.Collections.Generic;
using Db4oExplorer.Domain;
using NUnit.Framework;
using System.Linq;

namespace Db4oExplorer.Test.Db4o
{
	[TestFixture]
	public class Db4oLocalConnectionTest
	{
		private Db4oLocalConnection connection;

		#region Setup/Teardown

		[SetUp]
		public void SetUp()
		{
			connection = new Db4oLocalConnection(){Profile = new LocalConnectionProfile(){Path="demo.dbo"}};
			connection.TryConnect();
		}

		[TearDown]
		public void TearDown()
		{
			connection.Disconnect();
		}

		#endregion

		[Test]
		public void Can_get_data()
		{
			IEnumerable enumerable = connection.GetData(new Db4oStoredClass() {Name = "CSBC.DataTransferObjects.Letter, CSBC.DataTransferObjects"});
			Assert.That(enumerable,Has.Count.GreaterThan(0));
		}
		
		[Test]
		public void Indexers()
		{
			IStoredClass storedClass =
				connection.Objects.First(cl => cl.Name == "CSBC.DataTransferObjects.Letter, CSBC.DataTransferObjects");
			IList enumerable = connection.GetData(storedClass);
//			IEnumerable enumerable = connection.GetData(new Db4oStoredClass() {Name = "CSBC.DataTransferObjects.Letter, CSBC.DataTransferObjects"});
			Db4oObject first = enumerable.OfType<Db4oObject>().First();
			object text = first["Text"];
			Assert.That(text, Is.EqualTo("test"));
		}

		[Test]
		public void Get_statistics()
		{
			IList<NameValue> statistics = connection.Statistics;

			Assert.That(statistics,Has.Count.EqualTo(3));
		}
	}
}
