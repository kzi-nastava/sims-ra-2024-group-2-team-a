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
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookingApp.WPF.Tablet.Views{
    /// <summary>
    /// Interaction logic for FinishedTourPage.xaml
    /// </summary>
    public partial class FinishedTourPage : Page {
        private Frame _mainFrame, _menuBarFrame;
        private int _userId;

        private readonly PointOfInterestService _pointOfInterestService = new PointOfInterestService();

        public TourDTO tourDTO { get; set; }
        public ObservableCollection<PointOfInterestDTO> pointOfInterestDTOs { get; set; }
        public FinishedTourPage(TourDTO tDTO, Frame mainF, Frame menuBarF, int userId) {
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

        private void reviewsButton_Click(object sender, RoutedEventArgs e) {
            _mainFrame.Content = new TourReviewsPage(_userId ,tourDTO, _mainFrame);
        }

        private void statsButton_Click(object sender, RoutedEventArgs e) {
            _mainFrame.Content = new TourStatsPage(tourDTO, _mainFrame, _menuBarFrame, _userId);
        }

        private void closeButton_Click(object sender, RoutedEventArgs e) {
            _mainFrame.GoBack();
        }
    }
}
