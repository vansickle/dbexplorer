using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Data;

namespace Commons.UI.WPF.Converters
{
	[System.Windows.Markup.ContentProperty("Converters")]
	public class ValueConverterPipeline:IValueConverter
	{
		public ObservableCollection<IValueConverter> converters = new ObservableCollection<IValueConverter>();

		public ObservableCollection<IValueConverter> Converters
		{
			get { return converters; }
			set { converters = value; }
		}

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			object result = value;
			foreach (IValueConverter converter in converters)
			{
				result = converter.Convert(result,targetType,parameter,culture);
			}
			return result;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new System.NotImplementedException();
		}
	}
}
