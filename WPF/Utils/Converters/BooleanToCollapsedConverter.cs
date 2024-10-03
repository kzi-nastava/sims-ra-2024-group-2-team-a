using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace BookingApp.WPF.Utils.Converters {
    public class BooleanToCollapsedConverter : IValueConverter {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value is bool boolValue) {
                bool inverse = parameter != null && parameter.ToString() == "Inverse";
                return boolValue ^ inverse ? Visibility.Visible : Visibility.Collapsed;
            }
            return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value is Visibility visibilityValue) {
                bool inverse = parameter != null && parameter.ToString() == "Inverse";
                return visibilityValue == Visibility.Visible ^ inverse;
            }
            return DependencyProperty.UnsetValue;
        }

    }
}
