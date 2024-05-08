using BookingApp.Domain.Model;
using BookingApp.Services;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Xceed.Wpf.AvalonDock.Converters;

namespace BookingApp.WPF.Android.Views {
    /// <summary>
    /// Interaction logic for SideMenuPage.xaml
    /// </summary>
    public partial class SideMenuPage : Page {
        public Frame MainFrame { get; set; }

        public Frame SideFrame { get; set; }

        public Frame BlackFrame { get; set; }

        private User _user;
        public SideMenuPage(Frame mainFrame, Frame sideFrame, Frame blackFrame, User user) {
            InitializeComponent();
            MainFrame = mainFrame;
            SideFrame = sideFrame;
            _user = user;
            UsernameLabel.Content = user.Username;
            SetAverageAndSuperLabels(user.Id);
            BlackFrame = blackFrame;
        }

        private void SetAverageAndSuperLabels(int userId) {
            OwnerService ownerService = ServicesPool.GetService<OwnerService>(); 
            Owner owner = ownerService.GetByUserId(userId);
            AverageGradeLabel.Content = owner.AverageGrade.ToString();
            if (owner.IsSuper) {
                SuperImage.Source = new BitmapImage(new Uri("pack://application:,,,/BookingApp;component/Resources/Images/Icons/gold-star-icon.png"));
            }
            else {
                SuperImage.Source = new BitmapImage(new Uri("pack://application:,,,/BookingApp;component/Resources/Images/Icons/normal-owner-icon.png"));
                //SuperImage.Source = new BitmapImage(new Uri("pack://application:,,,/BookingApp;component/Resources/Images/Icons/gold-star-icon.png"));
            }
        }

        private void AccommodationsButton_Click(object sender, RoutedEventArgs e) {
            MainFrame.Content = new AccommodationPage(MainFrame, _user);
            SideFrame.Content = null;
            BlackFrame.Content = null;
        }

        private void InboxButton_Click(object sender, RoutedEventArgs e) {
            MainFrame.Content = new NotificationsPage(_user);
            SideFrame.Content = null;
            BlackFrame.Content = null;
        }

        private void ReservationsButton_Click(object sender, RoutedEventArgs e) {
            MainFrame.Content = new ReservationReviewsPage(_user);
            SideFrame.Content = null;
            BlackFrame.Content = null;
        }

        private void RenovationsButton_Click(object sender, RoutedEventArgs e) {
            MainFrame.Content = new AllRenovationsPage(_user);
            SideFrame.Content = null;
            BlackFrame.Content = null;
        }

        private void LogOutButton_Click(object sender, RoutedEventArgs e) {
            SignInForm signInForm = new SignInForm();
            Window.GetWindow(this).Close();
            signInForm.ShowDialog();
        }
    }
}
