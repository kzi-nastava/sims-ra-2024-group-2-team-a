using BookingApp.Domain.Model;
using BookingApp.Services;
using BookingApp.WPF.DTO;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace BookingApp.WPF.Tablet.ViewModels {
    public class PassengerWindowViewModel {
        private int _tourId;
        private int _pointOfInterestId;

        private readonly TourReservationService _tourReservationService = ServicesPool.GetService<TourReservationService>();
        private readonly PassengerService _passengerService = ServicesPool.GetService<PassengerService>();
        private readonly NotificationService _notificationService = ServicesPool.GetService<NotificationService>();
        private readonly VisitedTourService _visitedTourService = ServicesPool.GetService<VisitedTourService>();

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
                    if(passengerDTO.UserId != -1) {
                        _notificationService.SendTouristNotification(NotificationCategory.TourActive, passengerDTO.UserId, _tourId);
                        _visitedTourService.Save(passengerDTO.UserId, _tourId);
                    }     
                }
                _passengerService.Update(passengerDTO.ToModel());
            }
        }
    }
}
