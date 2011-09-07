using System;
using System.Collections;
using System.IO;
using Db4objects.Db4o;
using Db4objects.Db4o.Query;
using NUnit.Framework;

namespace Db4oExplorer.Demo
{
	[TestFixture]
	public class Db4oDemoBuilderTest
	{
		private Db4oDemoBuilder builder;
		private IObjectContainer container;
		private string path = "globe.dbo";

		#region Setup/Teardown

		[SetUp]
		public void SetUp()
		{
			builder = new Db4oDemoBuilder();

			File.Delete(path);
			
			container = Db4oFactory.OpenFile(path);
		}

		#endregion

		[Test]
		public void CreateDemoDb()
		{
			IList objects = builder.CreateAll();

			foreach (var o in objects)
			{
				container.Store(o);
			}

			IQuery query1 = container.Query();
			IObjectSet objectSet = query1.Execute();

			Assert.That(objectSet.Count,Is.EqualTo(objects.Count));
		}
		
		[TearDown]
		public void TearDown()
		{
			container.Close();
		}
	}
}
