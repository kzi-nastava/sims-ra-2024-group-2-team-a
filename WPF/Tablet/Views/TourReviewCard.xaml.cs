using BookingApp.DTO;
using BookingApp.Model;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookingApp.WPF.Tablet.Views {
    /// <summary>
    /// Interaction logic for TourReviewCard.xaml
    /// </summary>
    public partial class TourReviewCard : UserControl {
        private readonly TourReviewRepository _tourReviewRepository = new TourReviewRepository();
        private readonly TourReviewService _tourReviewService = new TourReviewService();
        public TourReviewDTO tourReviewDTO { get; set; }
        public PassengerDTO passengerDTO { get; set; }
        
        public TourReviewCard() {
            InitializeComponent();
        }

        private void reportButton_Click(object sender, RoutedEventArgs e) {
            tourReviewDTO.IsValid = false;
            _tourReviewService.Update(tourReviewDTO.ToModel());
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e) {
            tourReviewDTO = (TourReviewDTO)DataContext;
        }
    }
}
