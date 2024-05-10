using BookingApp.Domain.Model;
using System.Collections.Generic;

namespace BookingApp.Domain.RepositoryInterfaces {
    public interface IAccommodationRescheduleRequestRepository : IRepository<AccommodationRescheduleRequest>
    {
        public List<AccommodationRescheduleRequest> GetPendingRequestsByOwnerId(int ownerId);
        public List<AccommodationRescheduleRequest> GetByReservationId(int reservationId);
        public List<AccommodationRescheduleRequest> GetByGuestId(int guestId);
    }
}
