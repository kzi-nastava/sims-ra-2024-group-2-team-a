﻿using BookingApp.Domain.Model;
using System.Collections.Generic;

namespace BookingApp.Domain.RepositoryInterfaces {
    public interface ITourReviewRepository : IRepository<TourReview>
    {
        public List<TourReview> GetByTourId(int tourId);
        public List<TourReview> GetByTours(List<Tour> tours);

    }
}
