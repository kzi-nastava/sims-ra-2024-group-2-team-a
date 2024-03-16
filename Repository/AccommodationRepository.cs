using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BookingApp.Repository {
    public class AccommodationRepository : Repository<Accommodation> {

        public List<Accommodation> GetByOwnerId(int ownerId) {
            _items = _serializer.FromCSV();

            return _items.Where(a => a.OwnerId == ownerId).ToList();
        }

        public List<Accommodation> GetFilteredAccommodations(AccommodationFilterDTO filter) {
            _items = _serializer.FromCSV();

            return _items.Where(a => SatisfiesFilter(a, filter)).ToList();
        }

        private bool SatisfiesFilter(Accommodation accommodation, AccommodationFilterDTO filter) {

            bool matchesName = accommodation.Name.ToLower().Contains(filter.Name.ToLower()) || filter.Name == "";
            bool matchesLocation = accommodation.LocationId == filter.Location.Id || filter.Location.Id == -1;
            bool matchesType = accommodation.type == filter.Type || filter.Type == AccommodationType.any;
            bool matchesGuestNumber = accommodation.MaxGuestNumber >= filter.GuestNumber || filter.GuestNumber <= 0;
            bool matchesReservationDays = accommodation.MinReservationDays <= filter.ReservationDays || filter.ReservationDays <= 0;

            return matchesGuestNumber && matchesLocation && matchesName && matchesReservationDays && matchesType;
        }
    }
}