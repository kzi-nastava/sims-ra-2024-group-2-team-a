using BookingApp.Model;
using BookingApp.Serializer;
using System.Collections.Generic;
using System.Linq;

namespace BookingApp.Repository {
    public class PassengerRepository : Repository<Passenger> {
        public List<Passenger> GetUnJoined(int tourId) {
            _items = _serializer.FromCSV();
            return _items.FindAll(x => x.TourReservationId == tourId && x.JoinedPointOfInterestId == -1);
        }
    }
}
