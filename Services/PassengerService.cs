using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Repository;
using System.Collections.Generic;
using System.Linq;

namespace BookingApp.Services {
    public class PassengerService {
        private readonly PassengerRepository _passengerRepository = new PassengerRepository();
        private readonly TourRepository _tourRepository = new TourRepository();
        private readonly TourReservationRepository _tourReservationRepository = new TourReservationRepository();

        public List<int> GetAttendance(List<TourReservation> reservations) {
            List<Passenger> passengers = GetByReservations(reservations);
            List<int> attendances = new List<int>();
            attendances.Add(_passengerRepository.GetStatsTeen(passengers));
            attendances.Add(_passengerRepository.GetStatsMid(passengers));
            attendances.Add(_passengerRepository.GetStatsOld(passengers));
            return attendances;
        }
        public List<Passenger> GetByReservations(List<TourReservation> reservations) {
            return _passengerRepository.GetByReservations(reservations);
        }
        public bool DeleteByReservations(List<TourReservation> reservations) {
            return _passengerRepository.DeleteMultiple(GetByReservations(reservations));
        }
        public Passenger? GetByReservationAndTourist(int reservationId, int touristId) {
            return _passengerRepository.GetByReservationAndTourist(reservationId, touristId);
        }
        public List<Passenger> GetUnJoined(List<TourReservation> reservations) {
            return _passengerRepository.GetUnJoined(reservations);
        }
        public bool Update(Passenger? passenger) {
            return _passengerRepository.Update(passenger);
        }

        public List<Passenger> GetPresent(int touristId, TourDTO tour) {
            return _passengerRepository.GetAll().Where(r => r.TourReservationId == _tourReservationRepository.GetByTourAndTourist(tour.Id, touristId).Id && r.JoinedPointOfInterestId != -1).ToList();
        }
    }
}
