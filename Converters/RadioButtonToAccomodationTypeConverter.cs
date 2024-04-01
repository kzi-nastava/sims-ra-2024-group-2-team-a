using BookingApp.Model;
using System;
using System.Globalization;
using System.Windows.Data;

namespace BookingApp.Converters {
    public class RadioButtonToAccommodationTypeConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (!(value is AccommodationType))
                return false;

            AccommodationType accommodationType = (AccommodationType)value;

            switch (accommodationType) {
                case AccommodationType.apartment:
                    return parameter?.ToString() == "apartment";
                case AccommodationType.house:
                    return parameter?.ToString() == "house";
                case AccommodationType.hut:
                    return parameter?.ToString() == "hut";
                default:
                    return false;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            if (!(value is bool isChecked) || !(parameter is string accommodationTypeName))
                return null;

            if (isChecked) {
                Enum.TryParse(accommodationTypeName, out AccommodationType accommodationType);
                return accommodationType;
            }

            return null;
        }

    }
}