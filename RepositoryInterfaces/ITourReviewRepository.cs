using BookingApp.Model;
using System.Collections.Generic;

namespace BookingApp.RepositoryInterfaces {
    internal interface ITourReviewRepository : IRepository<TourReview> {
        public List<TourReview> GetByTourId(int tourId);

    }
}
