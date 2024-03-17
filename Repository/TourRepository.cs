using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Serializer;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookingApp.Repository {

    public class TourRepository : Repository<Tour> {

        private List<Tour> _tours;
        public List<Tour> GetToursToday() {
            DateTime today = DateTime.Today;
            _tours = _serializer.FromCSV();
            //DateTime dis = _tours[0].Beggining;
            //if (dis == today) return null;
            return _tours.FindAll(x => x.Beggining.Date == today.Date);
        }

        public List<Tour> GetFiltered(TourFilterDTO filter) {
            _items = GetAll();

            if (filter.isEmpty())
                return _items;

            return _items.Where(t => IsFiltered(t, filter)).ToList();
        }

        private bool IsFiltered(Tour tour, TourFilterDTO filter) {
            return MatchesLocation(tour, filter) && 
                   MatchesDuration(tour, filter) && 
                   MatchesLanguage(tour, filter) && 
                   MatchesTouristNumber(tour, filter);
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

        private bool MatchesTouristNumber(Tour tour, TourFilterDTO filter) {
            return tour.MaxTouristNumber >= filter.TouristNumber || filter.TouristNumber == 0;
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
