using BookingApp.Model;
using System.Collections.Generic;

namespace BookingApp.RepositoryInterfaces {
    public interface IAccommodationReservationRepository : IRepository<AccommodationReservation> {
        List<AccommodationReservation> GetByAccommodationId(int id);
        List<AccommodationReservation> GetByGuestId(int id);
    }
}
