using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BookingApp.DTO {
    public class TourDTO : INotifyPropertyChanged {
        public TourDTO() {

        }


        public TourDTO(string name, int locationId, string description, int languageId, int maxTouristNumber, double duration, int currentTouristNumber, DateTime beggining, int guideId, List<string> profilePictures) {

            Name = name;
            LocationId = locationId;
            Description = description;
            LanguageId = languageId;
            MaxTouristNumber = maxTouristNumber;
            Duration = duration;
            CurrentTouristNumber = currentTouristNumber;
            Beggining = beggining;
            GuideId = guideId;
            ProfilePictures = profilePictures;
        }

        public TourDTO(Tour t) {
            Id = t.Id;
            Name = t.Name;
            LocationId = t.LocationId;
            Description = t.Description;
            LanguageId = t.LanguageId;
            MaxTouristNumber = t.MaxTouristNumber;
            Duration = t.Duration;
            CurrentTouristNumber = t.CurrentTouristNumber;
            Beggining = t.Beggining;
            GuideId = t.GuideId;
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

        private int _languageId;
        public int LanguageId {
            get { return _languageId; }
            set {
                if (_languageId != value) {
                    _languageId = value; OnPropertyChanged();
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

        private DateOnly _dateOnly;
        public DateOnly DateOnly {
            get { return _dateOnly; }
            set {
                if (_dateOnly != value) {
                    _dateOnly = value;
                    OnPropertyChanged();
                }
            }
        }
        private int _timeOnly;
        public int TimeOnly {
            get { return _timeOnly; }
            set {
                if(_timeOnly != value) {
                    _timeOnly = value;
                    OnPropertyChanged();
                }
            }
        }

        private DateTime _beggining;
        public DateTime Beggining {
            get {
                return _beggining;
            }
            set {
                if (_beggining != new DateTime(_dateOnly.Year, _dateOnly.Month, _dateOnly.Day, _timeOnly, 0, 0)) {
                    _beggining = new DateTime(_dateOnly.Year, _dateOnly.Month, _dateOnly.Day, _timeOnly, 0, 0);
                    OnPropertyChanged();
                }
            }
        }

        private int _guideId;
        public int GuideId {
            get { return _guideId; } 
            set {
                if (_guideId != value) {
                    _guideId = value;
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
