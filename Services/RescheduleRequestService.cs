using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using System.Collections.Generic;
using System.Linq;

namespace BookingApp.Services {
    class RescheduleRequestService
    {
        private readonly IRescheduleRequestRepository _requestRepository = RepositoryInjector.GetInstance<IRescheduleRequestRepository>();

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
        
        public bool Update(RescheduleRequest rescheduleRequest) {
            return _requestRepository.Update(rescheduleRequest);
        }

        public bool CancelByReservationId(int reservationId) {
            var requests = _requestRepository.GetByReservationId(reservationId);
            if (requests.Count == 0) 
                return false;

            requests.ForEach(r => r.Status = RescheduleRequestStatus.Cancelled);
            requests.ForEach(r => _requestRepository.Update(r));
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

        public List<RescheduleRequest> GetSortedRequestsByOwnerId(int ownerId) {
            List<RescheduleRequest> requests = this.GetAll().FindAll(x => x.OwnerId == ownerId);
            return requests.OrderBy(x => x.Status).ToList();
        }
    }
}
