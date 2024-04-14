using BookingApp.Domain.Model;
using BookingApp.Services;
using BookingApp.WPF.DTO;

namespace BookingApp.WPF.Tablet.ViewModels {
    public class MenuBarViewModel {
        private int _userId;
        private readonly TourService _tourService = new TourService();
        public TourDTO tourDTO { get; set; }

        public MenuBarViewModel(int userId) {
            _userId = userId;
        }
        public bool IsTourActive() {
            Tour tour = _tourService.GetActive(_userId);
            if (tour == null) 
                return false;

            tourDTO = new TourDTO(tour);
            return true;
        }
    }
}
