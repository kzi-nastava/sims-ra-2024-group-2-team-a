using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Model {

    public class TourReservation {

        public int Id { get; set; }
        public int TouristId { get; set; }
        public int TourId { get; set; }
        public List<Passenger> PassengerList { get; set; }

        public TourReservation() { }

        public string[] ToCSV() {
            return new string[0];
        }

        public void FromCSV(string[] values) {
            
        }
    }
}
