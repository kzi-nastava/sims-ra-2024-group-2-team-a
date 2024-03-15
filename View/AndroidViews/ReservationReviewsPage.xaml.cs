using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.ObjectModel;
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

        private User _user;

        public ObservableCollection<AccommodationReservationDTO> ReservationCollection { get; set; }
        public AccommodationReservationDTO SelectedReservation { get; set; }

        public ReservationReviewsPage(User user) {
            InitializeComponent();
            this.DataContext = this;

            _user = user;

            _accommodationReservationRepository = new AccommodationReservationRepository();
            _reviewRepository = new ReviewRepository();
            _accommodationRepository = new AccommodationRepository();

            ReservationCollection = new ObservableCollection<AccommodationReservationDTO>();

            Update();
        }

        public void Update() {
            ReservationCollection.Clear();

            foreach (var acc in _accommodationRepository.GetByOwnerId(_user.Id)) {
                foreach (var accRes in _accommodationReservationRepository.GetByAccommodationId(acc.Id)) {
                    AccommodationReservationDTO accResDTO = new AccommodationReservationDTO(accRes);

                    accResDTO.Graded = false;
                    accResDTO.AccommodationName = acc.Name;

                    if (_reviewRepository.GetByReservationId(accRes.Id) != null) {
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
    }
}
