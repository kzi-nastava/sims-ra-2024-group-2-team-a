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
            bool matchesLocation = tour.LocationId == filter.Location.Id || filter.Location.Id == -1;
            bool matchesDuration = tour.Duration <= filter.Duration || filter.Duration == 0;
            bool matchesLanguage = tour.LanguageId == filter.Language.Id || filter.Language.Id == -1;
            bool matchesTouristNumber = tour.MaxTouristNumber >= filter.TouristNumber || filter.TouristNumber == 0;

            return matchesLocation && matchesDuration && matchesLanguage && matchesTouristNumber;
        }

        public List<Tour> GetToursByLocation(int locationId) {
            return GetAll().Where(t => (locationId == t.LocationId)).ToList();
        }

        public List<Tour> GetSameLocationTours(TourDTO tour) {
            return GetToursByLocation(tour.LocationId).Where(t => (tour.Id != t.Id)).ToList();
        }
    }
}
