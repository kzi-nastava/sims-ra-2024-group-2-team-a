﻿using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BookingApp.DTO {
    public class PassengerDTO : INotifyPropertyChanged {
        public PassengerDTO() { }
        public PassengerDTO(string name, string surname, int age) {
            Name = name;
            Surname = surname;
            Age = age;
        }

        private string _name;
        public string Name {
            get {
                return _name;
            }
            set {
                if (_name != value) {
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

        private int _age;
        public int Age {
            get {
                return _age;
            }
            set {
                if (_age != value) {
                    _age = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
