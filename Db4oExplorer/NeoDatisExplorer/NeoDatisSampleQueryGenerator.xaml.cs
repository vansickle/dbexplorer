using System;
using Db4oExplorer.Domain;
using LeifTools.QueryTool;

namespace NeoDatisExplorer
{
	public class NeoDatisSampleQueryGenerator : ISampleQueryGenerator
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
import clr
clr.AddReference(""NeoDatis"")
from NeoDatis.Odb.Core.Query.Criteria import Where

query = qo.GetQuery(""Commons.Data.DemoData.Sports.Sport,Commons.Data.DemoData"")
query.SetCriterion(Where.Equal(""name"", ""volley-ball""))
data = qo.GetData(query)

#your also can change data (not supported yet for NeoDatis):
#for item in data:
#	item.name = ""football""
#	qo.Save(item)");
		}
	}
}
