using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace BookingApp.WPF.DTO
{
    public class TourRequestFilterDTO{
        public int TouristNumber { get; set; } = 0;
        public LocationDTO Location { get; set; } = new LocationDTO();
        public LanguageDTO Language { get; set; } = new LanguageDTO();
        public DateOnly Start { get; set; } = DateOnly.MinValue;
        public DateOnly End { get; set; } = DateOnly.MaxValue;

        public TourRequestFilterDTO() { }
        public TourRequestFilterDTO(int numberOfTourists, LocationDTO location, LanguageDTO language, DateOnly start, DateOnly end) {
            TouristNumber = numberOfTourists;
            Location = location;
            Language = language;
            Start = start;
            End = end;
        }
        public bool IsEmpty() {
            return Location.Id == 0 && Language.Id == -1 && TouristNumber == 0 && Start == DateOnly.MinValue && End == DateOnly.MaxValue;

        }
    }
}
