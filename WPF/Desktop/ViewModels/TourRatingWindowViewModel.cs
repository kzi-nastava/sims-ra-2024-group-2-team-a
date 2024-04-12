using BookingApp.DTO;
using BookingApp.Repository;
using BookingApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.Desktop.ViewModels
{
    public class TourRatingWindowViewModel
    {
        private readonly TourReviewService _tourReviewService = new TourReviewService();
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
