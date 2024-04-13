using BookingApp.DTO;
using BookingApp.WPF.Tablet.ViewModels;
using BookingApp.WPF.Tablet.Views;
using System.Windows;
using System.Windows.Controls;

namespace BookingApp.View.TabletView {
    /// <summary>
    /// Interaction logic for LiveTourPage.xaml
    /// </summary>
    public partial class LiveTourPage : Page {
        private Frame _mainFrame, _menuBarFrame;
        private int _userId; 

        public LiveTourViewModel ViewModel { get; set; }    
        public LiveTourPage(TourDTO tDTO, Frame mainF, Frame menuBarF, int userId) {
            InitializeComponent();
            ViewModel = new LiveTourViewModel(tDTO, userId);
            DataContext = ViewModel;

            _mainFrame = mainF;
            _menuBarFrame = menuBarF;
            _menuBarFrame.IsHitTestVisible = false;
            _menuBarFrame.Opacity = 0.3;
        }
        private void checkButton_Click(object sender, RoutedEventArgs e) {
            bool isConfirmed = JoinPassengers();
            if (!isConfirmed)
                return;
            if(ViewModel.CheckKeyPoint())
                FinishTour();
        }

        private void finishButton_Click(object sender, RoutedEventArgs e) {
            FinishTour();
        }
        
        private void FinishTour() {
            MessageBox.Show("Finished", "Finished", MessageBoxButton.OK, MessageBoxImage.Information);
            ViewModel.FinishTour();
            _mainFrame.Content = new FollowLiveTourPage(_userId);
            _menuBarFrame.IsHitTestVisible = true;
            _menuBarFrame.Opacity = 1;
        }

        private bool JoinPassengers() {
            ViewModel.SetPointOfInterestDTO();
            PassengerWindow passengerWindow = new PassengerWindow(ViewModel.tourDTO.Id, ViewModel.pointOfInterestDTO.Id);
            bool isConfirmed = (bool)passengerWindow.ShowDialog();
            return isConfirmed;
        }

    }
}
