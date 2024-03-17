using BookingApp.Model;
using System.Collections.Generic;

namespace BookingApp.Repository {
    public class PointOfInterestRepository : Repository<PointOfInterest> {
        public List<PointOfInterest> GetAllByTourId(int tourId) {
            List<PointOfInterest> points = _items = _serializer.FromCSV();
            return points.FindAll(c => c.TourId == tourId);
        }
    }
}
