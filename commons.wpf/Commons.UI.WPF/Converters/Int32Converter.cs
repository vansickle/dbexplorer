using System;
using System.Globalization;
using System.Windows.Data;

namespace Commons.UI.WPF.Converters
{
	public class Int32Converter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return System.Convert.ToString(value);
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return System.Convert.ToInt32(value);
		}
	}
}
