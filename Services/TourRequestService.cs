using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Repository;
using BookingApp.WPF.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services {
    public class TourRequestService {
        private readonly ITourRequestRepository _tourRequestRepository;

        public TourRequestService(ITourRequestRepository tourRequestRepository) {
            _tourRequestRepository = tourRequestRepository;
            UpdateRequestStatus();
        }

        public List<TourRequest> GetByTouristId(int id) { 
            return _tourRequestRepository.GetByTouristId(id); 
        }

        private void UpdateRequestStatus() {
            foreach (TourRequest tourRequest in _tourRequestRepository.GetAll()) { 
                if ((tourRequest.StartDate.ToDateTime(TimeOnly.Parse("00:00 AM")) - DateTime.Now).TotalHours < 48 && tourRequest.Status == TourRequestStatus.OnHold) {
                    tourRequest.Status = TourRequestStatus.Expired;
                    _tourRequestRepository.Update(tourRequest);
                }
            }
        }

        public void CreateRequest(TourRequestDTO tourRequestDTO, int passengerNumber) {
            _tourRequestRepository.Save(new TourRequest(tourRequestDTO, passengerNumber));
        }

        private List<TourRequest> GetAccepted(int userId) {
            return GetByTouristId(userId).Where(r => r.Status == TourRequestStatus.Accepted).ToList();
        }

        public double GetAllTimeAcceptedPercentage(int userId) {
            return ((double)GetAccepted(userId).Count() / (double)GetByTouristId(userId).Count());
        }

        public double GetAllTimeNotAcceptedPercentage(int userId) {
            List<TourRequest> all = GetByTouristId(userId);
            return (((double)all.Count() - (double)GetAccepted(userId).Count()) / (double)all.Count());
        }

        public double GetAllTimeAveragePassengerNumber(int userId) {
            List<TourRequest> accepted = GetAccepted(userId);
            return accepted.Sum(a => a.PassengerNumber) / (double)accepted.Count();
        }

        public double GetThisYearAcceptedPercentage(int userId) {
            //return ((double)GetAccepted(userId).Where(r => r.).Count() / (double)GetByTouristId(userId).Count());
            return 0;
        }

        public double GetThisYearNotAcceptedPercentage(int userId) {
            List<TourRequest> all = GetByTouristId(userId);
            return (((double)all.Count() - (double)GetAccepted(userId).Count()) / (double)all.Count());
        }

        public double GetThisYearAveragePassengerNumber(int userId) {
            List<TourRequest> accepted = GetAccepted(userId);
            return accepted.Sum(a => a.PassengerNumber) / (double)accepted.Count();
        }
    }
}
