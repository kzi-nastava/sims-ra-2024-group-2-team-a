using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

           // AccommodationReservation accommodationReservation = new AccommodationReservation(1,3,2,new DateOnly(2013,2,2),new DateOnly(2013,2,5));
            //_accommodationReservationRepository.Save(accommodationReservation);
            //Review review = new Review(3,1,_user.Id);
            //_reviewRepository.Save(review);

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
            if(SelectedReservation == null) {
                return;
            }

            DateOnly currentDate = DateOnly.FromDateTime(DateTime.Now);

            DateOnly offsetDate = SelectedReservation.EndDate;
            offsetDate = offsetDate.AddDays(5);

            if (SelectedReservation.Graded) {
                AssignGradeButton.IsEnabled = false;
                return;
            }

            if (currentDate > SelectedReservation.EndDate && currentDate < offsetDate) {
                AssignGradeButton.IsEnabled = true;
            }
            else {
                AssignGradeButton.IsEnabled = false;
            }
        }

        private void AssignGradeButton_Click(object sender, RoutedEventArgs e) {
            if (SelectedReservation == null) {
                AssignGradeButton.IsEnabled = false;
            }
            else {
                AssignGradeButton.IsEnabled = true;
                AssignGradeWindow assignGradeWindow = new AssignGradeWindow(SelectedReservation,_user.Id,this);
                assignGradeWindow.ShowDialog();
            }
        }
    }
}
