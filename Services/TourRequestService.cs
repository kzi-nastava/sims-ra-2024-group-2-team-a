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

        public List<TourRequest> GetAllOnHold() {
            return _tourRequestRepository.GetAll().Where(r => r.Status == TourRequestStatus.OnHold).ToList();
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

        public void CreateRequest(TourRequestDTO tourRequestDTO, int passengerNumber) {
            _tourRequestRepository.Save(new TourRequest(tourRequestDTO, passengerNumber));
        }      

       /* public List<int> GetStats(int year, bool isLocation, int locationLanguageId) {
            if(year == -1) {
                return GetAll().Co;
            }
        }*/
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
    }
}
