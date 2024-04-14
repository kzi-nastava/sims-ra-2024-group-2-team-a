using BookingApp.Model;
using BookingApp.Services;
using BookingApp.WPF.DTO;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace BookingApp.WPF.Tablet.ViewModels {
    public class PassengerWindowViewModel {
        private int _tourId;
        private int _pointOfInterestId;

        private readonly TourReservationService _tourReservationService = new TourReservationService();
        private readonly PassengerService _passengerService = new PassengerService();
        private readonly NotificationService _notificationService = new NotificationService();

        public ObservableCollection<PassengerDTO> passengerDTOs { get; set; }
        public List<PassengerDTO> joinedPassengerDTOs { get; set; }
        public PassengerWindowViewModel(int tourId, int pointOfInterestId) {
            _tourId = tourId;
            _pointOfInterestId = pointOfInterestId;
            Load();
        }
        private void Load() {
            passengerDTOs = new ObservableCollection<PassengerDTO>();
            joinedPassengerDTOs = new List<PassengerDTO>();
            List<TourReservation> reservations = _tourReservationService.GetByTourId(_tourId);
            foreach (var passenger in _passengerService.GetUnJoined(reservations)) {
                passengerDTOs.Add(new PassengerDTO(passenger));
            }
        }
        public void JoinPassengers() {
            foreach (var passengerDTO in passengerDTOs) {
                if (passengerDTO.IsJoined) {
                    passengerDTO.JoinedPointOfInterestId = _pointOfInterestId;
                    _notificationService.SendTouristNotification(NotificationCategory.TourActive, passengerDTO.UserId, _tourId);
                }
                _passengerService.Update(passengerDTO.ToModel());
            }
        }
    }
}
