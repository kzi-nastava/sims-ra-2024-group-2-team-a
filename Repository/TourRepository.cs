using BookingApp.DTO;
using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookingApp.Repository {

    public class TourRepository : Repository<Tour> {
        public List<Tour> GetScheduled(int userId) {
            DateTime today = DateTime.Now;
            _items = _serializer.FromCSV();
            return _items.FindAll(x => x.GuideId == userId && x.Beggining >= today && !x.IsFinished);
        }


        public List<Tour> GetLive(int userId) {
            DateTime today = DateTime.Today;
            _items = _serializer.FromCSV();
            return _items.FindAll(x => x.GuideId == userId && x.Beggining.Date == today.Date && !x.IsFinished);
        }
        public List<Tour> GetFilteredScheduled(TourFilterDTO filter, int userId) {
            _items = GetScheduled(userId);

            if(filter.isEmpty())
                return _items;

            return _items.Where(x => IsFilteredScheduled(x, filter)).ToList();
        }

        public List<Tour> GetFilteredLive(TourFilterDTO filter, int userId) {
            _items = GetLive(userId);

            if (filter.isEmpty())
                return _items;

            return _items.Where(x => IsFilteredLive(x, filter)).ToList();
        }

        public List<Tour> GetFiltered(TourFilterDTO filter) {
            _items = GetAll();

            if (filter.isEmpty())
                return _items;

            return _items.Where(t => IsFiltered(t, filter)).ToList();
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
        private bool IsFiltered(Tour tour, TourFilterDTO filter) {
            return MatchesLocation(tour, filter) &&
                   MatchesDuration(tour, filter) &&
                   MatchesLanguage(tour, filter) &&
                   MatchesMaxTouristNumber(tour, filter);
        }
        private bool MatchesName(Tour tour, TourFilterDTO filter) {
            return tour.Name.Contains(filter.Name) || filter.Name == "";
        }

        private bool MatchesLocation(Tour tour, TourFilterDTO filter) {
            return tour.LocationId == filter.Location.Id || filter.Location.Id == -1;
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

        public List<Tour> GetToursByLocation(int locationId) {
            return GetAll().Where(t => (locationId == t.LocationId)).ToList();
        }

        public List<Tour> GetSameLocationTours(TourDTO tour) {
            return GetToursByLocation(tour.LocationId).Where(t => (tour.Id != t.Id)).ToList();
        }

        public int GetAvailableSpace(TourDTO tour) {
            return tour.MaxTouristNumber - tour.CurrentTouristNumber;
        }
    }
}
