using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Model {

    public class TourReservation : ISerializable, IIdentifiable {

        public int Id { get; set; }
        public int TouristId { get; set; }
        public int TourId { get; set; }

        public TourReservation() { }

        public TourReservation(int id, int touristId, int tourId) { 
            Id = id;
            TouristId = touristId;
            TourId = tourId;
        }

        public TourReservation(int touristId, int tourId) {
            TouristId = touristId;
            TourId = tourId;
        }

        public string[] ToCSV() {
            string[] csvValues = {
                Id.ToString(),
                TouristId.ToString(),
                TourId.ToString(),
                };

            return csvValues;
        }

        public void FromCSV(string[] values) {
            Id = int.Parse(values[0]);
            TouristId = int.Parse(values[1]);
            TourId = int.Parse(values[2]);
        }
    }
}
