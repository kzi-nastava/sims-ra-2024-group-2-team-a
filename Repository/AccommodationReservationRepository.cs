using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.RepositoryInterfaces;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository {
    class AccommodationReservationRepository : Repository<AccommodationReservation>, IAccommodationReservationRepository {

        public List<AccommodationReservation> GetByAccommodationId(int id) {
            var reservations = _serializer.FromCSV();

            return reservations.Where(r => r.AccommodationId == id).ToList();
        }

        public List<AccommodationReservation> GetByGuestId(int id) {
            var reservations = _serializer.FromCSV();

            return reservations.Where(r => r.GuestId == id).ToList();
        }
    }
}
