using BookingApp.Services;
using BookingApp.WPF.DTO;
using BookingApp.WPF.Utils.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.Android.ViewModels {
    public class AccommodationGuidanceViewmodel {

        private int _userId;

        private readonly LocationService _locationService;

        private readonly AccommodationStatisticsService _accommodationStatisticsService;

        private readonly AccommodationService _accommodationService;
        public LocationDTO FirstLocation { get; set; }
        public LocationDTO SecondLocation { get; set; }
        public LocationDTO ThirdLocation { get; set; }
        public LocationDTO FourthLocation { get; set; }
        public ObservableCollection<AccommodationDTO> AccommodationDTOs { get; set; }
        public AccommodationDTO SelectedAccommodation { get; set; }
        public AndroidCommand CloseCommand { get; set; }

        public bool IsNotValid;
        public AccommodationGuidanceViewmodel(int userId) {
            _userId = userId;

            _accommodationService = ServicesPool.GetService<AccommodationService>();
            _locationService = ServicesPool.GetService<LocationService>();
            _accommodationStatisticsService = ServicesPool.GetService<AccommodationStatisticsService>();

            AccommodationDTOs = new ObservableCollection<AccommodationDTO>();

            CloseCommand = new AndroidCommand(Execute, CanBeExecuted);
            IsNotValid = false;

            Update();
        }
        private void Update() {
            List<int> locationIds = _accommodationStatisticsService.GetHottestAndColdestLocations(_userId);

            if (locationIds.Count == 1) {
                IsNotValid = true;
                return;
            }

            FirstLocation = new LocationDTO(_locationService.GetById(locationIds[0]));
            SecondLocation = new LocationDTO(_locationService.GetById(locationIds[1]));
            ThirdLocation = new LocationDTO(_locationService.GetById(locationIds[2]));
            FourthLocation = new LocationDTO(_locationService.GetById(locationIds[3]));

            AccommodationDTOs.Clear();
            foreach (var acc in _accommodationService.GetByLocationAndOwnerId(ThirdLocation.Id, _userId)) {
                AccommodationDTO accDTO = new AccommodationDTO(acc);
                accDTO.Location = ThirdLocation;
                AccommodationDTOs.Add(accDTO);
            }
            foreach (var acc in _accommodationService.GetByLocationAndOwnerId(FourthLocation.Id, _userId)) {
                AccommodationDTO accDTO = new AccommodationDTO(acc);
                accDTO.Location = FourthLocation;
                AccommodationDTOs.Add(accDTO);
            }
        }

        private void Execute(object obj) {
        }
        public bool CloseButton() {
            _accommodationService.CloseAccommodation(SelectedAccommodation.Id);

            this.Update();

            if (IsNotValid) {
                return false;
            }

            return true;
        }

        private bool CanBeExecuted(object obj) {
            if (SelectedAccommodation == null) {
                return false;
            }
            return true;
        }

        public void SelectionChanged() {
            CloseCommand.RaiseCanExecuteChanged();
        }

    }
}
