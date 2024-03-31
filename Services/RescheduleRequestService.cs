using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services
{
    class RescheduleRequestService
    {
        private readonly RescheduleRequestRepository _requestRepository = new RescheduleRequestRepository();

        public List<RescheduleRequest> GetPendingRequestsByOwnerId(int ownerId) {
            return _requestRepository.GetPendingRequestsByOwnerId(ownerId);
        }

        public List<RescheduleRequest> GetByReservationId(int reservationId) {
            return _requestRepository.GetByReservationId(reservationId);
        }

        public List<RescheduleRequest> GetByGuestId(int guestId) {
            return _requestRepository.GetByGuestId(guestId);
        }
    }
}
