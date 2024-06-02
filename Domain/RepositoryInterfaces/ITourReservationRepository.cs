using BookingApp.Domain.Model;
using System.Collections.Generic;

namespace BookingApp.Domain.RepositoryInterfaces {
    public interface ITourReservationRepository : IRepository<TourReservation>
    {
        public List<TourReservation> GetByTourId(int tourId);
        public TourReservation GetByTourAndTourist(int tourId, int touristId);
        public bool DeleteMultiple(List<TourReservation> reservations);
        public List<TourReservation> GetByTours(List<Tour> tours);


    }
}
