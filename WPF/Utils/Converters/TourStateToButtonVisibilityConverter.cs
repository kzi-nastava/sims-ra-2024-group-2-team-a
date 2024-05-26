using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace BookingApp.WPF.Utils.Converters {
    public class TourStateToButtonVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string)
            {
                string status = (string)value;
                switch (status)
                {
                    case "Scheduled":
                        return Visibility.Collapsed;
                    case "Active":
                        return parameter as string == "FollowLiveButton" ? Visibility.Visible : Visibility.Collapsed;
                    case "Finished":
                        return parameter as string == "RateTourButton" ? Visibility.Visible : Visibility.Collapsed;
                    default:
                        return Visibility.Collapsed;
                }
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

