using BookingApp.Services;
using BookingApp.WPF.DTO;

namespace BookingApp.WPF.Desktop.ViewModels {
    public class TourRatingWindowViewModel
    {
        private readonly TourReviewService _tourReviewService = ServicesPool.GetService<TourReviewService>();
        public int UserId { get; set; }
        public TourDTO SelectedTour { get; set; }
        public TourReviewDTO TourReview { get; set; }
        public TourRatingWindowViewModel(TourDTO selectedTour, int userId) { 
            SelectedTour = selectedTour;
            UserId = userId;
            TourReview = new TourReviewDTO();
        }

        public void SendReview() {
            _tourReviewService.SendReview(SelectedTour, UserId, TourReview);
        }
    }
}
