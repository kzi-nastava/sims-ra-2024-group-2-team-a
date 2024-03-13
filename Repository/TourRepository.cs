using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository {
    public class TourRepository : Repository<Tour> {

        public List<Tour> GetFiltered(TourFilterDTO filter) {
            _items = GetAll();

            if (filter.isEmpty())
                return _items;

            return _items.Where(t => IsFiltered(t, filter)).ToList();
        }

        private bool IsFiltered(Tour tour, TourFilterDTO filter) {
            bool matchesLocation = tour.LocationId == filter.Location.Id || filter.Location.Id == -1;
            bool matchesDuration = tour.Duration <= filter.Duration || filter.Duration == 0;
            bool matchesLanguage = tour.Language.ToLower().Contains(filter.Language.ToLower()) || filter.Language == "";
            bool matchesTouristNumber = tour.MaxTouristNumber >= filter.TouristNumber || filter.TouristNumber == 0;

            return matchesLocation && matchesDuration && matchesLanguage && matchesTouristNumber;
        }
    }
}
