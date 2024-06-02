using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using System.Collections.Generic;
using System.Linq;

namespace BookingApp.Repository {
    public class PointOfInterestRepository : Repository<PointOfInterest>, IPointOfInterestRepository {
        public List<PointOfInterest> GetAllByTourId(int tourId) {
            List<PointOfInterest> points = _items = _serializer.FromCSV();
            return points.FindAll(c => c.TourId == tourId);
        }
        
        public bool DeleteMultiple(List<PointOfInterest> points) {
            _items = _serializer.FromCSV();
            if (_items.RemoveAll(x => points.Select(y => y.Id).Contains(x.Id)) != points.Count)
                return false;
            _serializer.ToCSV(_items);
            return true;
        }
        public bool DeleteByTours(List<Tour> tours) {
            _items = _serializer.FromCSV();
            if (_items.RemoveAll(x => tours.Select(y => y.Id).Contains(x.TourId)) <= 0)
                return false;
            _serializer.ToCSV(_items);
            return true;
        }

    }
}
