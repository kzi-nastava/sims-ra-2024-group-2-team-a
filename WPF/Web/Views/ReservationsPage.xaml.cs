using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Services;
using BookingApp.WPF.Web.ViewModels;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookingApp.WPF.Web.Views {
    /// <summary>
    /// Interaction logic for ReservationsPage.xaml
    /// </summary>
    public partial class ReservationsPage : Page {

        public ReservationsPageViewModel ViewModel { get; set; }

        private readonly int _guestId;

        public ReservationsPage(int guestId) {
            InitializeComponent();
            _guestId = guestId;
            ViewModel = new ReservationsPageViewModel(_guestId);
            DataContext = ViewModel;
        }

        public void Update() {
            ViewModel.UpdateAllCollections();
        }

        private void ButtonScheduledClick(object sender, RoutedEventArgs e) {
            ViewModel.FilterBySelection(true);
        }

        private void ButtonExpiredClick(object sender, RoutedEventArgs e) {
            ViewModel.FilterBySelection(false);
        }

        public void OpenRescheduleDialog(AccommodationReservationDTO reservation) {
            rectBlurBackground.Visibility = Visibility.Visible;
            mainGrid.Children.Add(new RescheduleReservationModalDialog(this, reservation));
        }

        public void OpenReviewDialog(AccommodationReservationDTO reservation) {
            rectBlurBackground.Visibility = Visibility.Visible;
            mainGrid.Children.Add(new ReviewAccommodationModalDialog(this, reservation));
        }

        public void CloseModalDialog() {
            rectBlurBackground.Visibility = Visibility.Hidden;
            mainGrid.Children.RemoveAt(mainGrid.Children.Count - 1);
        }

    }
}
