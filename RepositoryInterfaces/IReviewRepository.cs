using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.RepositoryInterfaces {
    public interface IReviewRepository : IRepository<Review> {
        Review GetByReservationId(int id);
        List<Review> GetByOwnerId(int ownerId);
    }
}
