using BookingApp.DTO;
using BookingApp.Model;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookingApp.View.WebViews {
    /// <summary>
    /// Interaction logic for ReviewAccommodationModalDialog.xaml
    /// </summary>
    public partial class ReviewAccommodationModalDialog : UserControl {

        public AccommodationReservationDTO Reservation { get; set; }
        public ImportanceType[] ImportanceTypes { get; set; } = (ImportanceType[]) Enum.GetValues(typeof(ImportanceType));
        public ReviewDTO Review { get; set; } = new ReviewDTO();

        private ReservationsPage _parentPage;

        private readonly ReviewService _reviewService = new ReviewService();

        public ReviewAccommodationModalDialog(ReservationsPage parentPage, AccommodationReservationDTO reservation) {
            InitializeComponent();
            Reservation = reservation;
            _parentPage = parentPage;
            DataContext = this;

            Review.ReservationId = Reservation.Id;
            Review.GuestId = Reservation.GuestId;
            Review.OwnerId = Reservation.Accommodation.OwnerId;
            comboBoxRenovationImportance.SelectedIndex = 0;
        }

        private void ButtonCancelClick(object sender, RoutedEventArgs e) {
            _parentPage.CloseModalDialog();
        }

        private void ButtonConfirmClick(object sender, RoutedEventArgs e) {
            _reviewService.GradeOwner(Review);
            _parentPage.CloseModalDialog();
        }

        private void checkBoxRequiresRenovationUnchecked(object sender, RoutedEventArgs e) {
            textBoxRenovationComment.Text = "";
            comboBoxRenovationImportance.SelectedIndex = 0;
        }
    }
}
