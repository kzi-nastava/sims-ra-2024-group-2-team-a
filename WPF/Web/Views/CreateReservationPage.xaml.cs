using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using BookingApp.Services;
using BookingApp.WPF.DTO;
using BookingApp.Domain.Model;
using BookingApp.WPF.Web.ViewModels;

namespace BookingApp.WPF.Web.Views {
    /// <summary>
    /// Interaction logic for CreateReservationPage.xaml
    /// </summary>
    public partial class CreateReservationPage : Page {

        public CreateReservationPageViewModel ViewModel { get; set; }

        public CreateReservationPage(AccommodationDTO accommodationDTO, int guestId) {
            InitializeComponent();

            ViewModel = new CreateReservationPageViewModel(accommodationDTO, guestId);

            DataContext = ViewModel;

            textBoxGuests.Maximum = accommodationDTO.MaxGuestNumber;
            datePickerStartDate.DisplayDateStart = DateTime.Today.AddDays(1);
            datePickerEndDate.IsEnabled = false;
        }

        private void GoBack(object sender, RoutedEventArgs e) {
            GuestMainWindow window = (GuestMainWindow)Window.GetWindow(this);
            window.ButtonBackClick(this, e);
        }

        private void UpdateSuggestedReservations(object sender, EventArgs e) {
            SetDatePickerEndDate();

            if(!IsReservationInputValid()) {
                ViewModel.SuggestedReservations = null;
                return;
            }

            ViewModel.ReservationDTO.ReservationDays = int.Parse(textBoxReservationDays.Text);
            ViewModel.ReservationDTO.StartDate = DateOnly.FromDateTime(datePickerStartDate.SelectedDate.Value);
            ViewModel.ReservationDTO.EndDate = DateOnly.FromDateTime(datePickerEndDate.SelectedDate.Value);

            ViewModel.UpdateSuggestedReservations();
        }

        private bool IsReservationInputValid() {
            if (!int.TryParse(textBoxReservationDays.Text, out int reservationDays))
                return false;

            if (reservationDays < ViewModel.Accommodation.MinReservationDays)
                textBoxReservationDays.Text = ViewModel.Accommodation.MinReservationDays.ToString();

            if (datePickerStartDate.SelectedDate == null || datePickerEndDate.SelectedDate == null)
                return false;

            TimeSpan span = datePickerEndDate.SelectedDate.Value - datePickerStartDate.SelectedDate.Value;
            if(span.Days < reservationDays)
                return false;

            return true;
        }

        private void SetDatePickerEndDate() {
            if (datePickerStartDate.SelectedDate >= datePickerEndDate.SelectedDate) {
                datePickerEndDate.SelectedDate = null;
                datePickerEndDate.DisplayDateStart = datePickerStartDate.SelectedDate.Value.AddDays(1);
                return;
            }

            if (datePickerStartDate.SelectedDate != null) {
                datePickerEndDate.IsEnabled = true;
                datePickerEndDate.DisplayDateStart = datePickerStartDate.SelectedDate.Value.AddDays(1);
                return;
            }
        }

        private void ButtonConfirmClick(object sender, RoutedEventArgs e) {
            AccommodationReservation selectedReservation = (AccommodationReservation) dataGridSuggestedDates.SelectedItem;
            selectedReservation.GuestsNumber = int.Parse(textBoxGuests.Text);

            ViewModel.SaveReservation(selectedReservation);

            GoBack(sender, e);
        }
    }
}
