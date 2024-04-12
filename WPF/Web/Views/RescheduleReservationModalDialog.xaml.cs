using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Services;
using BookingApp.WPF.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
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
    /// Interaction logic for RescheduleReservationModalDialog.xaml
    /// </summary>
    public partial class RescheduleReservationModalDialog : UserControl
    {
        private readonly ReservationsPage _parentPage;
        private RescheduleReservationModalDialogViewModel ViewModel { get; set; }

        public RescheduleReservationModalDialog(ReservationsPage parentPage, AccommodationReservationDTO reservation)
        {
            InitializeComponent();
            _parentPage = parentPage;
            ViewModel = new RescheduleReservationModalDialogViewModel(reservation);
            DataContext = ViewModel;
            dataPickerNewDate.SelectedDate = DateTime.Now;
        }

        private void ButtonCancelClick(object sender, RoutedEventArgs e) {
            _parentPage.CloseModalDialog();
        }

        private void ButtonConfirmClick(object sender, RoutedEventArgs e) {
            ViewModel.Reschedule();
            _parentPage.CloseModalDialog();
        }
    }
}
