using BookingApp.Domain.Model;
using BookingApp.Services;
using System.Windows;
using System.Windows.Controls;

namespace BookingApp.WPF.Android.Views {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public Frame MainFrame { get; set; }

        public Frame SideFrame { get; set; }

        public NotificationService notificationService = new NotificationService(); 

        public AccommodationRenovationService renovationService = ServicesPool.GetService<AccommodationRenovationService>();

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
        }
        private void HamburgerButton_Click(object sender, RoutedEventArgs e) {
            sideFrame.Content = new SideMenuPage(MainFrame, SideFrame, blackFrame ,_user);
            blackFrame.Content = new BlackPage();
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e) {
            MainFrame.Content = new AccommodationPage(MainFrame, _user);
            SideFrame.Content = null;
            blackFrame.Content = null;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e) {
            if (MainFrame.CanGoBack) {
                mainFrame.GoBack();
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
    }
}
