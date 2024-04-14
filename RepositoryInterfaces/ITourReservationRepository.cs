using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.RepositoryInterfaces {
    public interface ITourReservationRepository : IRepository<TourReservation>{
        public List<TourReservation> GetByTourId(int tourId);
        public TourReservation GetByTourAndTourist(int tourId, int touristId);
        public bool DeleteMultiple(List<TourReservation> reservations);

    }
}
