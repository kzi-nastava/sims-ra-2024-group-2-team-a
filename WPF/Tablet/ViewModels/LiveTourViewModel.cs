﻿using BookingApp.Domain.Model;
using BookingApp.Services;
using BookingApp.WPF.DTO;
using System;
using System.Collections.ObjectModel;

namespace BookingApp.WPF.Tablet.ViewModels {
    public class LiveTourViewModel {
        private int _userId;

        private readonly LocationService _locationService = ServicesPool.GetService<LocationService>();
        private readonly LanguageService _languageService = ServicesPool.GetService<LanguageService>();
        private readonly TourService _tourService = ServicesPool.GetService<TourService>();
        private readonly PointOfInterestService _pointOfInterestService = ServicesPool.GetService<PointOfInterestService>();

        private int _pointOfInterestIndex;
        public TourDTO tourDTO { get; set; }
        public PointOfInterestDTO pointOfInterestDTO { get; set; }
        public ObservableCollection<PointOfInterestDTO> pointOfInterestDTOs { get; set; }
        public ObservableCollection<LocationDTO> locationDTOs { get; set; }
        public ObservableCollection<LanguageDTO> languageDTOs { get; set; }
        public ObservableCollection<TourDTO> tourDTOs { get; set; }
        public GuideProfileDTO guideProfileDTO { get; set; }
        public LiveTourViewModel(int userId, GuideProfileDTO pGDTO) {
            _userId = userId;
            guideProfileDTO = pGDTO;
            LoadTours();
        }
        public LiveTourViewModel(TourDTO tDTO, int userId) {
            _userId = userId;
            tourDTO = tDTO;

            if(tDTO.State != TourState.Active){
                tourDTO.State = TourState.Active;
                _tourService.Update(tourDTO.ToModel());
            }
            _pointOfInterestIndex = -1;

            LoadTour();
        }
        private void LoadTour() {
            SetLocationLanguage(tourDTO, tourDTO.ToModel());

            pointOfInterestDTOs = new ObservableCollection<PointOfInterestDTO>();

            foreach (var point in _pointOfInterestService.GetAllByTourId(tourDTO.Id)) {
                if (!point.IsChecked && _pointOfInterestIndex == -1)
                    _pointOfInterestIndex = pointOfInterestDTOs.Count;
                pointOfInterestDTOs.Add(new PointOfInterestDTO(point));
            }
        }
        public void FinishTour() {
            tourDTO.State = TourState.Finished;
            tourDTO.End = DateTime.Now;
            _tourService.Update(tourDTO.ToModel());
        }
        public bool CheckKeyPoint() {
            pointOfInterestDTOs[_pointOfInterestIndex++].IsChecked = true;
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
        public bool HasStarted() {
            if(_pointOfInterestIndex == 0)
                return false;
            return true;
        }
    }
}
