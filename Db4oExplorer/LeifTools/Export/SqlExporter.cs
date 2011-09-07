using System;
using System.Collections;

namespace LeifTools.Export
{
	public class SqlExporter:AbstractTextExporter, ITextExporter
	{
		public string Export(IList dbObjects)
		{
			return Export(dbObjects, "Export/Templates/DbObjects_to_SQL.st");
		}
	}
}
