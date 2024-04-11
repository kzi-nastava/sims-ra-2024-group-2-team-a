using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookingApp.WPF.Tablet.Views {
    /// <summary>
    /// Interaction logic for ScheduledToursPage.xaml
    /// </summary>
    public partial class ScheduledToursPage : Page {

        private int _userId;

        private readonly TourRepository _tourRepository;
        private readonly LocationRepository _locationRepository;
        private readonly LanguageRepository _languageRepository;
        private TourFilterDTO _filterDTO;
        public TourDTO tourDTO { get; set; }
        public LocationDTO selectedLocationDTO { get; set; }
        public LanguageDTO selectedLanguageDTO { get; set; }
        public ObservableCollection<LocationDTO> locationDTOs { get; set; }
        public ObservableCollection<LanguageDTO> languageDTOs { get; set; }
        public ObservableCollection<TourDTO> tourDTOs { get; set; }
        public ScheduledToursPage(int userId) {
            InitializeComponent();
            _userId = userId;
            DataContext = this;

            _tourRepository = new TourRepository();
            _locationRepository = new LocationRepository();
            _languageRepository = new LanguageRepository();
            Load();
        }

        private void filterButton_Click(object sender, RoutedEventArgs e) {
            _filterDTO = SetFilter();

            tourDTOs.Clear();
            foreach (var tour in _tourRepository.GetFilteredScheduled(_filterDTO, _userId)) {
                TourDTO temp = new TourDTO(tour);
                setLocationLanguage(temp, tour);
                tourDTOs.Add(temp);
            }

            itemsControlScheduledTours.ItemsSource = tourDTOs;
        }

        private void clearButton_Click(object sender, RoutedEventArgs e) {
            textBoxName.Text = string.Empty;
            textBoxTouristNumber.Text = string.Empty;
            textBoxDuration.Text = string.Empty;
            textBoxTime.Text = string.Empty;
            comboBoxLanguage.SelectedIndex = 0;
            comboBoxLocation.SelectedIndex = 0;
            datePicker.SelectedDate = DateTime.MinValue;
        }
        private void Load() {
            locationDTOs = new ObservableCollection<LocationDTO>();
            languageDTOs = new ObservableCollection<LanguageDTO>();

            locationDTOs.Add(new LocationDTO());
            languageDTOs.Add(new LanguageDTO());

            foreach (var lan in _languageRepository.GetAll()) {
                languageDTOs.Add(new LanguageDTO(lan));
            }
            foreach (var loc in _locationRepository.GetAll()) {
                locationDTOs.Add(new LocationDTO(loc));
            }
            LoadScheduled();
        }

        private void LoadScheduled() {
            tourDTOs = new ObservableCollection<TourDTO>();
            foreach (var tour in _tourRepository.GetScheduled(_userId)) {
                TourDTO tempTourDTO = new TourDTO(tour);
                setLocationLanguage(tempTourDTO, tour);
                tourDTOs.Add(tempTourDTO);
            }
        }
        private void setLocationLanguage(TourDTO tempTourDTO, Tour tour) {
            Location location = _locationRepository.GetById(tour.LocationId);
            string city = location.City;
            string country = location.Country;
            tempTourDTO.setLocationTemplate(city, country);
            tempTourDTO.LanguageTemplate = _languageRepository.GetById(tour.LanguageId).Name;
        }

        private TourFilterDTO SetFilter() {
            string name = textBoxName.Text;
            LocationDTO location = (LocationDTO)comboBoxLocation.SelectedItem;
            if (location == null) {
                location = new LocationDTO();
            }
            LanguageDTO language = (LanguageDTO)comboBoxLanguage.SelectedItem;
            if (language == null) {
                language = new LanguageDTO();
            }
            if (!int.TryParse(textBoxTouristNumber.Text, out int touristsNumber)) {
                textBoxTouristNumber.Text = string.Empty;
            }
            if (!int.TryParse(textBoxDuration.Text, out int duration)) {
                textBoxDuration.Text = string.Empty;
            }

            DateOnly date = DateOnly.FromDateTime(datePicker.SelectedDate.Value);
            int time;
            int.TryParse(textBoxTime.Text, out time);
            DateTime beggining = new DateTime(date.Year, date.Month, date.Day, time, 0, 0);

            return new TourFilterDTO(name, duration, touristsNumber, location, language, beggining);
        }
    }
}
