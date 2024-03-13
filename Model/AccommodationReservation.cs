using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Model {

    public class AccommodationReservation : ISerializable {
        
        public int Id { get; set; }
        public int IdGuest { get; set; }
        public int IdAccommodation { get; set; }
        public int GuestsNumber { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }

        public AccommodationReservation() { }

        public AccommodationReservation(int idGuest, int idAccommodation, int guestsNumber, DateOnly startDate, DateOnly endDate) {
            IdGuest = idGuest;
            IdAccommodation = idAccommodation;
            GuestsNumber = guestsNumber;
            StartDate = startDate;
            EndDate = endDate;
        }

        public string[] ToCSV() {
            string[] csvValues = { Id.ToString(), IdGuest.ToString(), IdAccommodation.ToString(), GuestsNumber.ToString(), StartDate.ToString("dd-MM-yyyy"), EndDate.ToString("dd-MM-yyyy") };
            return csvValues;
        }

        public void FromCSV(string[] values) {
            Id = Convert.ToInt32(values[0]);
            IdGuest = Convert.ToInt32(values[1]);
            IdAccommodation = Convert.ToInt32(values[2]);
            GuestsNumber = Convert.ToInt32(values[3]);
            StartDate = DateOnly.Parse(values[4]);
            EndDate = DateOnly.Parse(values[5]);
        }
    }
}
