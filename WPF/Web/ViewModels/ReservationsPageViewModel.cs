using BookingApp.Services;
using BookingApp.WPF.DTO;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace BookingApp.WPF.Web.ViewModels {
    public class ReservationsPageViewModel : INotifyPropertyChanged {

        private readonly int _guestId;

        private readonly LocationService _locationService = new LocationService();
        private readonly AccommodationService _accommodationService = new AccommodationService();
        private readonly AccommodationReservationService _reservationService = new AccommodationReservationService();
        private readonly RescheduleRequestService _rescheduleRequestService = new RescheduleRequestService();
        private readonly ReviewService _reviewService = new ReviewService();

        private ObservableCollection<LocationDTO> _locationDTOs;
        private ObservableCollection<AccommodationDTO> _accommodationDTOs;

        private ObservableCollection<AccommodationReservationDTO> _reservationDTOs;
        public ObservableCollection<AccommodationReservationDTO> Reservations {
            get { return _reservationDTOs; }
            set {
                _reservationDTOs = value;
                OnPropertyChanged(nameof(Reservations));
            }
        }

        private ObservableCollection<AccommodationReservationDTO> _filteredReservationDTOs;
        public ObservableCollection<AccommodationReservationDTO> FilteredReservations {
            get { return _filteredReservationDTOs; }
            set {
                _filteredReservationDTOs = value;
                OnPropertyChanged(nameof(FilteredReservations));
            }
        }

        private ObservableCollection<RescheduleRequestDTO> _rescheduleRequestDTOs;
        public ObservableCollection<RescheduleRequestDTO> RescheduleRequests {
            get { return _rescheduleRequestDTOs; }
            set {
                _rescheduleRequestDTOs = value;
                OnPropertyChanged(nameof(RescheduleRequests));
            }
        }

        public bool ScheduledSelected { get; set; } = true;

        public ReservationsPageViewModel(int guestId) {
            _guestId = guestId;
            UpdateAllCollections();
        }

        public void UpdateLocationDTOs() {
            var locations = _locationService.GetAll();
            var dtos = locations.Select(l => new LocationDTO(l));
            _locationDTOs = new ObservableCollection<LocationDTO>(dtos);
        }

        public void UpdateAccommodationDTOs() {
            var accommodations = _accommodationService.GetAll();
            var dtos = accommodations.Select(a => new AccommodationDTO(a));
            _accommodationDTOs = new ObservableCollection<AccommodationDTO>(dtos);

            foreach (var acc in _accommodationDTOs) {
                var loc = _locationDTOs.FirstOrDefault(l => l.Id == acc.LocationId);
                acc.Location = loc;
            }
        }

        public void UpdateReservationDTOs() {
            var reservations = _reservationService.GetByGuestId(_guestId);
            var dtos = reservations.Select(r => new AccommodationReservationDTO(r));
            Reservations = new ObservableCollection<AccommodationReservationDTO>(dtos);

            foreach (var res in Reservations) {
                var acc = _accommodationDTOs.FirstOrDefault(a => a.Id == res.AccommodationId);
                res.Accommodation = acc;
                res.Graded = _reviewService.IsGradedByGuest(res.Id);
            }
        }

        public void UpdateRescheduleRequestDTOs() {

            var rescheduleRequests = _rescheduleRequestService.GetByGuestId(_guestId);
            var dtos = rescheduleRequests.Select(r => new RescheduleRequestDTO(r)).OrderByDescending(r => r.Id);
            RescheduleRequests = new ObservableCollection<RescheduleRequestDTO>(dtos);

            foreach (var req in RescheduleRequests) {
                var res = Reservations.FirstOrDefault(res => res.Id == req.ReservationId);
                req.Reservation = res;
            }
        }

        public void UpdateAllCollections() {
            UpdateLocationDTOs();
            UpdateAccommodationDTOs();
            UpdateReservationDTOs();
            UpdateRescheduleRequestDTOs();

            FilterBySelection(ScheduledSelected);
        }

        public void FilterBySelection(bool scheduledSelected) {
            ScheduledSelected = scheduledSelected;

            if (ScheduledSelected) {
                var dtos = Reservations.Where(x => !x.HasExpired).OrderByDescending(x => x.Id);
                FilteredReservations = new ObservableCollection<AccommodationReservationDTO>(dtos);
            }
            else {
                var dtos = Reservations.Where(x => x.HasExpired).OrderByDescending(x => x.Id);
                FilteredReservations = new ObservableCollection<AccommodationReservationDTO>(dtos);
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
