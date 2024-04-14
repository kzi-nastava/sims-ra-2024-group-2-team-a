using BookingApp.Model;
using BookingApp.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
