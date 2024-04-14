using BookingApp.Model;
using System.Collections.Generic;

namespace BookingApp.RepositoryInterfaces {
    public interface IPassengerRepository : IRepository<Passenger>{
        public int GetStatsTeen(List<Passenger> passengers);
        public int GetStatsMid(List<Passenger> passengers);
        public int GetStatsOld(List<Passenger> passengers);
        public Passenger? GetByReservationAndTourist(int reservationId, int touristId);
        public List<Passenger> GetUnJoined(List<TourReservation> reservations);
        public List<Passenger> GetByReservations(List<TourReservation> reservations);
        public bool DeleteMultiple(List<Passenger> passengers);
    }
}
