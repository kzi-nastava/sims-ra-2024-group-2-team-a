using System;
using System.Globalization;
using System.Windows.Data;

namespace BookingApp.WPF.Utils.Converters {

    public class CountDurationToBooleanConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {

            if (value is int count) {
                return count > 0 && count <= 365;
            }

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}