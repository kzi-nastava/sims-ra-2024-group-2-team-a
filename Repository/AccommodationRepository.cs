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

            return filter.MatchesName(accommodation.Name) 
                && filter.MatchesLocation(accommodation.LocationId)
                && filter.MatchesType(accommodation.Type) 
                && filter.MatchesGuestNumber(accommodation.MaxGuestNumber) 
                && filter.MatchesReservationDays(accommodation.MinReservationDays);
        }
    }
}