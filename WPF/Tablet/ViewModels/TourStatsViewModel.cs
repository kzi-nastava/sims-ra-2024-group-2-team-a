using BookingApp.Domain.Model;
using BookingApp.Services;
using BookingApp.WPF.DTO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using LiveCharts;
using LiveCharts.Wpf;
using System;

namespace BookingApp.WPF.Tablet.ViewModels {
    public class TourStatsViewModel {
        private int _userId;

        private readonly PointOfInterestService _pointOfInterestService = ServicesPool.GetService<PointOfInterestService>();
        private readonly TourService _tourService = ServicesPool.GetService<TourService>();
        private readonly LanguageService _languageService = ServicesPool.GetService<LanguageService>();
        private readonly LocationService _locationService = ServicesPool.GetService<LocationService>();
        public TourDTO tourDTO { get; set; }
        public ObservableCollection<PointOfInterestDTO> pointOfInterestDTOs { get; set; }
        public List<int> stats { get; set; }
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<int, string> Formatter { get; set; }

        public TourStatsViewModel(TourDTO tDTO, int userId) {
            if (tDTO == null) {
                if (!GetMostViewedByYear(-1)) {
                    MessageBox.Show("Nema tura iz te godine", "NEMA", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
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

            stats = _tourService.GetTourStats(tourDTO.Id);
            SeriesCollection = new SeriesCollection {
                new ColumnSeries {
                    Values = new ChartValues<int>{stats[0], stats[1], stats[2]}
                }
            };
            Labels = new[] {"Teen", "Mid", "Old" };
            Formatter = value => value.ToString("N");
            
        }
        public bool GetMostViewedByYear(int year) {
            Tour tour = _tourService.GetMostViewedByYear(year);
            if (tour == null)
                return false;
            tourDTO = new TourDTO(tour);
            SetLanguageLocation();
            return true;
        }
        private void SetLanguageLocation() {
            Location location = _locationService.GetById(tourDTO.LocationId);
            string city = location.City;
            string country = location.Country;
            tourDTO.setLocationTemplate(city, country);
            tourDTO.LanguageTemplate = _languageService.GetById(tourDTO.LanguageId).Name;
        }
    }
}
