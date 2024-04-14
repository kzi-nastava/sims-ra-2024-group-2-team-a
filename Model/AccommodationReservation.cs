using BookingApp.Serializer;
using System;

namespace BookingApp.Model {

    public class AccommodationReservation : ISerializable, IIdentifiable {

        public int Id { get; set; } = -1;
        public int GuestId { get; set; }
        public int AccommodationId { get; set; }
        public int GuestsNumber { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public int ReservationDays => EndDate.Day - StartDate.Day;
        public bool Cancelled { get; set; } = false;

        public AccommodationReservation() { }

        public AccommodationReservation(int GuestId, int accommodationId, int guestsNumber, DateOnly startDate, DateOnly endDate) {
            this.GuestId = GuestId;
            AccommodationId = accommodationId;
            GuestsNumber = guestsNumber;
            StartDate = startDate;
            EndDate = endDate;
        }

        public string[] ToCSV() {
            string[] csvValues = { 
                Id.ToString(), 
                GuestId.ToString(), 
                AccommodationId.ToString(), 
                GuestsNumber.ToString(), 
                StartDate.ToString("dd-MM-yyyy"), 
                EndDate.ToString("dd-MM-yyyy"),
                Cancelled.ToString()
            };
            
            return csvValues;
        }

        public void FromCSV(string[] values) {
            Id = Convert.ToInt32(values[0]);
            GuestId = Convert.ToInt32(values[1]);
            AccommodationId = Convert.ToInt32(values[2]);
            GuestsNumber = Convert.ToInt32(values[3]);
            StartDate = DateOnly.ParseExact(values[4], "dd-MM-yyyy");
            EndDate = DateOnly.ParseExact(values[5], "dd-MM-yyyy");
            Cancelled = Convert.ToBoolean(values[6]);
        }
    }
}
