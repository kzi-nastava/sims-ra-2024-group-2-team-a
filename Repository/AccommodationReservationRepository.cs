using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using System.Collections.Generic;
using System.Linq;

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
