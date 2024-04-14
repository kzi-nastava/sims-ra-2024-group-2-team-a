using BookingApp.WPF.DTO;
using BookingApp.WPF.Tablet.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace BookingApp.WPF.Tablet.Views {
    /// <summary>
    /// Interaction logic for TourReviewCard.xaml
    /// </summary>
    public partial class TourReviewCard : UserControl {
        public TourReviewViewModel ViewModel { get; set; }
        
        public TourReviewCard() {
            InitializeComponent();
        }

        private void reportButton_Click(object sender, RoutedEventArgs e) {
            ViewModel.Report();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e) {
            ViewModel = new TourReviewViewModel((TourReviewDTO)DataContext);
        }
    }
}
