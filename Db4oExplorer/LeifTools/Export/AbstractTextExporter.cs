using System.Collections;
using System.IO;
using Antlr3.ST;

namespace LeifTools.Export
{
	public class AbstractTextExporter
	{
		public string Export(IList dbObjects, string templatePath)
		{
			StringTemplate template = new StringTemplate();

			var text = File.ReadAllText(templatePath);

			//remove all tabs - it used only to format template code, not output code
			template.Template = text.Replace("\t", "");

			template.SetAttribute("dbObjects", dbObjects);
			var output = template.ToString();

			return output;
		}
	}
}
