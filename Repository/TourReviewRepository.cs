using BookingApp.Model;
using BookingApp.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookingApp.Repository {
    public class TourReviewRepository : Repository<TourReview>, ITourReviewRepository{
        public List<TourReview> GetByTourId(int tourId) {
            _items = _serializer.FromCSV();
            return _items.FindAll(x => x.TourId == tourId);
        }
    }
}
