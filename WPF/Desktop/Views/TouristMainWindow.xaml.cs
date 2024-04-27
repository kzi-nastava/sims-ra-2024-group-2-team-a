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

            this.Width = screenWidth * 0.75;
            this.Height = screenHeight * 0.75;

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

        private void LogOutButton_Click(object sender, RoutedEventArgs e) {
            SignInForm signInForm = new SignInForm();
            signInForm.Show();
            this.Close();
        }

        private void RequestsButton_Click(object sender, RoutedEventArgs e) {
            
        }
    }
}
