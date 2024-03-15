using BookingApp.DTO;
using BookingApp.Repository;
using BookingApp.Model;
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
    /// Interaction logic for CreateReservationPage.xaml
    /// </summary>
    public partial class CreateReservationPage : Page {

        private AccommodationDTO _accommodationDTO;

        private readonly int maxSuggestedReservationsCount = 20;

        public CreateReservationPage(AccommodationDTO accommodationDTO) {
            InitializeComponent();
            _accommodationDTO = accommodationDTO;
            DataContext = _accommodationDTO;
            
            sliderGuests.Maximum = _accommodationDTO.MaxGuestNumber;
            datePickerStartDate.DisplayDateStart = DateTime.Today.AddDays(1);
            datePickerEndDate.IsEnabled = false;
        }

        private void ButtonBackClick(object sender, RoutedEventArgs e) {
            Frame frame = (Frame)Window.GetWindow(this).FindName("mainFrame");
            frame.Content = new BookingPage(frame);
        }

        private void UpdateSuggestedReservations(object sender, EventArgs e) {
            SetDatePickerEndDate();

            if(!IsReservationInputValid()) {
                dataGridSuggestedDates.ItemsSource = null;
                return;
            }

            int reservationDays = int.Parse(textBoxReservationDays.Text);
            DateOnly startDate = DateOnly.FromDateTime(datePickerStartDate.SelectedDate.Value);
            DateOnly endDate = DateOnly.FromDateTime(datePickerEndDate.SelectedDate.Value);

            var accommodationReservationRepository = new AccommodationReservationRepository();
            var reservations = accommodationReservationRepository.GetPossibleReservations(_accommodationDTO.Id, startDate, endDate, reservationDays);

            if(reservations.Count > maxSuggestedReservationsCount)
                reservations = reservations.GetRange(0, maxSuggestedReservationsCount);

            dataGridSuggestedDates.ItemsSource = reservations;
        }

        private bool IsReservationInputValid() {
            if (!int.TryParse(textBoxReservationDays.Text, out int reservationDays))
                return false;

            if (reservationDays <= 0)
                return false;

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
            if(selectedReservation == null) {
                errorLabel.Content = "Please select a reservation";
                errorLabel.Foreground = Brushes.Red;
                return;
            }

            GuestMainWindow window = Window.GetWindow(this) as GuestMainWindow;
            User currentUser = window.User;

            selectedReservation.IdGuest = currentUser.Id;
            AccommodationReservationRepository accommodationReservationRepository = new AccommodationReservationRepository();
            accommodationReservationRepository.Save(selectedReservation);

            Frame frame = (Frame)Window.GetWindow(this).FindName("mainFrame");
            frame.Content = new BookingPage(frame);
        }
    }
}
