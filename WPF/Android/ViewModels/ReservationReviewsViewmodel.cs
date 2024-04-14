using System;
using BookingApp.Model;
using BookingApp.Services;
using System.Collections.ObjectModel;
using BookingApp.WPF.Android.Views;
using System.Windows;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using BookingApp.Commands;
using BookingApp.WPF.DTO;

namespace BookingApp.WPF.Android.ViewModels {
    public class ReservationReviewsViewmodel : INotifyPropertyChanged {

        private AccommodationService accommodationService = new AccommodationService();

        private ReviewService reviewService = new ReviewService();

        private AccommodationReservationService accReservationService = new AccommodationReservationService();

        private RescheduleRequestService requestService = new RescheduleRequestService();

        private User _user;
        public ObservableCollection<AccommodationReservationDTO> ReservationCollection { get; set; }
        public ObservableCollection<RescheduleRequestDTO> RescheduleRequestDTOs { get; set; }
        public AccommodationReservationDTO SelectedReservation { get; set; }
        public RescheduleRequestDTO SelectedRequest { get; set; }

        private bool _assignGradeEnabled;
        public bool AssignGradeEnabled {
            get {
                return _assignGradeEnabled;
            }
            set {
                if (value != _assignGradeEnabled) {
                    _assignGradeEnabled = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _viewGuestGradeEnabled;
        public bool ViewGuestGradeEnabled {
            get {
                return _viewGuestGradeEnabled;
            }
            set {
                if (value != _viewGuestGradeEnabled) {
                    _viewGuestGradeEnabled = value;
                    OnPropertyChanged();
                }
            }
        }
        public AndroidCommand AcceptButtonCommand{ get; set;}
        public AndroidCommand DeclineButtonCommand { get; set; }
        public ReservationReviewsViewmodel(User user) {
            _user = user;

            ReservationCollection = new ObservableCollection<AccommodationReservationDTO>();
            RescheduleRequestDTOs = new ObservableCollection<RescheduleRequestDTO>();

            Update();

            ViewGuestGradeEnabled = true;
            AssignGradeEnabled = true;

            AcceptButtonCommand = new AndroidCommand(AcceptButton_Executed, AcceptButton_CanExecute);
            DeclineButtonCommand = new AndroidCommand(DeclineButton_Executed , DeclineButton_CanExecute);
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

        public void ListViewSelectionChanged() {
            if (SelectedReservation == null)
                return;

            DateOnly currentDate = DateOnly.FromDateTime(DateTime.Now);
            DateOnly offsetDate = SelectedReservation.EndDate;
            offsetDate = offsetDate.AddDays(5);

            if (currentDate > SelectedReservation.EndDate && currentDate < offsetDate && !SelectedReservation.Graded)
                AssignGradeEnabled = true;
            else
                AssignGradeEnabled = false;

            ViewGuestGradeEnabled = true;
        }

        public void AssignGradeButton() {
            if (SelectedReservation == null) {
                AssignGradeEnabled = false;
            }
            else {
                AssignGradeEnabled = true;
                AssignGradeWindow assignGradeWindow = new AssignGradeWindow(SelectedReservation, _user.Id, this);
                assignGradeWindow.ShowDialog();
            }
        }

        public void ViewGradeButton() {
            if (SelectedReservation == null) {
                ViewGuestGradeEnabled = false;
            }
            else if (!reviewService.IsGradedByOwner(SelectedReservation.Id)) {
                MessageBox.Show("You must grade this reservation first!", "View guest grade", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else {
                ViewGuestGradeWindow viewGuestGradeWindow = new ViewGuestGradeWindow(SelectedReservation);
                viewGuestGradeWindow.ShowDialog();
            }
        }

        public bool AcceptButton_CanExecute(object obj) {
            if (SelectedRequest == null || SelectedRequest.Status != RescheduleRequestStatus.Pending) {
                return false;
            }
            else {
                return true;
            }
        }
        public void AcceptButton_Executed(object obj) {
            AccommodationReservation accommodationReservation = accReservationService.GetById(SelectedRequest.ReservationId);
            accommodationReservation.StartDate = SelectedRequest.NewStartDate;
            accommodationReservation.EndDate = SelectedRequest.NewEndDate;
            accReservationService.Update(accommodationReservation);

            RescheduleRequest rescheduleRequest = SelectedRequest.ToRescheduleRequest();
            rescheduleRequest.Status = RescheduleRequestStatus.Approved;
            requestService.Update(rescheduleRequest);
            this.Update();
        }

        public bool DeclineButton_CanExecute(object obj) {
            if (SelectedRequest == null || SelectedRequest.Status != RescheduleRequestStatus.Pending) {
                return false;
            }
            else {
                return true;
            }
        }

        public void DeclineButton_Executed(object obj) {
            RescheduleRequestDeclineWindow rescheduleRequestDeclineWindow = new RescheduleRequestDeclineWindow(SelectedRequest);
            rescheduleRequestDeclineWindow.ShowDialog();
            this.Update();
        }

        public void RequestsListSelectionChanged() {
            AcceptButtonCommand.RaiseCanExecuteChanged();
            DeclineButtonCommand.RaiseCanExecuteChanged();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

