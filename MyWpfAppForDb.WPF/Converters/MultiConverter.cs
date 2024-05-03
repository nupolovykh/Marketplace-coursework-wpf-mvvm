using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace MyWpfAppForDb.WPF.Converters
{
	public class MultiConverter : IMultiValueConverter
	{
		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture) // just example
		{
			var sb = new StringBuilder();
			sb.AppendLine(values[0].ToString());
			foreach (var kvp in (Dictionary<string, string>)values[1])
				sb.AppendLine(kvp.Key + "-" + kvp.Value);
			return sb.ToString();
		}

		public object[] ConvertBack(object value, Type[] targetType, object param, CultureInfo culture)
			=> throw new NotImplementedException();
	}
}
