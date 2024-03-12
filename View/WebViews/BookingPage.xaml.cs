using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
            SetItemSoruces();
        }

        public void Update() {
            var accommodations = _accommodationRepository.GetAll();
            var locations = _locationRepository.GetAll();

            _accommodationDTOs = accommodations.Select(a => new AccommodationDTO(a)).ToList();

            _locationDTOs = locations.Select(l => new LocationDTO(l)).ToList();
        }

        private void SetItemSoruces() {
            itemsControlAccommodations.ItemsSource = _accommodationDTOs;
            comboBoxLocation.ItemsSource = _locationDTOs;

            comboBoxType.ItemsSource = Enum.GetValues(typeof(AccommodationType));
            comboBoxType.SelectedItem = AccommodationType.none;
        }

        private void ButtonFilterClick(object sender, RoutedEventArgs e) {

            string name = textBoxName.Text;
            LocationDTO location = (comboBoxLocation.SelectedIndex == -1) ? new LocationDTO() : (LocationDTO) comboBoxLocation.SelectedItem;
            AccommodationType type = (AccommodationType) comboBoxType.SelectedItem;

            int guestNumber = 0;
            int.TryParse(textBoxGuests.Text, out guestNumber);
            int reservationDays = 0;
            int.TryParse(textBoxDays.Text, out reservationDays);

            AccommodationFilterDTO filter = new AccommodationFilterDTO(name, location, type, guestNumber, reservationDays);

            var accommodations = _accommodationRepository.GetFilteredAccommodations(filter);
            _accommodationDTOs = accommodations.Select(a => new AccommodationDTO(a)).ToList();

            itemsControlAccommodations.ItemsSource = _accommodationDTOs;
        }

        private void ButtonClearClick(object sender, RoutedEventArgs e) {
            comboBoxLocation.SelectedIndex = -1;
            comboBoxType.SelectedItem = AccommodationType.none;
            textBoxDays.Text = "";
            textBoxGuests.Text = "";
            textBoxName.Text = "";
        }
    }
}
