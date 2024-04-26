using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.DTO {
    public class TourRequestDTO : INotifyPropertyChanged {
        public TourRequestDTO() { }
        public TourRequestDTO(TourRequest tourRequest) {
            Id = tourRequest.Id;
            TouristId = tourRequest.TouristId;
            LocationId = tourRequest.LocationId;
            Description = tourRequest.Description;
            LanguageId = tourRequest.LanguageId;
            StartDate = tourRequest.StartDate;
            EndDate = tourRequest.EndDate;
            Status = tourRequest.Status.ToString();
        }

        public int Id { get; set; }

        private int _touristId;
        public int TouristId {
            get {
                return _touristId;
            }
            set {
                if (_touristId != value) {
                    _touristId = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _locationId;
        public int LocationId {
            get { 
                return _locationId; 
            }
            set {
                if (_locationId != value) {
                    _locationId = value; 
                    OnPropertyChanged();
                }
            }
        }

        private string _description;
        public string Description {
            get { return _description; }
            set {
                if (_description != value) {
                    _description = value; 
                    OnPropertyChanged();
                }
            }
        }

        private int _languageId;
        public int LanguageId {
            get { 
                return _languageId; 
            }
            set {
                if (_languageId != value) {
                    _languageId = value; 
                    OnPropertyChanged();
                }
            }
        }

        private DateOnly _startDate;
        public DateOnly StartDate {
            get {
                return _startDate;
            }
            set {
                if (_startDate != value) {
                    _startDate = value;
                    OnPropertyChanged();
                }
            }
        }
        private DateOnly _endDate;
        public DateOnly EndDate {
            get {
                return _endDate;
            }
            set {
                if (_endDate != value) {
                    _endDate = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _status;
        public string Status {
            get {
                return _status;
            }
            set {
                if (_status != value) {
                    _status = value;
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
