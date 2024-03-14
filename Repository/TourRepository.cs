using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Serializer;
using System.Collections.Generic;
using System.Linq;

namespace BookingApp.Repository {
    public class TourRepository : IRepository<Tour> {
        private readonly Serializer<Tour> _serializer;

        private List<Tour> _tours;

        public TourRepository() {
            _serializer = new Serializer<Tour>();
            _tours = _serializer.FromCSV();
        }

        public List<Tour> GetAll() {
            return _serializer.FromCSV();
        }

        public Tour Save(Tour tour) {
            tour.Id = NextId();
            _tours = _serializer.FromCSV();
            _tours.Add(tour);
            _serializer.ToCSV(_tours);
            return tour;
        }

        public bool Delete(Tour tour) {
            _tours = _serializer.FromCSV();
            Tour? found = _tours.Find(c => c.Id == tour.Id);
            if (found == null) return false;

            _tours.Remove(found);
            _serializer.ToCSV(_tours);
            return true;
        }

        public bool Update(Tour tour) {
            _tours = _serializer.FromCSV();
            Tour? current = _tours.Find(c => c.Id == tour.Id);
            int index = _tours.IndexOf(current);
            if (current == null) return false;

            _tours.Remove(current);
            _tours.Insert(index, tour);
            _serializer.ToCSV(_tours);

            return true;
        }

        public Tour GetById(int id) {
            return _tours.Find(c => c.Id == id);
        }

        private int NextId() {
            _tours = _serializer.FromCSV();
            if (_tours.Count < 1) {
                return 1;
            }
            return _tours.Max(c => c.Id) + 1;
        }

        public List<Tour> GetFiltered(TourFilterDTO filter) {
            _tours = _serializer.FromCSV();

            if (filter.isEmpty())
                return _tours;

            return _tours.Where(t => IsFiltered(t, filter)).ToList();
        }

        private bool IsFiltered(Tour tour, TourFilterDTO filter) {
            bool matchesLocation = tour.LocationId == filter.Location.Id || filter.Location.Id == -1;
            bool matchesDuration = tour.Duration <= filter.Duration || filter.Duration == 0;
            bool matchesLanguage = true;//tour.Language.ToLower().Contains(filter.Language.ToLower()) || filter.Language == "";
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
