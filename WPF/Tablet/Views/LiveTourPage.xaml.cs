using BookingApp.WPF.DTO;
using BookingApp.WPF.Tablet.ViewModels;
using BookingApp.WPF.Tablet.Views;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BookingApp.View.TabletView {
    /// <summary>
    /// Interaction logic for LiveTourPage.xaml
    /// </summary>
    public partial class LiveTourPage : Page {
        private Frame _mainFrame;
        private int _userId; 

        public LiveTourViewModel ViewModel { get; set; }    
        public LiveTourPage(TourDTO tDTO, Frame mainF, int userId) {
            InitializeComponent();
            ViewModel = new LiveTourViewModel(tDTO, userId);
            DataContext = ViewModel;

            if (!ViewModel.HasStarted())
                CheckKeypoint();
            _mainFrame = mainF;
        }
        private void CheckKeypoints_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = true;
        }

        private void CheckKeypoints_Executed(object sender, ExecutedRoutedEventArgs e) {
            CheckKeypoint();
        }

        private void Finish_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = true;
        }

        private void Finish_Executed(object sender, ExecutedRoutedEventArgs e) {
            FinishTour();
        }
        private void CheckKeypoint() {
            bool isConfirmed = JoinPassengers();
            if (!isConfirmed)
                return;
            if (ViewModel.CheckKeyPoint())
                FinishTour();
        }
        private void FinishTour() {
            MessageBox.Show("Finished", "Finished", MessageBoxButton.OK, MessageBoxImage.Information);
            ViewModel.FinishTour();
            _mainFrame.Content = new FollowLiveTourPage(_userId);
        }

        private bool JoinPassengers() {
            ViewModel.SetPointOfInterestDTO();
            PassengerWindow passengerWindow = new PassengerWindow(ViewModel.tourDTO.Id, ViewModel.pointOfInterestDTO.Id);
            bool isConfirmed = (bool)passengerWindow.ShowDialog();
            return isConfirmed;
        }

        /*private void Cancel_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e) {
            e.CanExecute = true;
        }

        private void Cancel_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e) {

        }*/

    }
}
