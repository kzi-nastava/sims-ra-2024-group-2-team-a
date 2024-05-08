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
        private LocationService _locationService;
        private LanguageService _languageService;

        public TourRequestService(ITourRequestRepository tourRequestRepository) {
            _tourRequestRepository = tourRequestRepository;
            UpdateRequestStatus();
        }

        public void InjectServices(LocationService locationService, LanguageService languageService) {
            _locationService = locationService;
            _languageService = languageService;
        }

        public List<TourRequest> GetByTouristId(int id) { 
            return _tourRequestRepository.GetByTouristId(id); 
        }

        public List<TourRequest> GetByTouristIdThisYear(int id) {
            return GetByTouristId(id).Where(a => a.EndDate.Year == DateOnly.FromDateTime(DateTime.Now).Year).ToList();
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

        public List<TourRequest> GetAcceptedThisYear(int userId) {
            return GetByTouristIdThisYear(userId).Where(r => r.Status == TourRequestStatus.Accepted).ToList();
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
            return ((double)GetAcceptedThisYear(userId).Count() / (double)GetByTouristIdThisYear(userId).Count());
        }

        public double GetThisYearNotAcceptedPercentage(int userId) {
            List<TourRequest> all = GetByTouristIdThisYear(userId);
            return (((double)all.Count() - (double)GetAcceptedThisYear(userId).Count()) / (double)all.Count());
        }

        public double GetThisYearAveragePassengerNumber(int userId) {
            List<TourRequest> accepted = GetAcceptedThisYear(userId);
            return accepted.Sum(a => a.PassengerNumber) / (double)accepted.Count();
        }

        public int GetRequestNumberByLocation(Location location, int userId) {
            return GetByTouristId(userId).Where(r => r.LocationId == location.Id).Count();
        }

        public int GetRequestNumberByLanguage(Language language, int userId) {
            return GetByTouristId(userId).Where(r => r.LanguageId == language.Id).Count();
        }

        public Dictionary<Location, int> GetRequestsByLocations(int userId) {
            Dictionary<Location, int> pairs = new Dictionary<Location, int>();

            foreach(Location location in _locationService.GetAll()) 
                pairs.Add(location, GetRequestNumberByLocation(location, userId));
            
            return pairs;
        }

        public Dictionary<Language, int> GetRequestsByLanguages(int userId) {
            Dictionary<Language, int> pairs = new Dictionary<Language, int>();

            foreach (Language language in _languageService.GetAll())
                pairs.Add(language, GetRequestNumberByLanguage(language, userId));

            return pairs;
        }
    }
}
