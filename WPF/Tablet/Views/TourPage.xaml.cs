using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.DirectoryServices.ActiveDirectory;
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

namespace BookingApp.WPF.Tablet.Views
{
    /// <summary>
    /// Interaction logic for TourPage.xaml
    /// </summary>
    public partial class TourPage : Page {

        private Frame _mainFrame, _menuBarFrame;
        private int _userId;

        private readonly TourRepository _tourRepository;
        private readonly PointOfInterestRepository _pointOfInterestRepository;
        private readonly TourReservationRepository _tourReservationRepository;
        private readonly PassengerRepository _passengerRepository;
        private readonly VoucherRepository _voucherRepository;

        public TourDTO tourDTO { get; set; }
        public ObservableCollection<PointOfInterestDTO> pointOfInterestDTOs { get; set; }
        public TourPage(TourDTO tDTO, Frame mainF, Frame menuBarF, int userId) {
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

        private void closeButton_Click(object sender, RoutedEventArgs e) {
            _mainFrame.GoBack();
        }

        private void deletehButton_Click(object sender, RoutedEventArgs e) {
            if(tourDTO.Beggining < DateTime.Now.AddHours(48)) {
                MessageBox.Show("Tour is scheduled in the next 48 hours. Too late to delete it!", "Unable to delete tour", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            if (MessageBox.Show("Are you sure you want to delete this tour?", "DELETE this tour", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                return;

            List<TourReservation> reservations = _tourReservationRepository.DeleteByTourId(tourDTO.Id);
            if (reservations == null) {
                MessageBox.Show("NESTO NE VALJA reservacija", "PROBLEEEEM1", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            } else if(reservations.Count > 0) {
                if(!_passengerRepository.DeleteByReservations(reservations)) {
                    MessageBox.Show("NESTO NE VALJA putnici", "PROBLEEEEM1", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if(!_voucherRepository.AddMultiple(reservations.Select(x => x.TouristId).Distinct().ToList(), DateTime.Today.AddYears(1))){
                    MessageBox.Show("NESTO NE VALJA vauceri", "PROBLEEEEM1", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            if (!_pointOfInterestRepository.DeleteByTourId(tourDTO.Id)) {
                MessageBox.Show("NESTO NE VALJA keypoints", "PROBLEEEEM1", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!_tourRepository.Delete(tourDTO.ToModel())){
                MessageBox.Show("NESTO NE VALJA tura", "PROBLEEEEM1", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            
            _mainFrame.Content = new ScheduledToursPage(_userId);
        }

    }
}
