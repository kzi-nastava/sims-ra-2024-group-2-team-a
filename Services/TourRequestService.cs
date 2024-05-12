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

        public List<TourRequest> GetFiltered(int userId, string filter) {
            if(Enum.TryParse<TourRequestStatus>(filter, out TourRequestStatus status)) {
                switch (status) {
                    case TourRequestStatus.Accepted:
                        return _tourRequestRepository.GetAccepted(userId);
                    case TourRequestStatus.OnHold:
                        return _tourRequestRepository.GetOnHold(userId);
                    default:
                        return _tourRequestRepository.GetExpired(userId);
                }
            }
            else {
                return _tourRequestRepository.GetByTouristId(userId);
            }
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

        public TouristStatistics GetStatistics(int userId, string year) {
            if (year == "All-time")
                return StatisticsCalculator.CalculateTouristStatistics(_tourRequestRepository.GetAccepted(userId), _tourRequestRepository.GetByTouristId(userId));
            return StatisticsCalculator.CalculateTouristStatistics(_tourRequestRepository.GetAcceptedForYear(userId, int.Parse(year)), _tourRequestRepository.GetByTouristIdForYear(userId, int.Parse(year)));
        }

        public IEnumerable<int> GetRequestYears(int userId) {
            return _tourRequestRepository.GetByTouristId(userId).Select(r => r.StartDate.Year).Distinct();
        }

        public int GetRequestNumberByLocation(Location location, int userId) {
            return _tourRequestRepository.GetByTouristId(userId).Where(r => r.LocationId == location.Id).Count();
        }

        public int GetRequestNumberByLanguage(Language language, int userId) {
            return _tourRequestRepository.GetByTouristId(userId).Where(r => r.LanguageId == language.Id).Count();
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
