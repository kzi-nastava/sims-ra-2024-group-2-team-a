using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using System.Collections.Generic;

namespace BookingApp.Repository {

    public class RescheduleRequestRepository : Repository<RescheduleRequest>, IRescheduleRequestRepository {

        public List<RescheduleRequest> GetPendingRequestsByOwnerId(int ownerId) {
            return this.GetAll().FindAll(x => x.OwnerId == ownerId && x.Status == RescheduleRequestStatus.Pending);
        }

        public List<RescheduleRequest> GetByReservationId(int reservationId) {
            return this.GetAll().FindAll(x => x.ReservationId == reservationId);
        }

        public List<RescheduleRequest> GetByGuestId(int guestId) {
            return this.GetAll().FindAll(x => x.GuestId == guestId);
        }
    }
}
