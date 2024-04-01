using BookingApp.Serializer;
using System;
using System.Globalization;

namespace BookingApp.Model
{
    public class Voucher : ISerializable, IIdentifiable {
        public int Id { get; set; }
        public DateOnly Expiries { get; set; }
        public int TouristId { get; set; }

        public Voucher() { }
        public Voucher(int id, DateOnly expiries, int touristId) {
            Id = id;
            Expiries = expiries;
            TouristId = touristId;
        }
        public Voucher(DateOnly expiries, int touristId) {
            Expiries = expiries;
            TouristId = touristId;
        }

        public string[] ToCSV() {
            string[] cssValues = {
                Id.ToString(),
                Expiries.ToString("dd-MM-yyyy"),
                TouristId.ToString()
            };
            return cssValues;
        }
        public void FromCSV(string[] values) {
            Id = int.Parse(values[0]);
            CultureInfo provider = CultureInfo.InvariantCulture;
            Expiries = DateOnly.ParseExact(values[1], "dd-MM-yyyy", provider);
            TouristId= int.Parse(values[2]);
        }
    }
}
