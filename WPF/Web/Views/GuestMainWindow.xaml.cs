using BookingApp.Domain.Model;
using BookingApp.Services;
using System.Windows;
using System.Windows.Controls;

namespace BookingApp.WPF.Web.Views {
    /// <summary>
    /// Interaction logic for GuestMainWindow.xaml
    /// </summary>
    public partial class GuestMainWindow : Window {

        public int GuestId { get; }

        public Frame MainFrame { get; set; }

        public GuestMainWindow(int guestId) {
            InitializeComponent();
            SetWindowProperties();
            GuestId = guestId;
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
            mainFrame.Content = new ReservationsPage(GuestId);
        }

        private void ButtonReviewsClick(object sender, RoutedEventArgs e) {
            mainFrame.Content = new ReviewsPage(GuestId);
        }

        private void ButtonForumsClick(object sender, RoutedEventArgs e) {
            mainFrame.Content = new ForumsPage();
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
