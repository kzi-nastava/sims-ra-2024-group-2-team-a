using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.ObjectModel;
using System.Security.AccessControl;
using System.Windows;
using System.Windows.Controls;

namespace BookingApp.View.AndroidViews {
    /// <summary>
    /// Interaction logic for ReservationReviewsPage.xaml
    /// </summary>
    public partial class ReservationReviewsPage : Page {

        private AccommodationReservationRepository _accommodationReservationRepository;

        private ReviewRepository _reviewRepository;

        private AccommodationRepository _accommodationRepository;

        private RescheduleRequestRepository _rescheduleRequestRepository;

        private User _user;

        public ObservableCollection<AccommodationReservationDTO> ReservationCollection { get; set; }
        public ObservableCollection<RescheduleRequestDTO> RescheduleRequestDTOs { get; set; }
        public AccommodationReservationDTO SelectedReservation { get; set; }
        public RescheduleRequestDTO SelectedRequest { get; set; }
        public ReservationReviewsPage(User user) {
            InitializeComponent();
            this.DataContext = this;

            _user = user;

            _accommodationReservationRepository = new AccommodationReservationRepository();
            _reviewRepository = new ReviewRepository();
            _accommodationRepository = new AccommodationRepository();
            _rescheduleRequestRepository = new RescheduleRequestRepository();

            ReservationCollection = new ObservableCollection<AccommodationReservationDTO>();
            RescheduleRequestDTOs = new ObservableCollection<RescheduleRequestDTO>();

            Update();
        }

        public void Update() {
            ReservationCollection.Clear();
            RescheduleRequestDTOs.Clear();

            foreach (var acc in _accommodationRepository.GetByOwnerId(_user.Id)) {
                foreach (var accRes in _accommodationReservationRepository.GetByAccommodationId(acc.Id)) {
                    AccommodationReservationDTO accResDTO = new AccommodationReservationDTO(accRes);

                    accResDTO.Graded = false;
                    accResDTO.AccommodationName = acc.Name;

                    if (_reviewRepository.IsGuestGraded(accResDTO.Id)) {
                        accResDTO.Graded = true;
                    }

                    ReservationCollection.Add(accResDTO);
                }
            }

            foreach (RescheduleRequest request in _rescheduleRequestRepository.GetPendingRequestsByOwnerId(_user.Id)) {
                RescheduleRequestDTO rescheduleRequestDTO = new RescheduleRequestDTO(request);
                AccommodationReservation accommodationReservation = _accommodationReservationRepository.GetById(request.ReservationId);

                rescheduleRequestDTO.SetDates(accommodationReservation.EndDate);
                rescheduleRequestDTO.AccommodationName = _accommodationRepository.GetById(accommodationReservation.AccommodationId).Name;
                rescheduleRequestDTO.IsAvailable = 
                    _accommodationReservationRepository.CheckAccommodationAvailability(accommodationReservation.AccommodationId, rescheduleRequestDTO.NewStartDate, rescheduleRequestDTO.NewEndDate);
                RescheduleRequestDTOs.Add(rescheduleRequestDTO);
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
            else if(!_reviewRepository.IsGradedByOwner(SelectedReservation.Id)){
                MessageBox.Show("You must grade this reservation first!","View guest grade",MessageBoxButton.OK,MessageBoxImage.Information);
            }
            else {
                ViewGuestGradeWindow viewGuestGradeWindow = new ViewGuestGradeWindow(SelectedReservation);
                viewGuestGradeWindow.ShowDialog();
            }
        }

        private void Decline_Click(object sender, RoutedEventArgs e) {
            if (SelectedRequest == null)
                return;

            RescheduleRequestDeclineWindow rescheduleRequestDeclineWindow = new RescheduleRequestDeclineWindow(SelectedRequest);
            rescheduleRequestDeclineWindow.ShowDialog();
            this.Update();
        }

        private void Accept_Click(object sender, RoutedEventArgs e) {
            if (SelectedRequest == null)
                return;

            AccommodationReservation accommodationReservation = _accommodationReservationRepository.GetById(SelectedRequest.ReservationId);
            accommodationReservation.StartDate = SelectedRequest.NewStartDate;
            accommodationReservation.EndDate = SelectedRequest.NewEndDate;
            _accommodationReservationRepository.Update(accommodationReservation);

            RescheduleRequest rescheduleRequest = SelectedRequest.ToRescheduleRequest();
            rescheduleRequest.Status = RescheduleRequestStatus.Approved;
            _rescheduleRequestRepository.Update(rescheduleRequest);
            this.Update();
        }

        private void RequestsList_SelectionChanged(object sender, SelectionChangedEventArgs e) {

        }
    }
}
