﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace BookingApp.Converters {
    public class BooleanToVisibilityConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {

            if (value is bool boolValue && boolValue) {
                return Visibility.Visible;
            }
            else {
                return Visibility.Hidden;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}