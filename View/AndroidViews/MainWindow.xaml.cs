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

        public Frame SmallNotificationFrame { get; set; }

        public NotificationService notificationService = new NotificationService(); 

        private readonly User _user;
        public MainWindow(User user) {
            InitializeComponent();
            MainFrame = mainFrame;
            SideFrame = sideFrame;
            _user = user;
            MainFrame.Content = new AccommodationPage(MainFrame, _user);
            SideFrame.Content = null;
            SmallNotificationFrame = smallNotificationFrame;

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
            sideFrame.Content = new SideMenuPage(MainFrame, SideFrame, _user, HeaderLabel);
            mainFrame.IsHitTestVisible = false;
            mainFrame.Opacity = 0.4;
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e) {
            MainFrame.Content = new AccommodationPage(MainFrame, _user);
            MainFrame.Opacity = 1;
            MainFrame.IsHitTestVisible = true;
            SideFrame.Content = null;
            HeaderLabel.Content = "My accommodations and statistics";
        }
    }
}
