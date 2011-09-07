using System;
using System.Collections.Generic;
using System.Text;

namespace Commons.Utils
{
    public class StringUtils
    {
        public static string SubString(string s, int startIndex, int length)
        {
            if (s.Length < length)
                return s.Substring(startIndex);
            return s.Substring(startIndex, length);
        }

        public static string SubStringOrEmpty(string s, int startIndex, int length)
        {
            if (s == null)
                return String.Empty;
            return SubString(s, startIndex, length);
        }


        public static string LastNsymbols(string sIn, int n)
        {
            if (sIn == null) return null;
            if (sIn.Length <= n) return sIn;
            return sIn.Substring(sIn.Length - n, n);
        }
 
        /// <summary>
        /// перенос по словам
        /// </summary>
        /// <param name="input">"большая" строка на входе</param>
        /// <param name="length">максимальная длина строк на выходе</param>
        /// <returns>массив строк "правильной" длины</returns>
        public static string[] Hyphenation(string input, int length)
        {
            int factor = input.Length/length;
            if (input.Length - factor*length > 0)
                factor++;
            string[] output = new string[factor];
            string[] strings = input.Split();
            int count = strings.Length;
            int j = 0;
            for (int i = 0; i < factor; i++)
            {
                output[i] += "";
                for (; j<count && output[i].Length + strings[j].Length <= length;j++ )
                    output[i] += " " + strings[j];
            }
            return output;
        }
    }
}
