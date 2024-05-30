using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace BookingApp.WPF.Utils.Converters {
    public class InverseBooleanToVisibilityConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {

            if (value is bool boolValue && boolValue) {
                return Visibility.Hidden;
            }
            else {
                return Visibility.Visible;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
