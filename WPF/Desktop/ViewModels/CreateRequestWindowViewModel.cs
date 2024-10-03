using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Services;
using BookingApp.WPF.DTO;
using BookingApp.WPF.Utils.Validation;
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
    public enum ValidationTarget { Tourist, Passenger, Confirmation, None }

    public class CreateRequestWindowViewModel : ValidationBase, INotifyPropertyChanged {
        private ValidationTarget _validationTarget = ValidationTarget.None;

        private readonly TourRequestService _tourRequestService = ServicesPool.GetService<TourRequestService>();
        private readonly LocationService _locationService = ServicesPool.GetService<LocationService>();
        private readonly LanguageService _languageService = ServicesPool.GetService<LanguageService>();
        private readonly TouristService _touristService = ServicesPool.GetService<TouristService>();
        public TourRequestDTO TourRequest { get; set; }
        public PassengerDTO Tourist { get; set; }
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
                }
            }
        }

        public Action CloseAction { get; set; }

        public int UserId { get; set; }

        private RequestsPageViewModel _parentViewModel;
        CreateComplexRequestWindowViewModel? ComplexViewModel { get; set; }
        public CreateRequestWindowViewModel(int userId, CreateComplexRequestWindowViewModel? complexViewModel, RequestsPageViewModel requestsPageViewModel) {
            UserId = userId;
            TourRequest = new TourRequestDTO(userId);
            Passengers = new ObservableCollection<PassengerDTO>();
            Locations = new ObservableCollection<LocationDTO>();
            Languages = new ObservableCollection<LanguageDTO>();

            SetRememberMe();

            Func<bool> alwaysTrue = () => true;
            AddPassengerCmd = new RelayCommand(AddPassenger, alwaysTrue);
            AddTouristCmd = new RelayCommand(AddTourist, AddTouristCanExecute);
            RemovePassengerCmd = new RelayCommand(RemovePassenger, alwaysTrue);
            CreateRequestCmd = new RelayCommand(CreateRequest, alwaysTrue);

            SetLocations();
            SetLanguages();
            ComplexViewModel = complexViewModel;
            _parentViewModel = requestsPageViewModel;
        }

        private bool AddTouristCanExecute() {
            return !Passengers.Any(p => p.UserId != -1);
        }

        private void SetRememberMe() {
            Tourist = new PassengerDTO(_touristService.GetById(UserId));

            TouristName = Tourist.Name;
            TouristSurname = Tourist.Surname;
            TouristAge = Tourist.Age;
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
            _validationTarget = ValidationTarget.Tourist;

            Validate();

            if (!IsValid)
                return;

            Passengers.Add(new PassengerDTO(TouristName, TouristSurname, TouristAge, UserId));
            ClearTouristFields();
        }

        public void AddPassenger(object parameter) {
            _validationTarget = ValidationTarget.Passenger;

            Validate();

            if (!IsValid)
                return;

            Passengers.Add(new PassengerDTO(PassengerName, PassengerSurname, PassengerAge, -1));
            ClearPassengerFields();
        }

        public void CreateRequest(object parameter) {
            _validationTarget = ValidationTarget.Confirmation;

            Validate();

            if (!IsValid)
                return;

            TourRequest.PassengerNumber = Passengers.Count();
            TourRequest.Passengers = Passengers.ToList();

            if(ComplexViewModel == null) {
                _tourRequestService.CreateRequest(TourRequest, 0);
                App.NotificationService.ShowSuccess("Tour request created successfully!");
                _parentViewModel.DisplayTourRequests();
            }            
            else
                ComplexViewModel.SimpleTourRequests.Add(TourRequest);

            CloseAction?.Invoke();
        }

        public void RemovePassenger(object parameter) {
            Passengers.Remove((PassengerDTO)parameter);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected override void ValidateSelf() {
            ValidationErrors.Clear();

            switch (_validationTarget) {
                case ValidationTarget.Tourist:
                    if (string.IsNullOrEmpty(TouristName))
                        ValidationErrors[nameof(TouristName)] = "Name is required!";

                    if (string.IsNullOrEmpty(TouristSurname))
                        ValidationErrors[nameof(TouristSurname)] = "Surname is required!";

                    if (TouristAge == 0)
                        ValidationErrors[nameof(TouristAge)] = "Age cannot be 0!";
                    break;
                case ValidationTarget.Passenger:
                    if (string.IsNullOrEmpty(PassengerName))
                        ValidationErrors[nameof(PassengerName)] = "Name is required!";

                    if (string.IsNullOrEmpty(PassengerSurname))
                        ValidationErrors[nameof(PassengerSurname)] = "Surname is required!";

                    if (PassengerAge == 0)
                        ValidationErrors[nameof(PassengerAge)] = "Age cannot be 0!";
                    break;
                case ValidationTarget.Confirmation:
                    if (AddTouristCanExecute())
                        ValidationErrors["Confirmation"] = "Tourist must be added!";

                    if (TourRequest.Location == null)
                        ValidationErrors["Location"] = "Location must be selected!";

                    if (TourRequest.Language == null)
                        ValidationErrors["Language"] = "Language must be selected!";

                    if (string.IsNullOrEmpty(TourRequest.Description))
                        ValidationErrors["Description"] = "Description is required!";

                    if (StartDate == DateTime.Parse("01-01-0001"))
                        ValidationErrors["StartDate"] = "Start date must be selected!";

                    if (EndDate == DateTime.Parse("01-01-0001"))
                        ValidationErrors["EndDate"] = "End date must be selected!";

                    break;
                case ValidationTarget.None:
                    break;
                default:
                    break;
            }

            OnPropertyChanged(nameof(ValidationErrors));
        }
    }
}
