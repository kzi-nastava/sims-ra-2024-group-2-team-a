using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.RepositoryInterfaces {
    public interface IAccommodationReservationRepository : IRepository<AccommodationReservation> {
        List<AccommodationReservation> GetByAccommodationId(int id);
        List<AccommodationReservation> GetByGuestId(int id);
    }
}
