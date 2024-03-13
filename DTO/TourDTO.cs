using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BookingApp.DTO {
    public class TourDTO : INotifyPropertyChanged {
        public TourDTO() {

        }


        public TourDTO(string name, int locationId, string description, string language, int maxTouristNumber, double duration, int currentTouristNumber, DateTime beggining, List<string> profilePictures) {

            Name = name;
            LocationId = locationId;
            Description = description;
            Language = language;
            MaxTouristNumber = maxTouristNumber;
            Duration = duration;
            CurrentTouristNumber = currentTouristNumber;
            Beggining = beggining;
            ProfilePictures = profilePictures;
        }

        public TourDTO(Tour t) {
            Name = t.Name;
            LocationId = t.LocationId;
            Description = t.Description;
            Language = t.Language;
            MaxTouristNumber = t.MaxTouristNumber;
            Duration = t.Duration;
            CurrentTouristNumber = t.CurrentTouristNumber;
            Beggining = t.Beggining;
            ProfilePictures = t.ProfilePictures;
        }

        private int _id;
        public int Id {
            get { return _id; }
            set {
                if (_id != value) {
                    _id = value; OnPropertyChanged();
                }
            }
        }

        private DateTime _beggining;
        public DateTime Beggining {
            get {
                return _beggining;
            }
            set {
                if(_beggining != value) { 
                    _beggining = value;
                    OnPropertyChanged();
                }
            }
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

        private int _locationId;
        public int LocationId {
            get { return _locationId; }
            set {
                if (_locationId != value) {
                    _locationId = value; OnPropertyChanged();
                }
            }
        }

        private string _description;
        public string Description {
            get { return _description; }
            set {
                if (_description != value) {
                    _description = value; OnPropertyChanged();
                }
            }
        }

        private string _language;
        public string Language {
            get { return _language; }
            set {
                if (_language != value) {
                    _language = value; OnPropertyChanged();
                }
            }
        }

        private int _maxTouristNumber;
        public int MaxTouristNumber {
            get { return _maxTouristNumber; }
            set {
                if (value != _maxTouristNumber) {
                    _maxTouristNumber = value; OnPropertyChanged();
                }
            }
        }

        private double _duration;
        public double Duration {
            get { return _duration; }
            set {
                if (_duration != value) {
                    _duration = value; OnPropertyChanged();
                }
            }
        }

        private int _currentTouristNumber;
        public int CurrentTouristNumber {
            get { return _currentTouristNumber; }
            set {
                if (_currentTouristNumber != value) {
                    _currentTouristNumber = value;
                    OnPropertyChanged();
                }
            }
        }
        public List<string> ProfilePictures { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
