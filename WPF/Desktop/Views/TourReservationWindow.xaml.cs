using System.Windows;
using System.Windows.Controls;
using BookingApp.WPF.Desktop.ViewModels;
using BookingApp.WPF.DTO;

namespace BookingApp.WPF.Desktop.Views {
    /// <summary>
    /// Interaction logic for ReservationWindow.xaml
    /// </summary>
    public partial class TourReservationWindow : Window {
        public TouristReservationWindowViewModel TourReservationViewModel { get; set; }
        public TourReservationWindow(TourDTO selectedTour, int userId)
        {
            InitializeComponent();
            TourReservationViewModel = new TouristReservationWindowViewModel(selectedTour, userId);
            DataContext = TourReservationViewModel;
        }

        private void OpenSameLocationsWindow() {
            SameLocationToursWindow sameLocationToursWindow = new SameLocationToursWindow(TourReservationViewModel.SelectedTour, this);
            sameLocationToursWindow.ShowDialog();
        }

        private void ShowNotEnoughSpaceMessage(int availableSpace) {
            string messageBoxOutput = "There is not enought space for all!\nAvailable space: " + availableSpace.ToString();
            MessageBox.Show(messageBoxOutput, "Title of the MessageBox", MessageBoxButton.OK);
        }

        private void HandleReservationRequest(int successIndicator) {
            if (successIndicator > 0)
                ShowNotEnoughSpaceMessage(successIndicator);
            else if (successIndicator == -1)
                OpenSameLocationsWindow();
            else
                this.Close();
        }

        private void ConfirmReservationButton_Click(object sender, RoutedEventArgs e) {
            HandleReservationRequest(TourReservationViewModel.MakeReservation());
        }

        private void AddPassengerButton_Click(object sender, RoutedEventArgs e) {
            TourReservationViewModel.AddPassenger();
        }

        private void AddTouristButton_Click(object sender, RoutedEventArgs e) {
            TourReservationViewModel.AddTourist();
        }

        private void RemovePassengerButton_Click(object sender, RoutedEventArgs e) {
            var button = (Button)sender;
            var passenger = (PassengerDTO)button.DataContext;

            TourReservationViewModel.RemovePassenger(passenger);
        }

        private void UseCouponsLink_Click(object sender, RoutedEventArgs e) {
            if (!TourReservationViewModel.IsVoucherSelected) {
                UseVouchersWindow useVouchersWindow = new UseVouchersWindow(TourReservationViewModel);
                useVouchersWindow.ShowDialog();
            }
            else {
                TourReservationViewModel.RemoveVoucher();
            }  
        }
    }
}
