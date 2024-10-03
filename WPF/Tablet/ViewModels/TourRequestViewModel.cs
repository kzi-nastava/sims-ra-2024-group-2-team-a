using BookingApp.Domain.Model;
using BookingApp.Services;
using BookingApp.WPF.DTO;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BookingApp.WPF.Tablet.ViewModels
{
    public class TourRequestViewModel{
        private int _userId;

        private readonly TourRequestService _tourRequestService = ServicesPool.GetService<TourRequestService>();
        private readonly ComplexTourRequestService _complexTourRequestService = ServicesPool.GetService<ComplexTourRequestService>();
        private readonly LocationService _locationService = ServicesPool.GetService<LocationService>();
        private readonly LanguageService _languageService = ServicesPool.GetService<LanguageService>();
        private readonly TourService _tourService = ServicesPool.GetService<TourService>();
        public ObservableCollection<TourRequestDTO> tourRequestDTOs { get; set; }
        public ObservableCollection<ComplexTourRequestDTO> complexTourRequestDTOs { get; set; }
        public ObservableCollection<LocationDTO> locationDTOs { get; set; }
        public ObservableCollection<LanguageDTO> languageDTOs { get; set; }
        public TourRequestDTO tourRequestDTO { get; set; }

        public TourRequestViewModel(int userId, bool isComplex) {
            _userId = userId;
                LoadLanguageLocations();
            if (isComplex) {
                LoadComplexRequests();
            }
            else {
                LoadRegularRequests();
            }
        }
        public TourRequestViewModel(TourRequestDTO trDTO, bool isComplex  ) {
            tourRequestDTO = trDTO;
            _userId = trDTO.GuideId;
            if (isComplex) {
                Calendar calendar = _tourService.GetUnavailableDates(_userId, tourRequestDTO.ComplexTourId, tourRequestDTO.StartDateTime, tourRequestDTO.EndDateTime);
                calendar.DisplayDateStart = tourRequestDTO.StartDateTime;
                calendar.DisplayDateEnd = tourRequestDTO.EndDateTime;
                tourRequestDTO.CalendarFrom = calendar;
                tourRequestDTO.CalendarTo = calendar;
            }
        }
        private void LoadLanguageLocations() {
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
        private void LoadRegularRequests() {
            tourRequestDTOs = new ObservableCollection<TourRequestDTO>();
            foreach (var request in _tourRequestService.GetAllOnHold()) {
                TourRequestDTO tempDTO = new TourRequestDTO(request);
                tempDTO.GuideId = _userId;
                tourRequestDTOs.Add(tempDTO);
            }
        }
        private void LoadComplexRequests() {
            complexTourRequestDTOs = new ObservableCollection<ComplexTourRequestDTO>();
            foreach(var complexRequest in _complexTourRequestService.GetAllOnHold(_userId)) {
                ComplexTourRequestDTO tempDTO = new ComplexTourRequestDTO(complexRequest, complexRequest.Id);

                foreach(TourRequest request in _tourRequestService.GetComplexForGuide(complexRequest.Id)) {
                    TourRequestDTO tempRequestDTO = new TourRequestDTO(request);

                    if(!_tourService.IsAvailableToShow(_userId, complexRequest.Id, tempRequestDTO.StartDateTime, tempRequestDTO.EndDateTime)) {
                        continue;
                    }

                    tempRequestDTO.GuideId = _userId;
                    tempDTO.TourRequests.Add(tempRequestDTO);
                }
                if(tempDTO.TourRequests.Count >= 1)
                    complexTourRequestDTOs.Add(tempDTO);
            }
        }
        public void SetBlackoutDates() {
            foreach(var dateRange in tourRequestDTO.CalendarFrom.BlackoutDates) {
                tourRequestDTO.BlackoutDatesStart.Add(dateRange);
                tourRequestDTO.BlackoutDatesEnd.Add(dateRange);
            }
        }
        public void SetFromDate(DateTime date) {
            tourRequestDTO.StartDate = new DateOnly(date.Year, date.Month, date.Day);
            tourRequestDTO.CastToDateTime();
        }
        public void SetToDate(DateTime date) {
            tourRequestDTO.EndDate = new DateOnly(date.Year, date.Month, date.Day);
            tourRequestDTO.CastToDateTime();

        }
        public bool IsAvailable() {
           if(_tourService.IsGuideAvailable(_userId, tourRequestDTO.ComplexTourId, tourRequestDTO.StartDateTime, tourRequestDTO.EndDateTime) && (tourRequestDTO.StartDateTime <= tourRequestDTO.EndDateTime)){
                return true;
           }
            return false;
           
        }
        public bool AcceptTourRequest() {
            tourRequestDTO.StatusReal = TourRequestStatus.Accepted;
            _tourRequestService.Update(tourRequestDTO.ToModel());
            ComplexTourRequest complexTourRequest = _complexTourRequestService.GetById(tourRequestDTO.ComplexTourId);

            if (complexTourRequest == null)
                return false;

            if(_tourRequestService.GetComplexForGuide(complexTourRequest.Id).Count <= 0) {
                complexTourRequest.Status = TourRequestStatus.Accepted;
                _complexTourRequestService.Update(complexTourRequest);
            }
            return true;

        }
        public int GetUserId() {
            return _userId;
        }
        public void FilterRequests(TourRequestFilterDTO filterDTO) {
            if (tourRequestDTOs == null)
                return;
            tourRequestDTOs.Clear();
            foreach(var request in _tourRequestService.GetFilteredByGuide(filterDTO)) {
                TourRequestDTO temp = new TourRequestDTO(request);
                tourRequestDTOs.Add(temp);
            }
        }
    }
}
