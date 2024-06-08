using BookingApp.Domain.Model;
using BookingApp.Repository;
using BookingApp.Services;
using BookingApp.WPF.DTO;
using BookingApp.WPF.Utils.Validation;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace BookingApp.WPF.Desktop.ViewModels {
    public class TouristReservationWindowViewModel : ValidationBase, INotifyPropertyChanged {
        private ValidationTarget _validationTarget = ValidationTarget.None;

        private readonly TourReservationService _tourReservationService = ServicesPool.GetService<TourReservationService>();
        private readonly TouristService _touristService = ServicesPool.GetService<TouristService>();

        public ObservableCollection<PassengerDTO> Passengers { get; set; }

        public PassengerDTO Tourist { get; set; }

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

        public TourDTO SelectedTour { get; set; }

        public int UserId { get; set; }

        public TouristReservationWindowViewModel(TourDTO selectedTour, int userId) {
            SelectedTour = selectedTour;
            UserId = userId;
            IsVoucherSelected = false;

            SetRememberMe();

            Passengers = new ObservableCollection<PassengerDTO>();

            Func<bool> alwaysTrue = () => true;
            AddPassengerCmd = new RelayCommand(AddPassenger, alwaysTrue);
            AddTouristCmd = new RelayCommand(AddTourist, AddTouristCanExecute);
            RemovePassengerCmd = new RelayCommand(RemovePassenger, alwaysTrue);
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

        public void RemovePassenger(object parameter) {
            PassengerDTO passenger = (PassengerDTO)parameter; 
            Passengers.Remove(passenger);
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
            _validationTarget = ValidationTarget.Confirmation;

            Validate();

            if (!IsValid)
                return -2;

            if (SelectedVoucher != null)
                SelectedVoucher.Used = IsVoucherSelected;
            return _tourReservationService.MakeReservation(this.UserId, SelectedTour, Passengers.ToList(), SelectedVoucher);
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
