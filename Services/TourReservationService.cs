using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.WPF.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookingApp.Services {
    public class TourReservationService {
        private readonly ITourReservationRepository _tourReservationRepository;

        private TourService _tourService;
        private PassengerService _passengerService;
        private VoucherService _voucherService;

        public TourReservationService(ITourReservationRepository tourReservationRepository){
            _tourReservationRepository = tourReservationRepository;
        }

        public void InjectServices(TourService tourService, PassengerService passengerService, VoucherService voucherService) {
            _tourService = tourService;
            _passengerService = passengerService;
            _voucherService = voucherService;
        }

        private void FillTourCapacity(int tourId, int addedPassengersNumber) {
            Tour selectedTour = _tourService.GetById(tourId);
            selectedTour.CurrentTouristNumber += addedPassengersNumber;
            _tourService.Update(selectedTour);
        }

        public int MakeReservation(int userId, TourDTO selectedTour, List<PassengerDTO> passengers, VoucherDTO selectedVoucher) {
            int availableSpace = _tourService.GetAvailableSpace(selectedTour);

            if (_tourService.GetAvailableSpace(selectedTour) == 0)
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
                _passengerService.Save(new Passenger(reservation.Id, passenger));
                addedPassengersNumber++;
            }
            return addedPassengersNumber;
        }

        private void UpdateVoucherStatus(VoucherDTO selectedVoucher) {
            if (selectedVoucher == null)
                return;
            Voucher newVoucher = _voucherService.GetById(selectedVoucher.Id);
            newVoucher.Used = selectedVoucher.Used;
            _voucherService.Update(newVoucher);
        }

        private List<TourReservation> GetByUserId(int userId) {
            return _tourReservationRepository.GetAll().Where(r => r.TouristId == userId).ToList();
        }

        public List<Tour> GetReservedTours(int userId, string filter) {
            List<Tour> tours = new List<Tour>();
           
            foreach(TourReservation reservation in GetByUserId(userId)) {
                var tour = _tourService.GetById(reservation.TourId);
                
                if(Enum.TryParse<TourState>(filter, out TourState state)) {
                    if (tour.State == state)
                        tours.Add(tour);
                    else
                        continue;
                }
                else {
                    tours.Add(tour);
                }
            }
                
            return tours;
        }
        public List<TourReservation> GetByTourId(int tourId) {
            return _tourReservationRepository.GetByTourId(tourId).ToList();
        }

        public List<TourReservation> GetByTours(List<Tour> tours) {
            return _tourReservationRepository.GetByTours(tours).ToList();
        }

        public TourReservation GetByTourAndTourist(int tourId, int touristId) {
            return _tourReservationRepository.GetByTourAndTourist(tourId, touristId);
        }
        public List<TourReservation> DeleteByTourId(int id) {
            List<TourReservation> reservations = GetByTourId(id);
            if (reservations == null)
                return new List<TourReservation>();
            if (_tourReservationRepository.DeleteMultiple(reservations))
                return reservations;
            return null;
        }
    }
}
