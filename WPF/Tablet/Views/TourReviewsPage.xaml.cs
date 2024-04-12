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
    /// Interaction logic for TourReviewsPage.xaml
    /// </summary>
    public partial class TourReviewsPage : Page {
        private int _userId;
        private Frame _mainFrame;
        private readonly TourReviewRepository _tourReviewRepository;
        private readonly PassengerRepository _passengerRepository;
        private readonly PointOfInterestRepository _pointOfInterestRepository;
        private readonly TourReservationRepository _tourReservationRepository;
        public ObservableCollection<TourReviewDTO> tourReviewDTOs { get; set; }
        public TourDTO tourDTO { get; set; }

        public TourReviewsPage(int userId, TourDTO tDTO, Frame mFrame) {
            InitializeComponent();

            _userId = userId;
            _mainFrame = mFrame;
            tourDTO = tDTO;
            _tourReviewRepository = new TourReviewRepository();
            _passengerRepository = new PassengerRepository();
            _tourReservationRepository = new TourReservationRepository();
            _pointOfInterestRepository = new PointOfInterestRepository();
            tourReviewDTOs = new ObservableCollection<TourReviewDTO>();
            
            foreach(var review in _tourReviewRepository.GetByTourId(tDTO.Id)) {
                TourReviewDTO tempDTO = new TourReviewDTO(review);
                TourReservation tourReservation = _tourReservationRepository.GetByTourAndTourist(tempDTO.TourId, tempDTO.TouristId); 
                PassengerDTO passengerDTO = new PassengerDTO(_passengerRepository.GetByReservationAndTourist(tourReservation.Id, tempDTO.TouristId));
                PointOfInterestDTO pointOfInterestDTO = new PointOfInterestDTO(_pointOfInterestRepository.GetById(passengerDTO.JoinedPointOfInterestId));
                passengerDTO.SetPointOfInterestName(pointOfInterestDTO.Name);

                tempDTO.TouristName = passengerDTO.Name;
                tempDTO.TouristSurname = passengerDTO.Surname;
                tempDTO.PointOfInterestName = passengerDTO.JoinedPointOfInterestName;
                tourReviewDTOs.Add(tempDTO);
            }
            DataContext = this;
        }

        private void closeButton_Click(object sender, RoutedEventArgs e) {
            _mainFrame.GoBack();   
        }
    }
}
