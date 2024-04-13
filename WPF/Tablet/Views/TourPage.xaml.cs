using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Services;
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


        private readonly TourService _tourService = new TourService();  
        private readonly VoucherService _voucherService = new VoucherService();
        private readonly PointOfInterestService _pointOfInterestService = new PointOfInterestService();
        private readonly PassengerService _passengerService = new PassengerService();
        private readonly TourReservationService _tourReservationService = new TourReservationService();
        public TourDTO tourDTO { get; set; }
        public ObservableCollection<PointOfInterestDTO> pointOfInterestDTOs { get; set; }
        public TourPage(TourDTO tDTO, Frame mainF, Frame menuBarF, int userId) {
            InitializeComponent();
            DataContext = this;

            tourDTO = tDTO;
            _mainFrame = mainF;
            _menuBarFrame = menuBarF;
            _userId = userId;


            pointOfInterestDTOs = new ObservableCollection<PointOfInterestDTO>();

            foreach (var point in _pointOfInterestService.GetAllByTourId(tourDTO.Id)) {
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

            DeleteReservations();
            DeleteTour();
            
            _mainFrame.Content = new ScheduledToursPage(_userId);
        }
        private void DeleteReservations() {
            List<TourReservation> reservations = _tourReservationService.DeleteByTourId(tourDTO.Id);
            if (reservations == null)
                return;
            else if (reservations.Count > 0) {
                if (!_passengerService.DeleteByReservations(reservations))
                    return;

                if (!_voucherService.AddMultiple(reservations.Select(x => x.TouristId).Distinct().ToList(), DateTime.Today.AddYears(1)))
                    return;
            }
        }
        private void DeleteTour() {
            if (!_pointOfInterestService.DeleteByTourId(tourDTO.Id))
                return;

            if (!_tourService.Delete(tourDTO.ToModel()))
                return;

        }
    }
}
