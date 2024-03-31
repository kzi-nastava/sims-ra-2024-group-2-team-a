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
        public AccommodationType Type { get; set; } = AccommodationType.any;
        public int GuestNumber { get; set; } = 0;
        public int ReservationDays { get; set; } = 0;

        public AccommodationFilterDTO() { }

        public AccommodationFilterDTO(string name, LocationDTO location, AccommodationType type, int guestNumber, int reservationDays) {
            Name = name;
            Location = location;
            Type = type;
            GuestNumber = guestNumber;
            ReservationDays = reservationDays;
        }

        public bool MatchesName(string name) {
            return name.ToLower().Contains(Name.ToLower()) || Name == "";
        }

        public bool MatchesLocation(int locationId) {
            return locationId == Location.Id || Location.Id == 0;
        }

        public bool MatchesType(AccommodationType type) {
            return type == Type || Type == AccommodationType.any;
        }

        public bool MatchesGuestNumber(int maxGuestNumber) {
            return maxGuestNumber >= GuestNumber || GuestNumber <= 0;
        }

        public bool MatchesReservationDays(int minReservationDays) {
            return minReservationDays <= ReservationDays || ReservationDays <= 0;
        }
    }
}
