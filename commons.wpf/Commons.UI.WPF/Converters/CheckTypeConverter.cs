using System;
using System.Globalization;
using System.Windows.Data;

namespace Commons.UI.WPF.Converters
{
	public class CheckTypeConverter:IValueConverter
	{
		private Type type;

		public Type Type
		{
			get { return type; }
			set { type = value; }
		}

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null) return false;
			return type == value.GetType();
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new System.NotImplementedException();
		}
	}
}
