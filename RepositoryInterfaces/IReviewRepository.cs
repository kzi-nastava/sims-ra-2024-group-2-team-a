using BookingApp.Model;
using System.Collections.Generic;

namespace BookingApp.RepositoryInterfaces {
    public interface IReviewRepository : IRepository<Review> {
        Review GetByReservationId(int id);
        List<Review> GetByOwnerId(int ownerId);
    }
}
