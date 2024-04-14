using BookingApp.Domain.Model;
using System.Collections.Generic;

namespace BookingApp.Domain.RepositoryInterfaces {
    public interface IRescheduleRequestRepository : IRepository<RescheduleRequest>
    {
        public List<RescheduleRequest> GetPendingRequestsByOwnerId(int ownerId);
        public List<RescheduleRequest> GetByReservationId(int reservationId);
        public List<RescheduleRequest> GetByGuestId(int guestId);
    }
}
