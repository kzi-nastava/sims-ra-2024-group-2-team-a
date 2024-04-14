using BookingApp.Model;
using BookingApp.Services;
using BookingApp.WPF.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace BookingApp.WPF.Tablet.ViewModels {
    public class ScheduledTourViewModel {
        private int _userId;

        private readonly LocationService _locationService = new LocationService();
        private readonly LanguageService _languageService = new LanguageService();
        private readonly TourService _tourService = new TourService();
        private readonly VoucherService _voucherService = new VoucherService();
        private readonly PointOfInterestService _pointOfInterestService = new PointOfInterestService();
        private readonly PassengerService _passengerService = new PassengerService();
        private readonly TourReservationService _tourReservationService = new TourReservationService();
        public TourDTO tourDTO { get; set; }
        public ObservableCollection<PointOfInterestDTO> pointOfInterestDTOs { get; set; }
        public ObservableCollection<LocationDTO> locationDTOs { get; set; }
        public ObservableCollection<LanguageDTO> languageDTOs { get; set; }
        public ObservableCollection<TourDTO> tourDTOs { get; set; }
        public ScheduledTourViewModel(int userId) {
            _userId = userId;
            Load();
        }
        public ScheduledTourViewModel(TourDTO tDTO, int userId) {
            tourDTO = tDTO;
            _userId = userId;

            pointOfInterestDTOs = new ObservableCollection<PointOfInterestDTO>();

            foreach (var point in _pointOfInterestService.GetAllByTourId(tourDTO.Id)) {
                pointOfInterestDTOs.Add(new PointOfInterestDTO(point));
            }
        }
        public void DeleteReservations() {
            List<TourReservation> reservations = _tourReservationService.DeleteByTourId(tourDTO.Id);
            if (reservations == null)
                return;
            else if (reservations.Count > 0) {
                if (!_passengerService.DeleteByReservations(reservations))
                    return;

                if (!_voucherService.AddMultiple(reservations.Select(x => x.TouristId).Distinct().ToList(), DateTime.Today.AddYears(1)))
                    return;
            }
        }
        public void DeleteTour() {
            if (!_pointOfInterestService.DeleteByTourId(tourDTO.Id))
                return;

            if (!_tourService.Delete(tourDTO.ToModel()))
                return;

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
            LoadScheduled();
        }

        private void LoadScheduled() {
            tourDTOs = new ObservableCollection<TourDTO>();
            foreach (var tour in _tourService.GetScheduled(_userId)) {
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
