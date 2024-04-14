using BookingApp.Model;
using System.Windows;
using System.Windows.Controls;

namespace BookingApp.WPF.Web.Views {
    /// <summary>
    /// Interaction logic for GuestMainWindow.xaml
    /// </summary>
    public partial class GuestMainWindow : Window {

        public User User { get; set; }

        public Frame MainFrame { get; set; }

        public GuestMainWindow(User user) {
            InitializeComponent();
            SetWindowProperties();
            User = user;
            MainFrame = mainFrame;
            MainFrame.Content = new BookingPage();
        }

        private void SetWindowProperties() {
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void ButtonBookClick(object sender, RoutedEventArgs e) {
            mainFrame.Content = new BookingPage();
        }

        private void ButtonReservationsClick(object sender, RoutedEventArgs e) {
            mainFrame.Content = new ReservationsPage(User.Id);
        }

        private void ButtonLogoutClick(object sender, RoutedEventArgs e) {
            SignInForm signInForm = new SignInForm();
            signInForm.Show();
            this.Close();
        }

        public void ButtonBackClick(object sender, RoutedEventArgs e) {
            mainFrame.GoBack();
        }
    }
}
