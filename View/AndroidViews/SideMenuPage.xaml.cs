using BookingApp.Model;
using BookingApp.Repository;
using System;
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
            UsernameLabel.Content = user.Username;
            SetAverageAndSuperLabels(user.Id);
        }

        private void SetAverageAndSuperLabels(int userId) {
            OwnerRepository ownerRepository = new OwnerRepository();
            Owner owner = ownerRepository.GetByUserId(userId);
            AverageGradeLabel.Content = owner.AverageGrade.ToString();
            if (owner.IsSuper) {
                SuperLabel.Content = "Super";
            }
            else {
                SuperLabel.Content = "Normal";
            }

        }

        private void AccommodationsButton_Click(object sender, RoutedEventArgs e) {
            MainFrame.Content = new AccommodationPage(MainFrame, _user);
            MainFrame.Opacity = 1;
            MainFrame.IsHitTestVisible = true;
            SideFrame.Content = null;
            HeaderLabel.Content = "My accommodations and statistics";
        }

        private void InboxButton_Click(object sender, RoutedEventArgs e) {
            MainFrame.Content = new NotificationsPage(_user);
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

        private void LogOutButton_Click(object sender, RoutedEventArgs e) {
            SignInForm signInForm = new SignInForm();
            Window.GetWindow(this).Close();
            signInForm.ShowDialog();
        }
    }
}
