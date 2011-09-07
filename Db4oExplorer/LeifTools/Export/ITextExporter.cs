using System.Collections;

namespace LeifTools.Export
{
	public interface ITextExporter
	{
		string Export(IList dbObjects);
	}
}
