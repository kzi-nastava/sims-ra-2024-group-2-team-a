using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookingApp.Repository {

    public class ReviewRepository : Repository<Review>, IReviewRepository {
        public Review GetByReservationId(int id) {
            return _serializer.FromCSV().Find(x => x.ReservationId == id);
        }

        public List<Review> GetByOwnerId(int ownerId) {
            return _serializer.FromCSV().Where(review => review.OwnerId == ownerId).ToList();
        }

    }
}
