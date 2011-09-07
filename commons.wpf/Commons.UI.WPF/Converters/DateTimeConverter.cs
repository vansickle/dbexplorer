using System;
using System.Globalization;
using System.Windows.Data;

namespace Commons.UI.WPF.Converters
{
	public class DateTimeConverter:IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			DateTime date = (DateTime)value;
			return date.ToString();
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			string strValue = value.ToString();
			DateTime resultDateTime;
			if (DateTime.TryParse(strValue, out resultDateTime))
			{
				return resultDateTime;
			}
			return value;
		}
	}
}
