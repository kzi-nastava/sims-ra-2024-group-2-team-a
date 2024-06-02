using System.Windows;

namespace BookingApp.WPF.Desktop.Views {
    /// <summary>
    /// Interaction logic for TouristMainWindow.xaml
    /// </summary>
    public partial class TouristMainWindow : Window {
        public int UserId { get; set; }
        public TouristMainWindow(int userId) {
            InitializeComponent();
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;

            this.Width = screenWidth * 0.85;
            this.Height = screenHeight * 0.85;

            DataContext = this;
            UserId = userId;
            PageFrame.Navigate(new TouristHomePage(UserId));
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e) {
            PageFrame.Navigate(new TouristHomePage(UserId));
        }

        private void ReservationsButton_Click(object sender, RoutedEventArgs e) {
            PageFrame.Navigate(new TouristReservationsPage(UserId));
        }

        private void CouponsButton_Click(object sender, RoutedEventArgs e) {
            PageFrame.Navigate(new TouristVouchersPage(UserId));
        }

        private void NotificationsButton_Click(object sender, RoutedEventArgs e) {
            PageFrame.Navigate(new TouristNotificationsPage(UserId));
        }

        private void RequestsButton_Click(object sender, RoutedEventArgs e) {
            PageFrame.Navigate(new RequestsPage(UserId));
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e) {
            SettingsWindow settingsWindow = new SettingsWindow(UserId);
            settingsWindow.Owner = this;
            settingsWindow.ShowDialog();
        }
    }
}
