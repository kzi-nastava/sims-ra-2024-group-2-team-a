using BookingApp.Services;
using BookingApp.WPF.DTO;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BookingApp.WPF.Desktop.ViewModels {
    public class TouristFollowLiveViewModel : INotifyPropertyChanged {

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private readonly PointOfInterestService _pointOfInterestService = ServicesPool.GetService<PointOfInterestService>();
        private readonly PassengerService _passengerService = ServicesPool.GetService<PassengerService>();
        public TourDTO SelectedTour { get; set; }
        public int UserId { get; set; }

        private bool _isTouristPresent;
        public bool IsTouristPresent {
            get {
                return _isTouristPresent;
            }
            set {
                if(_isTouristPresent != value) {
                    _isTouristPresent = value;
                    OnPropertyChanged();
                }
            }
        }
        public ObservableCollection<PointOfInterestDTO> PointsOfInterest { get; set; }
        public ObservableCollection<PassengerDTO> Passengers { get; set; }
        public TouristFollowLiveViewModel(TourDTO selectedTour, int userId) {
            SelectedTour = selectedTour;
            UserId = userId;
            IsTouristPresent = _passengerService.IsTouristPresent(UserId, SelectedTour);
            PointsOfInterest = new ObservableCollection<PointOfInterestDTO>();
            Passengers = new ObservableCollection<PassengerDTO>();
            Update();
        }

        public void Update() {
            PointsOfInterest.Clear();
            foreach (var point in _pointOfInterestService.GetAllByTourId(SelectedTour.Id)) {
                PointsOfInterest.Add(new PointOfInterestDTO(point));
            }

            if(IsTouristPresent) {
                Passengers.Clear();
                foreach (var passenger in _passengerService.GetPresent(UserId, SelectedTour)) {
                    Passengers.Add(new PassengerDTO(passenger));
                }
            }
        }
    }
}
