using System;
using System.Collections.Generic;
using System.Text;

namespace Commons.Utils.v35
{
	public static class ObjectHelper
	{
		public static void IfInstanceOf<T>(this Object obj, Action<T> action) where T : class
		{
			T typedObject = obj as T;

			if(typedObject!=null)
				action.Invoke(typedObject);
		}
	}
}
