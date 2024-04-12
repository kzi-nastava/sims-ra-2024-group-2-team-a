using BookingApp.DTO;
using BookingApp.WPF.Desktop.ViewModels;
using System;
using System.Collections.Generic;
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
            // TODO
        }
    }
}
