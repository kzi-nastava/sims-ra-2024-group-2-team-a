using BookingApp.Services;
using BookingApp.WPF.DTO;
using BookingApp.WPF.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookingApp.WPF.Web.Views {
    /// <summary>
    /// Interaction logic for ReviewsPage.xaml
    /// </summary>
    public partial class ReviewsPage : Page {

        public ObservableCollection<ReviewCardViewModel> ReviewCards { get; set; } = new ObservableCollection<ReviewCardViewModel>();

        private int _guestId;

        private readonly ReviewService _reviewService = ServicesPool.GetService<ReviewService>();

        public ReviewsPage(int guestId) {
            InitializeComponent();
            _guestId = guestId;

            var reviews = _reviewService.GetByGuestId(guestId).OrderByDescending(r => r.Id).ToList();

            foreach(var review in reviews) {
                AccommodationReviewDTO reviewDTO = new AccommodationReviewDTO(review);

                if (!reviewDTO.IsGradedByOwner && reviewDTO.IsGradedByGuest)
                    continue;

                ReviewCards.Add(new ReviewCardViewModel(reviewDTO));
            }

            DataContext = this;
        }
    }
}
