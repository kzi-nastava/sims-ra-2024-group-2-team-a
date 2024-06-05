using BookingApp.WPF.DTO;
using BookingApp.WPF.Web.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;

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
            dataPickerNewDate.DisplayDateStart = DateTime.Now.AddDays(1);
            buttonConfirm.IsEnabled = false;
        }

        private void ButtonCancelClick(object sender, RoutedEventArgs e) {
            _parentPage.CloseModalDialog();
        }

        private void ButtonConfirmClick(object sender, RoutedEventArgs e) {
            ViewModel.Reschedule();
            _parentPage.Update();
            App.NotificationService.ShowSuccess("Reschedule request created successfully!");
            _parentPage.CloseModalDialog();
        }

        private void SelectedDateChanged(object sender, SelectionChangedEventArgs e) {
            if (dataPickerNewDate.SelectedDate != null)
               buttonConfirm.IsEnabled = true;
        }
    }
}
