using System.Text.RegularExpressions;

namespace Commons.Utils
{
	public static class AutomaticPropertyUtils
	{
		private static readonly Regex REGEX_AUTO_PROPERTY_PATTERN = new Regex(@"\<(?<name>.*)\>.*", RegexOptions.Compiled);

		public static string GetFieldName(string propertyName)
		{
			return string.Format("<{0}>k__BackingField", propertyName);
		}

		public static string TryParseName(string name)
		{
			Match match = REGEX_AUTO_PROPERTY_PATTERN.Match(name);
			if (match.Success)
				return match.Groups["name"].Value;
			else
				return name;
		}
	}
}
