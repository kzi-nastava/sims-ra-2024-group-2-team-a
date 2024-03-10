using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Serializer;

namespace BookingApp.Model {
    public class Passenger : ISerializable {
        public int Id { get; set; }
        public int TourId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Age { get; set; }

        public Passenger() { }

        public string[] ToCSV() {
            return new string[0];
        }

        public void FromCSV(string[] values) {
            
        }
    }
}

