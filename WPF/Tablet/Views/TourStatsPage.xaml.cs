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

namespace BookingApp.WPF.Tablet.Views {
    /// <summary>
    /// Interaction logic for TourStatsPage.xaml
    /// </summary>
    public partial class TourStatsPage : Page {
        private Frame _mainFrame, _menuBarFrame;
        private int _userId;

        private readonly TourRepository _tourRepository;
        private readonly PointOfInterestRepository _pointOfInterestRepository;
        private readonly TourReservationRepository _tourReservationRepository;
        private readonly PassengerRepository _passengerRepository;
        private readonly VoucherRepository _voucherRepository;
        public TourDTO tourDTO { get; set; }
        public ObservableCollection<PointOfInterestDTO> pointOfInterestDTOs { get; set; }

        public TourStatsPage(TourDTO tDTO, Frame mainF, Frame menuBarF, int userId) {
            InitializeComponent();
            DataContext = this;

            tourDTO = tDTO;
            _mainFrame = mainF;
            _menuBarFrame = menuBarF;
            _userId = userId;
            _tourRepository = new TourRepository();
            _pointOfInterestRepository = new PointOfInterestRepository();
            _tourReservationRepository = new TourReservationRepository();
            _passengerRepository = new PassengerRepository();
            _voucherRepository = new VoucherRepository();


            pointOfInterestDTOs = new ObservableCollection<PointOfInterestDTO>();

            foreach (var point in _pointOfInterestRepository.GetAllByTourId(tourDTO.Id)) {
                pointOfInterestDTOs.Add(new PointOfInterestDTO(point));
            }
        }

        private void showButton_Click(object sender, RoutedEventArgs e) {
            int year = int.Parse( (string) yearComboBox.SelectedValue);
            Tour tour = _tourRepository.GetMostViewedByYear(year);
            if (tour == null) {
                MessageBox.Show("Nema tura iz te godine", "NEMA", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            _mainFrame.Content = new TourStatsPage(new DTO.TourDTO(tour), _mainFrame, _menuBarFrame, _userId);
            _menuBarFrame.Content = new MenuBarButtonPage(_menuBarFrame, _mainFrame, _userId);
        }
    }
}
