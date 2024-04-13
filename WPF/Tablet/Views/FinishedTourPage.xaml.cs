using BookingApp.DTO;
using BookingApp.WPF.Tablet.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace BookingApp.WPF.Tablet.Views{
    /// <summary>
    /// Interaction logic for FinishedTourPage.xaml
    /// </summary>
    public partial class FinishedTourPage : Page {
        private Frame _mainFrame, _menuBarFrame;
        private int _userId;
        public FinishedTourViewModel ViewModel { get; set; }
        public FinishedTourPage(TourDTO tDTO, Frame mainF, Frame menuBarF, int userId) {
            InitializeComponent();

            ViewModel = new FinishedTourViewModel(tDTO, userId);
            DataContext = ViewModel;

            _mainFrame = mainF;
            _menuBarFrame = menuBarF;
            _userId = userId;
        }

        private void reviewsButton_Click(object sender, RoutedEventArgs e) {
            _mainFrame.Content = new TourReviewsPage(ViewModel.tourDTO, _mainFrame);
        }

        private void statsButton_Click(object sender, RoutedEventArgs e) {
            _mainFrame.Content = new TourStatsPage(ViewModel.tourDTO, _mainFrame, _menuBarFrame, _userId);
        }

        private void closeButton_Click(object sender, RoutedEventArgs e) {
            _mainFrame.GoBack();
        }
    }
}
