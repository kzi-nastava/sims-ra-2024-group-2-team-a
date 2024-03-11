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
using System.Windows.Shapes;

namespace BookingApp.View.DesktopViews {
    /// <summary>
    /// Interaction logic for TouristMainWindow.xaml
    /// </summary>
    public partial class TouristMainWindow : Window {
        private readonly User _user;

        private readonly TourRepository _tourRepository;

        private readonly LocationRepository _locationRepository;

        public ObservableCollection<TourDTO> ToursOnDisplay { get; set; }
        public TouristMainWindow() {
            InitializeComponent();
            DataContext = this;
            _tourRepository = new TourRepository();
            _locationRepository = new LocationRepository();
            ToursOnDisplay = new ObservableCollection<TourDTO>();
            Update();
        }

        public void Update() {
            ToursOnDisplay.Clear();
            foreach (var tour in _tourRepository.GetAll()) {
                ToursOnDisplay.Add(new TourDTO(tour));
            }
        }
    }
}
