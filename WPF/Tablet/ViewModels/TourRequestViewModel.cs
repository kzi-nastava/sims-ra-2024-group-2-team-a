using BookingApp.Domain.Model;
using BookingApp.Services;
using BookingApp.WPF.DTO;
using LiveCharts;
using LiveCharts.Wpf;
using Syncfusion.Windows.Controls;
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
        private readonly LocationService _locationService = ServicesPool.GetService<LocationService>();
        private readonly LanguageService _languageService = ServicesPool.GetService<LanguageService>();
        private readonly TourService _tourService = ServicesPool.GetService<TourService>();
        public ObservableCollection<TourRequestDTO> tourRequestDTOs { get; set; }
        public ObservableCollection<LocationDTO> locationDTOs { get; set; }
        public ObservableCollection<LanguageDTO> languageDTOs { get; set; }
        public TourRequestDTO tourRequestDTO { get; set; }

        public TourRequestViewModel(int userId) {
            _userId = userId;
            Load();
        }
        public TourRequestViewModel(TourRequestDTO trDTO) {
            tourRequestDTO = trDTO;
            _userId = trDTO.GuideId;
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
            LoadRequested();
        }
        private void LoadRequested() {
            tourRequestDTOs = new ObservableCollection<TourRequestDTO>();
            foreach (var request in _tourRequestService.GetAllOnHold()) {
                TourRequestDTO tempDTO = new TourRequestDTO(request);
                tempDTO.GuideId = _userId;
                tourRequestDTOs.Add(tempDTO);
            }
        }
        public void SetFromDate(DateTime date) {
            tourRequestDTO.StartDate = new DateOnly(date.Year, date.Month, date.Day);
            
        }
        public void SetToDate(DateTime date) {
            tourRequestDTO.EndDate = new DateOnly(date.Year, date.Month, date.Day);
            
        }
        public bool IsAvailable() {
           return _tourService.IsGuideAvailable(_userId, tourRequestDTO.StartDate.ToDateTime(), tourRequestDTO.EndDate.ToDateTime()); 
           
        }
        public void AcceptTourRequest() {
            tourRequestDTO.StatusReal = TourRequestStatus.Accepted;
            _tourRequestService.Update(tourRequestDTO.ToModel());
        }
        public int GetUserId() {
            return _userId;
        }
        public void FilterRequests(TourRequestFilterDTO filterDTO) {
            tourRequestDTOs.Clear();
            foreach(var request in _tourRequestService.GetFilteredByGuide(filterDTO)) {
                TourRequestDTO temp = new TourRequestDTO(request);
                tourRequestDTOs.Add(temp);
            }
        }
    }
}
