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
    public class TourRequestService {
        private readonly ITourRequestRepository _tourRequestRepository;

        public TourRequestService(ITourRequestRepository tourRequestRepository) {
            _tourRequestRepository = tourRequestRepository;
            UpdateRequestStatus();
        }

        private void UpdateRequestStatus() {
            foreach (TourRequest tourRequest in _tourRequestRepository.GetAll()) { 
                if ((tourRequest.StartDate.ToDateTime(TimeOnly.Parse("00:00 AM")) - DateTime.Now).TotalHours < 48) {
                    tourRequest.Status = TourRequestStatus.Expired;
                    _tourRequestRepository.Update(tourRequest);
                }
            }
        }

        public void CreateRequest(TourRequestDTO tourRequestDTO, int passengerNumber) {
            _tourRequestRepository.Save(new TourRequest(tourRequestDTO, passengerNumber));
        }
    }
}
