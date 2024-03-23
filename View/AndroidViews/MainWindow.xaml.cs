using BookingApp.Model;
using BookingApp.Repository;
using System;
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

        private readonly NotificationRepository _notificationRepository;

        private readonly User _user;
        public MainWindow(User user) {
            InitializeComponent();
            MainFrame = mainFrame;
            SideFrame = sideFrame;
            _user = user;
            MainFrame.Content = new AccommodationPage(MainFrame, _user);
            SideFrame.Content = null;
            SmallNotificationFrame = smallNotificationFrame;
            _notificationRepository = new NotificationRepository();

            int ungradedReservations = CheckForNotGradedReservations();
            if (ungradedReservations != 0) {
                //SmallNotificationFrame.Content = new SmallNotificationPage(SmallNotificationFrame, ungradedReservations);
                string message = $"You have {ungradedReservations} ungraded reservations. Navigate to reservations tab to grade them!";
                Notification notification = new Notification(message,NotificationCategory.Review,user.Id,DateTime.Now,false);
                _notificationRepository.Save(notification);
            }
        }

        public int CheckForNotGradedReservations() {
            AccommodationReservationRepository accResRepository = new AccommodationReservationRepository();
            ReviewRepository reviewRepository = new ReviewRepository();
            int counter = 0;

            foreach (var reservation in accResRepository.GetAll()) {
                if (!CheckReservationOwner(reservation.AccommodationId))
                    continue;

                if (reviewRepository.GetByReservationId(reservation.Id) == null && CheckReservationDate(reservation))
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
            AccommodationRepository accommodationRepository = new AccommodationRepository();
            return (accommodationRepository.GetById(accommodationId).OwnerId == _user.Id);
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
