using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace VeterinarniKlinika.Converter
{
    public class NullableDateTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime dateTimeValue)
            {
                return dateTimeValue;
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string stringValue && DateTime.TryParse(stringValue, out DateTime dateTime))
            {
                return dateTime;
            }

            return null;
        }
    }
}
