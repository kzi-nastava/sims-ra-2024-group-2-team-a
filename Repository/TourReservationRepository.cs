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

        public int MakeReservation(int userId, TourDTO selectedTour, List<PassengerDTO> passengers) {
            PassengerRepository passengerRepository = new PassengerRepository();

            int availableSpace = selectedTour.MaxTouristNumber - selectedTour.CurrentTouristNumber;
            int addedPassengersNumber = 0;

            if (availableSpace == 0)
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