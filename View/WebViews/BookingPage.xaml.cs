using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace BookingApp.View.WebViews {
    /// <summary>
    /// Interaction logic for BookingPage.xaml
    /// </summary>
    public partial class BookingPage : Page {
        private List<AccommodationDTO> _accommodationDTOs = new List<AccommodationDTO>();
        private List<LocationDTO> _locationDTOs = new List<LocationDTO>();

        private AccommodationRepository _accommodationRepository = new AccommodationRepository();
        private LocationRepository _locationRepository = new LocationRepository();

        public BookingPage(Frame mainFrame) {
            InitializeComponent();
            Update();
            SetItemSources();
        }

        public void Update() {
            var locations = _locationRepository.GetAll();
            _locationDTOs = locations.Select(l => new LocationDTO(l)).ToList();
            _locationDTOs.Insert(0, new LocationDTO());

            UpdateAccommodationDTOs(_accommodationRepository.GetAll());
        }

        public void UpdateAccommodationDTOs(List<Accommodation> accommodations) {
            _accommodationDTOs = accommodations.Select(a => new AccommodationDTO(a)).ToList();

            foreach (var acc in _accommodationDTOs) {
                var loc = _locationDTOs.FirstOrDefault(l => l.Id == acc.LocationId);
                acc.SetDisplayLocation(loc.City, loc.Country);
            }

            itemsControlAccommodations.ItemsSource = _accommodationDTOs;
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

            var accommodations = _accommodationRepository.GetFilteredAccommodations(filter);
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
