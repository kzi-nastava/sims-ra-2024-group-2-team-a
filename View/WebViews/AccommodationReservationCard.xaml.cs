using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Services;
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

namespace BookingApp.View.WebViews {
    /// <summary>
    /// Interaction logic for AccommodationReservationCard.xaml
    /// </summary>
    public partial class AccommodationReservationCard : UserControl {

        private readonly AccommodationReservationService _reservationService = new AccommodationReservationService();
        private readonly RescheduleRequestService _rescheduleService = new RescheduleRequestService();

        public AccommodationReservationCard() {
            InitializeComponent();
        }

        private void ButtonCancelClick(object sender, RoutedEventArgs e) {
            AccommodationReservationDTO reservationDTO = DataContext as AccommodationReservationDTO;

            _rescheduleService.DeleteByReservationId(reservationDTO.Id);

            _reservationService.Delete(reservationDTO.Id);

            Update();
        }

        // NOTE: This should be implemented as observer
        private void Update() {

            GuestMainWindow window = (GuestMainWindow)Window.GetWindow(this);
            ReservationsPage reservationsPage = window.MainFrame.Content as ReservationsPage;
            reservationsPage.Update();
        }

        private void UserControlDataContextChanged(object sender, DependencyPropertyChangedEventArgs e) {

            AccommodationReservationDTO reservationDTO = DataContext as AccommodationReservationDTO;

            buttonRate.Visibility = reservationDTO.CanBeGraded ? Visibility.Visible : Visibility.Hidden;

            buttonRescheduleDate.Visibility = reservationDTO.CanBeRescheduled ? Visibility.Visible : Visibility.Hidden;

            buttonCancel.Visibility = reservationDTO.CanBeCancelled ? Visibility.Visible : Visibility.Hidden;
        }

        private void ButtonRescheduleDateClick(object sender, RoutedEventArgs e) {
            GuestMainWindow window = (GuestMainWindow)Window.GetWindow(this);
            ReservationsPage page =  window.MainFrame.Content as ReservationsPage;
            page.OpenRescheduleDialog(DataContext as AccommodationReservationDTO);
        }
    }
}
