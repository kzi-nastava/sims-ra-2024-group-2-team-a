using BookingApp.DTO;
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

namespace BookingApp.View.TabletView {
    /// <summary>
    /// Interaction logic for LiveTourPage.xaml
    /// </summary>
    public partial class LiveTourPage : Page {
        private Frame _mainFrame;
        private int _userId;

        private readonly TourRepository _tourRepository;
        private readonly PointOfInterestRepository _pointOfInterestRepository;

        private int _pointOfInterestIndex; 
        public TourDTO tourDTO { get; set; }
        public PointOfInterestDTO pointOfInterestDTO { get; set; }
        public ObservableCollection<PointOfInterestDTO> pointOfInterestDTOs { get; set; }
        public LiveTourPage(TourDTO tDTO, Frame mainF, int userId) {
            InitializeComponent();
            DataContext = this;
            _mainFrame = mainF;
            _userId = userId;
            tourDTO = tDTO;
            _tourRepository = new TourRepository();
            _pointOfInterestRepository = new PointOfInterestRepository();
            pointOfInterestDTOs = new ObservableCollection<PointOfInterestDTO>();
            _pointOfInterestIndex = -1;

            foreach(var point in _pointOfInterestRepository.GetAllByTourId(tourDTO.Id)) {
                if(!point.IsChecked && _pointOfInterestIndex == -1)
                    _pointOfInterestIndex = pointOfInterestDTOs.Count;
                pointOfInterestDTOs.Add(new PointOfInterestDTO(point));
            }
            if(_pointOfInterestIndex == 0) {
                pointOfInterestDTOs[0].IsChecked = true;
                pointOfInterestDTO = pointOfInterestDTOs[_pointOfInterestIndex++];
                _pointOfInterestRepository.Update(pointOfInterestDTO.ToModel());
            }
        }
        private void checkButton_Click(object sender, RoutedEventArgs e) {
            pointOfInterestDTO = pointOfInterestDTOs[_pointOfInterestIndex++];
            PassengerWindow passengerWindow = new PassengerWindow(tourDTO.Id, pointOfInterestDTO.Id);
            bool isConfirmed = (bool)passengerWindow.ShowDialog();
            if(!isConfirmed) {
                return;
            }
            pointOfInterestDTOs[_pointOfInterestIndex - 1].IsChecked = true;
            _pointOfInterestRepository.Update(pointOfInterestDTO.ToModel());
            if (_pointOfInterestIndex != pointOfInterestDTOs.Count)
                return;
            finishTour();
        }

        private void finishButton_Click(object sender, RoutedEventArgs e) {
            finishTour();
        }
        private void finishTour() {
            MessageBox.Show("Finished", "Finished", MessageBoxButton.OK, MessageBoxImage.Information);
            tourDTO.IsFinished = true;
            _tourRepository.Update(tourDTO.ToModel());
            _mainFrame.Content = new FollowLiveTourPage(_userId);
        }

    }
}
