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
        private void UserControl_Loaded(object sender, RoutedEventArgs e) {
            ViewModel = new TourReviewViewModel((TourReviewDTO)DataContext);
        }

        private void Report_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e) {
            if(ViewModel != null) {
                if(ViewModel.tourReviewDTO.IsValid)
                    e.CanExecute = true;
                else
                    e.CanExecute = false;
            }
            else {
                e.CanExecute = true;

            }

        }

        private void Report_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e) {
            ViewModel.Report();
        }
    }
}
