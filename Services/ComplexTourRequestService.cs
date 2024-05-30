using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Repository;
using BookingApp.WPF.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services {
    public class ComplexTourRequestService {
        private readonly IComplexTourRequestRepository _complexTourRequestRepository;
        private TourRequestService _tourRequestService;

        public ComplexTourRequestService(IComplexTourRequestRepository complexTourRequestRepository) {
            _complexTourRequestRepository = complexTourRequestRepository;         
        }

        public void InjectServices(TourRequestService tourRequestService) {
            _tourRequestService = tourRequestService;
            UpdateRequestStatus();
        }

        public ComplexTourRequest GetById(int id) {
            return _complexTourRequestRepository.GetById(id);
        }

        public IEnumerable<ComplexTourRequest> GetByTouristId(int touristId) {
            return _complexTourRequestRepository.GetByTouristId(touristId);
        }
        public List<ComplexTourRequest> GetAllOnHold(int guideId) {
            return _complexTourRequestRepository.GetAll().FindAll(x => x.Status == TourRequestStatus.OnHold && !_tourRequestService.IsAccepted(guideId, x.Id)); 
            
        }
        public bool Update(ComplexTourRequest complexTourRequest) {
            return _complexTourRequestRepository.Update(complexTourRequest);
        }
        public void CreateRequest(int touristId, List<TourRequestDTO> simpleRequests) {
            ComplexTourRequest complexRequest = _complexTourRequestRepository.Save(new ComplexTourRequest(touristId));
            foreach (TourRequestDTO simpleRequest in simpleRequests) {
                _tourRequestService.CreateRequest(simpleRequest, complexRequest.Id);
            }
        }
        
        private void UpdateRequestStatus() {
            foreach (var complexRequestPart in _tourRequestService.GetAll().Where(r => r.ComplexTourId != 0).GroupBy(r => r.ComplexTourId)) {
                TourRequest firstPart = complexRequestPart.First();
                ComplexTourRequest complexTourRequest = _complexTourRequestRepository.GetById(firstPart.ComplexTourId);
                if (firstPart.Status == TourRequestStatus.Expired) {
                    complexTourRequest.Status = TourRequestStatus.Expired;
                    _complexTourRequestRepository.Update(complexTourRequest);
                }
            }
        }
    }
}
