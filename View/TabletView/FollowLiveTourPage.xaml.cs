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
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookingApp.View.TabletView
{
    /// <summary>
    /// Interaction logic for FollowLiveTourPage.xaml
    /// </summary>
    public partial class FollowLiveTourPage : Page
    {
        private int _userId;

        private readonly TourRepository _tourRepository;
        private readonly LocationRepository _locationRepository;
        private readonly LanguageRepository _languageRepository;
        public TourDTO tourDTO { get; set; }
        public LocationDTO selectedLocationDTO { get; set; }
        public LanguageDTO selectedLanguageDTO { get; set; }
        public ObservableCollection<LocationDTO> locationDTOs { get; set; }
        public ObservableCollection<LanguageDTO> languageDTOs { get; set; }
        public ObservableCollection<TourDTO> tourDTOs { get; set; }
        public FollowLiveTourPage(int userId)
        {
            InitializeComponent();
            DataContext = this;
            _userId = userId;

            _tourRepository = new TourRepository();
            _locationRepository = new LocationRepository();
            _languageRepository = new LanguageRepository();

            locationDTOs = new ObservableCollection<LocationDTO>();
            languageDTOs = new ObservableCollection<LanguageDTO>();
            tourDTOs = new ObservableCollection<TourDTO>();

            foreach (var lan in _languageRepository.GetAll()) {
                languageDTOs.Add(new LanguageDTO(lan));
            }
            foreach (var loc in _locationRepository.GetAll()) {
                locationDTOs.Add(new LocationDTO(loc));
            }
            LoadLiveTours();
            
        }
        private void LoadLiveTours() {
            foreach (var tour in _tourRepository.GetLive(_userId)) {
                TourDTO tempTourDTO = new TourDTO(tour);
                Location location = _locationRepository.GetById(tour.LocationId);
                string city = location.City;
                string country = location.Country;
                tempTourDTO.setLocationTemplate(city, country);
                tempTourDTO.LanguageTemplate = _languageRepository.GetById(tour.LanguageId).Name;
                tourDTOs.Add(tempTourDTO);
            }
        }

        private void filterButton_Click(object sender, RoutedEventArgs e) {
            string name = textBoxName.Text;
            LocationDTO location = (LocationDTO)comboBoxLocation.SelectedItem;
            LanguageDTO language = (LanguageDTO)comboBoxLanguage.SelectedItem;
            if(!int.TryParse(textBoxTouristNumber.Text, out int touristsNumber)) {
                textBoxTouristNumber.Text = string.Empty;
            }
            if (!int.TryParse(textBoxDuration.Text, out int duration)) {
                textBoxDuration.Text = string.Empty;
            }
            TourFilterDTO filter = new TourFilterDTO(name, duration, touristsNumber, location, language);

            tourDTOs = new ObservableCollection<TourDTO>();
            foreach(var tour in _tourRepository.GetFiltered(filter)) {
                tourDTOs.Add(new TourDTO(tour));
            }

            itemsControlLiveTours.ItemsSource = tourDTOs;
        }

        private void clearButton_Click(object sender, RoutedEventArgs e) {
            textBoxName.Text = string.Empty;
            textBoxTouristNumber.Text = string.Empty;
            textBoxDuration.Text = string.Empty;
            comboBoxLanguage.SelectedIndex = 0;
            comboBoxLocation.SelectedIndex = 0;
        }
    }
}
