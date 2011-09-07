using System.Collections.Generic;

namespace Commons.Utils
{
	public class Transliter
	{
		private static readonly Dictionary<string, string> words =
			new Dictionary<string, string>
				{
					{"�", "a"},
					{"�", "b"},
					{"�", "v"},
					{"�", "g"},
					{"�", "d"},
					{"�", "e"},
					{"�", "yo"},
					{"�", "zh"},
					{"�", "z"},
					{"�", "i"},
					{"�", "j"},
					{"�", "k"},
					{"�", "l"},
					{"�", "m"},
					{"�", "n"},
					{"�", "o"},
					{"�", "p"},
					{"�", "r"},
					{"�", "s"},
					{"�", "t"},
					{"�", "u"},
					{"�", "f"},
					{"�", "h"},
					{"�", "c"},
					{"�", "ch"},
					{"�", "sh"},
					{"�", "sch"},
					{"�", "j"},
					{"�", "i"},
					{"�", "j"},
					{"�", "e"},
					{"�", "yu"},
					{"�", "ya"},
					{"�", "A"},
					{"�", "B"},
					{"�", "V"},
					{"�", "G"},
					{"�", "D"},
					{"�", "E"},
					{"�", "Yo"},
					{"�", "Zh"},
					{"�", "Z"},
					{"�", "I"},
					{"�", "J"},
					{"�", "K"},
					{"�", "L"},
					{"�", "M"},
					{"�", "N"},
					{"�", "O"},
					{"�", "P"},
					{"�", "R"},
					{"�", "S"},
					{"�", "T"},
					{"�", "U"},
					{"�", "F"},
					{"�", "H"},
					{"�", "C"},
					{"�", "Ch"},
					{"�", "Sh"},
					{"�", "Sch"},
					{"�", "J"},
					{"�", "I"},
					{"�", "J"},
					{"�", "E"},
					{"�", "Yu"},
					{"�", "Ya"}
				};


		public static string Rus2Lat(string source)
		{
			foreach (KeyValuePair<string, string> pair in words)
				source = source.Replace(pair.Key, pair.Value);

			return source;
		}
	}
}
