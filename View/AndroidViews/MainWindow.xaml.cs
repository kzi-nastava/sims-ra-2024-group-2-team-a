using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BookingApp.Model;
using BookingApp.Repository;

namespace BookingApp.View.AndroidViews
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public Frame MainFrame { get; set; }

        public Frame SideFrame { get; set; }

        public Frame SmallNotificationFrame { get; set; }

        private readonly User _user;
        public MainWindow(User user) {
            InitializeComponent();
            MainFrame = mainFrame;
            SideFrame = sideFrame;
            _user = user;
            MainFrame.Content = new AccommodationPage(MainFrame, _user);
            SideFrame.Content = null;
            SmallNotificationFrame = smallNotificationFrame;

            int ungradedReservations = CheckForNotGradedReservations();
            if (ungradedReservations != 0) {
                SmallNotificationFrame.Content = new SmallNotificationPage(SmallNotificationFrame, ungradedReservations);
            }

        }

        public int CheckForNotGradedReservations() {
            AccommodationReservationRepository accResRepository = new AccommodationReservationRepository();
            ReviewRepository reviewRepository = new ReviewRepository();
            int counter = 0;

            foreach (var reservation in accResRepository.GetAll()) {
                if(!CheckReservationOwner(reservation.IdAccommodation))
                    continue;

                if (reviewRepository.GetByReservationId(reservation.Id) == null && CheckReservationDate(reservation)) {
                    counter++;
                } 
            }

            return counter;
        }

        private bool CheckReservationDate(AccommodationReservation reservation) {
            DateOnly currentDate = DateOnly.FromDateTime(DateTime.Now);
            DateOnly offsetDate = reservation.EndDate;
            offsetDate = offsetDate.AddDays(5);
            if (currentDate > reservation.EndDate && currentDate < offsetDate)
                return true;
            return false;
        }
        private bool CheckReservationOwner(int accommodationId) {
            AccommodationRepository accommodationRepository = new AccommodationRepository();
            if (accommodationRepository.GetById(accommodationId).OwnerId == _user.Id)
                return true;
            return false;
        }
        private void HamburgerButton_Click(object sender, RoutedEventArgs e) {
                sideFrame.Content = new SideMenuPage(MainFrame,SideFrame,_user,HeaderLabel);
                mainFrame.IsHitTestVisible = false;
                mainFrame.Opacity = 0.4;
        }

        private void sideFrame_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e) {

        }
    }
}
