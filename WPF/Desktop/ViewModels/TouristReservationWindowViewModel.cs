using BookingApp.Domain.Model;
using BookingApp.Repository;
using BookingApp.Services;
using BookingApp.WPF.DTO;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace BookingApp.WPF.Desktop.ViewModels {
    public class TouristReservationWindowViewModel : INotifyPropertyChanged {
        private readonly TourReservationService _tourReservationService = ServicesPool.GetService<TourReservationService>();
        private readonly LocationService _locationService = ServicesPool.GetService<LocationService>();
        private readonly LanguageService _languageService = ServicesPool.GetService<LanguageService>();

        public ObservableCollection<PassengerDTO> Passengers { get; set; }

        public ICommand AddTouristCmd { get; set; }
        public ICommand AddPassengerCmd { get; set; }
        public ICommand RemovePassengerCmd { get; set; }

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

        public VoucherDTO SelectedVoucher { get; set; }

        private bool _isVoucherSelected;
        public bool IsVoucherSelected {
            get {
                return _isVoucherSelected;
            }
            set {
                if (value != _isVoucherSelected) {
                    _isVoucherSelected = value;
                    OnPropertyChanged();
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

        private bool _isConfirmationButtonEnabled;
        public bool IsConfirmationButtonEnabled {
            get {
                return _isConfirmationButtonEnabled;
            }
            set {
                if (value != _isConfirmationButtonEnabled) {
                    _isConfirmationButtonEnabled = value;
                    OnPropertyChanged();
                }
            }
        }

        public TourDTO SelectedTour { get; set; }

        public int UserId { get; set; }

        public TouristReservationWindowViewModel(TourDTO selectedTour, int userId) {
            SelectedTour = selectedTour;
            UserId = userId;
            IsConfirmationButtonEnabled = false;
            IsTouristButtonEnabled = false;
            IsPassengerButtonEnabled = false;
            IsVoucherSelected = false;

            Passengers = new ObservableCollection<PassengerDTO>();

            Func<bool> alwaysTrue = () => true;
            AddPassengerCmd = new RelayCommand(AddPassenger, alwaysTrue);
            AddTouristCmd = new RelayCommand(AddTourist, alwaysTrue);
            RemovePassengerCmd = new RelayCommand(RemovePassenger, alwaysTrue);

            GetPresentableTour();
        }

        public void AddTourist(object parameter) {
            Passengers.Add(new PassengerDTO(TouristName, TouristSurname, TouristAge, UserId));
            ClearTouristFields();
            IsConfirmationButtonEnabled = true;
        }

        public void AddPassenger(object parameter) {
            Passengers.Add(new PassengerDTO(PassengerName, PassengerSurname, PassengerAge, -1));
            ClearPassengerFields();
        }

        public void RemovePassenger(object parameter) {
            PassengerDTO passenger = (PassengerDTO)parameter; 
            Passengers.Remove(passenger);
            if (passenger.UserId != -1)
                IsConfirmationButtonEnabled = false;
        }

        private void GetPresentableTour() {
            Location location = _locationService.GetById(SelectedTour.LocationId);
            Language language = _languageService.GetById(SelectedTour.LanguageId);
            SelectedTour.setLocationTemplate(location.City, location.Country);
            SelectedTour.SetLanguageTemplate(language.Name);
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

        public void RemoveVoucher() {
            IsVoucherSelected = false;
        }

        public int MakeReservation() {
            if(SelectedVoucher != null)
                SelectedVoucher.Used = IsVoucherSelected;
            return _tourReservationService.MakeReservation(this.UserId, SelectedTour, Passengers.ToList(), SelectedVoucher);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
