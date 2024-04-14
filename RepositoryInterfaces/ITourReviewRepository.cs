using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.RepositoryInterfaces {
    internal interface ITourReviewRepository : IRepository<TourReview> {
        public List<TourReview> GetByTourId(int tourId);

    }
}
