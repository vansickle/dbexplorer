using System.Collections.Generic;

namespace Commons.Utils
{
	public class Transliter
	{
		private static readonly Dictionary<string, string> words =
			new Dictionary<string, string>
				{
					{"à", "a"},
					{"á", "b"},
					{"â", "v"},
					{"ã", "g"},
					{"ä", "d"},
					{"å", "e"},
					{"¸", "yo"},
					{"æ", "zh"},
					{"ç", "z"},
					{"è", "i"},
					{"é", "j"},
					{"ê", "k"},
					{"ë", "l"},
					{"ì", "m"},
					{"í", "n"},
					{"î", "o"},
					{"ï", "p"},
					{"ð", "r"},
					{"ñ", "s"},
					{"ò", "t"},
					{"ó", "u"},
					{"ô", "f"},
					{"õ", "h"},
					{"ö", "c"},
					{"÷", "ch"},
					{"ø", "sh"},
					{"ù", "sch"},
					{"ú", "j"},
					{"û", "i"},
					{"ü", "j"},
					{"ý", "e"},
					{"þ", "yu"},
					{"ÿ", "ya"},
					{"À", "A"},
					{"Á", "B"},
					{"Â", "V"},
					{"Ã", "G"},
					{"Ä", "D"},
					{"Å", "E"},
					{"¨", "Yo"},
					{"Æ", "Zh"},
					{"Ç", "Z"},
					{"È", "I"},
					{"É", "J"},
					{"Ê", "K"},
					{"Ë", "L"},
					{"Ì", "M"},
					{"Í", "N"},
					{"Î", "O"},
					{"Ï", "P"},
					{"Ð", "R"},
					{"Ñ", "S"},
					{"Ò", "T"},
					{"Ó", "U"},
					{"Ô", "F"},
					{"Õ", "H"},
					{"Ö", "C"},
					{"×", "Ch"},
					{"Ø", "Sh"},
					{"Ù", "Sch"},
					{"Ú", "J"},
					{"Û", "I"},
					{"Ü", "J"},
					{"Ý", "E"},
					{"Þ", "Yu"},
					{"ß", "Ya"}
				};


		public static string Rus2Lat(string source)
		{
			foreach (KeyValuePair<string, string> pair in words)
				source = source.Replace(pair.Key, pair.Value);

			return source;
		}
	}
}
