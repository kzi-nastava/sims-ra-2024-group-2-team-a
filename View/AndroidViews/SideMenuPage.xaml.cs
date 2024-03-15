using BookingApp.Model;
using System.Windows;
using System.Windows.Controls;

namespace BookingApp.View.AndroidViews {
    /// <summary>
    /// Interaction logic for SideMenuPage.xaml
    /// </summary>
    public partial class SideMenuPage : Page {
        public Frame MainFrame { get; set; }

        public Frame SideFrame { get; set; }

        public Label HeaderLabel { get; set; }

        private User _user;
        public SideMenuPage(Frame mainFrame, Frame sideFrame, User user, Label label) {
            InitializeComponent();
            MainFrame = mainFrame;
            SideFrame = sideFrame;
            _user = user;
            HeaderLabel = label;
        }

        private void AccommodationsButton_Click(object sender, RoutedEventArgs e) {
            MainFrame.Content = new AccommodationPage(MainFrame, _user);
            MainFrame.Opacity = 1;
            MainFrame.IsHitTestVisible = true;
            SideFrame.Content = null;
            HeaderLabel.Content = "My accommodations and statistics";
        }

        private void InboxButton_Click(object sender, RoutedEventArgs e) {
            // MainFrame.Content = new AccommodationPage(MainFrame, _user);
            MainFrame.Opacity = 1;
            MainFrame.IsHitTestVisible = true;
            SideFrame.Content = null;
            HeaderLabel.Content = "Inbox";
        }

        private void ReservationsButton_Click(object sender, RoutedEventArgs e) {
            MainFrame.Content = new ReservationReviewsPage(_user);
            MainFrame.Opacity = 1;
            MainFrame.IsHitTestVisible = true;
            SideFrame.Content = null;
            HeaderLabel.Content = "Reservations";
        }

        private void RenovationsButton_Click(object sender, RoutedEventArgs e) {
            // MainFrame.Content = new AccommodationPage(MainFrame, _user);
            MainFrame.Opacity = 1;
            MainFrame.IsHitTestVisible = true;
            SideFrame.Content = null;
            HeaderLabel.Content = "Renovations";
        }
    }
}
