using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BookingApp.DTO {
    public class TourDTO : INotifyPropertyChanged {
        public TourDTO() {
            ProfilePictures = new List<string>();
        }


        public TourDTO(string name, int locationId, string description, int languageId, int maxTouristNumber, double duration, int currentTouristNumber, DateTime beggining, bool isFinished, int guideId, List<string> profilePictures) {

            Name = name;
            LocationId = locationId;
            Description = description;
            LanguageId = languageId;
            MaxTouristNumber = maxTouristNumber;
            Duration = duration;
            CurrentTouristNumber = currentTouristNumber;
            Beggining = beggining;
            IsFinished = _isFinished;
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
            IsFinished = t.IsFinished;
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

        private DateOnly _justDate;
        public DateOnly JustDate {
            get { return _justDate; }
            set {
                if (_justDate != value) {
                    _justDate = value;
                    OnPropertyChanged();
                }
            }
        }
        private int _justTime;
        public int JustTime {
            get { return _justTime; }
            set {
                if(_justTime != value) {
                    _justTime = value;
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
                if (_beggining != value) {
                    _beggining = value;
                    OnPropertyChanged();
                }
            }
        }
        private bool _isFinished;
        public bool IsFinished{
            get {
                return _isFinished;
            }
            set {
                if (_isFinished != value) {
                    _isFinished = value;
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
        public string LanguageTemplate { get; set; }
        public string LocationTemplate { get; set; }

        public void setBeggining() {
            _beggining = new DateTime(JustDate.Year, JustDate.Month, JustDate.Day, JustTime, 0, 0);
        }
        public void setLanguageTemplate(string language) { 
            LanguageTemplate = language;
        }
        public void setLocationTemplate(string city, string country) {
            LocationTemplate = $"{country}, {city}";
        }

        public Tour ToModelNoId() {
            return new Tour(Name, LocationId, Description, LanguageId, MaxTouristNumber, Duration, CurrentTouristNumber, Beggining, IsFinished, GuideId, ProfilePictures);
        }
        public Tour ToModel() {
            return new Tour(Id, Name, LocationId, Description, LanguageId, MaxTouristNumber, Duration, CurrentTouristNumber, Beggining, IsFinished, GuideId, ProfilePictures);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
