using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DTO {
    public class AccommodationFilterDTO {

        public string Name { get; set; } = "";
        public LocationDTO Location { get; set; } = new LocationDTO();
        public AccommodationType Type { get; set; } = AccommodationType.none;
        public int guestNumber { get; set; } = 0;
        public int reservationDays { get; set; } = 0;

        public AccommodationFilterDTO() { }

        public AccommodationFilterDTO(string name, LocationDTO location, AccommodationType type, int guestNumber, int reservationDays) {
            Name = name;
            Location = location;
            Type = type;
            this.guestNumber = guestNumber;
            this.reservationDays = reservationDays;
        }

        public bool isEmpty() {
            return Name == "" && Location.Id == -1 && Type == AccommodationType.none && guestNumber == 0 && reservationDays == 0;
        }
    }
}
