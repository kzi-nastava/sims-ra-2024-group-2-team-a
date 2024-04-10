using BookingApp.Model;
using System.Collections.Generic;
using System.Linq;

namespace BookingApp.Repository {
    public class PointOfInterestRepository : Repository<PointOfInterest> {
        public List<PointOfInterest> GetAllByTourId(int tourId) {
            List<PointOfInterest> points = _items = _serializer.FromCSV();
            return points.FindAll(c => c.TourId == tourId);
        }
        public bool DeleteByTourId(int id) {
            return DeleteMultiple(GetAllByTourId(id));
        }
        public bool DeleteMultiple(List<PointOfInterest> points) {
            _items = _serializer.FromCSV();
            if (_items.RemoveAll(x => points.Select(y => y.Id).Contains(x.Id)) != points.Count)
                return false;
            _serializer.ToCSV(_items);
            return true;
        }
    }
}
