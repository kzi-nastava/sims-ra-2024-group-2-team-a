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
        }

        public void CreateRequest(int touristId, List<TourRequestDTO> simpleRequests) {
            ComplexTourRequest complexRequest = _complexTourRequestRepository.Save(new ComplexTourRequest(touristId));
            foreach (TourRequestDTO simpleRequest in simpleRequests) {
                _tourRequestService.CreateRequest(simpleRequest, complexRequest.Id);
            }
        }
    }
}
