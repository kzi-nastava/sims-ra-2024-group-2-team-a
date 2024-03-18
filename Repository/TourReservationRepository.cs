using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Serializer;
using System.Collections.Generic;
using System.Linq;

namespace BookingApp.Repository {
    public class TourReservationRepository : Repository<TourReservation> {

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

        public int MakeReservation(int userId, TourDTO selectedTour, List<PassengerDTO> passengers) {
            PassengerRepository passengerRepository = new PassengerRepository();
            TourRepository tourRepository = new TourRepository();

            int addedPassengersNumber = 0;
            int availableSpace = tourRepository.GetAvailableSpace(selectedTour);

            if (tourRepository.GetAvailableSpace(selectedTour) == 0)
                return -1;

            if (passengers.Count > availableSpace)
                return availableSpace;

            TourReservation reservation = new TourReservation(userId, selectedTour.Id);
            Save(reservation);
            foreach (var passenger in passengers) {
                passengerRepository.Save(new Passenger(reservation.Id, passenger));
                addedPassengersNumber++;
            }

            FillTourCapacity(selectedTour.Id, addedPassengersNumber);

            return 0;
        }
    }
}