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
        public List<TourRequest> GetAll() {
            return _tourRequestRepository.GetAll();
        }
        public List<TourRequest> GetByTouristId(int id) { 
            return _tourRequestRepository.GetByTouristId(id); 
        }

        public List<TourRequest> GetByTouristIdForYear(int id, int year) {
            return GetByTouristId(id).Where(a => a.EndDate.Year == year).ToList();
        }

        public List<TourRequest> GetOnHold(int userId) {
            return GetByTouristId(userId).Where(r => r.Status == TourRequestStatus.OnHold).ToList();
        }
        public List<TourRequest> GetAllOnHold() {
            return GetAll().Where(r => r.Status == TourRequestStatus.OnHold).ToList();
        }

        private List<TourRequest> GetExpired(int userId) {
            return GetByTouristId(userId).Where(r => r.Status == TourRequestStatus.Expired).ToList();
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
                        return GetAccepted(userId);
                    case TourRequestStatus.OnHold:
                        return GetOnHold(userId);
                    default:
                        return GetExpired(userId);
                }
            }
            else {
                return GetByTouristId(userId);
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

        public IEnumerable<int> GetRequestYears (int userId) {
            return GetByTouristId(userId).Select(r => r.StartDate.Year).Distinct();
        }

        private List<TourRequest> GetAccepted(int userId) {
            return GetByTouristId(userId).Where(r => r.Status == TourRequestStatus.Accepted).ToList();
        }

        public List<TourRequest> GetAcceptedForYear(int userId, int year) {
            return GetByTouristIdForYear(userId, year).Where(r => r.Status == TourRequestStatus.Accepted).ToList();
        }

        public TouristStatistics GetStatistics(int userId, string year) {
            if (year == "All-time")
                return StatisticsCalculator.CalculateTouristStatistics(GetAccepted(userId), GetByTouristId(userId));
            return StatisticsCalculator.CalculateTouristStatistics(GetAcceptedForYear(userId, int.Parse(year)), GetByTouristIdForYear(userId, int.Parse(year)));
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
    }
}
