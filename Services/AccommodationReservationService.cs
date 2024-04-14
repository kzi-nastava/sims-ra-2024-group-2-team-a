using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.WPF.DTO;
using System;
using System.Collections.Generic;

namespace BookingApp.Services {
    class AccommodationReservationService
    {
        private readonly IAccommodationReservationRepository _reservationRepository = RepositoryInjector.GetInstance<IAccommodationReservationRepository>();

        private readonly RescheduleRequestService _rescheduleService = new RescheduleRequestService();
        private readonly AccommodationService _accommodationService = new AccommodationService();
        private readonly ReservationRecommenderService _recommenderService = new ReservationRecommenderService();

        public List<AccommodationReservation> GetByAccommodationId(int id) {
            return _reservationRepository.GetByAccommodationId(id);
        }

        public AccommodationReservation GetById(int id) {
            return _reservationRepository.GetById(id);
        }

        public List<AccommodationReservation> GetAll() {
            return _reservationRepository.GetAll();
        }

        public List<AccommodationReservation> GetByGuestId(int id) {
            return _reservationRepository.GetByGuestId(id);
        }

        public AccommodationReservation Save(AccommodationReservation reservation) {
            return _reservationRepository.Save(reservation);
        }

        public void CancelReservation(int id) {
            var reservation = _reservationRepository.GetById(id);
            reservation.Cancelled = true;
            _reservationRepository.Update(reservation);

            _rescheduleService.CancelByReservationId(id);
        }

        public bool Update(AccommodationReservation accReservation) {
            return _reservationRepository.Update(accReservation);  
        }

        public List<AccommodationReservation> SuggestReservations(AccommodationReservationDTO rDTO) {
            return _recommenderService.SuggestReservations(rDTO);
        }

        public bool CheckAccommodationAvailability(int accommodationId, DateOnly startDate, DateOnly endDate) {
            foreach (var r in this.GetByAccommodationId(accommodationId)) {
                if (!(r.StartDate >= endDate || startDate >= r.EndDate))
                    return false;
            }

            return true;
        }

        public int CheckForNotGradedReservations(int ownerId) {
            ReviewService reviewService = new ReviewService();
            int counter = 0;

            foreach (var reservation in this.GetAll()) {
                if (!CheckReservationOwner(reservation.AccommodationId, ownerId))
                    continue;

                if (reviewService.GetByReservationId(reservation.Id) == null && CheckReservationDate(reservation))
                    counter++;
            }

            return counter;
        }

        private bool CheckReservationDate(AccommodationReservation reservation) {
            DateOnly currentDate = DateOnly.FromDateTime(DateTime.Now);
            DateOnly offsetDate = reservation.EndDate;
            offsetDate = offsetDate.AddDays(5);

            return (currentDate > reservation.EndDate && currentDate < offsetDate);
        }

        private bool CheckReservationOwner(int accommodationId, int ownerId) {
            return _accommodationService.GetById(accommodationId).OwnerId == ownerId;
        }

    }
}
