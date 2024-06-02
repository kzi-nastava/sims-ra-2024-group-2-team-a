using BookingApp.WPF.DTO;
using BookingApp.WPF.Web.ViewModels;
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

namespace BookingApp.WPF.Web.Views {
    /// <summary>
    /// Interaction logic for ConfirmReservationCancelModalDialog.xaml
    /// </summary>
    public partial class ConfirmReservationCancelModalDialog : UserControl {

        private readonly ReservationsPage _parentPage;

        public ConfirmReservationCancelModalDialogViewModel ViewModel { get; set; }

        public ConfirmReservationCancelModalDialog(ReservationsPage parentPage, AccommodationReservationDTO reservation) {
            InitializeComponent();
            _parentPage = parentPage;
            ViewModel = new ConfirmReservationCancelModalDialogViewModel(reservation);
            DataContext = ViewModel;
        }

        private void ButtonCancelClick(object sender, RoutedEventArgs e) {
            _parentPage.CloseModalDialog();
        }

        private void ButtonConfirmClick(object sender, RoutedEventArgs e) {
            ViewModel.CancelReservation();
            App.NotificationService.ShowSuccess("Reservation canceled successfully.");
            _parentPage.Update();
            _parentPage.CloseModalDialog();
        }
    }
}
