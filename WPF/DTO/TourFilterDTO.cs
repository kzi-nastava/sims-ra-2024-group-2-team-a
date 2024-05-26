using System;

namespace BookingApp.WPF.DTO {
    public class TourFilterDTO
    {
        public string Name { get; set; } = "";
        public LocationDTO Location { get; set; } = new LocationDTO();
        public LanguageDTO Language { get; set; } = new LanguageDTO();
        public int TouristNumber { get; set; } = 0;
        public double Duration { get; set; } = 0;
        public DateTime Beggining { get; set; } = DateTime.MinValue;

        public TourFilterDTO() { }
        public TourFilterDTO(string name, double duration, int touristNumber, LocationDTO location, LanguageDTO language, DateTime beggining)
        {
            Name = name;
            Location = location;
            Duration = duration;
            Language = language;
            TouristNumber = touristNumber;
            Beggining = beggining;
        }

        public bool isEmpty()
        {
            return Location.Id == 0 && Language.Id == -1 && Duration == 0 && TouristNumber == 0 && Name == "" && Beggining == DateTime.MinValue;
        }
    }
}