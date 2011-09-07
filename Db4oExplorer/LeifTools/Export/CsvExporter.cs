using System.Collections;

namespace LeifTools.Export
{
	public class CsvExporter : AbstractTextExporter, ITextExporter
	{
		public string Export(IList dbObjects)
		{
			return Export(dbObjects, "Export/Templates/DbObjects_to_CSV.st");
		}
	}
}
