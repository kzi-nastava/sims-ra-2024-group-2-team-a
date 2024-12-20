﻿using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using System.Collections.Generic;
using System.Linq;

namespace BookingApp.Repository {

    public class AccommodationReviewRepository : Repository<AccommodationReview>, IAccommodationReviewRepository {
        public AccommodationReview GetByReservationId(int id) {
            return _serializer.FromCSV().Find(x => x.ReservationId == id);
        }

        public List<AccommodationReview> GetByOwnerId(int ownerId) {
            return _serializer.FromCSV().Where(review => review.OwnerId == ownerId).ToList();
        }

        public List<AccommodationReview> GetByGuestId(int guestId) {
            return _serializer.FromCSV().Where(review => review.GuestId == guestId).ToList();
        }

    }
}
