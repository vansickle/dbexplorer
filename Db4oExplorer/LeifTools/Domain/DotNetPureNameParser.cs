using System.Text.RegularExpressions;

namespace LeifTools.Domain
{
	public class DotNetPureNameParser
	{
		private static readonly Regex REGEX_CLEAN_NAMESPACES_AND_ASSEMBLY = new Regex(@".*\.(?<name>.*),.*", RegexOptions.Compiled);

		public string Parse(string name)
		{
			Match match = REGEX_CLEAN_NAMESPACES_AND_ASSEMBLY.Match(name);
			if (match.Success)
				return match.Groups["name"].Value;
			else
				return name;
		}
	}
}
