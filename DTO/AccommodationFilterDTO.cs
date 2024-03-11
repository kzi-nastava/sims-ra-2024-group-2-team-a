using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DTO {
    public class AccommodationFilterDTO {

        public string? Name { get; set; }
        public LocationDTO? Location { get; set; }
        public AccomodationType? Type { get; set; }
        public int? guestNumber { get; set; }
        public int? reservationDays { get; set; }

        public AccommodationFilterDTO() { }

        public AccommodationFilterDTO(string? name, LocationDTO? location, AccomodationType? type, int? guestNumber, int? reservationDays) {
            Name = name;
            Location = location;
            Type = type;
            this.guestNumber = guestNumber;
            this.reservationDays = reservationDays;
        }

        public bool isEmpty() {
            return Name == null && Location == null && Type == null && guestNumber == null && reservationDays == null;
        }
    }
}
