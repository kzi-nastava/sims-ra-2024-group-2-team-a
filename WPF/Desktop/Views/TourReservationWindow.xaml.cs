using System.Windows;
using System.Windows.Controls;
using BookingApp.WPF.Desktop.ViewModels;
using BookingApp.WPF.DTO;

namespace BookingApp.WPF.Desktop.Views {
    /// <summary>
    /// Interaction logic for ReservationWindow.xaml
    /// </summary>
    public partial class TourReservationWindow : Window {
        private TouristHomePage? _touristHomePage;
        public TouristReservationWindowViewModel TourReservationViewModel { get; set; }
        public TourReservationWindow(TourDTO selectedTour, int userId, TouristHomePage? parentWindow)
        {
            InitializeComponent();

            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;

            this.Width = screenWidth * 0.7;
            this.Height = screenHeight * 0.7;

            TourReservationViewModel = new TouristReservationWindowViewModel(selectedTour, userId);
            DataContext = TourReservationViewModel;
            _touristHomePage = parentWindow;
        }

        private void OpenSameLocationsWindow() {
            SameLocationToursWindow sameLocationToursWindow = new SameLocationToursWindow(TourReservationViewModel.SelectedTour, this);
            sameLocationToursWindow.ShowDialog();
        }

        private void HandleReservationRequest(int successIndicator) {
            if (successIndicator > 0)
                App.NotificationService.ShowError("There is not enought space for all!\nAvailable space: " + successIndicator.ToString());
            else if (successIndicator == -1)
                OpenSameLocationsWindow();
            else if (successIndicator == 0) {
                App.NotificationService.ShowSuccess("Reservation successful!");
                _touristHomePage.Update();
                this.Close();
            }
        }

        private void ConfirmReservationButton_Click(object sender, RoutedEventArgs e) {
            HandleReservationRequest(TourReservationViewModel.MakeReservation());
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
