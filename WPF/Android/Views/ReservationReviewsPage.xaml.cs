using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Security.AccessControl;
using System.Windows;
using System.Windows.Controls;
using BookingApp.Services;

namespace BookingApp.WPF.Android.Views {
    /// <summary>
    /// Interaction logic for ReservationReviewsPage.xaml
    /// </summary>
    public partial class ReservationReviewsPage : Page {

        private AccommodationService accommodationService = new AccommodationService();

        private ReviewService reviewService = new ReviewService();

        private AccommodationReservationService accReservationService = new AccommodationReservationService();

        private RescheduleRequestService requestService = new RescheduleRequestService();

        private User _user;
        public ObservableCollection<AccommodationReservationDTO> ReservationCollection { get; set; }
        public ObservableCollection<RescheduleRequestDTO> RescheduleRequestDTOs { get; set; }
        public AccommodationReservationDTO SelectedReservation { get; set; }
        public RescheduleRequestDTO SelectedRequest { get; set; }
        public ReservationReviewsPage(User user) {
            InitializeComponent();
            this.DataContext = this;

            _user = user;

            ReservationCollection = new ObservableCollection<AccommodationReservationDTO>();
            RescheduleRequestDTOs = new ObservableCollection<RescheduleRequestDTO>();

            Update();
        }

        public void Update() {
            FillReservationCollection();
            FillRescheduleRequestCollection();
        }

        private void FillRescheduleRequestCollection() {
            RescheduleRequestDTOs.Clear();
            foreach (RescheduleRequest request in requestService.GetSortedRequestsByOwnerId(_user.Id)) {
                RescheduleRequestDTO rescheduleRequestDTO = new RescheduleRequestDTO(request);
                AccommodationReservation accommodationReservation = accReservationService.GetById(request.ReservationId);

                rescheduleRequestDTO.SetDates();
                rescheduleRequestDTO.AccommodationName = accommodationService.GetById(accommodationReservation.AccommodationId).Name;
                rescheduleRequestDTO.IsAvailable =
                    accReservationService.CheckAccommodationAvailability(accommodationReservation.AccommodationId, rescheduleRequestDTO.NewStartDate, rescheduleRequestDTO.NewEndDate);
                RescheduleRequestDTOs.Add(rescheduleRequestDTO);
            }
        }
        private void FillReservationCollection() {
            ReservationCollection.Clear();

            foreach (var acc in accommodationService.GetByOwnerId(_user.Id)) {
                foreach (var accRes in accReservationService.GetByAccommodationId(acc.Id)) {
                    AccommodationReservationDTO accResDTO = new AccommodationReservationDTO(accRes);

                    accResDTO.Graded = false;
                    accResDTO.AccommodationName = acc.Name;

                    if (reviewService.IsGuestGraded(accResDTO.Id)) {
                        accResDTO.Graded = true;
                    }

                    ReservationCollection.Add(accResDTO);
                }
            }
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (SelectedReservation == null)
                return;

            DateOnly currentDate = DateOnly.FromDateTime(DateTime.Now);
            DateOnly offsetDate = SelectedReservation.EndDate;
            offsetDate = offsetDate.AddDays(5);

            if (SelectedReservation.Graded)
                AssignGradeButton.IsEnabled = false;
            else if (currentDate > SelectedReservation.EndDate && currentDate < offsetDate)
                AssignGradeButton.IsEnabled = true;
            else
                AssignGradeButton.IsEnabled = false;

            ViewGradeButton.IsEnabled = true;
        }

        private void AssignGradeButton_Click(object sender, RoutedEventArgs e) {
            if (SelectedReservation == null) {
                AssignGradeButton.IsEnabled = false;
            }
            else {
                AssignGradeButton.IsEnabled = true;
                AssignGradeWindow assignGradeWindow = new AssignGradeWindow(SelectedReservation, _user.Id, this);
                assignGradeWindow.ShowDialog();
            }
        }

        private void ViewGradeButton_Click(object sender, RoutedEventArgs e) {
            if (SelectedReservation == null) {
                ViewGradeButton.IsEnabled = false;
            }
            else if(!reviewService.IsGradedByOwner(SelectedReservation.Id)){
                MessageBox.Show("You must grade this reservation first!","View guest grade",MessageBoxButton.OK,MessageBoxImage.Information);
            }
            else {
                ViewGuestGradeWindow viewGuestGradeWindow = new ViewGuestGradeWindow(SelectedReservation);
                viewGuestGradeWindow.ShowDialog();
            }
        }
        private void RequestsList_SelectionChanged(object sender, SelectionChangedEventArgs e) {

        }

        private void AcceptButtonCommand_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e) {
            if (SelectedRequest == null || SelectedRequest.Status!=RescheduleRequestStatus.Pending) {
                e.CanExecute = false;
            }
            else {
                e.CanExecute = true;
            }
        }
        private void AcceptButtonCommand_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e) {
            AccommodationReservation accommodationReservation = accReservationService.GetById(SelectedRequest.ReservationId);
            accommodationReservation.StartDate = SelectedRequest.NewStartDate;
            accommodationReservation.EndDate = SelectedRequest.NewEndDate;
            accReservationService.Update(accommodationReservation);

            RescheduleRequest rescheduleRequest = SelectedRequest.ToRescheduleRequest();
            rescheduleRequest.Status = RescheduleRequestStatus.Approved;
            requestService.Update(rescheduleRequest);
            this.Update();
        }

        private void DeclineCommand_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e) {
            if (SelectedRequest == null || SelectedRequest.Status != RescheduleRequestStatus.Pending) {
                e.CanExecute = false;
            }
            else {
                e.CanExecute = true;
            }
        }

        private void DeclineCommand_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e) {
            RescheduleRequestDeclineWindow rescheduleRequestDeclineWindow = new RescheduleRequestDeclineWindow(SelectedRequest);
            rescheduleRequestDeclineWindow.ShowDialog();
            this.Update();
        }
    }
}
