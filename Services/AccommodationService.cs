using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.WPF.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookingApp.Services {
    public class AccommodationService
    {
        private readonly IAccommodationRepository _accommodationRepository;

        private ReservationRecommenderService _reservationRecommenderService;

        public AccommodationService(IAccommodationRepository accommodationRepository) { 
            _accommodationRepository = accommodationRepository;
        }

        public void InjectServices(ReservationRecommenderService reservationRecommenderService) {
            _reservationRecommenderService = reservationRecommenderService;
        }

        public List<Accommodation> GetAll() {
            return _accommodationRepository.GetAll().Where(x => !x.IsClosed).ToList();
        }

        public Accommodation GetById(int id) {
            return _accommodationRepository.GetById(id);
        }

        public Accommodation Save(Accommodation accommodation) {
            return _accommodationRepository.Save(accommodation);
        }

        public List<Accommodation> GetByOwnerId(int ownerId) {
            return _accommodationRepository.GetByOwnerId(ownerId).Where(x => !x.IsClosed).ToList();
        }

        public List<Accommodation> GetFilteredAccommodations(AccommodationFilterDTO filter) {
            var accommodations = this.GetAll();

            return accommodations.Where(a => SatisfiesFilter(a, filter)).ToList();
        }

        public List<Accommodation> GetAvailableAccommodationsInDateRange(AccommodationFilterDTO accFilter, AccommodationReservationFilterDTO resFilter) {
            var accommodations = this.GetFilteredAccommodations(accFilter);

            DateOnly emptyDate = new DateOnly();

            if(resFilter.StartDate == emptyDate || resFilter.EndDate == emptyDate)
                return accommodations;

            List<Accommodation> filteredAccomodations = new List<Accommodation>();

            foreach(var acc in accommodations) {
                resFilter.AccommodationId = acc.Id;
                int count = _reservationRecommenderService.SuggestReservations(resFilter, false).Count;
                if (count > 0)
                    filteredAccomodations.Add(acc);
            }

            return filteredAccomodations;
        }

        private bool SatisfiesFilter(Accommodation accommodation, AccommodationFilterDTO filter) {

            return filter.MatchesName(accommodation.Name)
                && filter.MatchesLocation(accommodation.LocationId)
                && filter.MatchesType(accommodation.Type)
                && filter.MatchesGuestNumber(accommodation.MaxGuestNumber)
                && filter.MatchesReservationDays(accommodation.MinReservationDays);
        }

        public List<Accommodation> GetByLocationId(int locationId) {
            return this.GetAll().Where(x => x.LocationId == locationId).ToList();
        }

        public List<Accommodation> GetByLocationAndOwnerId(int locationId, int ownerId) {
            return this.GetByOwnerId(ownerId).Where(x => x.LocationId == locationId).ToList();
        }

        public void CloseAccommodation(int accommodationId) {
            Accommodation accommodation = this.GetById(accommodationId);

            accommodation.IsClosed = true;

            _accommodationRepository.Update(accommodation);
        }
    }
}
