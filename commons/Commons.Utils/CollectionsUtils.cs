using System;
using System.Collections;

namespace Commons.Utils
{
    /// <summary>
    /// Простейшие функции для коллекций
    /// </summary>
    public class CollectionsUtils
    {
        public static bool IsNotNullAndCountGreaterThan(ICollection collection, int count)
        {
            return (collection != null) && (collection.Count > count);
        }

        public static bool ListContainsEntityAndBothNotNull(IList collection, object entity)
        {
            return (collection != null) && (entity != null) && (collection.Contains(entity));
        }

        public static bool IsNotNullAndNotEmpty(ICollection collection)
        {
            return (collection != null) && (collection.Count > 0);
        }

        /// <summary>
        /// to array list
        /// </summary>
        /// <param name="enumerable"></param>
        /// <returns></returns>
        public static IList EnumerableToList(IEnumerable enumerable)
        {
            ArrayList arrayList = new ArrayList();
            foreach (object value in enumerable)
            {
                arrayList.Add(value);
            }
            return arrayList;
        }

    	public static bool IsNullOrEmpty(ICollection collection)
    	{
    		return collection == null || collection.Count == 0;
    	}
    }
}
