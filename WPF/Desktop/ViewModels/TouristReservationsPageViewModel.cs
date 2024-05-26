using BookingApp.Services;
using BookingApp.WPF.DTO;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BookingApp.WPF.Desktop.ViewModels {
    public class TouristReservationsPageViewModel : INotifyPropertyChanged {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private readonly TourReservationService _tourReservationService = ServicesPool.GetService<TourReservationService>();
        private readonly TourService _tourService = ServicesPool.GetService<TourService>();
        public int UserId { get; set; }
        public ObservableCollection<TourDTO> ReservedTours { get; set; }

        private string _filter;
        public string Filter {
            get {
                return _filter;
            }
            set {
                if (value != _filter) {
                    _filter = value;
                    OnPropertyChanged();
                    Update();
                }
            }
        }
        public TouristReservationsPageViewModel(int userId) { 
            UserId = userId;
            ReservedTours = new ObservableCollection<TourDTO>();
            Filter = "All";
        }

        public void Update() {
            ReservedTours.Clear();
            foreach (var tour in _tourReservationService.GetReservedTours(UserId, Filter)) {
                ReservedTours.Add(new TourDTO(tour));
            }
        }

        public bool IsRatingAvailable(TourDTO tour) {
            return _tourService.WasTouristPresent(UserId, tour) && !_tourService.IsTourReviewedForUser(UserId, tour);
        }
    }
}
