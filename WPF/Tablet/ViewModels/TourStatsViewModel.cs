using BookingApp.Model;
using BookingApp.Services;
using BookingApp.WPF.DTO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace BookingApp.WPF.Tablet.ViewModels {
    public class TourStatsViewModel {
        private int _userId;

        private readonly PointOfInterestService _pointOfInterestService = new PointOfInterestService();
        private readonly TourService _tourService = new TourService();
        public TourDTO tourDTO { get; set; }
        public ObservableCollection<PointOfInterestDTO> pointOfInterestDTOs { get; set; }
        public int teen { get; set; }
        public int mid { get; set; }
        public int old { get; set; }
        
        public TourStatsViewModel(TourDTO tDTO, int userId) {
            if (tDTO == null) {
                Tour tour = _tourService.GetMostViewedByYear(-1);
                if (tour == null) {
                    MessageBox.Show("Nema tura iz te godine", "NEMA", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                tourDTO = new TourDTO(tour);
            }
            else {
                tourDTO = tDTO;
            }
            Load();
        }
        private void Load() {
            pointOfInterestDTOs = new ObservableCollection<PointOfInterestDTO>();

            foreach (var point in _pointOfInterestService.GetAllByTourId(tourDTO.Id)) {
                pointOfInterestDTOs.Add(new PointOfInterestDTO(point));
            }

            List<int> stats = _tourService.GetTourStats(tourDTO.Id);
            teen = stats[0];
            mid = stats[1];
            old = stats[2];
        }
        public Tour GetMostViewedByYear(int year) {
            return _tourService.GetMostViewedByYear(year);
        }
    }
}
