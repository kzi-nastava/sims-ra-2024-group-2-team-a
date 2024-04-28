using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using BookingApp.Services;
using BookingApp.WPF.DTO;
using BookingApp.Domain.Model;

namespace BookingApp.WPF.Web.Views {
    /// <summary>
    /// Interaction logic for CreateReservationPage.xaml
    /// </summary>
    public partial class CreateReservationPage : Page {

        private AccommodationDTO _accommodationDTO;

        private readonly int maxSuggestedReservationsCount = 20;

        private readonly AccommodationReservationService _reservationService = new AccommodationReservationService();

        public CreateReservationPage(AccommodationDTO accommodationDTO) {
            InitializeComponent();
            _accommodationDTO = accommodationDTO;
            DataContext = _accommodationDTO;
            
            sliderGuests.Maximum = _accommodationDTO.MaxGuestNumber;
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
                dataGridSuggestedDates.ItemsSource = null;
                return;
            }

            AccommodationReservationDTO rDTO = new AccommodationReservationDTO();
            rDTO.AccommodationId = _accommodationDTO.Id;
            rDTO.ReservationDays = int.Parse(textBoxReservationDays.Text);
            rDTO.StartDate = DateOnly.FromDateTime(datePickerStartDate.SelectedDate.Value);
            rDTO.EndDate = DateOnly.FromDateTime(datePickerEndDate.SelectedDate.Value);

            var reservations = _reservationService.SuggestReservations(rDTO);

            if(reservations.Count > maxSuggestedReservationsCount)
                reservations = reservations.GetRange(0, maxSuggestedReservationsCount);

            reservations = reservations.OrderBy(r => r.StartDate).ToList();
            dataGridSuggestedDates.ItemsSource = reservations;
        }

        private bool IsReservationInputValid() {
            if (!int.TryParse(textBoxReservationDays.Text, out int reservationDays))
                return false;

            if (reservationDays < _accommodationDTO.MinReservationDays)
                textBoxReservationDays.Text = _accommodationDTO.MinReservationDays.ToString();

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

            SaveReservation(selectedReservation);

            GoBack(sender, e);
        }

        private void SaveReservation(AccommodationReservation selectedReservation) {
            GuestMainWindow window = Window.GetWindow(this) as GuestMainWindow;

            selectedReservation.GuestId = window.GuestId;
            selectedReservation.AccommodationId = _accommodationDTO.Id;
            selectedReservation.GuestsNumber = int.Parse(textBoxGuests.Text);
            _reservationService.Save(selectedReservation);
        }
    }
}
