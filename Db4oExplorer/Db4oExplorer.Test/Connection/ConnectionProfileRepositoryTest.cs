using System.Collections.Generic;
using Db4oExplorer.Connections;
using Db4oExplorer.Domain;
using NUnit.Framework;

namespace Db4oExplorer.Test.Connection
{
	[TestFixture]
	public class ConnectionProfileRepositoryTest
	{
		private ConnectionProfileRepository repository;

		#region Setup/Teardown

		[SetUp]
		public void SetUp()
		{
			repository = new ConnectionProfileRepository();
		}

		#endregion

		[Test]
		public void Can_create_new()
		{
			repository.CreateNew(new LocalConnectionProfile{Name = "test.path",Path="test"});
		}

		[Test]
		public void Can_create_and_load()
		{
			repository.CreateNew(new LocalConnectionProfile { Name = "test.path", Path = "test2" });

			IList<IConnectionProfile> profiles = repository.GetAll();

			Assert.That(profiles,Has.Count.GreaterThan(0));
		}
	}
}
