using System;
using System.Globalization;
using Xamarin.Forms;

namespace MnMFiber.Common.Xaml.Converters
{
    public class NullableBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var nullable = value as bool?;
            var result = false;

            if (nullable.HasValue)
            {
                result = nullable.Value;
            }

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool boolValue;
            bool? result = null;

            if (bool.TryParse(value.ToString(), out boolValue))
            {
                result = new Nullable<bool>(boolValue);
            }

            return result;
        }
    }
}
