using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Serializer;
using System.Collections.Generic;
using System.Linq;

namespace BookingApp.Repository {
    public class TourReservationRepository : IRepository<TourReservation> {

        private readonly Serializer<TourReservation> _serializer;

        private List<TourReservation> _tourReservations;

        public TourReservationRepository() {
            _serializer = new Serializer<TourReservation>();
            _tourReservations = _serializer.FromCSV();
        }

        public List<TourReservation> GetAll() {
            return _serializer.FromCSV();
        }

        public TourReservation Save(TourReservation tourReservation) {
            tourReservation.Id = NextId();
            _tourReservations = _serializer.FromCSV();
            _tourReservations.Add(tourReservation);
            _serializer.ToCSV(_tourReservations);
            return tourReservation;
        }

        public bool Delete(TourReservation tourReservation) {
            _tourReservations = _serializer.FromCSV();
            TourReservation? found = _tourReservations.Find(c => c.Id == tourReservation.Id);
            if (found == null) {
                return false;
            }
            _tourReservations.Remove(found);
            _serializer.ToCSV(_tourReservations);
            return true;
        }

        public bool Update(TourReservation tourReservation) {
            _tourReservations = _serializer.FromCSV();
            TourReservation? current = _tourReservations.Find(c => c.Id == tourReservation.Id);
            int index = _tourReservations.IndexOf(current);
            if (current == null) {
                return false;
            }
            _tourReservations.Remove(current);
            _tourReservations.Insert(index, tourReservation);       // keep ascending order of ids in file 
            _serializer.ToCSV(_tourReservations);
            return true;
        }

        public TourReservation GetById(int id) {
            return _tourReservations.Find(x => x.Id == id);
        }

        public int NextId() {
            _tourReservations = _serializer.FromCSV();
            if (_tourReservations.Count < 1) {
                return 1;
            }
            return _tourReservations.Max(c => c.Id) + 1;
        }

        private void FillTourCapacity(int tourId, int addedPassengersNumber) {
            TourRepository tourRepository = new TourRepository();
            Tour selectedTour = tourRepository.GetById(tourId);
            selectedTour.CurrentTouristNumber += addedPassengersNumber;
            tourRepository.Update(selectedTour);
        }

        public int MakeReservation(int userId, TourDTO selectedTour, List<PassengerDTO> passengers) {
            PassengerRepository passengerRepository = new PassengerRepository();
            UserRepository userRepository = new UserRepository();

            int availableSpace = selectedTour.MaxTouristNumber - selectedTour.CurrentTouristNumber;
            int addedPassengersNumber = 0;

            if (availableSpace == 0)
                return -1;

            if (passengers.Count > availableSpace)
                return 0;

            Save(new TourReservation(userId, selectedTour.Id));
            foreach (var passenger in passengers) {
                passengerRepository.Save(new Passenger(selectedTour.Id, passenger.Name, passenger.Surname, passenger.Age, userId));
                addedPassengersNumber++;
            }

            FillTourCapacity(selectedTour.Id, addedPassengersNumber);

            return 1;
        }
    }
}