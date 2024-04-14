using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.RepositoryInterfaces {
    public interface IRescheduleRequestRepository : IRepository<RescheduleRequest> {
        public List<RescheduleRequest> GetPendingRequestsByOwnerId(int ownerId);
        public List<RescheduleRequest> GetByReservationId(int reservationId);
        public List<RescheduleRequest> GetByGuestId(int guestId);
    }
}
