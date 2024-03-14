using BookingApp.Model;
using System;
using System.ComponentModel;
using System.Xml.Linq;

namespace BookingApp.DTO {
    public class TourFilterDTO {
        public LocationDTO Location { get; set; } = new LocationDTO();
        public LanguageState Language { get; set; } 
        public int TouristNumber { get; set; } = 0;
        public double Duration { get; set; } = 0;

        public TourFilterDTO() { }

        public TourFilterDTO(LocationDTO location, double duration, LanguageState language, int touristNumber) {
            Location = location;
            Duration = duration;
            Language = language;
            TouristNumber = touristNumber;
        }

        public bool isEmpty() {           
            return Location.Id == -1 && Language == null && Duration == 0 && TouristNumber == 0;
        }                       
    }                                                    
}