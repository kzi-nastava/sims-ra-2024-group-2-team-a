using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Repository;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace BookingApp.View.DesktopViews {
    /// <summary>
    /// Interaction logic for TouristMainWindow.xaml
    /// </summary>
    public partial class TouristMainWindow : Window {
        private readonly User _user;
        private readonly TourRepository _tourRepository;
        private readonly LocationRepository _locationRepository;

        public TourFilterDTO Filter { get; set; }
        public ObservableCollection<TourDTO> ToursOnDisplay { get; set; }
        public ObservableCollection<LocationDTO> LocationOptions { get; set; }
        public TouristMainWindow() {
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
        }

        public void Update() {
            ToursOnDisplay.Clear();
            foreach (var tour in _tourRepository.GetFiltered(Filter)) {
                ToursOnDisplay.Add(new TourDTO(tour));
            }
        }

        private void FilterButton_Click(object sender, RoutedEventArgs e) {
            Update();
        }

        private void LocationComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e) {
            if(LocationComboBox.SelectedItem != null) {
                Filter.Location = (LocationDTO)LocationComboBox.SelectedItem;
                return;
            }
            Filter.Location = new LocationDTO();
        }
    }
}
