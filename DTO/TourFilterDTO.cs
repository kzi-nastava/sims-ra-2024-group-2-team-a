namespace BookingApp.DTO {
    public class TourFilterDTO {
        public LocationDTO Location { get; set; } = new LocationDTO();
        public LanguageDTO Language { get; set; } = new LanguageDTO();
        public int TouristNumber { get; set; } = 0;
        public double Duration { get; set; } = 0;

        public TourFilterDTO() { }

        public TourFilterDTO(LocationDTO location, double duration, LanguageDTO language, int touristNumber) {
            Location = location;
            Duration = duration;
            Language = language;
            TouristNumber = touristNumber;
        }

        public bool isEmpty() {
            return Location.Id == -1 && Language.Id == -1 && Duration == 0 && TouristNumber == 0;
        }
    }
}