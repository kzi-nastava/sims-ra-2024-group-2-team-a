using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Services;
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

namespace BookingApp.View.WebViews {
    /// <summary>
    /// Interaction logic for ReservationsPage.xaml
    /// </summary>
    public partial class ReservationsPage : Page {

        private readonly LocationService _locationService = new LocationService();
        private readonly AccommodationService _accommodationService = new AccommodationService();
        private readonly AccommodationReservationService _reservationService = new AccommodationReservationService();
        private readonly RescheduleRequestService _rescheduleRequestService = new RescheduleRequestService();

        private List<LocationDTO> _locationDTOs = new List<LocationDTO>();
        private List<AccommodationDTO> _accommodationDTOs = new List<AccommodationDTO>();
        private List<AccommodationReservationDTO> _reservationDTOs = new List<AccommodationReservationDTO>();
        private List<RescheduleRequestDTO> _rescheduleRequestDTOs = new List<RescheduleRequestDTO>();

        private bool ScheduledSelected = true;

        public ReservationsPage() {
            InitializeComponent();
            Loaded += ReservationsPageLoaded;
        }

        private void ReservationsPageLoaded(object sender, RoutedEventArgs e) {
            Update();
        }

        public void UpdateLocationDTOs() {
            var locations = _locationService.GetAll();
            _locationDTOs = locations.Select(l => new LocationDTO(l)).ToList();
        }

        public void UpdateAccommodationDTOs() {
            var accommodations = _accommodationService.GetAll();
            _accommodationDTOs = accommodations.Select(a => new AccommodationDTO(a)).ToList();

            foreach (var acc in _accommodationDTOs) {
                var loc = _locationDTOs.Find(l => l.Id == acc.LocationId);
                acc.Location = loc;
            }
        }

        public void Update() {
            UpdateLocationDTOs();
            UpdateAccommodationDTOs();
            UpdateReservationDTOs();
            UpdateRescheduleRequestDTOs();

            if (ScheduledSelected) {
                _reservationDTOs = _reservationDTOs.Where(x => !x.HasExpired).OrderByDescending(x => x.Id).ToList();
            }
            else {
                _reservationDTOs = _reservationDTOs.Where(x => x.HasExpired).OrderByDescending(x => x.Id).ToList();
            }

            itemsControlReservations.ItemsSource = _reservationDTOs;
            itemsControlRescheduleRequests.ItemsSource = _rescheduleRequestDTOs;
        }

        public void UpdateReservationDTOs() {
            var reservations = _reservationService.GetAll();
            _reservationDTOs = reservations.Select(r => new AccommodationReservationDTO(r)).ToList();

            foreach(var res in _reservationDTOs) {
                var acc = _accommodationDTOs.FirstOrDefault(a => a.Id == res.AccommodationId);
                res.Accommodation = acc;
            }
        }
        
        public void UpdateRescheduleRequestDTOs() {
            var window = Window.GetWindow(this) as GuestMainWindow;
            int guestId = window.User.Id;

            var rescheduleRequests = _rescheduleRequestService.GetByGuestId(guestId);
            _rescheduleRequestDTOs = rescheduleRequests.Select(r => new RescheduleRequestDTO(r)).ToList();

            foreach(var req in _rescheduleRequestDTOs) {
                var res = _reservationDTOs.FirstOrDefault(res => res.Id == req.ReservationId);
                req.Reservation = res;
            }
        }

        private void ButtonScheduledClick(object sender, RoutedEventArgs e) {
            ScheduledSelected = true;
            Update();
        }

        private void ButtonExpiredClick(object sender, RoutedEventArgs e) {
            ScheduledSelected = false;
            Update();
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
