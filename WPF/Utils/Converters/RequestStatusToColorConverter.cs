using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace BookingApp.WPF.Utils.Converters
{
    public class RequestStatusToColorConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value is string status) {
                switch (status) {
                    case "Accepted":
                        return Brushes.Green;
                    case "OnHold":
                        return Brushes.Orange;
                    case "Expired":
                        return Brushes.Red;
                    default:
                        return Brushes.Transparent;
                }
            }
            return Brushes.Transparent;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
