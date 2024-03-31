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

        public List<RescheduleRequest> GetAll() {
            return _requestRepository.GetAll();
        }

        public bool Delete(int id) {
            var request = _requestRepository.GetById(id);
            return _requestRepository.Delete(request);
        }

        public RescheduleRequest Save(RescheduleRequest request) {
            return _requestRepository.Save(request);
        }

        public bool DeleteByReservationId(int reservationId) {
            var requests = _requestRepository.GetByReservationId(reservationId);
            requests.ForEach(r => _requestRepository.Delete(r));
            return true;
        }

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
