using System;
using System.Collections.Generic;

namespace Commons.Utils.v35
{
	public static class ListHelper
	{
		public static void ForEach<T>(this List<T> list, Action<T,int> action)
		{
			for (int i = 0; i < list.Count; i++)
			{
				action.Invoke(list[i],i);
			}
		}
	}
}
