using System.Collections.Generic;
using Db4oExplorer.Domain;
using NUnit.Framework;

namespace Db4oExplorer.Test.Domain
{
	[TestFixture]
	public class Db4oStoredClassTest
	{
		#region Setup/Teardown

		[SetUp]
		public void SetUp()
		{
			clazz = new Db4oStoredClass();
		}

		#endregion

		private Db4oStoredClass clazz;

		[Test]
		public void Pure_name_parsed_properly()
		{
			clazz.Name = "Db4o.Test.Data.Car, Db4o.Test";

			Assert.That(clazz.PureName, Is.EqualTo("Car"));
		}
	}
}
