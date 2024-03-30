using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Repository;
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

        private List<AccommodationReservationDTO> _reservationDTOs = new List<AccommodationReservationDTO>();
        private List<AccommodationDTO> _accommodationDTOs = new List<AccommodationDTO>();
        private List<LocationDTO> _locationDTOs = new List<LocationDTO>();

        private readonly AccommodationReservationRepository _reservationsRepository = new AccommodationReservationRepository();
        private readonly AccommodationRepository _accommodationRepository = new AccommodationRepository();
        private readonly LocationRepository _locationRepository = new LocationRepository();

        private bool ScheduledSelected = true;

        public ReservationsPage() {
            InitializeComponent();
            Update();
        }

        public void UpdateLocationDTOs() {
            var locations = _locationRepository.GetAll();
            _locationDTOs = locations.Select(l => new LocationDTO(l)).ToList();
        }

        public void UpdateAccommodationDTOs() {
            var accommodations = _accommodationRepository.GetAll();
            _accommodationDTOs = accommodations.Select(a => new AccommodationDTO(a)).ToList();

            foreach (var acc in _accommodationDTOs) {
                var loc = _locationDTOs.FirstOrDefault(l => l.Id == acc.LocationId);
                acc.SetDisplayLocation(loc.City, loc.Country);
            }
        }

        public void Update() {
            UpdateLocationDTOs();
            UpdateAccommodationDTOs();
            UpdateReservationDTOs();

            if (ScheduledSelected) {
                _reservationDTOs = _reservationDTOs.Where(x => !x.HasExpired).OrderByDescending(x => x.Id).ToList();
            }
            else {
                _reservationDTOs = _reservationDTOs.Where(x => x.HasExpired).OrderByDescending(x => x.Id).ToList();
            }

            itemsControlReservations.ItemsSource = _reservationDTOs;
        }

        public void UpdateReservationDTOs() {
            var reservations = _reservationsRepository.GetAll();
            _reservationDTOs = reservations.Select(r => new AccommodationReservationDTO(r)).ToList();

            foreach(var res in _reservationDTOs) {
                var acc = _accommodationDTOs.FirstOrDefault(a => a.Id == res.AccommodationId);
                res.AccommodationName = acc.Name;
                res.AccommodationType = acc.Type;
                res.AccommodationLocation = acc.DisplayLocation;
                res.LastCancellationDay = acc.LastCancellationDay;
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

        public void CloseModalDialog() {
            rectBlurBackground.Visibility = Visibility.Hidden;
            mainGrid.Children.RemoveAt(mainGrid.Children.Count - 1);
        }
    }
}
