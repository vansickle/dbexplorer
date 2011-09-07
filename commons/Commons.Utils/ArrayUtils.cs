using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Commons.Utils.Delegates;

namespace Commons.Utils
{
	public static class ArrayUtils
	{
		public static bool ArraysEqual(byte[] a1, byte[] a2)
		{
			if (a1 == a2)
				return true;

			if (a1 == null || a2 == null)
				return false;

			if (a1.Length != a2.Length)
				return false;

			for (int i = 0; i < a1.Length; i++)
			{
				if (a1[i] != a2[i])
					return false;
			}
			return true;
		}

		public static bool ArraysEqual<T>(T[] a1, T[] a2)
		{
			if (ReferenceEquals(a1, a2))
				return true;

			if (a1 == null || a2 == null)
				return false;

			if (a1.Length != a2.Length)
				return false;

			EqualityComparer<T> comparer = EqualityComparer<T>.Default;
			for (int i = 0; i < a1.Length; i++)
			{
				if (!comparer.Equals(a1[i], a2[i])) return false;
			}
			return true;
		}

		public static string ForEachToString<T>(T[] addresses, FuncDelegate<string,T> func)
		{
			var builder = new StringBuilder();
			foreach (T address in addresses)
			{
				builder.Append(func(address) + ";");
			}
			return builder.ToString();
		}
	}
}
