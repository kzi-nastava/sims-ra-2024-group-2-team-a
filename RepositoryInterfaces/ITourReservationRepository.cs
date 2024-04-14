using BookingApp.Model;
using System.Collections.Generic;

namespace BookingApp.RepositoryInterfaces {
    public interface ITourReservationRepository : IRepository<TourReservation>{
        public List<TourReservation> GetByTourId(int tourId);
        public TourReservation GetByTourAndTourist(int tourId, int touristId);
        public bool DeleteMultiple(List<TourReservation> reservations);

    }
}
