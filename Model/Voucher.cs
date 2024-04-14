using BookingApp.Serializer;
using System;
using System.Globalization;

namespace BookingApp.Model {
    public class Voucher : ISerializable, IIdentifiable {
        public int Id { get; set; }
        public DateTime ExpireDate { get; set; }
        public int TouristId { get; set; }
        public string Image { get; set; }

        public bool Used { get; set; } = false;

        public Voucher() { }
        public Voucher(int id, DateTime expireDate, int touristId) {
            Id = id;
            ExpireDate = expireDate;
            TouristId = touristId;
            Image = "../../../Resources/Images/default-coupon.png";
        }
        public Voucher(DateTime expireDate, int touristId) {
            ExpireDate = expireDate;
            TouristId = touristId;
            Image = "../../../Resources/Images/default-coupon.png";
        }

        public string[] ToCSV() {
            string[] cssValues = {
                Id.ToString(),
                ExpireDate.ToString("dd-MM-yyyy HH:mm"),
                TouristId.ToString(),
                Image,
                Used.ToString()
            };
            return cssValues;
        }
        public void FromCSV(string[] values) {
            Id = int.Parse(values[0]);
            CultureInfo provider = CultureInfo.InvariantCulture;
            ExpireDate = DateTime.ParseExact(values[1], "dd-MM-yyyy HH:mm", provider);
            TouristId= int.Parse(values[2]);
            Image = values[3];
            Used = bool.Parse(values[4]);
        }
    }
}
