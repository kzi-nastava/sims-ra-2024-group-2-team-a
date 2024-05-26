using BookingApp.WPF.Desktop.ViewModels;
using BookingApp.WPF.DTO;
using System.Windows;
using System.Windows.Controls;

namespace BookingApp.WPF.Desktop.Views {
    /// <summary>
    /// Interaction logic for TouristReservationsPage.xaml
    /// </summary>
    public partial class TouristReservationsPage : Page {
        public int UserId { get; set; }
        public TouristReservationsPageViewModel viewModel { get; set; }
        public TouristReservationsPage(int userId) {
            InitializeComponent();
            viewModel = new TouristReservationsPageViewModel(userId);
            DataContext = viewModel;
            UserId = userId;
        }

        private void RateTourButton_Click(object sender, RoutedEventArgs e) {
            var button = (Button)sender;
            var tour = (TourDTO)button.DataContext;

            if (viewModel.IsRatingAvailable(tour)) {
                TourRatingWindow ratingWindow = new TourRatingWindow(tour, UserId);
                ratingWindow.ShowDialog();
            }
            else {
                MessageBox.Show("You cannot rate a tour you haven't been present to or have reviewed already!", "Invalid request", MessageBoxButton.OK);
            }
        }

        private void FollowLiveButton_Click(object sender, RoutedEventArgs e) {
            var button = (Button)sender;
            var tour = (TourDTO)button.DataContext;

            TouristFollowLiveWindow followLiveWindow = new TouristFollowLiveWindow(tour, UserId);
            followLiveWindow.ShowDialog();
        }
    }
}
