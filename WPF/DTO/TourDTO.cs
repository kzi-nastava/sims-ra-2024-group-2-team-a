using BookingApp.Domain.Model;
using BookingApp.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BookingApp.WPF.DTO {
    public class TourDTO : INotifyPropertyChanged
    {
        private readonly LocationService _locationService = ServicesPool.GetService<LocationService>();
        private readonly LanguageService _languageService = ServicesPool.GetService<LanguageService>();

        public TourDTO()
        {
            ProfilePictures = new List<string>();
        }

        public TourDTO(Tour t)
        {
            Id = t.Id;
            Name = t.Name;
            LocationId = t.LocationId;
            Description = t.Description;
            LanguageId = t.LanguageId;
            Location = new LocationDTO(_locationService.GetById(LocationId));
            Language = new LanguageDTO(_languageService.GetById(LanguageId));
            MaxTouristNumber = t.MaxTouristNumber;
            Duration = t.Duration;
            CurrentTouristNumber = t.CurrentTouristNumber;
            Beggining = t.Beggining;
            State = t.State;
            End = t.End;
            GuideId = t.GuideId;
            ProfilePictures = t.ProfilePictures;
            TourStatus = t.State.ToString();
        }
        private int _id;
        public int Id
        {
            get { return _id; }
            set
            {
                if (_id != value)
                {
                    _id = value; OnPropertyChanged();
                }
            }
        }

        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _locationId;
        public int LocationId
        {
            get { return _locationId; }
            set
            {
                if (_locationId != value)
                {
                    _locationId = value; OnPropertyChanged();
                }
            }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set
            {
                if (_description != value)
                {
                    _description = value; OnPropertyChanged();
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

        private int _languageId;
        public int LanguageId
        {
            get { return _languageId; }
            set
            {
                if (_languageId != value)
                {
                    _languageId = value; OnPropertyChanged();
                }
            }
        }

        private int _maxTouristNumber;
        public int MaxTouristNumber
        {
            get { return _maxTouristNumber; }
            set
            {
                if (value != _maxTouristNumber)
                {
                    _maxTouristNumber = value; OnPropertyChanged();
                }
            }
        }

        private double _duration;
        public double Duration
        {
            get { return _duration; }
            set
            {
                if (_duration != value)
                {
                    _duration = value; OnPropertyChanged();
                }
            }
        }

        private int _currentTouristNumber;
        public int CurrentTouristNumber
        {
            get { return _currentTouristNumber; }
            set
            {
                if (_currentTouristNumber != value)
                {
                    _currentTouristNumber = value;
                    OnPropertyChanged();
                }
            }
        }

        private DateOnly _justDate;
        public DateOnly JustDate
        {
            get { return _justDate; }
            set
            {
                if (_justDate != value)
                {
                    _justDate = value;
                    OnPropertyChanged();
                }
            }
        }
        private int _justTime;
        public int JustTime
        {
            get { return _justTime; }
            set
            {
                if (_justTime != value)
                {
                    _justTime = value;
                    OnPropertyChanged();
                }
            }
        }

        private DateTime _beggining;
        public DateTime Beggining
        {
            get
            {
                return _beggining;
            }
            set
            {
                if (_beggining != value)
                {
                    _beggining = value;
                    OnPropertyChanged();
                }
            }
        }
        private DateTime _end;
        public DateTime End
        {
            get
            {
                return _end;
            }
            set
            {
                if (_end != value)
                {
                    _end = value;
                    OnPropertyChanged();
                }
            }
        }
        private TourState _state;
        public TourState State
        {
            get
            {
                return _state;
            }
            set
            {
                if (_state != value)
                {
                    _state = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _tourStatus;
        public string TourStatus
        {
            get
            {
                return _tourStatus;
            }
            set
            {
                if (_tourStatus != value)
                {
                    _tourStatus = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _guideId;
        public int GuideId
        {
            get { return _guideId; }
            set
            {
                if (_guideId != value)
                {
                    _guideId = value;
                    OnPropertyChanged();
                }
            }
        }
        public List<string> ProfilePictures { get; set; }
        public string LanguageTemplate { get; set; }
        public string LocationTemplate { get; set; }

        public void setBeggining(DateTime dateTime)
        {
            _beggining = dateTime;
        }
        public void setLocationTemplate(string city, string country)
        {
            LocationTemplate = $"{country}, {city}";
        }

        public void SetLanguageTemplate(string name)
        {
            LanguageTemplate = $"{name}";
        }

        public Tour ToModelNoId()
        {
            return new Tour(Name, LocationId, Description, LanguageId, MaxTouristNumber, Duration, CurrentTouristNumber, Beggining, State, GuideId, ProfilePictures);
        }
        public Tour ToModel()
        {
            return new Tour(Id, Name, LocationId, Description, LanguageId, MaxTouristNumber, Duration, CurrentTouristNumber, Beggining, End, State, GuideId, ProfilePictures);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)

        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
