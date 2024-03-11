using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            if(found == null) return false;

            _tours.Remove(found);
            _serializer.ToCSV(_tours);
            return true;
        }

        public bool Update(Tour tour) {
            _tours = _serializer.FromCSV();
            Tour? current = _tours.Find(c => c.Id == tour.Id);
            int index = _tours.IndexOf(current);
            if(current == null) return false;

            _tours.Remove(current);
            _tours.Insert(index, tour);
            _serializer.ToCSV(_tours);

            return true;
        }

        public Tour GetById(int id) {
            return _tours.Find(x => x.Id == id);
        }

        public int NextId() {
            _tours = _serializer.FromCSV();
            if (_tours.Count < 1) {
                return 1;
            }
            return _tours.Max(c => c.Id) + 1;
        }

        public List<Tour> GetFiltered() {
            return _serializer.FromCSV();
        }

        private bool IsFiltered(Tour tour, TourFilterDTO filter) {

            return true;
        }
    }
}
