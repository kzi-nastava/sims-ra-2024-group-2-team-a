using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using System.Collections.Generic;

namespace BookingApp.Repository {
    public class TourReviewRepository : Repository<TourReview>, ITourReviewRepository{
        public List<TourReview> GetByTourId(int tourId) {
            _items = _serializer.FromCSV();
            return _items.FindAll(x => x.TourId == tourId);
        }
    }
}
