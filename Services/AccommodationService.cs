using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services
{
    class AccommodationService
    {
        private readonly AccommodationRepository _accommodationRepository = new AccommodationRepository();

        public List<Accommodation> GetAll() {
            return _accommodationRepository.GetAll();
        }

        public Accommodation Save(Accommodation accommodation) {
            return _accommodationRepository.Save(accommodation);
        }

        public List<Accommodation> GetByOwnerId(int ownerId) {
            return _accommodationRepository.GetByOwnerId(ownerId);
        }

        public List<Accommodation> GetFilteredAccommodations(AccommodationFilterDTO filter) {
            var accommodations = _accommodationRepository.GetAll();

            return accommodations.Where(a => SatisfiesFilter(a, filter)).ToList();
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
