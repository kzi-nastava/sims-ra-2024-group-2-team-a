using BookingApp.Domain.Model;
using BookingApp.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BookingApp.WPF.DTO {
    public class TourRequestDTO : INotifyPropertyChanged {
        private readonly LocationService _locationService = ServicesPool.GetService<LocationService>();
        private readonly LanguageService _languageService = ServicesPool.GetService<LanguageService>();
        private readonly RequestPassengerService _requestPassengerService = ServicesPool.GetService<RequestPassengerService>();
        public TourRequestDTO() { }
        public TourRequestDTO(int touristId) {
            TouristId = touristId;
            GuideId = -1;
        }
        public TourRequestDTO(TourRequest tourRequest) {
            Id = tourRequest.Id;
            TouristId = tourRequest.TouristId;
            GuideId = tourRequest.GuideId;
            LocationId = tourRequest.LocationId;
            Description = tourRequest.Description;
            LanguageId = tourRequest.LanguageId;
            Location = new LocationDTO(_locationService.GetById(LocationId));
            Language = new LanguageDTO(_languageService.GetById(LanguageId));
            StartDate = tourRequest.StartDate;
            EndDate = tourRequest.EndDate;
            StatusReal = tourRequest.Status;
            Status = tourRequest.Status.ToString();
            PassengerNumber = tourRequest.PassengerNumber;
            CastToDateTime();
            CalendarFrom = new Calendar();
            CalendarTo = new Calendar();
            SetCalendars();
            SetLocationTemplate(Location.City, Location.Country);
            SetLanguageTemplate(Language.Name);
            ComplexTourId = tourRequest.ComplexTourId;
            LoadPassengers();
        }

        private void LoadPassengers() {
            foreach (var passenger in _requestPassengerService.GetByRequestId(Id)) 
                Passengers.Add(new PassengerDTO(passenger));
        }

        public TourRequestDTO(TourRequest tourRequest, int serialNumber, TourRequestStatus complexStatus) {
            Id = tourRequest.Id;
            TouristId = tourRequest.TouristId;
            GuideId = tourRequest.GuideId;
            LocationId = tourRequest.LocationId;
            Description = tourRequest.Description;
            LanguageId = tourRequest.LanguageId;
            Location = new LocationDTO(_locationService.GetById(LocationId));
            Language = new LanguageDTO(_languageService.GetById(LanguageId));
            StartDate = tourRequest.StartDate;
            EndDate = tourRequest.EndDate;
            if(complexStatus == TourRequestStatus.Expired)
                StatusVisible = false;
            else
                StatusVisible = true;
            StatusReal = tourRequest.Status;
            Status = tourRequest.Status.ToString();
            PassengerNumber = tourRequest.PassengerNumber;
            CastToDateTime();
            SetLocationTemplate(Location.City, Location.Country);
            SetLanguageTemplate(Language.Name);
            Title = "Destination " + serialNumber;
        }

        private bool _statusVisible;
        public bool StatusVisible {
            get {
                return _statusVisible;
            }
            set {
                if (_statusVisible != value) {
                    _statusVisible = value;
                    OnPropertyChanged();
                }
            }
        }

        public int Id { get; set; }

        private int _touristId;
        public int TouristId {
            get {
                return _touristId;
            }
            set {
                if (_touristId != value) {
                    _touristId = value;
                    OnPropertyChanged();
                }
            }
        }
        private int _guideId;
        public int GuideId {
            get {
                return _guideId;
            }
            set {
                if (_guideId != value) {
                    _guideId = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _title;
        public string Title {
            get {
                return _title;
            }
            set {
                if (_title != value) {
                    _title = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _passengerNumber;
        public int PassengerNumber {
            get {
                return _passengerNumber;
            }
            set {
                if (_passengerNumber != value) {
                    _passengerNumber = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _locationId;
        public int LocationId {
            get { 
                return _locationId; 
            }
            set {
                if (_locationId != value) {
                    _locationId = value; 
                    OnPropertyChanged();
                }
            }
        }

        private LocationDTO _locationDTO;
        public LocationDTO Location {
            get {
                return _locationDTO;
            }
            set {
                if (_locationDTO != value) {
                    _locationDTO = value;
                    OnPropertyChanged();
                }
            }
        }

        private LanguageDTO _languageDTO;
        public LanguageDTO Language {
            get {
                return _languageDTO;
            }
            set {
                if (_languageDTO != value) {
                    _languageDTO = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _description;
        public string Description {
            get { return _description; }
            set {
                if (_description != value) {
                    _description = value; 
                    OnPropertyChanged();
                }
            }
        }

        private int _languageId;
        public int LanguageId {
            get { 
                return _languageId; 
            }
            set {
                if (_languageId != value) {
                    _languageId = value; 
                    OnPropertyChanged();
                }
            }
        }

        private DateOnly _startDate;
        public DateOnly StartDate {
            get {
                return _startDate;
            }
            set {
                if (_startDate != value) {
                    _startDate = value;
                    OnPropertyChanged();
                }
            }
        }
        private DateOnly _endDate;
        public DateOnly EndDate {
            get {
                return _endDate;
            }
            set {
                if (_endDate != value) {
                    _endDate = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _status;
        public string Status {
            get {
                return _status;
            }
            set {
                if (_status != value) {
                    _status = value;
                    OnPropertyChanged();
                }
            }
        }

        private TourRequestStatus _statusReal;
        public TourRequestStatus StatusReal {
            get {
                return _statusReal;
            }
            set {
                if(_statusReal != value) {
                    _statusReal = value;
                    OnPropertyChanged();
                }
            }
        }
        private int _complexTourId;
        public int ComplexTourId {
            get {
                return _complexTourId;
            }
            set {
                if (_complexTourId != value) {
                    _complexTourId = value;
                    OnPropertyChanged();
                }
            }
        }

        private List<PassengerDTO> _passengers = new List<PassengerDTO>();
        public List<PassengerDTO> Passengers {
            get {
                return _passengers;
            }
            set {
                if (_passengers != value) {
                    _passengers = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public System.Windows.Controls.CalendarBlackoutDatesCollection BlackoutDatesStart { get; set; }
        public System.Windows.Controls.CalendarBlackoutDatesCollection BlackoutDatesEnd { get; set; }
        public Calendar CalendarFrom { get; set; }
        public Calendar CalendarTo { get; set; }
        public string LanguageTemplate { get; set; }
        public string LocationTemplate { get; set; }
        private void SetCalendars() {
            CalendarFrom.DisplayDateStart = StartDateTime;
            CalendarFrom.DisplayDateEnd = EndDateTime;

            CalendarTo.DisplayDateStart = StartDateTime;
            CalendarTo.DisplayDateEnd = EndDateTime;
        }
        public void CastToDateTime() {
            StartDateTime = new DateTime(StartDate.Year, StartDate.Month, StartDate.Day);
            EndDateTime = new DateTime(EndDate.Year, EndDate.Month, EndDate.Day);
        }
        public void SetLocationTemplate(string city, string country) {
            LocationTemplate = $"{country}, {city}";
        }

        public void SetLanguageTemplate(string name) {
            LanguageTemplate = $"{name}";
        }

        public TourRequest ToModel() {
            return new TourRequest(Id, TouristId, GuideId, LocationId, Description, LanguageId, StartDate, EndDate, StatusReal, PassengerNumber, ComplexTourId);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
