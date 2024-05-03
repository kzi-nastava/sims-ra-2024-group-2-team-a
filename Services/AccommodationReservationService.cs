﻿using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.WPF.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookingApp.Services {
    public class AccommodationReservationService
    {
        private readonly IAccommodationReservationRepository _reservationRepository;

        private readonly RescheduleRequestService _rescheduleService;
        private readonly AccommodationService _accommodationService;
        private readonly ReservationRecommenderService _recommenderService;
        private readonly AccommodationStatisticsService _statisticService;
        private readonly GuestService _guestService;
      
        public AccommodationReservationService(IAccommodationReservationRepository reservationRepository,
            RescheduleRequestService rescheduleService, AccommodationService accommodationService, ReservationRecommenderService recommenderService,
            AccommodationStatisticsService statisticService, GuestService guestService) {
            
            _reservationRepository = reservationRepository;
            _rescheduleService = rescheduleService;
            _accommodationService = accommodationService;
            _recommenderService = recommenderService;
            _statisticService = statisticService;
            _guestService = guestService;
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


            _statisticService.UpdateReservationStatisticsAndCheckDates(reservation.AccommodationId, reservation.StartDate, reservation.EndDate);
          
            AccommodationReservation newReservation = _reservationRepository.Save(reservation);

            int reservationsCount = _reservationRepository.CountReservationsInLastYear(newReservation.GuestId);
            _guestService.PromoteOrDecreaseBonusPoints(newReservation.GuestId, reservationsCount);

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

            _statisticService.UpdateCancellationStatisticsAndCheckDates(reservation.AccommodationId, reservation.StartDate, reservation.EndDate);
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


        // TODO: Move to another service
        public int CheckForNotGradedReservations(int ownerId) {
            ReviewService reviewService = ServicesPool.GetService<ReviewService>();
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
