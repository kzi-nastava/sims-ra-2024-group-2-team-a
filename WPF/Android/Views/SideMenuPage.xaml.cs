using BookingApp.Domain.Model;
using BookingApp.Services;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
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
        public string MessagesNumLabel { get; set; }
        public SideMenuPage(Frame mainFrame, Frame sideFrame, Frame blackFrame, User user) {
            InitializeComponent();
            DataContext = this;

            MainFrame = mainFrame;
            SideFrame = sideFrame;
            _user = user;
            UsernameLabel.Content = user.Username;
            SetAverageAndSuperLabels(user.Id);
            BlackFrame = blackFrame;
            SetMessagesNumber();
        }

        private void SetMessagesNumber() {
            NotificationService notificationService = ServicesPool.GetService<NotificationService>();
            MessagesNumLabel += "(";
            MessagesNumLabel += notificationService.GetByUserId(_user.Id).Where(x=> !x.IsRead).ToList().Count;
            MessagesNumLabel += ")";
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
            HideSideFrame();
            BlackFrame.Content = null;
        }

        private void InboxButton_Click(object sender, RoutedEventArgs e) {
            MainFrame.Content = new NotificationsPage(_user);
            HideSideFrame();
            BlackFrame.Content = null;
        }

        private void ReservationsButton_Click(object sender, RoutedEventArgs e) {
            MainFrame.Content = new ReservationReviewsPage(_user);
            HideSideFrame();
            BlackFrame.Content = null;
        }

        private void RenovationsButton_Click(object sender, RoutedEventArgs e) {
            MainFrame.Content = new AllRenovationsPage(_user);
            HideSideFrame();
            BlackFrame.Content = null;
        }

        private void LogOutButton_Click(object sender, RoutedEventArgs e) {
            SignInForm signInForm = new SignInForm();
            Window.GetWindow(this).Close();
            signInForm.ShowDialog();
        }

        private void HideSideFrame() {
            ThicknessAnimation animation = new ThicknessAnimation();
            animation.From = new Thickness(0);
            animation.To = new Thickness(-SideFrame.ActualWidth, 0, SideFrame.ActualWidth, 0);
            animation.Duration = TimeSpan.FromSeconds(0.4);
            animation.Completed += (s, e) => {
                SideFrame.Content = null;
            };
            SideFrame.BeginAnimation(Frame.MarginProperty, animation);
        }

        private void ForumsButton_Click(object sender, RoutedEventArgs e) {
            MainFrame.NavigationService.Navigate(new ForumsPage(_user, MainFrame));
            HideSideFrame();
            BlackFrame.Content = null;
        }
    }
}
