using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using System.Collections.Generic;
using System.Linq;

namespace BookingApp.Services {
    public class AccommodationRescheduleRequestService
    {
        private readonly IAccommodationRescheduleRequestRepository _requestRepository;

        public AccommodationRescheduleRequestService(IAccommodationRescheduleRequestRepository requestRepository) {
            _requestRepository = requestRepository;
        }

        public List<AccommodationRescheduleRequest> GetAll() {
            return _requestRepository.GetAll();
        }

        public bool Delete(int id) {
            var request = _requestRepository.GetById(id);
            return _requestRepository.Delete(request);
        }

        public AccommodationRescheduleRequest Save(AccommodationRescheduleRequest request) {
            return _requestRepository.Save(request);
        }
        
        public bool Update(AccommodationRescheduleRequest rescheduleRequest) {
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

        public List<AccommodationRescheduleRequest> GetPendingRequestsByOwnerId(int ownerId) {
            return _requestRepository.GetPendingRequestsByOwnerId(ownerId);
        }

        public List<AccommodationRescheduleRequest> GetByReservationId(int reservationId) {
            return _requestRepository.GetByReservationId(reservationId);
        }

        public List<AccommodationRescheduleRequest> GetByGuestId(int guestId) {
            return _requestRepository.GetByGuestId(guestId);
        }

        public List<AccommodationRescheduleRequest> GetSortedRequestsByOwnerId(int ownerId) {
            List<AccommodationRescheduleRequest> requests = this.GetAll().FindAll(x => x.OwnerId == ownerId);
            return requests.OrderBy(x => x.Status).ToList();
        }
    }
}
