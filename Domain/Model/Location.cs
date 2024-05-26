using BookingApp.Serializer;

namespace BookingApp.Domain.Model {
    public class Location : ISerializable, IIdentifiable
    {

        public int Id { get; set; } = 0;
        public string City { get; set; } = "";
        public string Country { get; set; } = "";

        public Location() { }

        public Location(string city, string country)
        {
            City = city;
            Country = country;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            City = values[1];
            Country = values[2];
        }

        public string[] ToCSV()
        {
            string[] values = { Id.ToString(), City, Country };
            return values;
        }
    }
}