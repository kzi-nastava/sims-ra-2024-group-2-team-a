using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Model {

    public class AccommodationReservation {
        
        public int Id { get; set; }
        public int IdGuest { get; set; }
        public int IdAccommodation { get; set; }
        public int numberOfPersons { get; set; }
        public DateOnly startDate { get; set; }
        public DateOnly endDate { get; set; }

        public AccommodationReservation() { }

        public AccommodationReservation(int idGuest, int idAccommodation, int numberOfPersons, DateOnly startDate, DateOnly endDate) {
            IdGuest = idGuest;
            IdAccommodation = idAccommodation;
            this.numberOfPersons = numberOfPersons;
            this.startDate = startDate;
            this.endDate = endDate;
        }

        public string[] ToCSV() {
            string[] csvValues = { Id.ToString(), IdGuest.ToString(), IdAccommodation.ToString(), numberOfPersons.ToString(), startDate.ToString("dd-MM-yyyy"), endDate.ToString("dd-MM-yyyy") };
            return csvValues;
        }

        public void FromCSV(string[] values) {
            Id = Convert.ToInt32(values[0]);
            IdGuest = Convert.ToInt32(values[1]);
            IdAccommodation = Convert.ToInt32(values[2]);
            numberOfPersons = Convert.ToInt32(values[3]);
            startDate = DateOnly.Parse(values[4]);
            endDate = DateOnly.Parse(values[5]);
        }
    }
}
