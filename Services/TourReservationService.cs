using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.WPF.DTO;
using System.Collections.Generic;
using System.Linq;

namespace BookingApp.Services {
    public class TourReservationService {
        private readonly ITourReservationRepository _tourReservationRepository;
        private readonly ITourRepository _tourRepository = RepositoryInjector.GetInstance<ITourRepository>();
        private readonly IPassengerRepository _passengerRepository = RepositoryInjector.GetInstance<IPassengerRepository>();
        private readonly IVoucherRepository _voucherRepository = RepositoryInjector.GetInstance<IVoucherRepository>();

        public TourReservationService(ITourReservationRepository tourReservationRepository){
            _tourReservationRepository = tourReservationRepository;
        }

        private void FillTourCapacity(int tourId, int addedPassengersNumber) {
            Tour selectedTour = _tourRepository.GetById(tourId);
            selectedTour.CurrentTouristNumber += addedPassengersNumber;
            _tourRepository.Update(selectedTour);
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
            if (selectedVoucher == null)
                return;
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
        public List<TourReservation> GetByTourId(int tourId) {
            return _tourReservationRepository.GetByTourId(tourId).ToList();
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
