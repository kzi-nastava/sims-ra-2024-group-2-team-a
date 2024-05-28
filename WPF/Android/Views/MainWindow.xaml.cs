using BookingApp.Domain.Model;
using BookingApp.Services;
using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;

namespace BookingApp.WPF.Android.Views {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public Frame MainFrame { get; set; }
        public Frame SideFrame { get; set; }

        public Frame DemoFrame { get; set; }

        public NotificationService notificationService = ServicesPool.GetService<NotificationService>(); 

        public AccommodationRenovationService renovationService = ServicesPool.GetService<AccommodationRenovationService>();

        public int RedIconVisibility { get; set; }

        private readonly User _user;
        public MainWindow(User user) {
            InitializeComponent();
            MainFrame = mainFrame;
            SideFrame = sideFrame;
            DemoFrame = demoFrame;
            _user = user;
            MainFrame.Content = new AccommodationPage(MainFrame, _user);
            SideFrame.Content = null;
            DemoFrame.Content = null;

            notificationService.CreateNotifications(_user.Id);
            renovationService.UpdateAllPendingRenovations();

            if (notificationService.GetByUserId(_user.Id).Where(x => !x.IsRead).ToList().Count > 0)
                redIcon.Visibility = Visibility.Visible;
            else
                redIcon.Visibility = Visibility.Hidden;
        }
        private void HamburgerButton_Click(object sender, RoutedEventArgs e) {
            SideFrame.Content = new SideMenuPage(MainFrame, SideFrame, blackFrame ,_user);
            ShowSideFrame();
        }
        private void ShowSideFrame() {
            ThicknessAnimation animation = new ThicknessAnimation();
            animation.From = new Thickness(-SideFrame.ActualWidth, 0, SideFrame.ActualWidth, 0);
            animation.To = new Thickness(0);
            animation.Duration = TimeSpan.FromSeconds(0.4);
            animation.Completed += (s, e) => {
                blackFrame.Content = new BlackPage();
            };
            SideFrame.BeginAnimation(Frame.MarginProperty, animation);
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e) {
            AccommodationPage newPage = new AccommodationPage(MainFrame, _user);
            MainFrame.Content = newPage;
            if(SideFrame.Content != null)
                HideSideFrame();

            ClearNavigationHistory();

            DoubleAnimation newAnimation = new DoubleAnimation();
            newAnimation.From = 0.0;
            newAnimation.To = 1.0;
            newAnimation.Duration = TimeSpan.FromSeconds(0.3);
            newPage.BeginAnimation(UIElement.OpacityProperty, newAnimation);
        }

        private void ClearNavigationHistory() {
            if (MainFrame.NavigationService.CanGoBack) {
                while (MainFrame.NavigationService.RemoveBackEntry() != null) { }
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e) {
            Page currentPage = MainFrame.Content as Page;

            if (SideFrame.Content != null)
                HideSideFrame();

            if (MainFrame.CanGoBack && currentPage != null) {
                ThicknessAnimation animation = new ThicknessAnimation();
                animation.From = new Thickness(0, 0, 0, 0);
                animation.To = new Thickness(currentPage.ActualWidth, 0, -currentPage.ActualWidth, 0);
                animation.Duration = TimeSpan.FromSeconds(0.15);
                animation.Completed += (s, e) => {
                    MainFrame.GoBack();
                };
                currentPage.BeginAnimation(Page.MarginProperty, animation);
            }
        }

        private void mainFrame_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e) {
            switch (e.Content as Page) {
                case AccommodationPage: {
                        AnimateLabelContentChange("My accommodations and statistics");
                        break;
                    }
                case ReservationReviewsPage: {
                        AnimateLabelContentChange("Reservations and reviews");
                        break;
                    }
                case NotificationsPage: {
                        AnimateLabelContentChange("Inbox");
                        break;
                    }
                case AllRenovationsPage: {
                        AnimateLabelContentChange("Renovations");
                        break;
                    }
                case ForumsPage: {
                        AnimateLabelContentChange("Forums");
                        break;
                    }
            }
        }
        private void AnimateLabelContentChange(string newContent) {
            DoubleAnimation animation = new DoubleAnimation();
            animation.From = -20;
            animation.To = 0;
            animation.Duration = TimeSpan.FromSeconds(0.4);

            TranslateTransform transform = new TranslateTransform();
            HeaderLabel.RenderTransform = transform;
            HeaderLabel.RenderTransform.BeginAnimation(TranslateTransform.XProperty, animation);

            HeaderLabel.Content = newContent;
        }

        private void MenuButton_Click(object sender, RoutedEventArgs e) {
            if (SideFrame.Content == null) {
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
        private void DemoButton_Click(object sender, RoutedEventArgs e) {
            if (MainFrame.Content is IDemo && MainFrame.Content != null) {
                DemoFrame.Content = new DemoPage(DemoFrame);

                IDemo demoPage = MainFrame.Content as IDemo;
                demoPage.StartDemo();
            }
        }

        private void demoFrame_Navigated(object sender, NavigationEventArgs e) {
            if (DemoFrame.Content == null) {
                IDemo demoPage = MainFrame.Content as IDemo;
                if(demoPage != null)
                    demoPage.StopDemo();
            }
        }
    }
}
