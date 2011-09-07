using System;
using System.Collections;
using System.Globalization;
using System.Windows.Data;
using Db4oExplorer.Domain;

namespace Db4oExplorer.Explorer
{
	public class StoredClassSortConverter:IValueConverter
	{
		private ByName comparer;

		public SortDirection Direction
		{
			set { comparer.Direction = value; }
		}

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			IList list = (IList) value;
			var arrayList = new ArrayList(list);
			comparer = new ByName();
			arrayList.Sort(comparer);
			return arrayList;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}

	public class ByName : IComparer
	{
		public SortDirection Direction = SortDirection.ASC;

		public int Compare(object x, object y)
		{
			IStoredClass xClass = (IStoredClass) x;
			IStoredClass yClass = (IStoredClass) y;

			return xClass.Name.CompareTo(yClass.Name)*(int) Direction;
		}
	}
}
