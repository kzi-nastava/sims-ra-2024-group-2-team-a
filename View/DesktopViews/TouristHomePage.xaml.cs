using BookingApp.DTO;
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

        public int UserId { get; set; } 
        public TourFilterDTO Filter { get; set; }
        public ObservableCollection<TourDTO> ToursOnDisplay { get; set; }
        public ObservableCollection<LocationDTO> LocationOptions { get; set; }
        public TouristHomePage(int userId) {
            InitializeComponent();

            DataContext = this;
            _tourRepository = new TourRepository();
            _locationRepository = new LocationRepository();
            ToursOnDisplay = new ObservableCollection<TourDTO>();
            LocationOptions = new ObservableCollection<LocationDTO>();

            Filter = new TourFilterDTO();
            LocationOptions.Clear();
            LocationOptions.Add(new LocationDTO("", ""));
            foreach (var location in _locationRepository.GetAll()) {
                LocationOptions.Add(new LocationDTO(location));
            }

            Update();
            UserId = userId;
        }
        public void Update() {
            ToursOnDisplay.Clear();
            foreach (var tour in _tourRepository.GetFiltered(Filter)) {
                ToursOnDisplay.Add(new TourDTO(tour));
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
        private void ClearButton_Click(object sender, RoutedEventArgs e) {
            LocationComboBox.SelectedIndex = -1;
            LanguageTextBox.Text = "";
            DurationTextBox.Text = Convert.ToString(0);
            TouristNumberTextBox.Text = Convert.ToString(0);
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
    }
}
