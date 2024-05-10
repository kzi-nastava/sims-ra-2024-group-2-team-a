using BookingApp.Domain.Model;
using System.Collections.Generic;

namespace BookingApp.Domain.RepositoryInterfaces {
    public interface IReviewRepository : IRepository<AccommodationReview>
    {
        AccommodationReview GetByReservationId(int id);
        List<AccommodationReview> GetByOwnerId(int ownerId);
        List<AccommodationReview> GetByGuestId(int guestId);
    }
}
