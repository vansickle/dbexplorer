using System;
using System.Collections.Generic;
using Db4oExplorer.Domain;
using LeifTools.Export;
using NUnit.Framework;

namespace Db4oExplorer.Test.Export
{
	[TestFixture]
	public class SqlExporterTest
	{
		private SqlExporter exporter;

		#region Setup/Teardown

		[SetUp]
		public void SetUp()
		{
			exporter = new SqlExporter();
		}

		#endregion

		[Test]
		public void Export()
		{
			var export = exporter.Export(new List<DbObject>()
			                             	{
			                             		new DbObject() {Fields = new object[] {"field1", "field2"}},
			                             		new DbObject() {Fields = new object[] {"field1", "field2"}},
			                             		new DbObject() {Fields = new object[] {"field1", "field2"}},
			                             	});

			Console.Out.WriteLine(export);
		}
	}
}
