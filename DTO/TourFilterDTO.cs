namespace BookingApp.DTO {
    public class TourFilterDTO {
        public string Name { get; set; } = "";
        public LocationDTO Location { get; set; } = new LocationDTO();
        public LanguageDTO Language { get; set; } = new LanguageDTO();
        public int TouristNumber { get; set; } = 0;
        public double Duration { get; set; } = 0;

        public TourFilterDTO() { }
        public TourFilterDTO(double duration, int touristNumber, LocationDTO location, LanguageDTO language) {
            Location = location;
            Duration = duration;
            Language = language;
            TouristNumber = touristNumber;
        }
        public TourFilterDTO(string name, double duration, int touristNumber , LocationDTO location, LanguageDTO language) {
            Name = name;
            Location = location;
            Duration = duration;
            Language = language;
            TouristNumber = touristNumber;
        }

        public bool isEmpty() {
            return Location.Id == -1 && Language.Id == -1 && Duration == 0 && TouristNumber == 0 && Name == "";
        }
    }
}