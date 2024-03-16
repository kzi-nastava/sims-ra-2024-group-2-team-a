using BookingApp.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository {
    public class PointOfInterestRepository : IRepository<PointOfInterest> {
        private readonly Serializer<PointOfInterest> _serializer;

        private List<PointOfInterest> _pointsOfInterest;
        public PointOfInterestRepository() {
            _serializer = new Serializer<PointOfInterest>();
            _pointsOfInterest = _serializer.FromCSV();
        }

        public List<PointOfInterest> GetAll() {
            return _serializer.FromCSV();
        }

        public PointOfInterest Save(PointOfInterest pointOfInterest) {
            pointOfInterest.Id = NextId();
            _pointsOfInterest = _serializer.FromCSV();
            _pointsOfInterest.Add(pointOfInterest);
            _serializer.ToCSV(_pointsOfInterest);
            return pointOfInterest;
        }

        public bool Delete(PointOfInterest pointOfInterest) {
            _pointsOfInterest = _serializer.FromCSV();
            PointOfInterest? found = _pointsOfInterest.Find(c => c.Id == pointOfInterest.Id);
            if (found == null) return false;

            _pointsOfInterest.Remove(found);
            _serializer.ToCSV(_pointsOfInterest);

            return true;
        }

        public bool Update(PointOfInterest pointOfInterest) {
            _pointsOfInterest = _serializer.FromCSV();
            PointOfInterest? current = _pointsOfInterest.Find(c => c.Id == pointOfInterest.Id);
            int index = _pointsOfInterest.IndexOf(current);
            if (current == null) return false;

            _pointsOfInterest.Remove(current);
            _pointsOfInterest.Insert(index, pointOfInterest);   
            _serializer.ToCSV(_pointsOfInterest);

            return true;
        }

        public PointOfInterest GetById(int id) {
            return _pointsOfInterest.Find(c => c.Id == id);
        }

        private int NextId() {
            _pointsOfInterest = _serializer.FromCSV();
            if (_pointsOfInterest.Count < 1) {
                return 1;
            }
            return _pointsOfInterest.Max(c => c.Id) + 1;
        }

        public List<PointOfInterest> GetAllByTourId(int tourId) {
            List<PointOfInterest> points = _pointsOfInterest = _serializer.FromCSV();
            return points.FindAll(c => c.TourId == tourId);
        }
    }
}
