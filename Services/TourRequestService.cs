using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Repository;
using BookingApp.WPF.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services {
    public class TourRequestService {
        private readonly ITourRequestRepository _tourRequestRepository;
        private LocationService _locationService;
        private LanguageService _languageService;
        private UserService _userService;

        public TourRequestService(ITourRequestRepository tourRequestRepository) {
            _tourRequestRepository = tourRequestRepository;
            UpdateRequestStatus();
        }

        public void InjectServices(LocationService locationService, LanguageService languageService, UserService userService) {
            _locationService = locationService;
            _languageService = languageService;
            _userService = userService;
        }

        public IEnumerable<TourRequest> GetForComplexRequest(int id) {
            return GetAll().Where(r => r.ComplexTourId == id);
        }
        public List<TourRequest> GetComplexForGuide(int complexId) {
            return GetAll().FindAll(x => x.ComplexTourId == complexId);
        }

        public List<TourRequest> GetAll() {
            return _tourRequestRepository.GetAll();
        }
        public List<TourRequest> GetAllOnHold() {
            return GetAll().Where(r => r.Status == TourRequestStatus.OnHold).ToList();
        }

        public IEnumerable<User> GetTouristsForNotification(Tour tour) {
            List<User> users = new List<User>();
            foreach(User tourist in _userService.GetTourists()) {
                foreach(TourRequest request in  _tourRequestRepository.GetNotAccepted(tourist.Id)) {
                    if (request.LocationId == tour.LocationId || request.LanguageId == tour.LanguageId) {
                        users.Add(tourist);
                        break;
                    }
                }
            }
            return users;
        }
      
        public List<TourRequest> GetFilteredByGuide(TourRequestFilterDTO filter) {
            if(filter.IsEmpty())
                return GetAllOnHold();

            return GetAllOnHold().Where(x => IsFiltered(x, filter)).ToList();
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

        public void CreateRequest(TourRequestDTO tourRequestDTO, int complexRequestId) {           
            _tourRequestRepository.Save(new TourRequest(tourRequestDTO, complexRequestId));
        }      
        public TouristStatistics GetStatistics(int userId, string year) {
            if (year == "All-time")
                return StatisticsCalculator.CalculateTouristStatistics(_tourRequestRepository.GetAccepted(userId), _tourRequestRepository.GetByTouristId(userId));
            return StatisticsCalculator.CalculateTouristStatistics(_tourRequestRepository.GetAcceptedForYear(userId, int.Parse(year)), _tourRequestRepository.GetByTouristIdForYear(userId, int.Parse(year)));
        }

        public IEnumerable<int> GetRequestYears(int userId) {
            return _tourRequestRepository.GetByTouristId(userId).Select(r => r.StartDate.Year).Distinct();
        } 

        public Dictionary<Location, int> GetRequestsByLocations(int userId) {
            Dictionary<Location, int> pairs = new Dictionary<Location, int>();

            foreach(Location location in _locationService.GetAll()) 
                pairs.Add(location, _tourRequestRepository.GetRequestNumberByLocation(location, userId));
            
            return pairs;
        }

        public Dictionary<Language, int> GetRequestsByLanguages(int userId) {
            Dictionary<Language, int> pairs = new Dictionary<Language, int>();

            foreach (Language language in _languageService.GetAll())
                pairs.Add(language, _tourRequestRepository.GetRequestNumberByLanguage(language, userId));

            return pairs;
        }
        public void Update(TourRequest tRequest) {
            _tourRequestRepository.Update(tRequest);
        }
        private bool IsFiltered(TourRequest tRequest, TourRequestFilterDTO filter) {
            return MatchesTouristNumber(tRequest, filter) &&
                    MatchesLocation(tRequest, filter) &&
                    MatchesLanguage(tRequest, filter) &&
                    MatchesDateStart(tRequest, filter) &&
                    MatchesDateEnd(tRequest, filter);
        }
        private bool MatchesLocation(TourRequest tRequest, TourRequestFilterDTO filter) {
            return tRequest.LocationId == filter.Location.Id || filter.Location.Id == 0;
        }

        private bool MatchesLanguage(TourRequest tRequest, TourRequestFilterDTO filter) {
            return tRequest.LanguageId == filter.Language.Id || filter.Language.Id == -1;
        }

        private bool MatchesTouristNumber(TourRequest tRequest, TourRequestFilterDTO filter) {
            return tRequest.PassengerNumber >= filter.TouristNumber || filter.TouristNumber == 0;
        }
        private bool MatchesDateStart(TourRequest tRequest, TourRequestFilterDTO filter) {
            return tRequest.StartDate >= filter.Start || filter.Start == DateOnly.MinValue;
        }
        private bool MatchesDateEnd(TourRequest tRequest, TourRequestFilterDTO filter) {
            return tRequest.EndDate <= filter.End || filter.End == DateOnly.MaxValue;
        }
        public List<int> GetStatsByLocation(int locationId, int year) {
            List<int> stats = new List<int>() {0,0,0,0,0,0,0,0,0,0,0,0 };
            if(year == -1) {
                foreach (var request in GetAll().Where(x => x.LocationId == locationId)) {
                    StatisticsCalculator.CalculateRequestStats(request.StartDate.Year, stats);
                }
            }
            else {
                foreach(var request in GetAll().Where(x => x.LocationId == locationId && x.StartDate.Year == year)) {
                    StatisticsCalculator.CalculateRequestStats(request.StartDate.Month, stats);
                }
            }
            return stats;
        }
        public List<int> GetStatsByLanguage(int languageId, int year) {
            List<int> stats = new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            if (year == -1) {
                foreach (var request in GetAll().Where(x => x.LanguageId == languageId)) {
                    StatisticsCalculator.CalculateRequestStats(request.StartDate.Year, stats);
                }
            }
            else {
                foreach (var request in GetAll().Where(x => x.LanguageId == languageId && x.StartDate.Year == year)) {
                    StatisticsCalculator.CalculateRequestStats(request.StartDate.Month, stats);
                }
            }
            return stats;
        }
        public int GetMostWantedLocation() {
            var locationIds = GetAll().Select(x => x.LocationId).Distinct().ToList();
            int mostWantedId = -1;
            int mostRequests = 0;
            foreach (var locId in locationIds) {
                DateOnly today = new DateOnly(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);
                int temp = GetAll().Count(x => x.LocationId == locId && x.StartDate.AddYears(1) > today);
                if (mostRequests <= temp) {
                    mostRequests = temp;
                    mostWantedId = locId;
                }
            }
            return mostWantedId;
        }
        public int GetMostWantedLanguage() {
            var languageIds = GetAll().Select(x => x.LanguageId).Distinct().ToList();
            int mostWantedId = -1;
            int mostRequests = 0;
            foreach (var lanId in languageIds) {
                DateOnly today = new DateOnly(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);
                int temp = GetAll().Count(x => x.LanguageId == lanId && x.StartDate.AddYears(1) > today);
                if (mostRequests <= temp) {
                    mostRequests = temp;
                    mostWantedId = lanId;
                }
            }
            return mostWantedId;
        }
    }
}
