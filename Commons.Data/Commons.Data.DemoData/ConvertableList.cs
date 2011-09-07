using System.Collections;
using Commons.Utils.Delegates;

namespace Commons.Data.DemoData
{
	public class ConvertableList<T>:ArrayList
	{
		public ConvertableList<object> Convert(FuncDelegate<object,T> convert)
		{
			var arrayList = new ConvertableList<object>();
			foreach (T entity in this)
			{
				arrayList.Add(convert.Invoke(entity));
			}
			return arrayList;
		}
	}
}
