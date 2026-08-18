using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;

namespace Marketplace.Wpf.Converters
{
	public class EqualValueToParameterConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object param, CultureInfo culture)
			=> value.ToString() == param.ToString();

		public object ConvertBack(object value, Type targetType, object param, CultureInfo culture)
			=> throw new NotImplementedException();
	}
}
