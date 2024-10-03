using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using BookingApp.Services;
using BookingApp.WPF.DTO;
using BookingApp.Domain.Model;
using BookingApp.WPF.Web.ViewModels;
using LiveCharts.Wpf;
using LiveCharts;

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

            textBoxReservationDays.Minimum = ViewModel.Accommodation.MinReservationDays;
            textBoxReservationDays.Value = textBoxReservationDays.Minimum;
            textBoxGuests.Value = 1;

            buttonLeft.IsEnabled = false;
            if(ViewModel.Accommodation.ProfilePictures.Count == 1)
                buttonRight.IsEnabled = false;

            SetupRatingChart();
        }

        private void GoBack(object sender, RoutedEventArgs e) {
            App.GuestMainWindowReference.ButtonBackClick(this, e);
        }

        private void UpdateSuggestedReservations(object sender, EventArgs e) {
            SetDatePickerEndDate();

            if(!IsDateInputValid()) {
                ViewModel.SuggestedReservations = null;
                return;
            }

            ViewModel.ReservationFilter.StartDate = DateOnly.FromDateTime(datePickerStartDate.SelectedDate.Value);
            ViewModel.ReservationFilter.EndDate = DateOnly.FromDateTime(datePickerEndDate.SelectedDate.Value);

            ViewModel.UpdateSuggestedReservations();
        }

        private bool IsDateInputValid() {
            if (datePickerStartDate.SelectedDate == null || datePickerEndDate.SelectedDate == null)
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
                datePickerEndDate.DisplayDateStart = datePickerStartDate.SelectedDate.Value.AddDays(ViewModel.ReservationFilter.ReservationDays);
                return;
            }
        }

        private void ButtonConfirmClick(object sender, RoutedEventArgs e) {
            App.NotificationService.ShowSuccess("Reservation successfully created!");
            GoBack(sender, e);
        }

        private void ButtonLeftClick(object sender, RoutedEventArgs e) {
            ViewModel.ChangePictureLeft();

            if(ViewModel.PicturesIndex == 0) {
                buttonLeft.IsEnabled = false;
                buttonRight.IsEnabled = true;
            }
        }

        private void ButtonRightClick(object sender, RoutedEventArgs e) {
            ViewModel.ChangePictureRight();

            if (ViewModel.PicturesIndex == ViewModel.MaxPictureIndex) {
                buttonRight.IsEnabled = false;
                buttonLeft.IsEnabled = true;
            }
        }

        private void SetupRatingChart() {
            var series = RatingChart.Series[0] as RowSeries;
            series.Values = new ChartValues<double> { ViewModel.AverageCleannessGrade, ViewModel.AverageCorrectnessGrade };
        } 
    }
}
