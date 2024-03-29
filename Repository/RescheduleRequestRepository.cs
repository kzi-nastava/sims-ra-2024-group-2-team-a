using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository {

    public class RescheduleRequestRepository: Repository<RescheduleRequest> {

        public List<RescheduleRequest> GetPendingRequestsByOwnerId(int ownerId) {
            return this.GetAll().FindAll(x => x.OwnerId == ownerId && x.Status == RescheduleRequestStatus.Pending);
        }

        public List<RescheduleRequest> GetRequestsByReservationId(int reservationId) {
            return this.GetAll().FindAll(x => x.ReservationId == reservationId);
        }
    }
}
