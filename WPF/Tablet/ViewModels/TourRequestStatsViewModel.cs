using BookingApp.Services;
using BookingApp.WPF.DTO;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.Tablet.ViewModels
{
    public class TourRequestStatsViewModel : INotifyPropertyChanged{
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private readonly TourRequestService _tourRequestService = ServicesPool.GetService<TourRequestService>();
        private readonly LocationService _locationService = ServicesPool.GetService<LocationService>();
        private readonly LanguageService _languageService = ServicesPool.GetService<LanguageService>();
        public ObservableCollection<LocationDTO> locationDTOs { get; set; }
        public ObservableCollection<LanguageDTO> languageDTOs { get; set; }
        public TourRequestStatsDTO tourRequestStatsDTO { get; set; }
        private SeriesCollection _seriesCollection = new SeriesCollection();
        public SeriesCollection SeriesCollection {
            get {
                return _seriesCollection;
            }
            set {
                if(value != _seriesCollection) {
                    _seriesCollection = value;
                    OnPropertyChanged();
                }
            }
        }
        private string[] _labels;
        public string[] Labels {
            get {
                return _labels;
            }
            set {
                if(value != _labels) {
                    _labels = value;
                    OnPropertyChanged();
                }
            }
        }
        private Func<int, string> _formatter;
        public Func<int, string> Formatter {
            get {
                return _formatter;
            }
            set {
                if(value != _formatter) {
                    _formatter = value;
                    OnPropertyChanged();
                }
            }
        }
        public TourRequestStatsViewModel() {

            tourRequestStatsDTO = new TourRequestStatsDTO();

            locationDTOs = new ObservableCollection<LocationDTO>();
            languageDTOs = new ObservableCollection<LanguageDTO>();

            locationDTOs.Add(new LocationDTO());
            languageDTOs.Add(new LanguageDTO());

            foreach (var lan in _languageService.GetAll()) {
                languageDTOs.Add(new LanguageDTO(lan));
            }
            foreach (var loc in _locationService.GetAll()) {
                locationDTOs.Add(new LocationDTO(loc));
            }
        }

        public void SetStats() {
            List<int> stats;

            if(tourRequestStatsDTO.IsLocationSelected) {
                stats = _tourRequestService.GetStatsByLocation(tourRequestStatsDTO.LocationLanguageId, tourRequestStatsDTO.TimeSelected);
            }
            else {
                stats = _tourRequestService.GetStatsByLanguage(tourRequestStatsDTO.LocationLanguageId, tourRequestStatsDTO.TimeSelected);
            }

            if(tourRequestStatsDTO.TimeSelected == -1) {
                SeriesCollection = new SeriesCollection {
                    new ColumnSeries {
                        Values = new ChartValues<int>{stats[0], stats[1], stats[2], stats[3], stats[4]}
                    }
                };
                Labels = new[] { "2021", "2022", "2023", "2024", "2025" }; 
            }
            else {
                SeriesCollection = new SeriesCollection {
                    new ColumnSeries {
                        Values = new ChartValues<int>{stats[0], stats[1], stats[2], stats[3], stats[4], stats[5], stats[6], stats[7], stats[8], stats[9] , stats[10], stats[11]}
                    }
                };
                Labels = new[] { "Januar", "Februar", "Mart", "April", "May", "June", "July", "August", "September", "Oktober", "November", "December" };
            }
            
            Formatter = value => value.ToString("N0");
        }
        public void SetDTO(LanguageDTO langDTO, LocationDTO locDTO, int timeSelected, bool isLocationSelected) {
            tourRequestStatsDTO.TimeSelected = timeSelected;
            if (timeSelected == -1)
                tourRequestStatsDTO.Time = "Year";
            else
                tourRequestStatsDTO.Time = "Month";

            tourRequestStatsDTO.IsLocationSelected = isLocationSelected;

            if (isLocationSelected) {
                tourRequestStatsDTO.LocationLanguageId = locDTO.Id;
                tourRequestStatsDTO.TitleTemplate = "Location";
                tourRequestStatsDTO.DescriptionTemplate = locDTO.LocationInfoTemplate;
            }
            else {
                tourRequestStatsDTO.LocationLanguageId = langDTO.Id;
                tourRequestStatsDTO.TitleTemplate = "Language";
                tourRequestStatsDTO.DescriptionTemplate = langDTO.Name;

            }
        }
    }
}
