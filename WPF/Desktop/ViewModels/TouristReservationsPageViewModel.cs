using BookingApp.Services;
using BookingApp.WPF.DTO;
using System.Collections.ObjectModel;

namespace BookingApp.WPF.Desktop.ViewModels {
    public class TouristReservationsPageViewModel {
        private readonly TourReservationService _tourReservationService = ServicesPool.GetService<TourReservationService>();
        private readonly TourService _tourService = ServicesPool.GetService<TourService>();
        public int UserId { get; set; }
        public ObservableCollection<TourDTO> ReservedTours { get; set; }
        public TouristReservationsPageViewModel(int userId) { 
            UserId = userId;
            ReservedTours = new ObservableCollection<TourDTO>();
            Update();
        }

        public void Update() {
            ReservedTours.Clear();
            foreach (var tour in _tourReservationService.GetReservedTours(UserId)) {
                ReservedTours.Add(new TourDTO(tour));
            }
        }

        public bool IsRatingAvailable(TourDTO tour) {
            return _tourService.WasTouristPresent(UserId, tour) && !_tourService.IsTourReviewedForUser(UserId, tour);
        }
    }
}
