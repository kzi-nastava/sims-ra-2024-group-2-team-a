using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Services;
using BookingApp.WPF.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BookingApp.WPF.Desktop.ViewModels
{
    public class CreateRequestWindowViewModel : INotifyPropertyChanged {
        private readonly TourRequestService _tourRequestService = ServicesPool.GetService<TourRequestService>();
        private readonly LocationService _locationService = ServicesPool.GetService<LocationService>();
        private readonly LanguageService _languageService = ServicesPool.GetService<LanguageService>();
        public TourRequestDTO TourRequest { get; set; } 
        public ObservableCollection<PassengerDTO> Passengers { get; set; }
        public ObservableCollection<LocationDTO> Locations { get; set; }
        public ObservableCollection<LanguageDTO> Languages { get; set; }
        public ICommand AddTouristCmd { get; set; }
        public ICommand AddPassengerCmd { get; set; }
        public ICommand RemovePassengerCmd { get; set; }
        public ICommand CreateRequestCmd { get; set; }

        private DateTime _startDate;
        public DateTime StartDate {
            get {
                return _startDate;
            }
            set {
                if (_startDate != value) {
                    _startDate = value;
                    OnPropertyChanged();
                    TourRequest.StartDate = DateOnly.FromDateTime(value);
                }
            }
        }

        private DateTime _endDate;
        public DateTime EndDate {
            get {
                return _endDate;
            }
            set {
                if (_endDate != value) {
                    _endDate = value;
                    OnPropertyChanged();
                    TourRequest.EndDate = DateOnly.FromDateTime(value);
                }
            }
        }

        private string _passengerName;
        public string PassengerName {
            get {
                return _passengerName;
            }
            set {
                if (_passengerName != value) {
                    _passengerName = value;
                    OnPropertyChanged();
                    UpdatePassengerButtonState();
                }
            }
        }

        private string _passengerSurname;
        public string PassengerSurname {
            get {
                return _passengerSurname;
            }
            set {
                if (_passengerSurname != value) {
                    _passengerSurname = value;
                    OnPropertyChanged();
                    UpdatePassengerButtonState();
                }
            }
        }

        private int _passengerAge;
        public int PassengerAge {
            get {
                return _passengerAge;
            }
            set {
                if (_passengerAge != value) {
                    _passengerAge = value;
                    OnPropertyChanged();
                    UpdatePassengerButtonState();
                }
            }
        }

        private string _touristName;
        public string TouristName {
            get {
                return _touristName;
            }
            set {
                if (_touristName != value) {
                    _touristName = value;
                    OnPropertyChanged();
                    UpdateTouristButtonState();
                }
            }
        }

        private string _touristSurname;
        public string TouristSurname {
            get {
                return _touristSurname;
            }
            set {
                if (_touristSurname != value) {
                    _touristSurname = value;
                    OnPropertyChanged();
                    UpdateTouristButtonState();
                }
            }
        }

        private int _touristAge;
        public int TouristAge {
            get {
                return _touristAge;
            }
            set {
                if (_touristAge != value) {
                    _touristAge = value;
                    OnPropertyChanged();
                    UpdateTouristButtonState();
                }
            }
        }

        private bool _isTouristButtonEnabled;
        public bool IsTouristButtonEnabled {
            get {
                return _isTouristButtonEnabled;
            }
            set {
                if (value != _isTouristButtonEnabled) {
                    _isTouristButtonEnabled = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isPassengerButtonEnabled;
        public bool IsPassengerButtonEnabled {
            get {
                return _isPassengerButtonEnabled;
            }
            set {
                if (value != _isPassengerButtonEnabled) {
                    _isPassengerButtonEnabled = value;
                    OnPropertyChanged();
                }
            }
        }

        public int UserId { get; set; }

        private RequestsPageViewModel _parentViewModel;
        CreateComplexRequestWindowViewModel? ComplexViewModel { get; set; }
        public CreateRequestWindowViewModel(int userId, CreateComplexRequestWindowViewModel? complexViewModel, RequestsPageViewModel requestsPageViewModel) {
            UserId = userId;
            TourRequest = new TourRequestDTO(userId);
            IsTouristButtonEnabled = false;
            IsPassengerButtonEnabled = false;
            Passengers = new ObservableCollection<PassengerDTO>();
            Locations = new ObservableCollection<LocationDTO>();
            Languages = new ObservableCollection<LanguageDTO>();

            Func<bool> alwaysTrue = () => true;
            AddPassengerCmd = new RelayCommand(AddPassenger, alwaysTrue);
            AddTouristCmd = new RelayCommand(AddTourist, alwaysTrue);
            RemovePassengerCmd = new RelayCommand(RemovePassenger, alwaysTrue);
            CreateRequestCmd = new RelayCommand(CreateRequest, alwaysTrue);

            SetLocations();
            SetLanguages();
            ComplexViewModel = complexViewModel;
            _parentViewModel = requestsPageViewModel;
        }

        private void SetLocations() {
            Locations.Clear();
            foreach (Location location in _locationService.GetAll()) {
                Locations.Add(new LocationDTO(location));
            }
        }

        private void SetLanguages() {
            Languages.Clear();
            foreach (Language language in _languageService.GetAll()) {
                Languages.Add(new LanguageDTO(language));
            }
        }

        private void UpdateTouristButtonState() {
            IsTouristButtonEnabled = !string.IsNullOrWhiteSpace(TouristName) && !string.IsNullOrWhiteSpace(TouristSurname) && TouristAge != 0;
        }

        private void UpdatePassengerButtonState() {
            IsPassengerButtonEnabled = !string.IsNullOrWhiteSpace(PassengerName) && !string.IsNullOrWhiteSpace(PassengerSurname) && PassengerAge != 0;
        }

        private void ClearTouristFields() {
            TouristName = string.Empty;
            TouristSurname = string.Empty;
            TouristAge = 0;
        }

        private void ClearPassengerFields() {
            PassengerName = string.Empty;
            PassengerSurname = string.Empty;
            PassengerAge = 0;
        }


        public void AddTourist(object parameter) {
            Passengers.Add(new PassengerDTO(TouristName, TouristSurname, TouristAge, UserId));
            ClearTouristFields();
        }

        public void AddPassenger(object parameter) {
            Passengers.Add(new PassengerDTO(PassengerName, PassengerSurname, PassengerAge, -1));
            ClearPassengerFields();
        }

        public void CreateRequest(object parameter) {
            TourRequest.PassengerNumber = Passengers.Count();
            TourRequest.Passengers = Passengers.ToList();

            if(ComplexViewModel == null) {
                _tourRequestService.CreateRequest(TourRequest, 0);
                _parentViewModel.DisplayTourRequests();
            }            
            else
                ComplexViewModel.SimpleTourRequests.Add(TourRequest);
        }

        public void RemovePassenger(object parameter) {
            Passengers.Remove((PassengerDTO)parameter);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
