using System;
using Db4oExplorer.Domain;
using LeifTools.QueryTool;

namespace Db4oExplorer
{
	public class Db4oSampleQueryGenerator : ISampleQueryGenerator
	{
		public string Generate(IStoredClass storedClass)
		{
			return String.Format(
				"query = qo.GetQuery(\"{0}\")\ndata = qo.GetData(query)", storedClass.Name);
		}

		public string Generate()
		{
			return String.Format(
				@"#Sample code to query data:
query = qo.GetQuery(""Commons.Data.DemoData.Sports.Sport, Commons.Data.DemoData"")
data = qo.GetData(query)"
				);
		}
	}
}
