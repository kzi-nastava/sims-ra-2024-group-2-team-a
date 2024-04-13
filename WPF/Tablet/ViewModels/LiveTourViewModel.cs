using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Services;
using System;
using System.Collections.ObjectModel;

namespace BookingApp.WPF.Tablet.ViewModels {
    public class LiveTourViewModel {
        private int _userId;

        private readonly LocationService _locationService = new LocationService();
        private readonly LanguageService _languageService = new LanguageService();
        private readonly TourService _tourService = new TourService();
        private readonly PointOfInterestService _pointOfInterestService = new PointOfInterestService();

        private int _pointOfInterestIndex;
        public TourDTO tourDTO { get; set; }
        public PointOfInterestDTO pointOfInterestDTO { get; set; }
        public ObservableCollection<PointOfInterestDTO> pointOfInterestDTOs { get; set; }
        public ObservableCollection<LocationDTO> locationDTOs { get; set; }
        public ObservableCollection<LanguageDTO> languageDTOs { get; set; }
        public ObservableCollection<TourDTO> tourDTOs { get; set; }
        public LiveTourViewModel(int userId) {
            _userId = userId;
            LoadTours();
        }
        public LiveTourViewModel(TourDTO tDTO, int userId) {
            _userId = userId;
            tourDTO = tDTO;

            _pointOfInterestIndex = 0;

            LoadTour();
        }
        private void LoadTour() {
            tourDTO.State = Model.TourState.Active;
            _tourService.Update(tourDTO.ToModel());

            pointOfInterestDTOs = new ObservableCollection<PointOfInterestDTO>();

            foreach (var point in _pointOfInterestService.GetAllByTourId(tourDTO.Id)) {
                /*if (!point.IsChecked && _pointOfInterestIndex == -1)
                    _pointOfInterestIndex = pointOfInterestDTOs.Count;            Ovo isto */
                pointOfInterestDTOs.Add(new PointOfInterestDTO(point));
            }
            pointOfInterestDTOs[0].IsChecked = true;
            pointOfInterestDTO = pointOfInterestDTOs[_pointOfInterestIndex++];
            _pointOfInterestService.Update(pointOfInterestDTO.ToModel());
            /*if (_pointOfInterestIndex == 0) {
                pointOfInterestDTOs[0].IsChecked = true;
                pointOfInterestDTO = pointOfInterestDTOs[_pointOfInterestIndex++];
                _pointOfInterestRepository.Update(pointOfInterestDTO.ToModel());  Ovo sacuvaj ako vudes omogucio
                                                                                  Izlazenje tokom startovanja ture
            }*/
        }
        public void FinishTour() {
            tourDTO.State = Model.TourState.Finished;
            tourDTO.End = DateTime.Now;
            _tourService.Update(tourDTO.ToModel());
        }
        public bool CheckKeyPoint() {
            pointOfInterestDTOs[_pointOfInterestIndex].IsChecked = true;
            _pointOfInterestIndex++;
            _pointOfInterestService.Update(pointOfInterestDTO.ToModel());

            if (_pointOfInterestIndex != pointOfInterestDTOs.Count)
                return false;
            return true;
        }
        public void SetPointOfInterestDTO() {
            pointOfInterestDTO = pointOfInterestDTOs[_pointOfInterestIndex];

        }
        private void LoadTours() {
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
            LoadLive();
        }
        private void LoadLive() {
            tourDTOs = new ObservableCollection<TourDTO>();
            foreach (var tour in _tourService.GetLive(_userId)) {
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
            foreach (var tour in _tourService.GetFilteredLive(filterDTO, _userId)) {
                TourDTO temp = new TourDTO(tour);
                SetLocationLanguage(temp, tour);
                tourDTOs.Add(temp);
            }
        }
    }
}
