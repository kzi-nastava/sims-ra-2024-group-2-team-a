using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.WPF.DTO;
using System.Collections.Generic;
using System.Linq;

namespace BookingApp.Services {
    class AccommodationService
    {
        private readonly IAccommodationRepository _accommodationRepository = RepositoryInjector.GetInstance<IAccommodationRepository>();

        public List<Accommodation> GetAll() {
            return _accommodationRepository.GetAll();
        }

        public Accommodation GetById(int id) {
            return _accommodationRepository.GetById(id);
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
