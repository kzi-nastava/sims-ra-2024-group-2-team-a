using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Services;
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

namespace BookingApp.WPF.Tablet.Views {
    /// <summary>
    /// Interaction logic for TourStatsPage.xaml
    /// </summary>
    public partial class TourStatsPage : Page {
        private Frame _mainFrame, _menuBarFrame;
        private int _userId;

        private readonly PointOfInterestService _pointOfInterestService = new PointOfInterestService();
        private readonly TourService _tourService = new TourService();
        public TourDTO tourDTO { get; set; }
        public ObservableCollection<PointOfInterestDTO> pointOfInterestDTOs { get; set; }
        public int teen { get; set; }
        public int mid { get; set; }
        public int old { get; set; }

        public TourStatsPage(TourDTO tDTO, Frame mainF, Frame menuBarF, int userId) {
            InitializeComponent();
            DataContext = this;

            if (tDTO == null) {
                Tour tour = _tourService.GetMostViewedByYear(-1);
                if(tour == null) {
                    MessageBox.Show("Nema tura iz te godine", "NEMA", MessageBoxButton.OK, MessageBoxImage.Error);
                    mainF.GoBack();
                }
                tourDTO = new TourDTO(tour);
            }

            tourDTO = tDTO;
            _mainFrame = mainF;
            _menuBarFrame = menuBarF;
            _userId = userId;

            pointOfInterestDTOs = new ObservableCollection<PointOfInterestDTO>();

            foreach (var point in _pointOfInterestService.GetAllByTourId(tourDTO.Id)) {
                pointOfInterestDTOs.Add(new PointOfInterestDTO(point));
            }

            List<int> stats = _tourService.GetTourStats(tDTO.Id);
            teen = stats[0];
            mid = stats[1];
            old = stats[2];
        }

        private void closeButton_Click(object sender, RoutedEventArgs e) {
            _mainFrame.GoBack();
        }

        private void showButton_Click(object sender, RoutedEventArgs e) {
            int year = int.Parse( (string) yearComboBox.SelectedValue);
            Tour tour = _tourService.GetMostViewedByYear(year);
            if (tour == null) {
                MessageBox.Show("Nema tura iz te godine", "NEMA", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            _mainFrame.Content = new TourStatsPage(new DTO.TourDTO(tour), _mainFrame, _menuBarFrame, _userId);
            _menuBarFrame.Content = new MenuBarButtonPage(_menuBarFrame, _mainFrame, _userId);
        }
    }
}
