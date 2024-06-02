using BookingApp.Domain.Model;
using BookingApp.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BookingApp.WPF.Desktop.ViewModels {
    public class SettingsWindowViewModel : INotifyPropertyChanged {
        private readonly TouristService _touristService = ServicesPool.GetService<TouristService>();

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ICommand SubmitInformationCommand { get; set; }

        public int UserId { get; set; }

        private string _name;
        public string Name {
            get {
                return _name;
            }
            set {
                if(_name != value) {
                    _name = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _surname;
        public string Surname {
            get {
                return _surname;
            }
            set {
                if (_surname != value) {
                    _surname = value;
                    OnPropertyChanged();
                }
            }
        }

        private DateTime _dob;
        public DateTime DOB {
            get {
                return _dob;
            }
            set {
                if (_dob != value) {
                    _dob = value;
                    OnPropertyChanged();
                    TouristDOB = DateOnly.FromDateTime(value);
                }
            }
        }

        private DateOnly _touristdob;
        public DateOnly TouristDOB {
            get {
                return _touristdob;
            }
            set {
                if (_touristdob != value) {
                    _touristdob = value;
                    OnPropertyChanged();
                }
            }
        }

        public SettingsWindowViewModel(int userId) { 
            UserId = userId;
            Tourist thisTourist = _touristService.GetById(userId);
            Name = thisTourist.Name;
            Surname = thisTourist.Surname;
            DOB = thisTourist.DateOfBirth.ToDateTime(TimeOnly.MinValue);
            Func<bool> alwaysTrue = () => true;
            SubmitInformationCommand = new RelayCommand(SubmitInformation, alwaysTrue);
        }

        public void SubmitInformation(object parameter) {
            _touristService.Update(UserId, Name, Surname, TouristDOB);
        }
    }
}
