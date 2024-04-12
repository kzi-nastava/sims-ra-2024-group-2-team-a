using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Serializer;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Documents;

namespace BookingApp.Repository {
    public class TourReservationRepository : Repository<TourReservation> {
        private readonly VoucherRepository _voucherRepository = new VoucherRepository();

        private void FillTourCapacity(int tourId, int addedPassengersNumber) {
            TourRepository tourRepository = new TourRepository();
            Tour selectedTour = tourRepository.GetById(tourId);
            selectedTour.CurrentTouristNumber += addedPassengersNumber;
            tourRepository.Update(selectedTour);
        }

        public List<TourReservation> GetByTourId(int tourId) {
            _items = _serializer.FromCSV();
            return _items.FindAll(x => x.TourId == tourId);
        }

        private int UpdatePassengerNumber(TourReservation reservation, List<PassengerDTO> passengers, PassengerRepository passengerRepository) {
            int addedPassengersNumber = 0;
            foreach (var passenger in passengers) {
                passengerRepository.Save(new Passenger(reservation.Id, passenger));
                addedPassengersNumber++;
            }
            return addedPassengersNumber;
        }

        private void UpdateVoucherStatus(VoucherDTO selectedVoucher) {
            Voucher newVoucher = _voucherRepository.GetById(selectedVoucher.Id);
            newVoucher.Used = selectedVoucher.Used;
            _voucherRepository.Update(newVoucher);
        }

        public int MakeReservation(int userId, TourDTO selectedTour, List<PassengerDTO> passengers, VoucherDTO selectedVoucher) {
            PassengerRepository passengerRepository = new PassengerRepository();
            TourRepository tourRepository = new TourRepository();

            int availableSpace = tourRepository.GetAvailableSpace(selectedTour);

            if (tourRepository.GetAvailableSpace(selectedTour) == 0)
                return -1;

            if (passengers.Count > availableSpace)
                return availableSpace;

            TourReservation reservation = new TourReservation(userId, selectedTour.Id);
            Save(reservation);
            FillTourCapacity(selectedTour.Id, UpdatePassengerNumber(reservation, passengers, passengerRepository));
            UpdateVoucherStatus(selectedVoucher);

            return 0;
        }
        public List<TourReservation> DeleteByTourId(int id) {
            List<TourReservation> reservations = GetByTourId(id);
            if (reservations == null)
                return new List<TourReservation>();
            if(DeleteMultiple(reservations))
                return reservations;
            return null;
        }
        public bool DeleteMultiple(List<TourReservation> reservations) {
            _items = _serializer.FromCSV();
            if (_items.RemoveAll(x => reservations.Select(y => y.Id).Contains(x.Id)) != reservations.Count)
                    return false;
            _serializer.ToCSV(_items);
            return true;
        }
    }
}