using BookingApp.DTO;
using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookingApp.Repository {

    public class TourRepository : Repository<Tour> {
        public List<Tour> GetScheduled(int userId) {
            DateTime today = DateTime.Today;
            _items = _serializer.FromCSV();
            return _items.FindAll(x => x.GuideId == userId && x.Beggining >= today && x.State.Equals(TourState.Scheduled));
        }
        public List<Tour> GetFinished(int userId) {
            _items = _serializer.FromCSV();
            return _items.FindAll(x => x.GuideId == userId && x.State.Equals(TourState.Finished));
        }

        public List<Tour> GetLive(int userId) {
            DateTime today = DateTime.Today;
            _items = _serializer.FromCSV();
            return _items.FindAll(x => x.GuideId == userId && x.Beggining.Date == today.Date && x.State.Equals(TourState.Scheduled));
        }

        public List<Tour> GetByYear(int year) {
            _items = _serializer.FromCSV();
            if (year == -1) 
                return _items.FindAll(x => x.State == TourState.Finished);
            else
                return _items.FindAll(x => x.Beggining.Year == year && x.State == TourState.Finished);
           
        }

        public int GetAvailableSpace(TourDTO tour) {
            return tour.MaxTouristNumber - tour.CurrentTouristNumber;
        }

        public bool MatchesName(Tour tour, TourFilterDTO filter) {
            return tour.Name.Contains(filter.Name) || filter.Name == "";
        }

        public bool MatchesLocation(Tour tour, TourFilterDTO filter) {
            return tour.LocationId == filter.Location.Id || filter.Location.Id == -1;
        }

        public bool MatchesDuration(Tour tour, TourFilterDTO filter) {
            return tour.Duration <= filter.Duration || filter.Duration == 0;
        }

        public bool MatchesLanguage(Tour tour, TourFilterDTO filter) {
            return tour.LanguageId == filter.Language.Id || filter.Language.Id == -1;
        }

        public bool MatchesMaxTouristNumber(Tour tour, TourFilterDTO filter) {
            return tour.MaxTouristNumber >= filter.TouristNumber || filter.TouristNumber == 0;
        }

        public bool MatchesCurrentTouristNumber(Tour tour, TourFilterDTO filter) {
            return tour.CurrentTouristNumber >= filter.TouristNumber || filter.TouristNumber == 0;
        }
        public bool MatchesDate(Tour tour, TourFilterDTO filter) {
            return tour.Beggining >= filter.Beggining || filter.Beggining == DateTime.MinValue;
        }
    }
}
