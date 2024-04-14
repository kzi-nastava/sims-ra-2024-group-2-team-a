using BookingApp.Domain.Model;
using System.Collections.Generic;

namespace BookingApp.Domain.RepositoryInterfaces {
    internal interface ITourReviewRepository : IRepository<TourReview>
    {
        public List<TourReview> GetByTourId(int tourId);

    }
}
