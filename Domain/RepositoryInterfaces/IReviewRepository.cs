using BookingApp.Domain.Model;
using System.Collections.Generic;

namespace BookingApp.Domain.RepositoryInterfaces {
    public interface IReviewRepository : IRepository<Review>
    {
        Review GetByReservationId(int id);
        List<Review> GetByOwnerId(int ownerId);
        List<Review> GetByGuestId(int guestId);
    }
}
