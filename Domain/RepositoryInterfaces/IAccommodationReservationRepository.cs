using BookingApp.Domain.Model;
using System.Collections.Generic;

namespace BookingApp.Domain.RepositoryInterfaces {
    public interface IAccommodationReservationRepository : IRepository<AccommodationReservation>
    {
        List<AccommodationReservation> GetByAccommodationId(int id);
        List<AccommodationReservation> GetByGuestId(int id);
    }
}
