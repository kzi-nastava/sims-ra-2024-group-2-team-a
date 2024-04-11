using BookingApp.DTO;
using BookingApp.Repository;
using BookingApp.Services;
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

namespace BookingApp.View.AndroidViews {
    /// <summary>
    /// Interaction logic for ViewGuestGradeWindow.xaml
    /// </summary>
    public partial class ViewGuestGradeWindow : Window {

        public AccommodationReservationDTO AccommodationReservationDTO { get; set; }
        public ReviewDTO ReviewDTO { get; set; }
        public string GuestUsername { get; set; }

        //private readonly ReviewRepository _reviewRepository;

        private ReviewService reviewService = new ReviewService();

        private readonly UserRepository _userRepository;
        public ViewGuestGradeWindow(AccommodationReservationDTO selectedReservationDTO) {
            InitializeComponent();
            DataContext = this;

            _userRepository = new UserRepository();

            AccommodationReservationDTO = selectedReservationDTO;
            ReviewDTO = new ReviewDTO(reviewService.GetByReservationId(selectedReservationDTO.Id));
            GuestUsername = _userRepository.GetById(selectedReservationDTO.GuestId).Username;
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }
    }
}
