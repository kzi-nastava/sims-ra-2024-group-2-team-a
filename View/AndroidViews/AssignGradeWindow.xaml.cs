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
using System.Windows.Shapes;
using BookingApp.DTO;
using BookingApp.Repository;
using BookingApp.Model;

namespace BookingApp.View.AndroidViews {
    /// <summary>
    /// Interaction logic for AssignGradeWindow.xaml
    /// </summary>
    public partial class AssignGradeWindow : Window {
        public AccommodationReservationDTO AccReservationDTO { get; set; }
        public ReviewDTO ReviewDTO { get; set; }

        private ReviewRepository _reviewRepository;

        private int _ownerId;

        private ReservationReviewsPage _reservationReviewsPage;
        public AssignGradeWindow(AccommodationReservationDTO accResDTO, int ownerId, ReservationReviewsPage page) {
            InitializeComponent();
            DataContext = this;

            _reviewRepository = new ReviewRepository();
            AccReservationDTO = accResDTO;
            _ownerId = ownerId;
            ReviewDTO = new ReviewDTO();
            _reservationReviewsPage = page;
        }

        private void DoneButton_Click(object sender, RoutedEventArgs e) {
            ReviewDTO.GuestId = AccReservationDTO.GuestId;
            ReviewDTO.OwnerId = _ownerId;
            ReviewDTO.ReservationId = AccReservationDTO.Id;
            Review review = ReviewDTO.ToReview();
            _reviewRepository.Save(review);
            _reservationReviewsPage.Update();
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }
    }
}
