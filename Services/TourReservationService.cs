﻿using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services {
    public class TourReservationService {
        private readonly TourReservationRepository _tourReservationRepository = new TourReservationRepository();
        private readonly TourRepository _tourRepository = new TourRepository();
        private readonly PassengerRepository _passengerRepository = new PassengerRepository();
        private readonly VoucherRepository _voucherRepository = new VoucherRepository();

        private void FillTourCapacity(int tourId, int addedPassengersNumber) {
            TourRepository tourRepository = new TourRepository();
            Tour selectedTour = tourRepository.GetById(tourId);
            selectedTour.CurrentTouristNumber += addedPassengersNumber;
            tourRepository.Update(selectedTour);
        }

        public int MakeReservation(int userId, TourDTO selectedTour, List<PassengerDTO> passengers, VoucherDTO selectedVoucher) {
            int availableSpace = _tourRepository.GetAvailableSpace(selectedTour);

            if (_tourRepository.GetAvailableSpace(selectedTour) == 0)
                return -1;

            if (passengers.Count > availableSpace)
                return availableSpace;

            TourReservation reservation = new TourReservation(userId, selectedTour.Id);
            _tourReservationRepository.Save(reservation);
            FillTourCapacity(selectedTour.Id, UpdatePassengerNumber(reservation, passengers));
            UpdateVoucherStatus(selectedVoucher);

            return 0;
        }

        private int UpdatePassengerNumber(TourReservation reservation, List<PassengerDTO> passengers) {
            int addedPassengersNumber = 0;
            foreach (var passenger in passengers) {
                _passengerRepository.Save(new Passenger(reservation.Id, passenger));
                addedPassengersNumber++;
            }
            return addedPassengersNumber;
        }

        private void UpdateVoucherStatus(VoucherDTO selectedVoucher) {
            Voucher newVoucher = _voucherRepository.GetById(selectedVoucher.Id);
            newVoucher.Used = selectedVoucher.Used;
            _voucherRepository.Update(newVoucher);
        }

        private List<TourReservation> GetByUserId(int userId) {
            return _tourReservationRepository.GetAll().Where(r => r.TouristId == userId).ToList();
        }

        public List<Tour> GetReservedTours(int userId) {
            List<Tour> tours = new List<Tour>();
           
            foreach(TourReservation reservation in GetByUserId(userId)) 
                tours.Add(_tourRepository.GetById(reservation.TourId));
            
            return tours;
        }
    }
}