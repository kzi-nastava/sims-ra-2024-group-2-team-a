using BookingApp.Model;
using BookingApp.Serializer;
using System.Collections.Generic;
using System.Linq;

namespace BookingApp.Repository {
    public class PassengerRepository : Repository<Passenger> {
        public List<Passenger> GetUnJoined(List<TourReservation> reservations) {
            _items = _serializer.FromCSV();
            List<int> ids = new List<int>();
            foreach (var reservatuon in reservations) {
                ids.Add(reservatuon.Id);
            }
            return _items.FindAll(x => ids.Contains(x.TourReservationId) && x.JoinedPointOfInterestId == -1);
        }
    }
}
