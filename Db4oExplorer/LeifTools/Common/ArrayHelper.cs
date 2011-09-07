using System;

namespace Db4oExplorer.Common
{
	public static class ArrayHelper
	{
		public static void ForEach<T>(this T[] val, Action<int,T> action)
		{
			int i = 0;
			foreach(T o in val)
			{
				action.Invoke(i,o);
				i++;
			}
		}
	}
}
