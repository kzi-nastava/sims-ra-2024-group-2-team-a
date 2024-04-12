using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Repository;
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

namespace BookingApp.WPF.Tablet.Views {
    /// <summary>
    /// Interaction logic for TourReviewCard.xaml
    /// </summary>
    public partial class TourReviewCard : UserControl {
        private readonly PassengerRepository _passengerRepository = new PassengerRepository();
        private readonly PointOfInterestRepository _pointOfInterestRepository = new PointOfInterestRepository();
        private readonly TourReservationRepository _tourReservationRepository = new TourReservationRepository();
        private readonly TourReviewRepository _tourReviewRepository = new TourReviewRepository();
        public TourReviewDTO tourReviewDTO { get; set; }
        public PassengerDTO passengerDTO { get; set; }
        
        public TourReviewCard() {
            InitializeComponent();
        }

        private void reportButton_Click(object sender, RoutedEventArgs e) {
            tourReviewDTO.IsValid = false;
            _tourReviewRepository.Update(tourReviewDTO.ToModel());
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e) {
            tourReviewDTO = (TourReviewDTO)DataContext;
        }
    }
}
