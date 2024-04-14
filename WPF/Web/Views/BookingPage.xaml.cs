using BookingApp.Model;
using BookingApp.Services;
using BookingApp.WPF.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace BookingApp.WPF.Web.Views {
    /// <summary>
    /// Interaction logic for BookingPage.xaml
    /// </summary>
    public partial class BookingPage : Page {
        private List<AccommodationDTO> _accommodationDTOs = new List<AccommodationDTO>();
        private List<LocationDTO> _locationDTOs = new List<LocationDTO>();

        private readonly AccommodationService _accommodationService = new AccommodationService();
        private readonly LocationService _locationService = new LocationService();
        private readonly OwnerService _ownerService = new OwnerService();

        public BookingPage() {
            InitializeComponent();
            Update();
            SetItemSources();
        }

        public void Update() {
            UpdateLocationDTOs();
            UpdateAccommodationDTOs(_accommodationService.GetAll());
        }

        private void UpdateLocationDTOs() {
            var locations = _locationService.GetAll();
            _locationDTOs = locations.Select(l => new LocationDTO(l)).ToList();
            _locationDTOs.Insert(0, new LocationDTO());
        }

        public void UpdateAccommodationDTOs(List<Accommodation> accommodations) {
            _accommodationDTOs = accommodations.Select(a => new AccommodationDTO(a)).ToList();

            foreach (var acc in _accommodationDTOs) {
                var loc = _locationDTOs.Find(l => l.Id == acc.LocationId);
                acc.Location = loc;
            }

            _accommodationDTOs = _accommodationDTOs.OrderByDescending(a => IsAccommodationOwnerSuper(a.OwnerId)).ToList();
            itemsControlAccommodations.ItemsSource = _accommodationDTOs;
        }

        private bool IsAccommodationOwnerSuper(int ownerId) {
            return _ownerService.GetByUserId(ownerId).IsSuper;
        }

        private void SetItemSources() {
            itemsControlAccommodations.ItemsSource = _accommodationDTOs;

            comboBoxLocation.ItemsSource = _locationDTOs;
            comboBoxLocation.SelectedIndex = 0;

            comboBoxType.ItemsSource = Enum.GetValues(typeof(AccommodationType));
            comboBoxType.SelectedIndex = 0;
        }

        private void ButtonFilterClick(object sender, RoutedEventArgs e) {
            string name = textBoxName.Text;
            LocationDTO location = (LocationDTO)comboBoxLocation.SelectedItem;
            AccommodationType type = (AccommodationType)comboBoxType.SelectedItem;

            if (!int.TryParse(textBoxGuests.Text, out int guestNumber)) {
                textBoxGuests.Text = "";
            }

            if (!int.TryParse(textBoxDays.Text, out int reservationDays)) {
                textBoxDays.Text = "";
            }

            AccommodationFilterDTO filter = new AccommodationFilterDTO(name, location, type, guestNumber, reservationDays);

            var accommodations = _accommodationService.GetFilteredAccommodations(filter);
            UpdateAccommodationDTOs(accommodations);
        }

        private void ButtonClearClick(object sender, RoutedEventArgs e) {
            comboBoxLocation.SelectedIndex = 0;
            comboBoxType.SelectedIndex = 0;
            textBoxDays.Text = "";
            textBoxGuests.Text = "";
            textBoxName.Text = "";
        }
    }
}
