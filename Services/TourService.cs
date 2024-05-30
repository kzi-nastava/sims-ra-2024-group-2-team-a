using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.WPF.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace BookingApp.Services {
    public class TourService {
        private readonly ITourRepository _tourRepository;
        
        private PassengerService _passengerService;
        private TourReservationService _tourReservationService;
        private TourReviewService _tourReviewService;
        private PointOfInterestService _pointOfInterestService;
        private TourRequestService _tourRequestService;
        private NotificationService _notificationService;
        public TourService(ITourRepository tourRepository) {
            _tourRepository = tourRepository;
        }
        public void InjectService(PassengerService passengerService, TourReservationService tourReservationService, TourReviewService tourReviewService, PointOfInterestService pointOfInterestService, TourRequestService tourRequestService, NotificationService notificationService) {
            _passengerService = passengerService;
            _tourReservationService = tourReservationService;
            _tourReviewService = tourReviewService;
            _pointOfInterestService = pointOfInterestService;
            _tourRequestService = tourRequestService;
            _notificationService = notificationService;

        }
        public double GetTourGrade(int Id) {
            List<TourReview> reviews = _tourReviewService.GetByTourId(Id);
            if (reviews.Count == 0)
                return 0;
            return reviews.Average(x => x.AvrageGrade);
        }
        private bool IsFiltered(Tour tour, TourFilterDTO filter) {
            return MatchesLocation(tour, filter) &&
                   MatchesDuration(tour, filter) &&
                   MatchesLanguage(tour, filter) &&
                   MatchesMaxTouristNumber(tour, filter);
        }

        private bool MatchesName(Tour tour, TourFilterDTO filter) {
            return tour.Name.ToLower().Contains(filter.Name.ToLower()) || filter.Name == "";
        }

        private bool MatchesLocation(Tour tour, TourFilterDTO filter) {
            return tour.LocationId == filter.Location.Id || filter.Location.Id == 0;
        }

        private bool MatchesDuration(Tour tour, TourFilterDTO filter) {
            return tour.Duration <= filter.Duration || filter.Duration == 0;
        }

        private bool MatchesLanguage(Tour tour, TourFilterDTO filter) {
            return tour.LanguageId == filter.Language.Id || filter.Language.Id == -1;
        }

        private bool MatchesMaxTouristNumber(Tour tour, TourFilterDTO filter) {
            return tour.MaxTouristNumber >= filter.TouristNumber || filter.TouristNumber == 0;
        }

        private bool MatchesCurrentTouristNumber(Tour tour, TourFilterDTO filter) {
            return tour.CurrentTouristNumber >= filter.TouristNumber || filter.TouristNumber == 0;
        }
        private bool MatchesDate(Tour tour, TourFilterDTO filter) {
            return tour.Beggining >= filter.Beggining || filter.Beggining == DateTime.MinValue;
        }

        public List<Tour> GetFiltered(TourFilterDTO filter) {
            List<Tour> allTours = _tourRepository.GetAll();

            if (filter.isEmpty())
                return allTours;

            return allTours.Where(t => IsFiltered(t, filter)).ToList();
        }

        public Tour GetById(int tourId) {
            return _tourRepository.GetById(tourId);
        }

        public int GetAvailableSpace(TourDTO tour) {
            return _tourRepository.GetAvailableSpace(tour);
        }

        public List<Passenger> GetTouristEntries(int touristId) {
            return _passengerService.GetAll().Where(p => p.UserId == touristId).ToList();
        }

        public bool WasTouristPresent(int touristId, TourDTO tour) {
            List<PointOfInterest> checkpoints = _pointOfInterestService.GetAllByTourId(tour.Id);

            foreach (var tourist in GetTouristEntries(touristId)) {
                if (checkpoints.Any(c => c.Id == tourist.JoinedPointOfInterestId))
                    return true;
            }

            return false;
        }

        public bool IsTourReviewedForUser(int userId, TourDTO tour) {
            return _tourReviewService.GetAll().Find(t => t.TourId == tour.Id && t.TouristId == userId) != null;
        }

        public List<int> GetTourStats(int tourId) {
            return _passengerService.GetAttendance(_tourReservationService.GetByTourId(tourId));
        }
        public Tour GetActive(int userId) {
            return _tourRepository.GetActive(userId);
        }
        public List<Tour> GetScheduled(int userId) {
            return _tourRepository.GetScheduled(userId);
        }
        public List<Tour> GetFinished(int userId) {
            return _tourRepository.GetFinished(userId);
        }
        public List<Tour> GetFinishedByLanguage(int userId, Language lang) {
            return GetFinished(userId).FindAll(x => x.LanguageId == lang.Id);
        }
        public List<Tour> GetLive(int userId) {
            return _tourRepository.GetLive(userId);
        }
        public Tour GetMostViewedByYear(int year) {
            List<Tour> tours = _tourRepository.GetByYear(year);
            if (tours == null)
                return null;
            return tours.MaxBy(y => y.CurrentTouristNumber);
        }
        public List<Tour> GetFilteredScheduled(TourFilterDTO filter, int userId) {
            List<Tour> tours = GetScheduled(userId);

            if (filter.isEmpty())
                return tours;

            return tours.Where(x => IsFilteredScheduled(x, filter)).ToList();
        }
        public List<Tour> GetFilteredLive(TourFilterDTO filter, int userId) {
            List<Tour> tours = GetLive(userId);

            if (filter.isEmpty())
                return tours;

            return tours.Where(x => IsFilteredLive(x, filter)).ToList();
        }
        private bool IsFilteredScheduled(Tour tour, TourFilterDTO filter) {
            return MatchesName(tour, filter) &&
                   MatchesLocation(tour, filter) &&
                   MatchesDuration(tour, filter) &&
                   MatchesLanguage(tour, filter) &&
                   MatchesCurrentTouristNumber(tour, filter) &&
                   MatchesDate(tour, filter);
        }
        private bool IsFilteredLive(Tour tour, TourFilterDTO filter) {
            return MatchesName(tour, filter) &&
                   MatchesLocation(tour, filter) &&
                   MatchesDuration(tour, filter) &&
                   MatchesLanguage(tour, filter) &&
                   MatchesCurrentTouristNumber(tour, filter);
        }
        public Tour Save(Tour tour) {           
            Tour savedTour = _tourRepository.Save(tour);
            foreach (User tourist in _tourRequestService.GetTouristsForNotification(tour))
                _notificationService.SendTouristNotification(NotificationCategory.TourRequest, tourist.Id, savedTour.Id);
            return savedTour;
        }

        public void SendTouristsTourNotifications(Tour tour) {

        }
        public bool DeleteMultiple(List<Tour> tours) {
            foreach (Tour tour in tours) {
                if(!Delete(tour)) return false;
            }
            return true;
        }
        public bool Delete(Tour tour) => _tourRepository.Delete(tour);
        public bool Update(Tour tour) {
            return _tourRepository.Update(tour);
        }

        public List<Tour> GetToursByLocation(int locationId) {
            return _tourRepository.GetAll().Where(t => (locationId == t.LocationId)).ToList();
        }

        public List<Tour> GetSameLocationTours(TourDTO tour) {
            return GetToursByLocation(tour.LocationId).Where(t => (tour.Id != t.Id)).ToList();
        }
        public bool IsGuideAvailable(int userId, int complexId, DateTime from, DateTime to) {
            Calendar calendar = GetUnavailableDates(userId, complexId, from, to);
            if (calendar.BlackoutDates.All(x => x.Start.Date > to.Date || x.End.Date < from.Date))
                return true;
            return false;

        }
        public Calendar GetUnavailableDates(int userId, int complexId, DateTime from, DateTime to) {
            Calendar calendar = new Calendar();
            if (complexId >= 1)
                calendar = _tourRequestService.GetBusyDates(complexId);

            calendar.DisplayDateStart = from;
            calendar.DisplayDateEnd = to;
            foreach (Tour tour in  GetScheduled(userId)) {
                if(tour.Beggining.Date <= to.Date && tour.End.Date >= from.Date) {
                    calendar.BlackoutDates.Add(new CalendarDateRange(tour.Beggining.Date, tour.End.Date));
                }
            }
            
            return calendar;
        }
        public bool IsAvailableToShow(int userId, int complexId, DateTime from, DateTime to) {
            Calendar calendar = GetUnavailableDates(userId, complexId, from, to);
            for(var date = from.Date; date <= to.Date; date = date.AddDays(1)) {
                if (IsDateAvailable(calendar, date.Date))
                    return true;
            }
            return false;
        }
        private bool IsDateAvailable(Calendar calendar, DateTime date) {
            foreach(var calendarRange in calendar.BlackoutDates) {
                if (date.Date >= calendarRange.Start.Date && date.Date <= calendarRange.End.Date)
                    return false;
            }
            return true;
        }
    }
}
