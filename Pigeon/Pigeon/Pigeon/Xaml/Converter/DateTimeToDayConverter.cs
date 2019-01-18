using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Pigeon.Xaml.Converter
{
    public class DateTimeToDayConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var dt = (DateTime)value;
            var now = DateTime.Now;
            if (dt.Date == now.Date)
            {
                return "Today";
            }
            if (dt.Date == now.AddDays(-1).Date)
            {
                return "Yesterday";
            }
            return dt.ToString("dd-MMM-yyyy");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
