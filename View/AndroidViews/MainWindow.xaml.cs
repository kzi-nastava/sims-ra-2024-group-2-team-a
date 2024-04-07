using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Services;
using System;
using System.Security.AccessControl;
using System.Windows;
using System.Windows.Controls;

namespace BookingApp.View.AndroidViews {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public Frame MainFrame { get; set; }

        public Frame SideFrame { get; set; }

        public NotificationService notificationService = new NotificationService(); 

        private readonly User _user;
        public MainWindow(User user) {
            InitializeComponent();
            MainFrame = mainFrame;
            SideFrame = sideFrame;
            _user = user;
            MainFrame.Content = new AccommodationPage(MainFrame, _user);
            SideFrame.Content = null;

            CreateNotifications();
        }

        public void CreateNotifications() {
            RescheduleRequestService rescheduleRequestService = new RescheduleRequestService(); 

            int ungradedReservations = CheckForNotGradedReservations();
            int pendingRescheduleRequests = rescheduleRequestService.GetPendingRequestsByOwnerId(_user.Id).Count;

            if (ungradedReservations != 0) {
                string message = $"You have {ungradedReservations} ungraded reservations. Navigate to reservations tab to grade them!";
                Notification notification = new Notification(message, NotificationCategory.Review, _user.Id, DateTime.Now, false);
                notificationService.Save(notification);
            }
            if (pendingRescheduleRequests > 0) {
                string message = $"You have {pendingRescheduleRequests} pending reschedule requests. Navigate to reservations/requests tab to accept/decline them!";
                Notification notification = new Notification(message, NotificationCategory.Request, _user.Id, DateTime.Now, false);
                notificationService.Save(notification);
            }
        }

        public int CheckForNotGradedReservations() {
            AccommodationReservationService accResService = new AccommodationReservationService();
            ReviewService reviewService = new ReviewService();
            int counter = 0;

            foreach (var reservation in accResService.GetAll()) {
                if (!CheckReservationOwner(reservation.AccommodationId))
                    continue;

                if (reviewService.GetByReservationId(reservation.Id) == null && CheckReservationDate(reservation))
                    counter++;
            }

            return counter;
        }

        private bool CheckReservationDate(AccommodationReservation reservation) {
            DateOnly currentDate = DateOnly.FromDateTime(DateTime.Now);
            DateOnly offsetDate = reservation.EndDate;
            offsetDate = offsetDate.AddDays(5);

            return (currentDate > reservation.EndDate && currentDate < offsetDate);
        }
        private bool CheckReservationOwner(int accommodationId) {
            AccommodationService accommodationService = new AccommodationService();
            return (accommodationService.GetById(accommodationId).OwnerId == _user.Id);
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
