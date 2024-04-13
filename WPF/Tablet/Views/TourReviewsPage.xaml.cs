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
    /// Interaction logic for TourReviewsPage.xaml
    /// </summary>
    public partial class TourReviewsPage : Page {
        private int _userId;
        private Frame _mainFrame;

        private readonly PointOfInterestService _pointOfInterestService = new PointOfInterestService();
        private readonly TourReviewService _tourReviewService = new TourReviewService();
        private readonly TourReservationService _tourReservationService = new TourReservationService();
        private readonly PassengerService _passengerService = new PassengerService();
        public ObservableCollection<TourReviewDTO> tourReviewDTOs { get; set; }
        public TourDTO tourDTO { get; set; }

        public TourReviewsPage(int userId, TourDTO tDTO, Frame mFrame) {
            InitializeComponent();

            _userId = userId;
            _mainFrame = mFrame;
            tourDTO = tDTO;

            tourReviewDTOs = new ObservableCollection<TourReviewDTO>();
            
            foreach(var review in _tourReviewService.GetByTourId(tDTO.Id)) {
                TourReviewDTO tempDTO = new TourReviewDTO(review);
                TourReservation tourReservation = _tourReservationService.GetByTourAndTourist(tempDTO.TourId, tempDTO.TouristId); 
                PassengerDTO passengerDTO = new PassengerDTO(_passengerService.GetByReservationAndTourist(tourReservation.Id, tempDTO.TouristId));
                PointOfInterest point = _pointOfInterestService.GetById(passengerDTO.JoinedPointOfInterestId);
                if(point != null ) {
                    PointOfInterestDTO pointOfInterestDTO = new PointOfInterestDTO(point);
                    passengerDTO.SetPointOfInterestName(pointOfInterestDTO.Name);
                    tempDTO.PointOfInterestName = passengerDTO.JoinedPointOfInterestName;
                }
                else {
                    tempDTO.PointOfInterestName = "!NEVER!";
                }

                tempDTO.TouristName = passengerDTO.Name;
                tempDTO.TouristSurname = passengerDTO.Surname;
                tourReviewDTOs.Add(tempDTO);
            }
            DataContext = this;
        }

        private void closeButton_Click(object sender, RoutedEventArgs e) {
            _mainFrame.GoBack();   
        }
    }
}
