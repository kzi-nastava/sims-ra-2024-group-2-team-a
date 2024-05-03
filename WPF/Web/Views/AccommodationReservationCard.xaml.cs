using BookingApp.Services;
using BookingApp.WPF.DTO;
using BookingApp.WPF.Web.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace BookingApp.WPF.Web.Views {
    /// <summary>
    /// Interaction logic for AccommodationReservationCard.xaml
    /// </summary>
    public partial class AccommodationReservationCard : UserControl {

        private ReservationsPage _parentPage;
        public AccommodationReservationCardViewModel ViewModel { get; set; }

        public AccommodationReservationCard() {
            InitializeComponent();
        }

        private void UserControlLoaded(object sender, RoutedEventArgs e) {
            if (DataContext is AccommodationReservationCardViewModel)
                return;

            GuestMainWindow window = (GuestMainWindow)Window.GetWindow(this);
            _parentPage = window.MainFrame.Content as ReservationsPage;

            AccommodationReservationDTO reservation= DataContext as AccommodationReservationDTO;
            ViewModel = new AccommodationReservationCardViewModel(reservation);
            DataContext = ViewModel;
        }

        private void ButtonCancelClick(object sender, RoutedEventArgs e) {
            ViewModel.CancelReservation();
            _parentPage.Update();
        }

        private void ButtonRescheduleDateClick(object sender, RoutedEventArgs e) {
            _parentPage.OpenRescheduleDialog(ViewModel.Reservation);
        }

        private void ButtonReviewClick(object sender, RoutedEventArgs e) {
            _parentPage.OpenReviewDialog(ViewModel.Reservation);
        }
    }
}
