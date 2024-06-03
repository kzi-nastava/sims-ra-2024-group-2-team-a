using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.WPF.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookingApp.Services {
    public class AccommodationReservationService
    {
        private readonly IAccommodationReservationRepository _reservationRepository;

        private AccommodationRescheduleRequestService _rescheduleService;
        private AccommodationService _accommodationService;
        private ReservationRecommenderService _recommenderService;
        private AccommodationStatisticsService _statisticService;
        private GuestService _guestService;
        private AccommodationReviewService _reviewService;
      
        public AccommodationReservationService(IAccommodationReservationRepository reservationRepository) {
           _reservationRepository = reservationRepository;
        }

        public void InjectServices(AccommodationRescheduleRequestService rescheduleService, AccommodationService accommodationService, ReservationRecommenderService recommenderService,
                       AccommodationStatisticsService statisticService, GuestService guestService, AccommodationReviewService reviewService) {
            _rescheduleService = rescheduleService;
            _accommodationService = accommodationService;
            _recommenderService = recommenderService;
            _statisticService = statisticService;
            _guestService = guestService;
            _reviewService = reviewService;
        }

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
          
            AccommodationReservation newReservation = _reservationRepository.Save(reservation);

            int reservationsCount = _reservationRepository.CountReservationsInLastYear(newReservation.GuestId);
            _guestService.ManageGuestStatus(newReservation.GuestId, reservationsCount);

            _statisticService.UpdateReservationStatisticsAndCheckDates(newReservation.AccommodationId, reservation.StartDate, reservation.EndDate, reservation.GuestsNumber);

            return newReservation;
        }

        public AccommodationReservation GetOldestReservation(int accommodationId) {
            return this.GetByAccommodationId(accommodationId).OrderBy(x => x.StartDate).FirstOrDefault();
        }

        public AccommodationReservation GetNewestReservation(int accId) {
            return this.GetByAccommodationId(accId).OrderByDescending(x => x.StartDate).FirstOrDefault();
        }

        public void CancelReservation(int id) {

            var reservation = _reservationRepository.GetById(id);
            reservation.Cancelled = true;
            _reservationRepository.Update(reservation);

            _rescheduleService.CancelByReservationId(id);

            _statisticService.UpdateCancellationStatisticsAndCheckDates(reservation.AccommodationId, reservation.StartDate, reservation.EndDate, reservation.GuestsNumber);
        }

        public bool Update(AccommodationReservation accReservation) {
            return _reservationRepository.Update(accReservation);  
        }

        public List<AccommodationReservation> SuggestReservations(AccommodationReservationFilterDTO rDTO) {
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
            int counter = 0;

            foreach (var reservation in this.GetAll()) {
                if (!CheckReservationOwner(reservation.AccommodationId, ownerId))
                    continue;

                if (_reviewService.GetByReservationId(reservation.Id) == null && CheckReservationDate(reservation))
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

        public bool WasVisitedByGuest(int guestId, DateTime dateTime, int locationId) {
            foreach (var res in this.GetByGuestId(guestId)) {
                Accommodation acc = _accommodationService.GetById(res.AccommodationId);

                if (res.Cancelled == false && 
                    res.StartDate <= DateOnly.FromDateTime(dateTime) && 
                    acc.LocationId == locationId) {

                        return true;
                }

            }

            return false;
        }
    }
}
