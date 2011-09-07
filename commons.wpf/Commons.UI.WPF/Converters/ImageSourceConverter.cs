using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Commons.UI.WPF.Converters
{
	public class ImageSourceConverter:IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			try
			{
				return new BitmapImage(new Uri((string)value,UriKind.Relative));
			}
			catch(Exception e)
			{
				Console.WriteLine(e);
				return new BitmapImage();
			}
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
