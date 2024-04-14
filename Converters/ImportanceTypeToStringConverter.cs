using BookingApp.Domain.Model;
using System;
using System.Globalization;
using System.Windows.Data;

namespace BookingApp.Converters {
    class ImportanceTypeToStringConverter : IValueConverter {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {

            value = (ImportanceType) value;

            switch(value) {
                case ImportanceType.Level_1:
                    return "Level 1 - It would be nice to renovate some small things, but everything works fine without it";
                case ImportanceType.Level_2:
                    return "Level 2 - Minor complaints about the accommodation that, if addressed, would make it perfect";
                case ImportanceType.Level_3:
                    return "Level 3 - Several things that really bothered should be renovated";
                case ImportanceType.Level_4:
                    return "Level 4 - There are quite a few bad things, and renovation is really necessary";
                case ImportanceType.Level_5:
                    return "Level 5 - The accommodation is in very poor condition and it's not worth renting at all unless it's renovated";
                default:
                    return "No level";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
