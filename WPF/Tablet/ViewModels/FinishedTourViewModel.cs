using BookingApp.Domain.Model;
using BookingApp.Services;
using BookingApp.WPF.DTO;
using System.Collections.ObjectModel;

namespace BookingApp.WPF.Tablet.ViewModels {
    public class FinishedTourViewModel {
        private int _userId;

        private readonly PointOfInterestService _pointOfInterestService = new PointOfInterestService();
        private readonly LocationService _locationService = new LocationService();
        private readonly LanguageService _languageService = new LanguageService();
        private readonly TourService _tourService = new TourService();

        public TourDTO tourDTO { get; set; }
        public ObservableCollection<LocationDTO> locationDTOs { get; set; }
        public ObservableCollection<LanguageDTO> languageDTOs { get; set; }
        public ObservableCollection<TourDTO> tourDTOs { get; set; }

        public ObservableCollection<PointOfInterestDTO> pointOfInterestDTOs { get; set; }
        public FinishedTourViewModel(TourDTO tDTO, int userId) {
            _userId = userId;
            tourDTO = tDTO;
            pointOfInterestDTOs = new ObservableCollection<PointOfInterestDTO>();

            foreach (var point in _pointOfInterestService.GetAllByTourId(tourDTO.Id)) {
                pointOfInterestDTOs.Add(new PointOfInterestDTO(point));
            }
        }
        public FinishedTourViewModel(int userId) {
            _userId = userId;
            Load();
        }
        private void Load() {
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
            LoadFinished();
        }

        private void LoadFinished() {
            tourDTOs = new ObservableCollection<TourDTO>();
            foreach (var tour in _tourService.GetFinished(_userId)) {
                TourDTO tempTourDTO = new TourDTO(tour);
                SetLocationLanguage(tempTourDTO, tour);
                tourDTOs.Add(tempTourDTO);
            }
        }
        private void SetLocationLanguage(TourDTO tempTourDTO, Tour tour) {
            Location location = _locationService.GetById(tour.LocationId);
            string city = location.City;
            string country = location.Country;
            tempTourDTO.setLocationTemplate(city, country);
            tempTourDTO.LanguageTemplate = _languageService.GetById(tour.LanguageId).Name;
        }
        public void FilterTours(TourFilterDTO filterDTO) {
            tourDTOs.Clear();
            foreach (var tour in _tourService.GetFilteredScheduled(filterDTO, _userId)) {
                TourDTO temp = new TourDTO(tour);
                SetLocationLanguage(temp, tour);
                tourDTOs.Add(temp);
            }
        }
    }
}
