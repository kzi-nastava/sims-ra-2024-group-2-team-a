using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace BookingApp.View.DesktopViews {
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class TouristHomePage : Page {
        private readonly TourRepository _tourRepository;
        private readonly LocationRepository _locationRepository;
        private readonly LanguageRepository _languageRepository;

        public int UserId { get; set; } 
        public TourFilterDTO Filter { get; set; }
        public ObservableCollection<TourDTO> ToursOnDisplay { get; set; }
        public ObservableCollection<LocationDTO> LocationOptions { get; set; }
        public ObservableCollection<LanguageDTO> LanguageOptions { get; set; }
        public TouristHomePage(int userId) {
            InitializeComponent();
            DataContext = this;

            _tourRepository = new TourRepository();
            _locationRepository = new LocationRepository();
            _languageRepository = new LanguageRepository();
            ToursOnDisplay = new ObservableCollection<TourDTO>();
            LocationOptions = new ObservableCollection<LocationDTO>();
            LanguageOptions = new ObservableCollection<LanguageDTO>();
            Filter = new TourFilterDTO();

            FillComboBoxItemSources();
            Update();

            UserId = userId;
        }

        private void LoadLocations() {
            LocationOptions.Clear();
            LocationOptions.Add(new LocationDTO("", ""));
            foreach (var location in _locationRepository.GetAll())
                LocationOptions.Add(new LocationDTO(location));
        }

        private void LoadLanguages() {
            LanguageOptions.Clear();
            LanguageOptions.Add(new LanguageDTO(""));
            foreach (var language in _languageRepository.GetAll())
                LanguageOptions.Add(new LanguageDTO(language));
        }

        private void FillComboBoxItemSources() {
            LoadLocations();
            LoadLanguages(); 
        }

        private TourDTO GetPresentableTour(Tour tour) {
            TourDTO tourDTO = new TourDTO(tour);
            Location location = _locationRepository.GetById(tourDTO.LocationId);
            Language language = _languageRepository.GetById(tourDTO.LanguageId);
            tourDTO.setLocationTemplate(location.City, location.Country);
            tourDTO.SetLanguageTemplate(language.Name);

            return tourDTO;
        }

        public void Update() {
            ToursOnDisplay.Clear();
            foreach (var tour in _tourRepository.GetFiltered(Filter)) {
                ToursOnDisplay.Add(GetPresentableTour(tour));
            }
        }
        private void LocationComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e) {
            if (LocationComboBox.SelectedItem != null) {
                Filter.Location = (LocationDTO)LocationComboBox.SelectedItem;
                return;
            }
            Filter.Location = new LocationDTO();
        }

        private void FilterButton_Click(object sender, RoutedEventArgs e) {
            Update();
        }

        private void ClearFilterFields() {
            LocationComboBox.SelectedIndex = -1;
            LanguageComboBox.SelectedIndex = -1;
            DurationTextBox.Text = Convert.ToString(0);
            TouristNumberTextBox.Text = Convert.ToString(0);
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e) {
            ClearFilterFields();
            Update();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void ReservationButton_Click(object sender, RoutedEventArgs e) {
            var button = (Button)sender;
            var selectedTour = (TourDTO)button.DataContext;

            TourReservationWindow reservationWindow = new TourReservationWindow(selectedTour, UserId);
            reservationWindow.ShowDialog();
        }

        private void LanguageComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e) {
            if (LanguageComboBox.SelectedItem != null) {
                Filter.Language = (LanguageDTO)LanguageComboBox.SelectedItem;
                return;
            }
            Filter.Language = new LanguageDTO();
        }
    }
}
