using BookingApp.Services;
using BookingApp.WPF.Android.ViewModels;
using BookingApp.WPF.DTO;
using System.Windows;

namespace BookingApp.WPF.Android.Views {
    /// <summary>
    /// Interaction logic for AssignGradeWindow.xaml
    /// </summary>
    public partial class AssignGradeWindow : Window {
        public AccommodationReservationDTO AccReservationDTO { get; set; }
        public AccommodationReviewDTO ReviewDTO { get; set; }

        private ReviewService reviewService = ServicesPool.GetService<ReviewService>();

        private int _ownerId;

        private ReservationReviewsViewmodel _reservationReviewsViewmodel;

        public string GuestUsername { get; set; }
        public AssignGradeWindow(AccommodationReservationDTO accResDTO, int ownerId, ReservationReviewsViewmodel viewmodel) {
            InitializeComponent();
            DataContext = this;

            AccReservationDTO = accResDTO;
            _ownerId = ownerId;
            ReviewDTO = new AccommodationReviewDTO();
            _reservationReviewsViewmodel = viewmodel;

            UserService userService = ServicesPool.GetService<UserService>();
            GuestUsername = userService.GetById(accResDTO.GuestId).Username;
        }

        private void DoneButton_Click(object sender, RoutedEventArgs e) {
            if(ReviewDTO.GuestCleannessGrade == 0){
                ReviewDTO.GuestCleannessGrade = 1;
            }
            if (ReviewDTO.RuleFollowingGrade == 0) {
                ReviewDTO.RuleFollowingGrade = 1;
            }

            ReviewDTO.GuestId = AccReservationDTO.GuestId;
            ReviewDTO.OwnerId = _ownerId;
            ReviewDTO.ReservationId = AccReservationDTO.Id;

            reviewService.GradeGuest(ReviewDTO);
            _reservationReviewsViewmodel.Update();
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }
    }
}
