using BookingApp.Model;
using BookingApp.Serializer;
using System.Collections.Generic;
using System.Linq;

namespace BookingApp.Repository {
    public class PassengerRepository : Repository<Passenger> {
        public int GetStatsTeen(List<Passenger> passengers) {
            return passengers.Count(x => x.Age < 18 && x.JoinedPointOfInterestId != -1);
        }
        public int GetStatsMid(List<Passenger> passengers) {
            return passengers.Count(x => x.Age >= 18 && x.Age <= 50 && x.JoinedPointOfInterestId != -1);
        }
        public int GetStatsOld(List<Passenger> passengers) {
            return passengers.Count(x => x.Age > 50 && x.JoinedPointOfInterestId != -1);
        }
        public List<Passenger> GetUnJoined(List<TourReservation> reservations) {
            _items = _serializer.FromCSV();
            List<int> ids = new List<int>();
            foreach (var reservatuon in reservations) {
                ids.Add(reservatuon.Id);
            }
            return _items.FindAll(x => ids.Contains(x.TourReservationId) && x.JoinedPointOfInterestId == -1);
        }
        public bool DeleteByReservations(List<TourReservation> reservations) {
            return DeleteMultiple(GetByReservations(reservations));
        }
        public List<Passenger> GetByReservations(List<TourReservation> reservations) {
            _items = _serializer.FromCSV();
            return _items.FindAll(x => reservations.Any(y => y.Id == x.TourReservationId));
        }

        public bool DeleteMultiple(List<Passenger> passengers) {
            _items = _serializer.FromCSV();
            if(_items.RemoveAll(x => passengers.Select(y => y.Id).Contains(x.Id)) != passengers.Count)
                return false;
            _serializer.ToCSV(_items);
            return true;
        }
        
    }
}
