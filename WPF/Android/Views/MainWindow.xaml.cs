using BookingApp.Domain.Model;
using BookingApp.Services;
using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Navigation;

namespace BookingApp.WPF.Android.Views {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public Frame MainFrame { get; set; }

        public Frame SideFrame { get; set; }

        public NotificationService notificationService = ServicesPool.GetService<NotificationService>(); 

        public AccommodationRenovationService renovationService = ServicesPool.GetService<AccommodationRenovationService>();

        public int RedIconVisibility { get; set; }

        private readonly User _user;
        public MainWindow(User user) {
            InitializeComponent();
            MainFrame = mainFrame;
            SideFrame = sideFrame;
            _user = user;
            MainFrame.Content = new AccommodationPage(MainFrame, _user);
            SideFrame.Content = null;

            notificationService.CreateNotifications(_user.Id);
            renovationService.UpdateAllPendingRenovations();

            if (notificationService.GetByUserId(_user.Id).Where(x => !x.IsRead).ToList().Count > 0)
                redIcon.Visibility = Visibility.Visible;
            else
                redIcon.Visibility = Visibility.Hidden;
        }
        private void HamburgerButton_Click(object sender, RoutedEventArgs e) {
            sideFrame.Content = new SideMenuPage(MainFrame, SideFrame, blackFrame ,_user);
            ShowSideFrame();
        }
        private void ShowSideFrame() {
            ThicknessAnimation animation = new ThicknessAnimation();
            animation.From = new Thickness(-sideFrame.ActualWidth, 0, sideFrame.ActualWidth, 0);
            animation.To = new Thickness(0);
            animation.Duration = TimeSpan.FromSeconds(0.4);
            animation.Completed += (s, e) => {
                blackFrame.Content = new BlackPage();
            };
            sideFrame.BeginAnimation(Frame.MarginProperty, animation);
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e) {
            Page currentPage = MainFrame.Content as Page;
            if (currentPage == null)
                return;

            DoubleAnimation currentAnimation = new DoubleAnimation();
            currentAnimation.From = 1.0;
            currentAnimation.To = 0.0;
            currentAnimation.Duration = TimeSpan.FromSeconds(0.3);
            currentPage.BeginAnimation(UIElement.OpacityProperty, currentAnimation);

            AccommodationPage newPage = new AccommodationPage(MainFrame, _user);
            MainFrame.Content = newPage;
            HideSideFrame();

            DoubleAnimation newAnimation = new DoubleAnimation();
            newAnimation.From = 0.0;
            newAnimation.To = 1.0;
            newAnimation.Duration = TimeSpan.FromSeconds(0.3);
            newPage.BeginAnimation(UIElement.OpacityProperty, newAnimation);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e) {
            Page currentPage = mainFrame.Content as Page;
            if (MainFrame.CanGoBack && currentPage != null) {
                ThicknessAnimation animation = new ThicknessAnimation();
                animation.From = new Thickness(0, 0, 0, 0);
                animation.To = new Thickness(currentPage.ActualWidth, 0, -currentPage.ActualWidth, 0);
                animation.Duration = TimeSpan.FromSeconds(0.15);
                animation.Completed += (s, e) => {
                    mainFrame.GoBack();
                };
                currentPage.BeginAnimation(Page.MarginProperty, animation);
            }
        }

        private void mainFrame_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e) {
            switch (e.Content as Page) {
                case AccommodationPage: {
                        HeaderLabel.Content = "My accommodations and statistics";
                        break;
                    }
                case ReservationReviewsPage: {
                        HeaderLabel.Content = "Reservations";
                        break;
                    }
                case NotificationsPage: {
                        HeaderLabel.Content = "Inbox";
                        break;
                    }
            }
        }

        private void MenuButton_Click(object sender, RoutedEventArgs e) {
            if (sideFrame.Content == null) {
                HamburgerButton_Click(sender, e);
            }
            else {
                HideSideFrame();
            }
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
            blackFrame.Content = null;
        }


    }
}
