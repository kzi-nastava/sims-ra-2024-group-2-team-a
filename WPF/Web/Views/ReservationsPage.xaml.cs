using BookingApp.WPF.DTO;
using BookingApp.WPF.Web.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace BookingApp.WPF.Web.Views {
    /// <summary>
    /// Interaction logic for ReservationsPage.xaml
    /// </summary>
    public partial class ReservationsPage : Page {

        public ReservationsPageViewModel ViewModel { get; set; }

        public ReservationsPage(int guestId) {
            InitializeComponent();
            ViewModel = new ReservationsPageViewModel(guestId);
            DataContext = ViewModel;
        }

        public void Update() {
            ViewModel.UpdateAllCollections();
        }

        private void ButtonScheduledClick(object sender, RoutedEventArgs e) {
            ViewModel.FilterBySelection(true);
        }

        private void ButtonExpiredClick(object sender, RoutedEventArgs e) {
            ViewModel.FilterBySelection(false);
        }

        public void OpenRescheduleDialog(AccommodationReservationDTO reservation) {
            rectBlurBackground.Visibility = Visibility.Visible;
            mainGrid.Children.Add(new RescheduleReservationModalDialog(this, reservation));
        }

        public void OpenReviewDialog(AccommodationReservationDTO reservation) {
            rectBlurBackground.Visibility = Visibility.Visible;
            mainGrid.Children.Add(new ReviewAccommodationModalDialog(this, reservation));
        }

        public void CloseModalDialog() {
            rectBlurBackground.Visibility = Visibility.Hidden;
            mainGrid.Children.RemoveAt(mainGrid.Children.Count - 1);
        }

    }
}
